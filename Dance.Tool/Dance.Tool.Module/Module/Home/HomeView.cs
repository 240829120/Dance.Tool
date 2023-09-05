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
    /// 首页
    /// </summary>
    public class HomeView : Control
    {
        static HomeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HomeView), new FrameworkPropertyMetadata(typeof(HomeView)));
        }

        public HomeView()
        {
            HomeViewModel vm = DanceDomain.Current.LifeScope.Resolve<HomeViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }
    }
}
