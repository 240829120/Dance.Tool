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

namespace Dance.Tool.Module
{
    /// <summary>
    /// 欢迎视图
    /// </summary>
    public class WelcomeView : Control
    {
        static WelcomeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WelcomeView), new FrameworkPropertyMetadata(typeof(WelcomeView)));
        }
    }
}
