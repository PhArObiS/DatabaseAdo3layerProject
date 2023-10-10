
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
    public partial class Form1 : Form
    {
        internal enum Grids
        {
            Students,
            Programs,
            Courses,
            Enrollments
        }
        internal static Form1 current;


        internal Grids grid;
        public Form1()
        {
            current = this;
            InitializeComponent();
        }



        

        private void Form1_Load(object sender, EventArgs e)
        {
            new Form2();
            Form2.current.Visible = false;
            new Form3();
            Form3.current.Visible = false;



            dataGridView1.Dock = DockStyle.Fill;
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bindingSource2;
            if (BusinessLayer.Programs.UpdatePrograms() != -1)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Programs.GetPrograms();
                bindingSource1.Sort = "ProgId";
                dataGridView1.DataSource = bindingSource1;
                dataGridView1.Columns["ProgId"].HeaderText = "Program ID";
                dataGridView1.Columns["progName"].HeaderText = "Program Name";
                dataGridView1.Columns["ProgId"].DisplayIndex = 0;
                dataGridView1.Columns["progName"].DisplayIndex = 1;

            }
        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bindingSource2;
            if (BusinessLayer.Courses.UpdateCourses() != -1)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Courses.GetCourses();
                bindingSource2.Sort = "CId";
                dataGridView1.DataSource = bindingSource2;
                dataGridView1.Columns["CId"].HeaderText = "Course ID";
                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["ProgId"].HeaderText = "Program ID";
                dataGridView1.Columns["CId"].DisplayIndex = 0;
                dataGridView1.Columns["CName"].DisplayIndex = 1;
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;
            }

        }
        internal static void DALMessage(string s)
        {
            MessageBox.Show("Data Layer: " + s);
        }
        internal static void BLLMessage(string s)
        {
            MessageBox.Show("Business Layer: " + s);
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bindingSource2;
            if (BusinessLayer.Students.UpdateStudents() != -1)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource3.DataSource = Data.Students.GetStudents();
                bindingSource3.Sort = "StId";
                dataGridView1.DataSource = bindingSource3;
            }
        }

        private void enrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.DataSource = Data.Enrollments.GetDisplayEnrollments();

                dataGridView1.Columns["StId"].HeaderText = "Student Id";
                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["StName"].HeaderText = "Student Name";
                dataGridView1.Columns["StName"].DisplayIndex = 1;

                dataGridView1.Columns["CId"].HeaderText = "Course Id";
                dataGridView1.Columns["CId"].DisplayIndex = 2;
                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["CName"].DisplayIndex = 3; 

                dataGridView1.Columns["ProgId"].HeaderText = "Program Id";
                dataGridView1.Columns["ProgId"].DisplayIndex = 4;
                dataGridView1.Columns["ProgName"].HeaderText = "Prograsm Name";
                dataGridView1.Columns["ProgName"].DisplayIndex = 5;

                dataGridView1.Columns["FinalGrade"].HeaderText = "Final Grade";
                dataGridView1.Columns["FinalGrade"].DisplayIndex = 6;


            }
           
        }
        private void manageFinalGradeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("A line must be selected for final grade update");
            }
            else if (selectedRows.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update");
            }
            else
            {
                if (Form3.current == null)
                    new Form3();

                Form3.current.Start(Form3.Modes.Manage_Final_Grade, selectedRows);
            }

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Programs.UpdatePrograms();

        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Courses.UpdateCourses();
        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Students.UpdateStudents();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Impossible to insert / update / delete");
            dataGridView1.CancelEdit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form2.current == null)
                new Form2();

            Form2.current.Start(Form2.Modes.INSERT, null);
        }


        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to modify.");
                return;
            }

            Form2.current.Start(Form2.Modes.MODIFY, selectedRows);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("At least one line must be selected for deletion");
                return;
            }

            List<string[]> LId = new List<string[]>();
            for (int i = 0; i < selectedRows.Count; i++)
            {
                LId.Add(new string[] {
            selectedRows[i].Cells["StId"].Value.ToString(),
            selectedRows[i].Cells["CId"].Value.ToString()});
            }

            // Assuming DeleteEnrollments method is part of your data layer
            int result = Data.Enrollments.DeleteEnrollments(LId);
            if (result == 0)
            {
                MessageBox.Show("Enrollments deleted successfully.");
            }
            else
            {
                MessageBox.Show("Error deleting enrollments.");
            }
        }


    }
}

    





