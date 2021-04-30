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
        String connectionString = "Server=RAED_COMPUTER\\SQLEXPRESS;Database=SchoolManagementSystem;Trusted_Connection=True;";
        [HttpGet]
        public string StudentSave(string name, int malayalam, int hindi, int english)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            DateTime currentTime = DateTime.Now;
            currentTime.ToString("hh:mm:tt");
            SqlCommand command = new SqlCommand("StudentSave", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("Malayalam", malayalam);
            command.Parameters.AddWithValue("Hindi", hindi);
            command.Parameters.AddWithValue("English", english);
            command.Parameters.Add("@LastStudentId", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
            command.Parameters.Add("@CreateTime", System.Data.SqlDbType.VarChar,50).Direction = System.Data.ParameterDirection.Output;

            //command.Parameters.AddWithValue("Time", currentTime);
            command.ExecuteNonQuery();
            //connection.Close();
            //Student student = new Student();
            //student.Name = name;
            //student.Malayalam = malayalam;
            //student.Hindi = hindi;
            //student.English = english;
            //student.CurrentTime = currentTime.ToString();


            string id = command.Parameters["@LastStudentId"].Value.ToString();
            string time = command.Parameters["@CreateTime"].Value.ToString();
            return "Inserted Sucessfully Id=" + id + " " + " CreatedTime=" + time;


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
        public Student StudentDetail(int id)
        {
            
            //List<Student> stud = new List<Student>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("StudentDetails", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", id);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            
            Student student = new Student();
            student.Id = Convert.ToInt32(id);
                 //student.Id = Convert.ToInt32(reader["Id"]);
            student.Name = reader["Name"].ToString();
            student.Malayalam = Convert.ToInt32(reader["Malayalam"]);
            student.Hindi = Convert.ToInt32(reader["Hindi"]);
            student.English = Convert.ToInt32(reader["English"]);
            student.Total = Convert.ToInt32(reader["Total"]);
            student.Average = Convert.ToInt32(reader["Average"]);
            
          
            reader.Close();
            connection.Close();
            return student;
            

            

           

           
        }
        [HttpGet]
        public Student StudentList(int order)
        {
            
            
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("StudentList", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                 command.Parameters.AddWithValue("Order", order);


            SqlDataReader reader = command.ExecuteReader();

            
            
                Student student = new Student();


                student.Name = reader["Name"].ToString();
                student.Malayalam = Convert.ToInt32(reader["Malayalam"]);
                student.Hindi = Convert.ToInt32(reader["Hindi"]);
                student.English = Convert.ToInt32(reader["English"]);
              
            
                reader.Close();
                connection.Close();
            return student;

            
            

        }
        [HttpGet]
        public Student StudentUpdate()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("StudentLastUpdate", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            Student student = new Student();
            student.Id = Convert.ToInt32(reader["Id"]);
            
            student.Name = reader["Name"].ToString();
            
            reader.Close();
            connection.Close();
            return student;
        }
        //[HttpGet]
        //public Student StudentSave()
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    connection.Open();
        //    SqlCommand command = new SqlCommand("StudentSave", connection);
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    SqlDataReader reader = command.ExecuteReader();
        //    reader.Read();

        //    Student student = new Student();
        //    student.Id = Convert.ToInt32(reader["Id"]);

        //    student.CurrentTime = reader["Time"].ToString();

        //    reader.Close();
        //    connection.Close();
        //    return student;

        //}

    } }
