using LoaTool.Define.Exceptions;
using LoaTool.Define.Interfaces;

namespace LoaTool.Util;
public class DialogService : IDialogService
{
    private readonly static string VIEW_NAME_TAG = "Window";
    private readonly static string VIEWMODEL_NAME_TAG = "ViewModel";
    private IList<Type> _dialogTypes = new List<Type>();
    private IList<IDialog> _openedDialogs = new List<IDialog>();


    public void Clear()
    {
        foreach (var item in _openedDialogs)
        {
            try
            {
                item.Close();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            _openedDialogs.Clear();
            _dialogTypes.Clear();
        }
    }

    public void Close<TContext>(TContext targetContext) where TContext : IContext
    {
        Close((T) =>
        {
            var dataContext = T.DataContext;

            if(dataContext is IDialogContext dialogContext)
            {
                return dialogContext.Context.Equals(targetContext);
            }

            if(dataContext is IContext context)
            {
                return context.Equals(targetContext);
            }

            throw new NullReferenceException(T.GetType().Name + " is not type.");
        });
    }

    public void Close<TContext, TDialog>(TContext context)
        where TContext : IContext
        where TDialog : IDialog
    {
        Close((d) =>
        {
            var dialogType = d.GetType();
            return dialogType.Equals(typeof(TDialog));
        });
    }

    public void DeActivate()
    {
        this.Clear();
    }

    public void Show<TContext>(TContext context) where TContext : IContext
    {
        Show(FindDialog(context), context);
    }

    public void Show<TContext, TDialog>(TContext context)
        where TContext : IContext
        where TDialog : IDialog
    {
        Type type = typeof(TDialog);
        Show((T) => T.Equals(type), context);
    }

    public void ShowDialog<TContext>(TContext context) where TContext : IContext
    {
        Show(FindDialog(context), context, true);
    }

    public void ShowDialog<TContext, TDialog>(TContext context)
        where TContext : IContext
        where TDialog : IDialog
    {
        Type type = typeof(TDialog);
        Show((T) => T.Equals(type), context, true);
    }

    public void Register<TDialog>() where TDialog : class, IDialog
    {
        Type type = typeof(TDialog);

        if(_dialogTypes.Any(dialog => dialog.Equals(type)))
        {
            return;
        }

        _dialogTypes.Add(type);
    }

    private void Close(Func<IDialog, bool> isExist)
    {
        var opened = _openedDialogs.Where(isExist).ToList();

        try
        {
            foreach(var dialog in opened)
            {
                dialog.Close();
                dialog.DataContext = null;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        finally
        {
            foreach (var dialog in opened)
            {
                this._openedDialogs.Remove(dialog);
            }
        }
    }

    private bool? Show<TContext>(Func<Type, bool> isExist, TContext context, bool isModal = false) where TContext : IContext
    {
        if(_dialogTypes.Count == 0)
        {
            throw new NotImplementedException("There is no matched dialog type");
        }

        var dialogType = _dialogTypes.First(isExist);
        var dialog = Activator.CreateInstance(dialogType) as IDialog;

        if(dialog == null)
        {
            throw new NotImplementedException(dialogType.Name + " is not IDialog type.");
        }

        var dataContext = dialog.DataContext;

        if(dataContext is IDialogContext &&
            isModal)
        {
            _openedDialogs.Add(dialog);
            return dialog.ShowDialog();
        }

        if(dataContext is IContext)
        {
            _openedDialogs.Add(dialog);
            dialog.DataContext = context;
            dialog.Show();
            dialog.Focus();
            return null;
        }

        throw new NotImplementedException(dialog.GetType().Name + " has not type.");
    }

    IDialog IDialogService.GetDialog<TContext>()
    {
        foreach(IDialog dialog in _openedDialogs)
        {
            if(dialog.DataContext is TContext)
            {
                return dialog;
            }
        }

        throw new NotImplementedException(typeof(TContext).Name + " is not contain in opened dialog list.");
    }

    private static Func<Type, bool> FindDialog<TContext>(TContext context) where TContext : IContext
    {
        return (T) =>
        {
            if(!T.Name.Contains(VIEW_NAME_TAG))
            {
                throw new InvalidNamingException(T.Name + " is not contain " + VIEW_NAME_TAG);
            }

            if(!context.GetType().Name.Contains(VIEWMODEL_NAME_TAG))
            {
                throw new InvalidNamingException(context.GetType().Name + " is not contain " + VIEWMODEL_NAME_TAG);
            }

            return T.Name.Replace(VIEW_NAME_TAG, string.Empty).Equals(context.GetType().Name.Replace(VIEWMODEL_NAME_TAG, string.Empty));
        };
    }

    public void Dispose()
    {
        this.Clear();
    }
}
