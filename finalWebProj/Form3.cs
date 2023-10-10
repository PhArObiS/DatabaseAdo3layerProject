using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace finalWebProj
{
    public partial class Form3 : Form
    {
        internal enum Modes
        {
            
            Manage_Final_Grade

        }

        internal static Form3 current;

        private Modes mode = Modes.Manage_Final_Grade;
        private string[] assignInitial;


        public Form3()
        {
            current = this;
            InitializeComponent();
        }

        internal void Start(Modes m, DataGridViewSelectedRowCollection c)
        {
            mode = m;
            Text = "" + mode;



            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;



            if (mode == Modes.Manage_Final_Grade && c != null)
            {
                textBox1.ReadOnly = true;
                textBox1.Text = "" + c[0].Cells["StId"].Value;
                textBox2.Text = "" + c[0].Cells["StName"].Value;
                textBox3.Text = "" + c[0].Cells["CId"].Value;
                textBox4.Text = "" + c[0].Cells["CName"].Value;
                textBox5.Text = c[0].Cells["FinalGrade"].Value.ToString();
                assignInitial = new string[] { c[0].Cells["StId"].Value.ToString(), c[0].Cells["StName"].Value.ToString(), c[0].Cells["CId"].Value.ToString(), c[0].Cells["CName"].Value.ToString(), c[0].Cells["FinalGrade"].Value.ToString() };

            }

            ShowDialog();
        }

       

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = -1;

            if (mode == Modes.Manage_Final_Grade)
            {
                result = BusinessLayer.Enrollments.UpdateFinalGrade(assignInitial, textBox5.Text);
            }

            if (result == 0)
            {
                Close();
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}



































