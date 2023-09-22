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
    /// ControlView.xaml 的交互逻辑
    /// </summary>
    public partial class ControlView : UserControl
    {
        public ControlView()
        {
            InitializeComponent();

            ControlViewModel vm = DanceDomain.Current.LifeScope.Resolve<ControlViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }

        private void btBlue_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new() { Source = new Uri("pack://application:,,,/Dance.Tool.Plugin;component/Control/Theme_Blue.xaml") };

            foreach (var item in resource.Keys)
            {
                Application.Current.Resources[item] = resource[item];
            }
        }

        private void btRed_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new() { Source = new Uri("pack://application:,,,/Dance.Tool.Plugin;component/Control/Theme_Red.xaml") };

            foreach (var item in resource.Keys)
            {
                Application.Current.Resources[item] = resource[item];
            }
        }

        private void btGreen_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new() { Source = new Uri("pack://application:,,,/Dance.Tool.Plugin;component/Control/Theme_Green.xaml") };

            foreach (var item in resource.Keys)
            {
                Application.Current.Resources[item] = resource[item];
            }
        }

        private void btOrange_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new() { Source = new Uri("pack://application:,,,/Dance.Tool.Plugin;component/Control/Theme_Orange.xaml") };

            foreach (var item in resource.Keys)
            {
                Application.Current.Resources[item] = resource[item];
            }
        }
    }
}
