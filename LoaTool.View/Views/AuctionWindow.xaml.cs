using System.Windows;
using LoaTool.Define.Interfaces;
using LoaTool.Util;

namespace LoaTool.View.Views
{
    /// <summary>
    /// AuctionWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AuctionWindow : Window, IDialog
    {
        public AuctionWindow()
        {
            InitializeComponent();
        }

        public void Positioning(IDialog? parent = null)
        {
            Define.View.WindowLocation windowLocation = ViewUtil.GetWindowLocation(Width);

            double left = windowLocation.Left;
            double top = windowLocation.Top;

            if(parent != null)
            {
                top += parent.Height;
            }

            Left = left;
            Top = top;
        }
    }
}
