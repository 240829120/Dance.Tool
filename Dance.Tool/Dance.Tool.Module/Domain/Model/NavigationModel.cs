using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Module
{
    /// <summary>
    /// 导航模型
    /// </summary>
    public class NavigationModel : DanceModel
    {
        #region ViewType -- 视图类型

        private Type? viewType;
        /// <summary>
        /// 视图类型
        /// </summary>
        public Type? ViewType
        {
            get { return viewType; }
            set { viewType = value; this.OnPropertyChanged(nameof(ViewType)); }
        }

        #endregion
    }
}
