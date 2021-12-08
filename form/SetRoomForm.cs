using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountDownWinform
{
    public partial class SetRoomForm : Form
    {
        public SetRoomForm()
        {
            InitializeComponent();
        }

        private int myIndex = 0;
        private int zzIndex = 0;
        private int fdIndex = 0;

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetLabel(sender, "贸易站", Color.FromArgb(51, 204, 255), ++myIndex);
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetLabel(sender, "制造站", Color.FromArgb(255, 204, 0), ++zzIndex);
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SetLabel(sender, "发电站", Color.FromArgb(203, 252, 105), ++fdIndex);
        }

        private void SetLabel(object sender, string name, Color color, int index)
        {
            var label = ((System.Windows.Forms.ContextMenuStrip)((System.Windows.Forms.ToolStripDropDownItem)sender).GetCurrentParent()).SourceControl;
            label.Text = name + index;
            label.ForeColor = color;
            SaveConfig();
        }


        private void SaveConfig()
        {
            var roomList = new List<ComboBoxEntity>();
            foreach (Control label in this.Controls)
            {
                if (label.GetType().ToString() == "System.Windows.Forms.Label" && label.Name != "label14" && label.Name != "label13" && label.Text != "未设定")
                {
                    roomList.Add(new ComboBoxEntity()
                    {
                        Index = label.Name.Substring(5),
                        Name = label.Text,
                        TypeName = label.Text.Substring(0,3),
                        Color = string.Format("{0},{1},{2}", label.ForeColor.R, label.ForeColor.G, label.ForeColor.B)
                    });
                }
            }
            SettingOptions so = new SettingOptions();
            so.SaveOption("roomList", JsonConvert.SerializeObject(roomList));
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void SetRoomForm_Load(object sender, EventArgs e)
        {
            List<ComboBoxEntity> roomList = CommonHelper.GetCboxData();
            foreach (var item in roomList)
            {
                var label = this.Controls["label" + item.Index];
                label.Text = item.Name;
                var listColor = item.Color.Split(',');
                label.ForeColor = Color.FromArgb(int.Parse(listColor[0]), int.Parse(listColor[1]), int.Parse(listColor[2]));
            }

        }

    }
}
