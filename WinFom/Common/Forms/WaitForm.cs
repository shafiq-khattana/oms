using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFom.Common.Forms
{
    public partial class WaitForm : Form
    {
        private Action formAction = null;
        Random rnd = new Random();
        public WaitForm(Action action, string msg = "Please wait...")
        {
            InitializeComponent();
            formAction = action;
            label1.Text = msg;
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            int tick = rnd.Next(11) + 1;
            switch(tick)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.animated1;
                    break;

                case 2:
                    pictureBox1.Image = Properties.Resources.animated2;
                    break;

                case 3:
                    pictureBox1.Image = Properties.Resources.animated3;
                    break;

                case 4:
                    pictureBox1.Image = Properties.Resources.animated4;
                    break;


                case 5:
                    pictureBox1.Image = Properties.Resources.animated5;
                    break;

                case 6:
                    pictureBox1.Image = Properties.Resources.animated5;
                    break;

                case 7:
                    pictureBox1.Image = Properties.Resources.animated6;
                    break;

                case 8:
                    pictureBox1.Image = Properties.Resources.animated8;
                    break;

                case 9:
                    pictureBox1.Image = Properties.Resources.animated7;
                    break;

                case 10:
                    pictureBox1.Image = Properties.Resources.animated8;
                    break;

                case 11:
                    pictureBox1.Image = Properties.Resources.animated9;
                    break;

                


            }

            Task.Factory.StartNew(formAction)
                .ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
