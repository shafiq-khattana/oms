using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFom.Test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(panel1.Visible == false)
            {
                bunifuTransition1.ShowSync(panel1);
            }
            else
            {
                bunifuTransition1.HideSync(panel1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == false)
            {
                bunifuTransition2.ShowSync(panel1);
            }
            else
            {
                bunifuTransition2.HideSync(panel1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == false)
            {
                bunifuTransition3.ShowSync(panel1);
            }
            else
            {
                bunifuTransition3.HideSync(panel1);
            }
        }
    }
}
