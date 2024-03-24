using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
