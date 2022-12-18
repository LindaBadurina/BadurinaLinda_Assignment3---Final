using System;
using Assignment3.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;


namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Commenting out this line because it probably doesn't work properly on a PC. public class TeacherDataController : ApiControllerAttribute
    public class TeacherDataController: ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();
        //GET api/TeacherData/ListTeachers
        [Route("ListTeachers/{SearchKey?}")]
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers(string SearchKey)
        { 
            //This works perfectly when controlled by the TeacherController.cs
            /*The video provided by the instructor displayed a document tree when /api/TeacherData/ListTeachers was run.
             With this code, only a few empty pairs of curly braces are displayed; even in Firefox. I don't know why. At least it runs with
            /Teacher/List*/
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "Select * from Teachers where employeenumber like @key";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> Teachers = new List<Teacher>();
            while (ResultSet.Read())
            {
                String teacherFName = (string)ResultSet["teacherfname"];
                String teacherLName = (string)ResultSet["teacherlname"];
                
                int teacherId = (int)ResultSet["teacherid"];
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];
                string employeenumber = (string)ResultSet["employeenumber"];
                Teacher newTeach = new Teacher();
                newTeach.teacherfname = teacherFName;

                newTeach.teacherlname = teacherLName;
                newTeach.teacherid = teacherId;
                newTeach.employeenumber = employeenumber;
                newTeach.hiredate = hiredate;
                newTeach.salary = salary;

                Teachers.Add(newTeach);
                
            }
            Conn.Close();
            return Teachers;

        }
        [Route("FindTeacher/{employeenumber}")]
        [HttpGet]
        public Teacher FindTeacher(int teacherid)
        {
            /*This does **not** work. Why? I have no idea. There is virtually no difference between this code and the code that the instructor used in the
             provided videos. I hope that this actually works on the instructor's machine, and that the issue arises as a result of the two of us using two
            different versions of Visual Studio, or something. I hope whoever checks this can tell me where I went wrong, if I did.*/
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid LIKE " + "\'teacherid\'";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                String teacherFName = (string)ResultSet["teacherfname"];
                String teacherLName = (string)ResultSet["teacherlname"];
                int teacherId = (int)ResultSet["teacherid"];
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];
                string eNumber = (string)ResultSet["employeenumber"];
                NewTeacher.teacherfname = teacherFName;
                NewTeacher.teacherlname = teacherLName;
                NewTeacher.teacherid = teacherId;
                NewTeacher.employeenumber = eNumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;

            }


            return NewTeacher;
        }
        //<example> POST : /api/TeacherData/DeleteTeacher/T401</example>
        //Deletes a teacher from the database. Can't verify that this works because I'm using a Mac
        [HttpPost]
        public void DeleteTeacher(string id)
        {
            MySqlConnection Con = School.AccessDatabase();
            Con.Open();
            MySqlCommand cmd = Con.CreateCommand();
            cmd.CommandText = "Delete from teachers where employeenumber like @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        //Adds a new teacher to the system using a MYSQL command. However, because I'm using a Mac, I can't verify that this works properly.
        //I'm using nearly identical code to that which was provided in lecture, however. 
        [HttpPost]
        public void AddTeacher([FromBody]Teacher newTeacher) {

            MySqlConnection Con = School.AccessDatabase();
            Con.Open();
            MySqlCommand cmd = Con.CreateCommand();
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary)" +
                "values(@teacherfname,@teacherlname,@employeenumber, @hiredate, @salary)";
            cmd.Parameters.AddWithValue("@teacherfname", newTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", newTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@employeenumber", newTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", newTeacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", newTeacher.salary);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        //Updates a selected teacher with the values retrieved from the webpage textboxes when the Teacher Update form is submitted
        //Non-deterministics
        //Robust; works not only when the website calls it after having a user interact with the corresponding view, but works also when
        //A used uses a command prompt to submit an http request to it directly. 
        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            MySqlConnection Con = School.AccessDatabase();
            Con.Open();
            MySqlCommand cmd = Con.CreateCommand();
            cmd.CommandText = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname, employeenumber=@employeenumber, hiredate=@hiredate, salary=@salary where teacherid = @teacherid";
            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname",TeacherInfo.teacherlname);
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", TeacherInfo.hiredate);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.salary);
            cmd.Parameters.AddWithValue("@teacherid", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

    }
}