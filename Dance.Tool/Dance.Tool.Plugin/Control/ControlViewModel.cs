using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// 控件视图模型
    /// </summary>
    [DanceSingleton(typeof(ControlViewModel))]
    public class ControlViewModel : DanceViewModel
    {
        public ControlViewModel()
        {
            this.itemsSource = new();

            for (int i = 0; i < 30; i++)
            {
                this.itemsSource.Add(new() { Index = i, Name = $"name_{i}", Age = i + 17 });
            }
        }

        #region ItemsSource -- 数据源

        private ObservableCollection<ControlTestModel> itemsSource;
        /// <summary>
        /// 数据源
        /// </summary>
        public ObservableCollection<ControlTestModel> ItemsSource
        {
            get { return itemsSource; }
            set { itemsSource = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
