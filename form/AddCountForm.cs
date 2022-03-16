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
    public partial class AddCountForm : Form
    {
        public AddCountForm()
        {
            InitializeComponent();
        }

        private List<ComboBoxEntity> roomList;

        private void AddCountForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        private void LoadData()
        {
            roomList = CommonHelper.GetCboxData();
            if (roomList.Count < 1)
            {
                MessageBox.Show("检测到程序未初始化，点击确认进入设置页面");
                SetRoomForm srf = new SetRoomForm();
                DialogResult result = srf.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    LoadData();
                }
            }
            else {
                Control groupBox1 = this.Controls["groupBox1"];
                var changRoomList = roomList.FindAll(x => int.Parse(x.Index) <= 9);
                foreach (var item in changRoomList)
                {
                    Control button = groupBox1.Controls["room" + item.Index];
                    if (button != null) {
                        button.Text = item.Name;
                        if (item.Color != "0,0,0")
                        {
                            var listColor = item.Color.Split(',');
                            button.BackColor = Color.FromArgb(int.Parse(listColor[0]), int.Parse(listColor[1]), int.Parse(listColor[2]));
                        }
                    }
                   
                }
            }
          
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SetRoomForm srf = new SetRoomForm();
            DialogResult result = srf.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                LoadData();
            }
        }


        private void buttonRoom_Click(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            var info = (ButtonBase)sender;
            var roonData = roomList.Find(x => x.Name == info.Text);
            if (mouse.Button == MouseButtons.Left)
            {
                MainForm md = (MainForm) this.Owner;
                var timeStop = DateTime.Now.AddHours(roonData.Time);
                md.AddDataGridView(timeStop, info.Text, "");
            }
            else if (mouse.Button == MouseButtons.Right)
            {
                AddUserCountDown aucd = new AddUserCountDown();
                aucd.Text = "为【" + roonData.Name + "】添加自定义倒计时";
                aucd.Controls["label1"].Text = roonData.Name;
                aucd.ShowDialog(this.Owner);
            }
        }

        private void buttonRecruit_Click(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            var info = (ButtonBase)sender;
            if (mouse.Button == MouseButtons.Left)
            {
                MainForm md = (MainForm)this.Owner;
                var timeStop = DateTime.Now.AddHours(9);
                md.AddDataGridView(timeStop, info.Text, "");
            }
            else if (mouse.Button == MouseButtons.Right)
            {
                AddUserCountDown aucd = new AddUserCountDown();
                aucd.Text = "为【" + info.Text + "】添加自定义倒计时";
                aucd.Controls["label1"].Text = info.Text;
                aucd.ShowDialog(this.Owner);
            }
            label2.Text = "已经添加【" + info.Text + "】的倒计时";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUserCountDown aucd = new AddUserCountDown();
            aucd.Text = "添加自定义倒计时";
            aucd.Controls["label1"].Text = "自定义倒计时";
            aucd.ShowDialog(this.Owner);
            label2.Text = "已经添加自定义倒计时";
        }

    }
}
