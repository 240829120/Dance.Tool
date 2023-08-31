using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Tool.Module
{
    /// <summary>
    /// 主视图
    /// </summary>
    public class MainView : Control
    {
        static MainView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainView), new FrameworkPropertyMetadata(typeof(MainView)));
        }

        public MainView()
        {
            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }
    }
}
