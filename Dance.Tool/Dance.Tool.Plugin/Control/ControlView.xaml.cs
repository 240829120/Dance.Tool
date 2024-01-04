using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Tool.Plugin
{
    [ExpandableObject]
    public class Lession
    {
        [Category("基础"), Description("描述描述123")]
        public string? Name { get; set; }

        [Category("基础"), Description("描述描述123")]
        public int Age { get; set; }


    }

    [ExpandableObject]
    public class Teacher
    {

        [Category("基础"), Description("描述描述123")]
        public string? Name { get; set; }

        [Category("基础"), Description("描述描述123")]
        public int Age { get; set; }

        [Category("基础"), Description("描述描述1231111")]
        public List<Lession> Lessions { get; set; } = [new Lession { Name = "t1" }];


        [Category("基础"), Description("描述描述1231111")]
        public Lession Lession2 { get; set; } = new Lession();
    }

    public class Student
    {
        [Category("基础"), Description("描述描述123")]
        public string? Property_1_1 { get; set; }


        [Category("基础")]
        public int Property_1_2 { get; set; }


        [Category("基础")]
        public string? Property_1_3 { get; set; }

        [Category("基础")]
        public string? Property_1_4 { get; set; }


        [Category("基础2")]
        public System.Windows.Media.SolidColorBrush Brush { get; set; }

        [Category("基础2")]
        public Thickness Thickness { get; set; } = new Thickness(1, 2, 3, 4);

        [Category("基础2")]
        public DateTime DateTime { get; set; }

        [Category("基础2")]
        public TimeSpan TimeSpan { get; set; }

        [Category("基础2")]
        public System.Windows.Media.Color Color { get; set; }

        [Category("老师")]
        public Teacher Teacher { get; set; } = new();

        [Category("列表")]
        public List<Teacher> Items { get; set; } = [new Teacher { Name = "zs", Age = 17 }];

        [Category("列表")]
        public List<string> Items2 { get; set; } = [];

        [Category("列表")]
        public List<int> Items3 { get; set; } = [1, 2, 3];


        [Category("测试")]
        public HorizontalAlignment HorizontalAlignment { get; set; }
    }

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

            this.propertyGrid.SelectedObject = new Student();
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
