using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoaTool.Define.Interfaces;
public interface IDialogService : IDisposable
{
    void Register<TDialog>() where TDialog : class, IDialog;
    void Show<TContext>(TContext context) where TContext : IContext;
    void Show<TContext, TDialog>(TContext context) where TContext : IContext where TDialog : IDialog;
    void ShowDialog<TContext>(TContext context) where TContext : IContext;
    void ShowDialog<TContext, TDialog>(TContext context) where TContext : IContext where TDialog : IDialog;
    void Close<TContext>(TContext context) where TContext : IContext;
    void Close<TContext, TDialog>(TContext context) where TContext : IContext where TDialog : IDialog;
    void Clear();
}
