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
    /// ParticleView.xaml 的交互逻辑
    /// </summary>
    public partial class ParticleView : UserControl
    {
        public ParticleView()
        {
            InitializeComponent();

            ParticleViewModel vm = DanceDomain.Current.LifeScope.Resolve<ParticleViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.box == null)
                return;

            this.box.GenerateSpeed = (int)this.slider.Value;
            this.box2.GenerateSpeed = (int)this.slider.Value;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.box == null || this.box2 == null)
                return;

            this.box.IsEnabled = true;
            this.box2.IsEnabled = false;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (this.box == null || this.box2 == null)
                return;

            this.box.IsEnabled = false;
            this.box2.IsEnabled = true;
        }
    }
}
