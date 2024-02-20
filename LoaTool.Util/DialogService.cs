using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoaTool.Define.Interfaces;

namespace LoaTool.Util;
public class DialogService : IDialogService
{
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

    public void Close<TContext>(TContext context) where TContext : IContext
    {
        Close((T) =>
        {
            var dataContext = T.DataContext as IDialogContext;
            if(dataContext == null)
            {
                throw new NullReferenceException(T.GetType().Name + " is not IDialogContext type.");
            }
            return dataContext.Context.Equals(context);
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

    public void Dispose()
    {
        this.Clear();
    }

    public void Show<TContext>(TContext context) where TContext : IContext
    {
        Show((T) => true, context);
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
        Show((T) => true, context, true);
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
}
