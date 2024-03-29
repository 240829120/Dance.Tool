﻿using Dance.Wpf;
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
    /// PhysicsView.xaml 的交互逻辑
    /// </summary>
    public partial class PhysicsView : UserControl
    {
        public PhysicsView()
        {
            InitializeComponent();

            PhysicsViewModel vm = DanceDomain.Current.LifeScope.Resolve<PhysicsViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }
    }
}
