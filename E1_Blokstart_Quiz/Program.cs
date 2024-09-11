using CarDB.Data;
using E1_Blokstart_Quiz.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WheelyGoodCars;

DataContext dbContext = new DataContext();

void MainMenu()
{
    string option = "";
    do
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Quiz!\n");

        Console.WriteLine("1. Kies een quiz");
        Console.WriteLine("2. Quiz uploaden");
        Console.WriteLine("3. Antwoorden controleren");
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
            case "3":
                ViewSubmissions();
                break;
            case "x":
                option = "x";
                break;
        }
    }
    while (option != "x");

    Console.WriteLine("Goodbye!");
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

        string answer;
        bool isCorrect = false;

        if (question.Type == 0) // gesloten vraag
        {
            Console.WriteLine($"{question.Question}\n");
            Console.WriteLine($"A) {question.AnswerA}");
            Console.WriteLine($"B) {question.AnswerB}");
            Console.WriteLine($"C) {question.AnswerC}");
            
            answer = Helpers.AskNotEmpty("\nGeef je antwoord (A, B of C):\n").ToUpper();

            if (question.CorrectAnswer.Contains(answer))
            {
                Console.WriteLine("Correct!");
                score++;
                isCorrect = true;
            }
            else
            {
                Console.WriteLine($"Incorrect! Het correcte antwoord is: {question.CorrectAnswer}");
            }
        }
        else if (question.Type == 1) // open vraag
        {
            var keywords = question.CorrectAnswer.Split(',');

            Console.WriteLine($"{question.Question}\n");

            answer = Helpers.AskNotEmpty("\nGeef je antwoord:\n").ToUpper();

            bool correct = true;
            foreach (var keyword in keywords)
            {
                if (!answer.Contains(keyword))
                {
                    correct = false;
                    break;
                }
            }
            if (correct)
            {
                Console.WriteLine("Correct!");
                score++;
            }
            else
            {
                Console.WriteLine($"Incorrect! Jouw antwoord bevat niet alle keywords: {question.CorrectAnswer}");
            }

            isCorrect = correct;
        }
        else // meerkeuze vraag
        {
            Console.WriteLine($"{question.Question}\n");
            Console.WriteLine($"A) {question.AnswerA}");
            Console.WriteLine($"B) {question.AnswerB}");
            Console.WriteLine($"C) {question.AnswerC}");
            
            answer = Helpers.AskNotEmpty("\nGeef je antwoord (A, B, C):\n").ToUpper();

            if (answer == question.CorrectAnswer)
            {
                Console.WriteLine("Correct!");
                score++;
                isCorrect = true;
            }
            else
            {
                Console.WriteLine($"Incorrect! Het correcte antwoord is: {question.CorrectAnswer}");
            }
        }

        // submit to database
        using (DataContext newContext = new())
        {
            UserSubmission submission = new()
            {
                UserId = 1, // hardcoded student id
                QuizId = selectedQuiz.Id,
                QuestionId = question.Id,
                Answer = answer,
                IsCorrect = isCorrect,
            };
            newContext.Submissions.Add(submission);
            newContext.SaveChanges();
        }

        Console.ReadKey();
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
    string path = Helpers.AskNotEmpty("Geef het pad naar het CSV bestand:\n");
    if (!File.Exists(path))
    {
        Console.WriteLine("Bestand niet gevonden!");
        Console.ReadKey();
        return;
    }
    else if (!path.ToLower().EndsWith(".csv"))
    {
        // Check if the file is a CSV file
        Console.WriteLine("Bestand is geen CSV!");
        Console.ReadKey();
        return;
    }

    // Vraag om de naam van de quiz
    string quizName = Helpers.AskNotEmpty("\nGeef de naam van de quiz:\n");

    // Create a new quiz
    Quiz newQuiz = new()
    {
        Name = quizName,
    };

    // Add the quiz to the database
    dbContext.Quizzes.Add(newQuiz);

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

            // Get all correct answers (1 = closed, 2+ = open)
            var correctAnswers = values[5].Split(',');

            QuizQuestion daQuestion = new()
            {
                QuizId = newQuiz.Id, // foreign key
                Question = values[1],
                CorrectAnswer = values[5].ToUpper(),
            };

            if (correctAnswers.Length > 1)
            {
                // More than one correct answer, so it's an open question or multiple choice
                if (!string.IsNullOrEmpty(values[2]) && !string.IsNullOrEmpty(values[3]) && !string.IsNullOrEmpty(values[4]))
                {
                    // multiple choice question (A, B, C)
                    daQuestion.AnswerA = values[2];
                    daQuestion.AnswerB = values[3];
                    daQuestion.AnswerC = values[4];
                    daQuestion.Type = 2;
                }
                else
                {
                    // open question (with keywords)
                    daQuestion.Type = 1;
                }
            }
            else
            {
                // closed question
                daQuestion.AnswerA = values[2];
                daQuestion.AnswerB = values[3];
                daQuestion.AnswerC = values[4];
                daQuestion.Type = 0;
            }

            // Add the question to the database
            dbContext.Questions.Add(daQuestion);
            dbContext.SaveChanges();
        }
    }

    dbContext.SaveChanges();

    Console.WriteLine("\nQuiz uploaded!");
    Console.ReadKey();
}

