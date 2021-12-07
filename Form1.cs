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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            comboBox1.DataSource = CommonHelper.GetCboxData();
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Text";

            toolTip1.SetToolTip(this.checkBox1, "强力模式为置顶弹窗，会影响全屏游戏，非强力模式为类似小刻食堂的气泡");
            toolTip1.SetToolTip(this.dataGridView1, "选中后按delete键删除该行");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var timeStop = DateTime.Now;
            timeStop = timeStop.AddHours(dateTimePicker1.Value.Hour).AddMinutes(dateTimePicker1.Value.Minute).AddSeconds(dateTimePicker1.Value.Second);
            dataGridView1.Rows.Insert(0, comboBox1.Text, textBox1.Text, timeStop.ToString("yyyy/MM/dd HH:mm:ss"), CommonHelper.TimespanToText(timeStop - DateTime.Now));
            textBox1.Text = "";
            if (dataGridView1.Rows.Count > 0 && !timer1.Enabled)
            {
                timer1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var time = 0;
            int.TryParse(comboBox1.SelectedValue.ToString(), out time);
            if (time == 24)
            {
                dateTimePicker1.Value = new DateTime(2021, 1, 1, 23, 59, 59);
            }
            else
            {
                dateTimePicker1.Value = new DateTime(2021, 1, 1, time, 0, 0);
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
            var data = dataGridView1.SelectedRows[0];
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

        public void CheckHasRow()
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                timer1.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
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

    }
}
