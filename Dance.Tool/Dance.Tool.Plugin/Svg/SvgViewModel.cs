using CsvHelper;
using Dance.Wpf;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// Svg视图
    /// </summary>
    [DanceSingleton]
    public class SvgViewModel : DanceViewModel
    {
        public SvgViewModel()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Svg");

            foreach (string p in Directory.GetFiles(path, "*.svg"))
            {
                this.ItemsSource.Add(Path.GetFileName(p));
            }

            this.SelectedValue = this.ItemsSource.FirstOrDefault();
        }

        #region ItemsSoruce -- 源

        /// <summary>
        /// 源
        /// </summary>
        public List<string> ItemsSource { get; } = [];

        #endregion

        #region SelectedValue -- 选中值

        private string? selectedValue;
        /// <summary>
        /// 选中值
        /// </summary>
        public string? SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                this.OnPropertyChanged();

                if (string.IsNullOrWhiteSpace(value))
                {
                    this.ImageSource = null;
                    return;
                }

                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Svg", value);

                if (!File.Exists(path))
                {
                    this.ImageSource = null;
                    return;
                }

                this.ImageSource = DanceXamlExpansion.GetSvgImageSource(new Uri(path));
            }
        }


        #endregion

        #region ImageSource -- 图片源

        private ImageSource? imageSource;
        /// <summary>
        /// 图片源
        /// </summary>
        public ImageSource? ImageSource
        {
            get { return imageSource; }
            private set { imageSource = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
