using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E1_Blokstart_Quiz.Model
{
    internal class QuizQuestion
    {
        public string Question { get; set; }
        public Dictionary<string, bool> Answers { get; set; }
    }
}
