using E1_Blokstart_Quiz.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDB.Data
{
    internal class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<QuizQuestion> Questions { get; set; }
        public DbSet<UserSubmission> Submissions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;" +
                    "port=3306;" +
                    "user=root;" +
                    "password=;" +
                    "database=Blok_E_Blokstart_1;"
                    , Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.17-mariadb")
                );
            }
        }
    }
}
