using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountDownWinform
{
    public class ComboBoxEntity
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// 房间类型名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 房间名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 倒计时默认值(小时)
        /// </summary>
        public int Time { get; set; } = 24;

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

    }

    public class CountDownEntity {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string Time { get; set; }

    }
}
