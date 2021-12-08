using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountDownWinform
{
    public class CommonHelper
    {

        public static List<ComboBoxEntity> GetCboxData()
        {
            SettingOptions so = new SettingOptions();
            var data = so.ReadOption("roomList");
            var roomList = new List<ComboBoxEntity>();
            if (data != null)
            {
                roomList = JsonConvert.DeserializeObject<List<ComboBoxEntity>>(data);
            }
            return roomList;
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
