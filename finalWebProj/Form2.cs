using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalWebProj
{
    public partial class Form2 : Form
    {
        internal enum Modes
        {
            INSERT,
            MODIFY,
        }

        internal static Form2 current;
        private Modes mode = Modes.INSERT;
        private string[] assignInitial;

        public Form2()
        {
            current = this;
            InitializeComponent();
        }

        internal void Start(Modes m, DataGridViewSelectedRowCollection c)
        {
            mode = m;
            Text = "" + mode;

            comboBox1.DisplayMember = "StId";
            comboBox1.ValueMember = "StId";
            comboBox1.DataSource = Data.Students.GetStudents();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            comboBox2.DisplayMember = "CId";
            comboBox2.ValueMember = "CId";
            comboBox2.DataSource = Data.Courses.GetCourses();
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = 0;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;

            if ((mode != Modes.INSERT) && (c != null))
            {
                comboBox1.SelectedValue = c[0].Cells["StId"].Value;
                comboBox2.SelectedValue = c[0].Cells["CId"].Value;
                assignInitial = new string[] { c[0].Cells["StId"].Value.ToString(), c[0].Cells["CId"].Value.ToString() };
            }

            if (mode != Modes.INSERT)
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = true;
            }

            ShowDialog();
        }


        private void comboBox1_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var student = (DataRowView)comboBox1.SelectedItem;
                textBox4.Text = student["StName"].ToString();
            }
        }

        private void comboBox2_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var course = (DataRowView)comboBox2.SelectedItem;
                textBox2.Text = course["CName"].ToString();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int r = -1;
            if (mode == Modes.INSERT)
            {
                r = Data.Enrollments.InsertData(new string[] { Convert.ToString(comboBox1.SelectedValue), Convert.ToString(comboBox2.SelectedValue) });
            }
            else if (mode == Modes.MODIFY)
            {
                r = Data.Enrollments.UpdateEnrollments(new string[] { (string)comboBox1.SelectedValue, (string)comboBox2.SelectedValue });
            }


            if (r >= 0)
            {
                Close();
            }
            else
            {
                MessageBox.Show("An error occurred while saving the enrollment.");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