void ViewSubmissions()
{
    Console.Clear();

    // selecteer een student
    foreach (var user in dbContext.Users)
    {
        Console.WriteLine($"{user.Id}) {user.Name}");
    }

    int studentId = Helpers.AskInt("\nGeef het student id:\n");

    User? selectedUser = dbContext.Users.Where(u => u.Id == studentId).FirstOrDefault();
    if (selectedUser == null)
    {
        Console.WriteLine("Student niet gevonden!");
        Console.ReadKey();
        return;
    }

    Console.Clear();
    Console.WriteLine($"Student: {selectedUser.Name}\n");

    // selecteer quiz
    foreach (var quiz in dbContext.Quizzes)
    {
        Console.WriteLine($"{quiz.Id}) {quiz.Name}");
    }

    int quizId = Helpers.AskInt("\nGeef het quiz id:\n");

    Quiz? selectedQuiz = dbContext.Quizzes.Where(q => q.Id == quizId).FirstOrDefault();
    if (selectedQuiz == null)
    {
        Console.WriteLine("Quiz niet gevonden!");
        Console.ReadKey();
        return;
    }

    // toon alle quizvragen ingevuld door de student
    Console.Clear();
    Console.WriteLine($"Student: {selectedUser.Name}");
    Console.WriteLine($"Quiz: {selectedQuiz.Name}");

    int totalQuestions = dbContext.Questions.Where(q => q.QuizId == selectedQuiz.Id).Count();
    int score = dbContext.Submissions.Where(s => s.UserId == selectedUser.Id && s.QuizId == selectedQuiz.Id && s.IsCorrect).Count() - 1;
    decimal percentage = Math.Round((decimal)score / totalQuestions * 100, 1);
    Console.WriteLine($"Score: {score}/{totalQuestions} ({percentage}%)\n");

    IEnumerable<QuizQuestion> questions = dbContext.Questions.Where(q => q.QuizId == selectedQuiz.Id);

    foreach (var question in questions)
    {
        UserSubmission? submission = new DataContext().Submissions.Where(s => s.UserId == selectedUser.Id && s.QuizId == selectedQuiz.Id && s.QuestionId == question.Id).FirstOrDefault();
        if (submission != null)
        {
            Console.WriteLine($"Vraag: {question.Question}");
            Console.WriteLine($"Antwoord: {submission.Answer}");
            Console.WriteLine($"Correct: {submission.IsCorrect}");

            if (!submission.IsCorrect)
            {
                Console.WriteLine($"Correct antwoord: {question.CorrectAnswer}");
            }

            Console.WriteLine();
        }
    }

    Console.ReadKey();
}

// manual seed
if (!dbContext.Users.Any())
{
    dbContext.Users.Add(new User { Name = "John Doe", Password = "test123", IsTeacher = false });
    dbContext.Users.Add(new User { Name = "Jane Doe", Password = "test123", IsTeacher = true });
    dbContext.SaveChanges();
}

// Start the application
MainMenu();