using CarDB.Data;
using E1_Blokstart_Quiz.Model;
using Microsoft.EntityFrameworkCore;
using WheelyGoodCars;

DataContext dbContext = new DataContext();

void MainMenu()
{
    Console.Clear();
    Console.WriteLine("Welcome to the Quiz!\n");

    Console.WriteLine("1. Kies een quiz");
    Console.WriteLine("2. Quiz uploaden");
    // indien teacher, dan ook resultaten kunnen bekijken als 3e optie
    Console.WriteLine("X. Exit");

    string input = Helpers.AskNotEmpty("\nKies een optie:\n");
    switch (input.ToLower())
    {
        case "1":
            ShowQuizMenu();
            break;
        case "2":
            UploadQuiz();
            break;
        case "x":
            Environment.Exit(0);
            break;
    }
}

void ShowQuizMenu()
{
    Console.Clear();

    // toon lijst met quizzen en laat de gebruiker een quiz kiezen
    foreach (var quiz in dbContext.Quizzes)
    {
        Console.WriteLine($"{quiz.Id}) {quiz.Name}");
    }

    // Vraag om een quiz id
    int input = Helpers.AskInt("\nKies een quiz (id):\n");

    // Zoek de geselecteerde quiz
    Quiz? selectedQuiz = dbContext.Quizzes.Where(q => q.Id == input).FirstOrDefault();
    if (selectedQuiz == null)
    {
        Console.WriteLine("Quiz niet gevonden!");
        Console.ReadKey();
        return;
    }

    // Start de geselecteerde quiz
    StartQuiz(selectedQuiz);
}

void StartQuiz(Quiz selectedQuiz)
{
    IEnumerable<QuizQuestion> questions = dbContext.Questions.Where(q => q.QuizId == selectedQuiz.Id);
    int questionIndex = 1;
    int questionCount = questions.Count();
    int score = 0;

    // Toon de vragen van de geselecteerde quiz
    foreach (var question in questions)
    {
        Console.Clear();
        Console.WriteLine($"Quiz: {selectedQuiz.Name}");
        Console.WriteLine($"Vraag: {questionIndex}/{questionCount}\n");

        // Toon de vraag en antwoorden
        Console.WriteLine(question.Question);
        Console.WriteLine($"A) {question.AnswerA}");
        Console.WriteLine($"B) {question.AnswerB}");
        Console.WriteLine($"C) {question.AnswerC}");

        // Vraag om een antwoord
        string answer = Helpers.AskNotEmpty("\nKies een antwoord (A, B of C):\n").ToUpper();
        if (answer == question.CorrectAnswer)
        {
            Console.WriteLine("Correct!");
            score++;
        }
        else
        {
            Console.WriteLine($"Incorrect! Het correcte antwoord was: {question.CorrectAnswer}");
        }

        // wacht even zodat de gebruiker de feedback kan lezen
        Console.ReadKey();

        questionIndex++;
    }

    // Toon de score
    decimal scorePercentage = Math.Round((decimal)score / questionCount * 100, 1);

    Console.Clear();
    Console.WriteLine($"Quiz: {selectedQuiz.Name}\n");
    Console.WriteLine($"Je score is: {score}/{questionCount} ({scorePercentage}%)!");
    Console.ReadKey();
}

void UploadQuiz()
{
    Console.Clear();
    
    // Vraag om het pad naar het CSV bestand (quiz uploaden)
    string path = Helpers.AskNotEmpty("Geef het pad naar het CSV bestand:");
    if (!File.Exists(path))
    {
        Console.WriteLine("Bestand niet gevonden!");
        Console.ReadKey();
        return;
    }

    // Vraag om de naam van de quiz
    string quizName = Helpers.AskNotEmpty("Geef de naam van de quiz:");

    // Create a new quiz
    Quiz newQuiz = new()
    {
        Name = quizName,
    };

    // Add the quiz to the database
    dbContext.Quizzes.Add(newQuiz);
    dbContext.SaveChanges();

    // Read the CSV file
    using (var reader = new StreamReader(path))
    {
        reader.ReadLine(); // skip first line (headers)

        while (!reader.EndOfStream)
        {
            // Read a line from the CSV file
            var line = reader.ReadLine();

            // Split the line by colons (you can adjust this for other delimiters)
            var values = line.Split(';');

            // Add the split values to the list
            QuizQuestion parsedQuestion = new()
            {
                QuizId = newQuiz.Id, // foreign key
                Question = values[1],
                AnswerA = values[2],
                AnswerB = values[3],
                AnswerC = values[4],
                CorrectAnswer = values[5].ToUpper()
            };

            dbContext.Questions.Add(parsedQuestion);
        }
    }

    dbContext.SaveChanges();

    Console.WriteLine("Quiz uploaded!");
    Console.ReadKey();
}

// Start the application
MainMenu();