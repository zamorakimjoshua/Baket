using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using ClassroomManagementModels;

namespace ClassroomManagementData
{
    public class SqlDbData
    {
        static string connectionString = "Data Source=LAPTOP-QQJD4TKI\\SQLEXPRESS;Initial Catalog=Class;Integrated Security=True;";


        private static string smtpHost = "smtp.mailtrap.io";
        private static int smtpPort = 587;
        private static string smtpUser = "a0408f045ba439";
        private static string smtpPass = "1af02d03af6fcf";
        private static string fromEmail = "zamorakimjoshua@gmail.com";

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string selectStatement = "SELECT prof, roomNum, status FROM integs";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User readUser = new User
                            {
                                prof = reader["prof"].ToString(),
                                roomNum = reader["roomNum"].ToString(),
                                status = reader["status"].ToString()
                            };
                            users.Add(readUser);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");

            }
            return users;
        }

        public int AddUser(string prof, string roomNum, string status)
        {
            string insertStatement = "INSERT INTO integs (prof, roomNum, status) VALUES (@prof, @roomNum, @status)";
            int result = ExecuteNonQuery(insertStatement, prof, roomNum, status);

            if (result > 0)
            {
                SendEmailNotification($"User Added: {prof}", $"Professor {prof} has been added to room {roomNum} with status {status}");
            }

            return result;
        }

        public int UpdateUser(string prof, string roomNum)
        {
            string updateStatement = "UPDATE integs SET roomNum = @roomNum WHERE prof = @prof";
            int result = ExecuteNonQuery(updateStatement, prof, roomNum);

            if (result > 0)
            {
                SendEmailNotification($"User Updated: {prof}", $"Professor {prof} has been updated to room {roomNum}");
            }

            return result;
        }

        public int DeleteUser(string prof)
        {
            string deleteStatement = "DELETE FROM integs WHERE prof = @prof";
            int result = ExecuteNonQuery(deleteStatement, prof);

            if (result > 0)
            {
                SendEmailNotification($"User Deleted: {prof}", $"Professor {prof} has been removed from the database.");
            }

            return result;
        }

        private int ExecuteNonQuery(string query, string prof, string roomNum = null, string status = null)
        {
            int result = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@prof", prof);

                    if (!string.IsNullOrEmpty(roomNum))
                    {
                        command.Parameters.AddWithValue("@roomNum", roomNum);
                    }

                    if (!string.IsNullOrEmpty(status))
                    {
                        command.Parameters.AddWithValue("@status", status);
                    }

                    result = command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");

            }
            return result;
        }


        private void SendEmailNotification(string subject, string body)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(fromEmail);
                        mail.To.Add("zamorakimjoshua@gmail.com");
                        mail.Subject = subject;
                        mail.Body = body;

                        smtpClient.Send(mail);
                        Console.WriteLine(" New Prof.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        internal int UpdateUser(string prof, string roomNum, string status)
        {
            throw new NotImplementedException();
        }
    }
}
