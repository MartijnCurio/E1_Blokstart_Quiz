using E1_Blokstart_Quiz.Model;

List<object> questions = new()
{
    new QuizQuestion()
    {
        Question = "What is the capital of the Netherlands?",
        Answers = new Dictionary<string, bool>()
        {
            { "Amsterdam", true },
            { "Rotterdam", false },
            { "The Hague", false },
            { "Utrecht", false }
        }
    }
};

