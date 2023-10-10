using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using finalWebProj;

namespace BusinessLayer
{
    internal class Students
    {
        internal static int UpdateStudents()
        {
            DataSet ds = Data.DataTables.GetDataSet();

            DataTable dt = ds.Tables["Students"].GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrEmpty(row["StName"].ToString()))
                    {
                        throw new Exception("Student name cannot be empty.");
                    }


                    if (string.IsNullOrEmpty(row["ProgId"].ToString()))
                    {
                        throw new Exception("Program ID cannot be empty.");
                    }


                }
            }

            return Data.Students.UpdateStudents();
        }
    }
    internal class Programs
    {
        internal static int UpdatePrograms()
        {
            DataSet ds = Data.DataTables.GetDataSet();

            DataTable dt = ds.Tables["Programs"]
                .GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    if (string.IsNullOrEmpty(row["ProgName"].ToString()))
                    {
                        throw new Exception("Program name cannot be empty.");
                    }


                    if (string.IsNullOrEmpty(row["ProgId"].ToString()))
                    {
                        throw new Exception("Program ID cannot be empty.");
                    }


                }
            }


            return Data.Programs.UpdatePrograms();
        }
    }

    internal class Courses
    {
        internal static int UpdateCourses()
        {
            DataSet ds = Data.DataTables.GetDataSet();

            DataTable dtCourses = ds.Tables["Courses"].GetChanges(DataRowState.Added | DataRowState.Modified);
            DataTable dtPrograms = ds.Tables["Programs"]; 

            if (dtCourses != null)
            {
                foreach (DataRow row in dtCourses.Rows)
                {
                    if (string.IsNullOrEmpty(row["CNAME"].ToString()))
                    {
                        throw new Exception("Course name cannot be empty.");
                    }

                    string progId = row["ProgId"].ToString(); 

                    if (string.IsNullOrEmpty(progId))
                    {
                        throw new Exception("Program ID cannot be empty.");
                    }
                    else
                    {
                        if (dtPrograms.AsEnumerable().Any(p => p.Field<string>("ProgId") == progId) == false)
                        {
                            throw new Exception($"Course cannot be updated as it does not belong to any program. Program ID: {progId} does not exist.");
                        }
                    }
                }
            }

            return Data.Courses.UpdateCourses();

        }
    }
   
    internal class Enrollments
    {
        internal static int UpdateFinalGrade(string[] a, string fg)
        {
            Nullable<int> finalGrade;
            int temp;
            
            if (fg == "")
            {
                finalGrade = null;

            }

            else if (int.TryParse(fg, out temp) && (0 <= temp && temp <= 100))
            {
                finalGrade = temp;
            }
            else
            {
                Form1.BLLMessage(
                    "Final grade must be an integer between 0 and 100"
                );
                return -1;
            }

            return Data.Enrollments.UpdateGrade(a, finalGrade);
        }

    }
}


