using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E1_Blokstart_Quiz.Model
{
    internal class UserSubmission
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
