using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CountDownWinform
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox1, "强力模式为置顶弹窗，会影响全屏游戏，非强力模式为类似小刻食堂的气泡");
            toolTip1.SetToolTip(this.dataGridView1, "选中后按delete键删除该行");
            var data = new SettingOptions().ReadOption("countDownList");
            if (data != null)
            {
                List<CountDownEntity> list = JsonConvert.DeserializeObject<List<CountDownEntity>>(data);
                list.Sort((x, y) => { return x.Index.CompareTo(y.Index); });
                foreach (var item in list)
                {
                    AddDataGridView(Convert.ToDateTime(item.Time), item.Name, item.Remark);
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var time = Convert.ToDateTime(dataGridView1.Rows[i].Cells[2].Value.ToString());
                var timespan = time - DateTime.Now;

                if (timespan.TotalSeconds > 0)
                {
                    dataGridView1.Rows[i].Cells[3].Value = CommonHelper.TimespanToText(timespan);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(5000, "小刻食堂：倒计时已经结束", "倒计时名称：" + dataGridView1.Rows[i].Cells[0].Value.ToString(), ToolTipIcon.Info);
                    if (checkBox1.Checked)
                    {
                        this.TopMost = true;
                        if (WindowState == FormWindowState.Minimized)
                        {
                            WindowState = FormWindowState.Normal;
                            ShowInTaskbar = true;
                        }
                        timer1.Enabled = false;
                        MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString() + "倒计时已经结束");
                        timer1.Enabled = true;
                        this.TopMost = false;
                    }
                    dataGridView1.Rows.RemoveAt(i);
                    CheckHasRow();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var data = dataGridView1.SelectedRows[0];
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("确认要删除选中的行吗?", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    CheckHasRow();
                }
            }
        }

        private void CheckHasRow()
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
            }
            SaveCountDown();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon1.ShowBalloonTip(5000, "小刻食堂倒计时程序已最小化", "小刻食堂继续为你分秒必争", ToolTipIcon.None);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
            this.Activate();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCountForm acf = new AddCountForm();
            acf.ShowDialog(this);
        }

        public void AddDataGridView(DateTime timeStop, string name, string remake)
        {
            dataGridView1.Rows.Insert(0, name, remake, timeStop.ToString("yyyy/MM/dd HH:mm:ss"), CommonHelper.TimespanToText(timeStop - DateTime.Now));
            CheckHasRow();
        }

        public void SaveCountDown()
        {
            List<CountDownEntity> list = new List<CountDownEntity>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                list.Add(new CountDownEntity
                {
                    Index = row.Index,
                    Name = row.Cells["Column1"].Value.ToString(),
                    Time = row.Cells["Column2"].Value.ToString(),
                    Remark = row.Cells["Column3"].Value.ToString(),
                });
            }
            new SettingOptions().SaveOption("countDownList", JsonConvert.SerializeObject(list));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/LiuZiYang1/Ceobe-Canteen-Count-Down");
        }
    }
}
