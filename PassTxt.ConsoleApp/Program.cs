// Program.cs
using System;
using Microsoft.EntityFrameworkCore;
using PassTxt.Shared;
using PassTxt.Shared.Models;

//Original logic injecting code fails to run ef migrations ****************
//Should put migrations in a separate project *****************************

namespace PassTxt.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            using var context = new AppDbContextFactory().CreateDbContext(null);

            // Apply any pending migrations and create the database if it doesn’t exist
            context.Database.Migrate();

            // Add a sample note
            var note = new NoteModel
            {
                Title = "Sample Note",
                Note = "This is a sample note"
            };

            context.Notes.Add(note);
            context.SaveChanges();
            Console.WriteLine("Note added to database.");
        }
    }
}
