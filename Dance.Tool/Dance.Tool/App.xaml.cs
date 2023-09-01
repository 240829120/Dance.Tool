﻿using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Tool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            DanceDomain.Current = new();
            DanceDomain.Current.IocBuilder.AddAssemblys("Dance.Tool.Module");
            DanceDomain.Current.Build();

            IDanceMonitorManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceMonitorManager>();
            manager.MonitorInfo = new DanceMonitorInfo();
        }
    }
}