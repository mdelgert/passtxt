// Program.cs
using System;
using System.Linq;
using PassTxt.ConsoleEF;
using PassTxt.ConsoleEF.Models;

namespace PassTxt.ConsoleEF
{
    class Program
    {
        static void Main()
        {
            using (var context = new AppDbContext())
            {
                // Create a new user
                var user = new User { Username = "jdoe", Email = "jdoe@example.com" };
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine($"User {user.Username} added with ID {user.Id}.");

                // Read user
                var readUser = context.Users.FirstOrDefault(u => u.Username == "jdoe");
                Console.WriteLine($"Read User: {readUser.Username}, {readUser.Email}");

                // Update user
                if (readUser != null)
                {
                    readUser.Email = "john.doe@example.com";
                    context.SaveChanges();
                    Console.WriteLine($"User {readUser.Username} updated to {readUser.Email}.");
                }

                // Delete user
                //var deleteUser = context.Users.FirstOrDefault(u => u.Username == "jdoe");
                //if (deleteUser != null)
                //{
                //    context.Users.Remove(deleteUser);
                //    context.SaveChanges();
                //    Console.WriteLine($"User {deleteUser.Username} deleted.");
                //}
            }
        }
    }
}