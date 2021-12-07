using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountDownWinform
{
    public class CommonHelper
    {
        private static string Name = "公招1,公招2,公招3,公招4,发电站1,发电站2,制造站1,制造站2,制造站3,制造站4,贸易站1,贸易站2,贸易站3,线索交流,专精,办公室,备用";
        private static string Time = "9,9,9,9,24,24,24,24,24,24,24,24,24,24,24,24,24";

        public static IList<ComboBoxEntity> GetCboxData()
        {
            IList<ComboBoxEntity> infoList = new List<ComboBoxEntity>();
            var nameList = Name.Split(',');
            var timeList = Time.Split(',');
            for (int i = 0; i < nameList.Length; i++)
            {
                ComboBoxEntity comboBoxItem = new ComboBoxEntity()
                {
                    Text = nameList[i],
                    Value = timeList[i]
                };
                infoList.Add(comboBoxItem);
            }
            return infoList;
        }

        public static string TimespanToText(TimeSpan ts)
        {
            string str = "";
            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString() + "小时 " + ts.Minutes.ToString() + "分钟 " + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString() + "分钟 " + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = ts.Seconds + "秒";
            }
            return str;
        }
    }
}
