// Program.cs
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration; // Required for ConfigurationBuilder
using PassTxt.ConsoleEF;
using PassTxt.ConsoleEF.Models;

namespace PassTxt.ConsoleEF
{
    class Program
    {
        static void Main()
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the connection string
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Pass the connection string to AppDbContext
            using (var context = new AppDbContext(connectionString))
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
                var deleteUser = context.Users.FirstOrDefault(u => u.Username == "jdoe");
                if (deleteUser != null)
                {
                    context.Users.Remove(deleteUser);
                    context.SaveChanges();
                    Console.WriteLine($"User {deleteUser.Username} deleted.");
                }
            }
        }
    }
}
