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
    /// 主视图顶部
    /// </summary>
    public class MainTopView : Control
    {
        static MainTopView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainTopView), new FrameworkPropertyMetadata(typeof(MainTopView)));
        }

        public MainTopView()
        {
            MainTopViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainTopViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }
    }
}
