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

void MainMenu()
{
    Console.Clear();
    Console.WriteLine("Welcome to the Quiz!");

    Console.WriteLine("1. Start Quiz");
    Console.WriteLine("2. ");
    Console.WriteLine("3. ");
    Console.WriteLine("X. Exit");
}

void StartQuiz()
{

}

// Start the application
MainMenu();