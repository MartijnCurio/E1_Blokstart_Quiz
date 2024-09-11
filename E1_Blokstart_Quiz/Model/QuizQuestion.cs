using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E1_Blokstart_Quiz.Model
{
    internal class QuizQuestion
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Question { get; set; }
        public int Type { get; set; } // 0 = closed, 1 = open, 2 = multiple choice
        public string? AnswerA { get; set; }
        public string? AnswerB { get; set; }
        public string? AnswerC { get; set; }
        public string? CorrectAnswer { get; set; } // A, B, C or keywords
    }
}
