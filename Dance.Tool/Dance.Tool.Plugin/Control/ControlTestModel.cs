using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// 控件测试模型
    /// </summary>
    public class ControlTestModel : DanceModel
    {
        #region Index -- 索引

        private int index;
        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Age -- 年龄

        private int age;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Sex -- 性别

        private string? sex;
        /// <summary>
        /// 性别
        /// </summary>
        public string? Sex
        {
            get { return sex; }
            set { sex = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region City -- 城市

        private string? city;
        /// <summary>
        /// 城市
        /// </summary>
        public string? City
        {
            get { return city; }
            set { city = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region School -- 学校

        private string? school;
        /// <summary>
        /// 学校
        /// </summary>
        public string? School
        {
            get { return school; }
            set { school = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
