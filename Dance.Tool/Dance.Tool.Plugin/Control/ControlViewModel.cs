using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
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
    [DanceSingleton]
    public class ControlViewModel : DanceViewModel
    {
        public ControlViewModel()
        {
            this.itemsSource = new();

            for (int i = 0; i < 30; i++)
            {
                this.itemsSource.Add(new()
                {
                    Index = i,
                    Name = $"name_{i}",
                    Age = i + 17,
                    Sex = $"sex_{i}",
                    City = $"city_{i}",
                    School = $"school_{i}"
                });
            }

            this.ShowMessageBoxCommand = new RelayCommand(this.ShowMessageBox);
            this.ShowIconMessageBoxCommand = new RelayCommand(this.ShowIconMessageBox);
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

        #region ShowMessageBoxCommand -- 显示消息框命令

        /// <summary>
        /// 显示消息框命令
        /// </summary>
        public RelayCommand ShowMessageBoxCommand { get; private set; }

        /// <summary>
        /// 显示消息框
        /// </summary>
        private void ShowMessageBox()
        {
            DanceMessageBox.Show("头部", "内容");
        }

        #endregion

        #region ShowIconMessageBoxCommand -- 显示图标消息框命令

        /// <summary>
        /// 显示图标消息框命令
        /// </summary>
        public RelayCommand ShowIconMessageBoxCommand { get; private set; }

        /// <summary>
        /// 显示图标消息框
        /// </summary>
        private void ShowIconMessageBox()
        {
            DanceMessageBox.Show("头部", DanceResourceIcons.Success, "内容", DanceMessageBoxAction.YES);
        }

        #endregion
    }
}
