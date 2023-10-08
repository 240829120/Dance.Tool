using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// 物理引擎
    /// </summary>
    [DanceSingleton]
    public class PhysicsViewModel : DanceViewModel
    {
        public PhysicsViewModel()
        {
            int count = this.Random.Next(20, 100);

            for (int i = 0; i < count; i++)
            {
                this.testModels.Add(new()
                {
                    Index = i,
                    Age = i,
                    City = $"City_{i}",
                    Name = $"Name_{i}",
                    School = $"School_{i}",
                    Sex = $"Sex_{i}"
                });
            }
        }

        private Random Random = new();

        #region TestModels -- 测试数据源

        private List<ControlTestModel> testModels = new();
        /// <summary>
        /// 测试数据源
        /// </summary>
        public List<ControlTestModel> TestModels
        {
            get { return testModels; }
            set { testModels = value; this.OnPropertyChanged(); }
        }

        #endregion
    }

}
