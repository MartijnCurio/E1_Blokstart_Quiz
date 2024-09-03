using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E1_Blokstart_Quiz.Model
{
    internal class Submission
    {
        public int Id { get; set; }
        public string QuizId { get; set; }
        public string QuestionId { get; set; }
        public string UserId { get; set; }
        public string Answer { get; set; }
    }
}
