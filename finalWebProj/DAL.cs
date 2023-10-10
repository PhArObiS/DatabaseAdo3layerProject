using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using finalWebProj;

namespace Data
{
    internal class Connect
    {
        private static string cliComConnectionString = GetConnectString();

        internal static string ConnectionString { get => cliComConnectionString; }

        private static string GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "College1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;
        }
    }

    internal class DataTables
    {
        private static SqlDataAdapter adapterPrograms = InitAdapterPrograms();
        private static SqlDataAdapter adapterCourses = InitAdapterCourses();
        private static SqlDataAdapter adapterStudents = InitAdapterStudents();
        private static SqlDataAdapter adapterEnrollments = InitAdapterEnrollments();
        private static SqlDataAdapter adapterDisplayEnrollments = InitAdapterDisplayEnrollments();

        private static DataSet ds = InitDataSet();

        private static SqlDataAdapter InitAdapterStudents()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Students ORDER BY StId",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterPrograms()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Programs ORDER BY ProgId",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }

        private static SqlDataAdapter InitAdapterCourses()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Courses ORDER BY CId",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }


        private static SqlDataAdapter InitAdapterEnrollments()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT * FROM Enrollments ORDER BY StId, CId",
                Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(r);
            r.UpdateCommand = builder.GetUpdateCommand();

            return r;
        }
        private static SqlDataAdapter InitAdapterDisplayEnrollments()
        {
            SqlDataAdapter r = new SqlDataAdapter(
                "SELECT s.StId, s.StName, c.CId, c.CName, c.ProgId, p.ProgName, e.FinalGrade " +
                "FROM Enrollments e, Students s, Courses c , Programs p " +
                "WHERE e.StId = s.StId AND e.CId = c.CId AND c.ProgId = p.ProgId " +
                "ORDER BY StId, CId ;",
                Connect.ConnectionString);

            return r;
        }


        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();
            LoadPrograms(ds);
            LoadCourses(ds);
            LoadStudents(ds);
            LoadEnrollments(ds);
            LoadDisplayEnrollments(ds);

            return ds;
        }

        private static void LoadPrograms(DataSet ds)
        {
            adapterPrograms.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterPrograms.Fill(ds, "Programs");
            ds.Tables["Programs"].PrimaryKey = new DataColumn[] { ds.Tables["Programs"].Columns["ProgId"] };
        }

        private static void LoadCourses(DataSet ds)
        {
            adapterCourses.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterCourses.Fill(ds, "Courses");
            ds.Tables["Courses"].PrimaryKey = new DataColumn[] { ds.Tables["Courses"].Columns["CId"] };


            ForeignKeyConstraint fkProgramsCourses = new ForeignKeyConstraint(
                "FK_Programs_Courses",
                ds.Tables["Programs"].Columns["ProgId"],
                ds.Tables["Courses"].Columns["ProgId"]
            );
            fkProgramsCourses.DeleteRule = Rule.Cascade;
            fkProgramsCourses.UpdateRule = Rule.Cascade;
            ds.Tables["Courses"].Constraints.Add(fkProgramsCourses);
        }

        private static void LoadStudents(DataSet ds)
        {
            adapterStudents.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterStudents.Fill(ds, "Students");
            ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["StId"] };


            ForeignKeyConstraint fkProgramsStudents = new ForeignKeyConstraint(
                "FK_Programs_Students",
                ds.Tables["Programs"].Columns["ProgId"],
                ds.Tables["Students"].Columns["ProgId"]
            );
            fkProgramsStudents.DeleteRule = Rule.Cascade;
            fkProgramsStudents.UpdateRule = Rule.Cascade;
            ds.Tables["Students"].Constraints.Add(fkProgramsStudents);
        }

        private static void LoadEnrollments(DataSet ds)
        {
            adapterEnrollments.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterEnrollments.Fill(ds, "Enrollments");
            ds.Tables["Enrollments"].PrimaryKey = new DataColumn[] { ds.Tables["Enrollments"].Columns["StId"], ds.Tables["Enrollments"].Columns["CId"] };

            ForeignKeyConstraint fkStudentsEnrollments = new ForeignKeyConstraint(
                "FK_Students_Enrollments",
                ds.Tables["Students"].Columns["StId"],
                ds.Tables["Enrollments"].Columns["StId"]
            );
            fkStudentsEnrollments.DeleteRule = Rule.Cascade;
            fkStudentsEnrollments.UpdateRule = Rule.Cascade;
            ds.Tables["Enrollments"].Constraints.Add(fkStudentsEnrollments);

            ForeignKeyConstraint fkCoursesEnrollments = new ForeignKeyConstraint(
                "FK_Courses_Enrollments",
                ds.Tables["Courses"].Columns["CId"],
                ds.Tables["Enrollments"].Columns["CId"]
            );
            fkCoursesEnrollments.DeleteRule = Rule.None;
            fkCoursesEnrollments.UpdateRule = Rule.None;
            ds.Tables["Enrollments"].Constraints.Add(fkCoursesEnrollments);
        }
        private static void LoadDisplayEnrollments(DataSet ds)
        {
            adapterDisplayEnrollments.Fill(ds, "DisplayEnrollments");
        }


            internal static SqlDataAdapter GetAdapterPrograms()
        {
            return adapterPrograms;
        }

        internal static SqlDataAdapter GetAdapterCourses()
        {
            return adapterCourses;
        }

        internal static SqlDataAdapter GetAdapterStudents()
        {
            return adapterStudents;
        }

        internal static SqlDataAdapter GetAdapterEnrollments()
        {
            return adapterEnrollments;
        }

        internal static SqlDataAdapter GetAdapterDisplayEnrollments()
        {
            return adapterDisplayEnrollments;
        }


        internal static DataSet GetDataSet()
        {
            return ds;
        }
    }

    internal class Programs
    {
        private static SqlDataAdapter adapter = DataTables.GetAdapterPrograms();
        private static DataSet ds = DataTables.GetDataSet();

        internal static DataTable GetPrograms()
        {
            return ds.Tables["Programs"];
        }

        internal static int UpdatePrograms()
        {
            if (!ds.Tables["Programs"].HasErrors)
            {
                return adapter.Update(ds.Tables["Programs"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Courses
    {
        private static SqlDataAdapter adapter = DataTables.GetAdapterCourses();
        private static DataSet ds = DataTables.GetDataSet();

        internal static DataTable GetCourses()
        {
            return ds.Tables["Courses"];
        }

        internal static int UpdateCourses()
        {
            if (!ds.Tables["Courses"].HasErrors)
            {
                return adapter.Update(ds.Tables["Courses"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Students
    {
        private static SqlDataAdapter adapter = DataTables.GetAdapterStudents();
        private static DataSet ds = DataTables.GetDataSet();

        internal static DataTable GetStudents()
        {
            return ds.Tables["Students"];
        }

        internal static int UpdateStudents()
        {
            if (!ds.Tables["Students"].HasErrors)
            {
                return adapter.Update(ds.Tables["Students"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Enrollments
    {
        private static SqlDataAdapter adapter = DataTables.GetAdapterEnrollments();
        private static DataSet ds = DataTables.GetDataSet();
        private static DataTable DisplayEnroll = null;


        internal static DataTable GetDisplayEnrollments()
        {
            DisplayEnroll = ds.Tables["DisplayEnrollments"];
            return DisplayEnroll;
            
        }


        internal static int InsertData(string[] a)
        {
            var existingEnrollment = (
                from enroll in ds.Tables["Enrollments"].AsEnumerable()
                where enroll.Field<string>("StId") == a[0]
                where enroll.Field<string>("CId") == a[1]
                select enroll);

            if (existingEnrollment.Count() > 0)
            {
                finalWebProj.Form1.DALMessage("This enrollment already exists");
                return -1;
            }

            
            var studentProgId = ds.Tables["Students"].AsEnumerable()
                .Where(s => s.Field<string>("StId") == a[0])
                .Select(s => s.Field<string>("ProgId")).Single();

            var courseProgId = ds.Tables["Courses"].AsEnumerable()
                .Where(c => c.Field<string>("CId") == a[1])
                .Select(c => c.Field<string>("ProgId")).Single();

            if (studentProgId != courseProgId)
            {
                finalWebProj.Form1.DALMessage("A student can only enroll in courses of their program");
                return -1;
            }

            
            try
            {
                DataRow line = ds.Tables["Enrollments"].NewRow();
                line.SetField("StId", a[0]);
                line.SetField("CId", a[1]);
                line.SetField<Nullable<int>>("FinalGrade", null);

                ds.Tables["Enrollments"].Rows.Add(line);

                adapter.Update(ds.Tables["Enrollments"]);

                if (DisplayEnroll != null)
                {
                    var query = (
                        from std in ds.Tables["Students"].AsEnumerable()
                        from crs in ds.Tables["Courses"].AsEnumerable()
                        where std.Field<string>("StId") == a[0]
                        where crs.Field<string>("CId") == a[1]
                        select new
                        {
                            StId = std.Field<string>("StId"),
                            StName = std.Field<string>("StName"),
                            CId = crs.Field<string>("CId"),
                            CName = crs.Field<string>("CName"),
                            FinalGrade = line.Field<Nullable<int>>("FinalGrade")
                        });

                    var r = query.Single();
                    DisplayEnroll.Rows.Add(new object[] { r.StId, r.StName, r.CId, r.CName, r.FinalGrade });
                }
                return 0;
            }
            catch (Exception)
            {
                finalWebProj.Form1.DALMessage("Insertion / Update rejected");
                return -1;
            }
        }

        internal static int UpdateEnrollments(string[] a)
        {
            if (ds.Tables["Enrollments"].HasErrors)
            {
                foreach (DataRow row in ds.Tables["Enrollments"].GetErrors())
                {
                    // Log or show the row error
                    Console.WriteLine(row.RowError);
                }
                return -1;
            }
            else
            {
                return adapter.Update(ds.Tables["Enrollments"]);
            }

        }


        internal static int DeleteEnrollments(List<string[]> lId)
        {
            try
            {
                var lines = ds.Tables["Enrollments"].AsEnumerable()
                                    .Where(s =>
                                       lId.Any(x => (x[0] == s.Field<string>("StId") && x[1] == s.Field<string>("CId"))));

                foreach (var line in lines)
                {
                    if (line.Field<Nullable<int>>("FinalGrade") != null)
                    {
                        finalWebProj.Form1.DALMessage("Enrollment cannot be deleted because final grade has been assigned.");
                        return -1;
                    }
                    line.Delete();
                }

                adapter.Update(ds.Tables["Enrollments"]);

                if (DisplayEnroll != null)
                {
                    foreach (var p in lId)
                    {
                        var r = DisplayEnroll.AsEnumerable()
                                    .Where(s => (s.Field<string>("StId") == p[0] && s.Field<string>("CId") == p[1]))
                                    .Single();
                        DisplayEnroll.Rows.Remove(r);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }


        internal static int UpdateGrade(string[] a, Nullable<int> grade)
        {
            try
            {
                var line = ds.Tables["Enrollments"].AsEnumerable()
                                .Where(s =>
                                (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                .Single();

                if (line.Field<Nullable<int>>("FinalGrade") != null && grade != null)
                {
                    finalWebProj.Form1.DALMessage("Final grade has already been assigned, you can only modify the grade or delete it.");
                    return -1;
                }

                line.SetField("FinalGrade", grade);

                adapter.Update(ds.Tables["Enrollments"]);

                if (DisplayEnroll != null)
                {
                    var r = DisplayEnroll.AsEnumerable()
                                .Where(s =>
                                (s.Field<string>("StId") == a[0] && s.Field<string>("CId") == a[1]))
                                .Single();
                    r.SetField("FinalGrade", grade);
                }
                return 0;
            }
            catch (Exception)
            {
                Form1.DALMessage("Update / Deletion rejected");
                return -1;
            }
        }
    }
      
}




