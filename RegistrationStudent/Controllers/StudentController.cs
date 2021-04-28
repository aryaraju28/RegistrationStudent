using RegistrationStudent.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RegistrationStudent.Controllers
{
    public class StudentController : ApiController
    {
        String connectionString = "Server=RAED_COMPUTER\\SQLEXPRESS;Database=SchoolManagement;Trusted_Connection=True;";
        [HttpGet]
        public string StudentInsert(string name, int malayalam, int hindi, int english)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            DateTime currentTime = DateTime.Now;
            currentTime.ToString("hh:mm:tt");
            SqlCommand command = new SqlCommand("StudentInsert", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("Malayalam", malayalam);
            command.Parameters.AddWithValue("Hindi", hindi);
            command.Parameters.AddWithValue("English", english);
            command.Parameters.AddWithValue("Time", currentTime);
            command.ExecuteNonQuery();
            connection.Close();
            Student student = new Student();
            student.Name = name;
            student.Malayalam = malayalam;
            student.Hindi = hindi;
            student.English = english;
            student.CurrentTime = currentTime.ToString();

            return student.ToString();


        }
        [HttpGet]
        public string Update(int id,string name)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("StudentUpdate", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", id);
            command.Parameters.AddWithValue("Name", name);
            command.ExecuteNonQuery();
            Student student = new Student();
            student.Id = id;
            student.Name = name;
            return student.ToString();



        }
        [HttpGet]
        public IHttpActionResult StudentDetail(int id)
        {
            List<Student> stud = new List<Student>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("StudentDetails", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", id);

            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                var student = new Student();
                student.Id = id;
                //student.Id = Convert.ToInt32(reader["Id"]);
                student.Name = reader["Name"].ToString();
                student.Malayalam = Convert.ToInt32(reader["Malayalam"]);
                student.Hindi = Convert.ToInt32(reader["Hindi"]);
                student.English = Convert.ToInt32(reader["English"]);
               
                stud.Add(student);
                //stud.Add(new Student()
                //{
                //    Id = Convert.ToInt32(reader.GetValue(0)),
                //    Name = reader.GetValue(1).ToString(),
                //    Malayalam = Convert.ToInt32(reader.GetValue(2)),
                //    Hindi = Convert.ToInt32(reader.GetValue(3)),
                //    English = Convert.ToInt32(reader.GetValue(4))
                //});

            }

            return Ok(stud);

           
        }
        public IHttpActionResult StudentList()
    } }
