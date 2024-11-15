using System;
using ClassroomManagementModels;
using ClassroomManagementData;
using ClassroomManagementServices;
using System.Net.Mail;
using System.Net;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool active = true;
            UserGetServices userGetServices = new UserGetServices();
            UserTransactionServices userTransactionServices = new UserTransactionServices();


            string smtpHost = "smtp.mailtrap.io";
            int smtpPort = 2525;
            string smtpUser = "a0408f045ba439";
            string smtpPass = "1af02d03af6fcf";
            string fromEmail = "zamorakimjoshua@gmail.com.com";
            string recipientEmail = "VisualStudio.com";

            while (active)
            {
                Console.WriteLine("Classroom Reservation");
                Console.WriteLine("1. Reservation?");
                Console.WriteLine("2. Already Done?");
                Console.WriteLine("3. Occupied Rooms");
                Console.WriteLine("4. Exit");

                Console.WriteLine("Enter the number:");
                string number = Console.ReadLine();

                switch (number)
                {
                    case "1":
                        Console.WriteLine("Who is the Professor?");
                        string prof = Console.ReadLine();

                        Console.WriteLine("What is the room number?");
                        string roomNum = Console.ReadLine();

                        User newUser = new User { prof = prof, roomNum = roomNum, status = "settled" };
                        userTransactionServices.CreateUser(newUser);


                        SendEmailNotification(smtpHost, smtpPort, smtpUser, smtpPass, fromEmail, recipientEmail,
                            $"New Reservation: {prof}",
                            $"Professor {prof} has reserved room {roomNum}.");

                        Console.WriteLine("Keep the room clean Please!");
                        break;

                    case "2":
                        Console.WriteLine("Who is the Professor?");
                        string unregisterIgn = Console.ReadLine();

                        User userToDelete = new User { prof = unregisterIgn };
                        userTransactionServices.DeleteUser(userToDelete);


                        SendEmailNotification(smtpHost, smtpPort, smtpUser, smtpPass, fromEmail, recipientEmail,
                            $"Reservation Cancelled: {unregisterIgn}",
                            $"Professor {unregisterIgn} has canceled the reservation.");

                        Console.WriteLine("Hope you cleaned the room!");
                        break;

                    case "3":
                        Console.WriteLine("Here is the List:");
                        DisplayUsers(userGetServices.GetAllUsers());
                        break;

                    case "4":
                        active = false;
                        break;

                    default:
                        Console.WriteLine("ERROR: Invalid input, please try again.");
                        break;
                }
            }
        }


        private static void SendEmailNotification(string smtpHost, int smtpPort, string smtpUser, string smtpPass,
            string fromEmail, string recipientEmail, string subject, string body)
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
                        mail.To.Add(recipientEmail);
                        mail.Subject = subject;
                        mail.Body = body;

                        smtpClient.Send(mail);
                        Console.WriteLine("Email notification sent.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        private static void DisplayUsers(List<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Professor: {user.prof}, Room Number: {user.roomNum}, Status: {user.status}");
            }
        }
    }
}
