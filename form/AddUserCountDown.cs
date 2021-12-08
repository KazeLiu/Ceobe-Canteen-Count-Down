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
    public partial class AddUserCountDown : Form
    {
        public AddUserCountDown()
        {
            InitializeComponent();
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Hour <= 0 && dateTimePicker1.Value.Minute <= 0 && dateTimePicker1.Value.Second <= 0) {
                MessageBox.Show("小于一秒的业务不接");
                return;
            }
            var timeStop = DateTime.Now;
            timeStop = timeStop.AddHours(dateTimePicker1.Value.Hour).AddMinutes(dateTimePicker1.Value.Minute).AddSeconds(dateTimePicker1.Value.Second);
            MainForm md = (MainForm)this.Owner;
            md.AddDataGridView(timeStop, label1.Text, textBox1.Text);
            this.Close();
        }
    }
}
