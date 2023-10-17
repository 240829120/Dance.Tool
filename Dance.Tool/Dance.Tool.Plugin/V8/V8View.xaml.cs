using Dance.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// V8View.xaml 的交互逻辑
    /// </summary>
    public partial class V8View : UserControl
    {
        public V8View()
        {
            InitializeComponent();

            V8ViewModel vm = DanceDomain.Current.LifeScope.Resolve<V8ViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }
    }
}
