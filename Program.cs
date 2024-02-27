// MathsQuiz, Author: Yusef_Noor, Date: 13/02/2024

public class Program
{
    public static void Main(string[] args)
    {
        // Prompt to enter Name
        Console.WriteLine("Hi, Please enter your name to begin the test.");
        string StudentName = Console.ReadLine();
        string result = "";
        Console.Clear();


        // create question lists
        int CorrectCount = 0;

        List<SimpleQuestion> SimpleQuestions = new List<SimpleQuestion>();
        List<HarderQuestion> HarderQuestions = new List<HarderQuestion>();

        // add 10 of each question type to the respective lists - can easily change size of test by changing this
        for (int i = 0; i < 10; i++) 
        {
            SimpleQuestion question = new SimpleQuestion(); 
            HarderQuestion hQuestion = new HarderQuestion();
            SimpleQuestions.Add(question); 
            HarderQuestions.Add(hQuestion);
        }

        // Ask questions in the list
        foreach (SimpleQuestion question in SimpleQuestions)
        {
            question.AskQuestion();

            if (question.IsCorrect == true)
            {
                CorrectCount ++;
            }
        }
        foreach (HarderQuestion question in HarderQuestions)
        {
            question.AskQuestion();
            if (question.IsCorrect == true)
            {
                CorrectCount++;
            }
        }

        // grading based on number of correct answers
        if (CorrectCount >= 0 && CorrectCount <= 4)
        {
            Console.WriteLine($"Well done for completing the Test {StudentName}, unfortunately you failed :( ");
            result = "Fail";
        }

        if (CorrectCount >= 5 && CorrectCount <= 10)
        {
            Console.WriteLine($"Well done for completing the Test {StudentName}, you achieved: Pass ");
            result = "Pass";
        }

        if (CorrectCount >= 11 && CorrectCount <= 16)
        {
            Console.WriteLine($"Well done for completing the Test {StudentName}, you achieved: Merit");
            result = "Merit";
        }

        if (CorrectCount >= 17 && CorrectCount <= 20)
        {
            Console.WriteLine($"Well done for completing the Test {StudentName}, you achieved: Distinction");
            result = "Distinction";
        }

        // GENERATING TEACHER REPORT - Report will appear in same directory as executable compiled from this script)
        int AdditionCount = 0;
        int SubtractionCount = 0;
        int MultiplicationCount = 0;
        int DivisionCount = 0;

        foreach (SimpleQuestion question in SimpleQuestions)
        {

            if (question.IsCorrect == true && question.Operator == "+")
            {
                AdditionCount++;
            }

            if (question.IsCorrect == true && question.Operator == "-")
            {
                SubtractionCount++;
            }

            if (question.IsCorrect == true && question.Operator == "x")
            {
                MultiplicationCount++;
            }

            if (question.IsCorrect == true && question.Operator == "/")
            {
                DivisionCount++;
            }
        }


        string fileName = $"{StudentName}TestReport.txt";
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine($"Student Name: {StudentName}");
            writer.WriteLine($"Result: {result}");
            writer.WriteLine($"Correct Answers: {CorrectCount}");
            writer.WriteLine($"Breakdown: Addition questions correct:{AdditionCount}");
            writer.WriteLine($"Breakdown: Subtraction questions correct:{SubtractionCount}");
            writer.WriteLine($"Breakdown: Multiplication questions correct:{MultiplicationCount}");
            writer.WriteLine($"Breakdown: Division questions correct:{DivisionCount}");
        }

        Console.WriteLine($"Report generated: {fileName}");


    }


    // Question classes
    public class SimpleQuestion
    {
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public string Operator { get; set; }
        public string Question { get; set; }
        public object? Choice { get; set; }
        public double Answer { get; set; }
        public bool IsCorrect { get; set; }

        public SimpleQuestion()
        {
            Random random = new Random();
            this.Num1 = random.Next(1, 13); // Random number between 1 and 12
            this.Num2 = random.Next(1, 13);

            string[] operators = { "+", "-", "x", "/" };
            this.Operator = operators[random.Next(0, operators.Length)]; // assigning random operator

            this.Question = $"What is {Num1} {Operator} {Num2}? Provide your answer to 3 decimal places";

            if (this.Operator == "+")
            {
                this.Answer = Num1 + Num2;
            }

            if (this.Operator == "-")
            {
                this.Answer = Num1 - Num2;
            }

            if (this.Operator == "x")
            {
                this.Answer = Num1 * Num2;
            }

            if (this.Operator == "/")
            {
                this.Answer = Num1 / Num2;
            }

        }

        // Method prints question to console prompting user, and assigns their answer (rounded to 3.d.p) to the choice property
        public virtual void AskQuestion()
        {
            Console.WriteLine(this.Question);
            string input = Console.ReadLine();
            this.Choice = input;
            // Trying to parse the input into a double
            if (double.TryParse(input, out double parsedInput))
            {
                // Rounding the parsed input and the correct answer to one decimal place
                double roundedInput = Math.Round(parsedInput, 3);
                double roundedAnswer = Math.Round(this.Answer, 3);

                // Comparing the rounded parsed input with the rounded correct answer
                if (roundedInput == roundedAnswer)
                {

                    IsCorrect = true;
                }
                else
                {

                    IsCorrect = false;
                }

                Console.Clear();
            }
            else
            {
                Console.WriteLine(
                    "Invalid input. Please enter just a number next time eg. '42' rather than 'answer is 42'.");
                IsCorrect = false;
            }
        }

    }

    public class HarderQuestion : SimpleQuestion
    {
        public int Num3 { get; set; }
        public string Operator2 { get; set; }

        // Custom Constructor
        public HarderQuestion() : base()
        {
            Random random = new Random();
            this.Num3 = random.Next(1, 51); // Random number between 1 and 50

            string[] operators = { "+", "-", "x", "/" };
            this.Operator = operators[random.Next(0, operators.Length)]; // assigning random operator
            this.Operator2 = operators[random.Next(0, operators.Length)]; // assigning random second operator

            this.Question =
                $"What is ({Num1} {Operator} {Num2}) {Operator2} {this.Num3} ? Provide your answer to 1 decimal place";

            if (this.Operator == "+")
            {
                this.Answer = Num1 + Num2;
            }

            if (this.Operator == "-")
            {
                this.Answer = Num1 - Num2;
            }

            if (this.Operator == "x")
            {
                this.Answer = Num1 * Num2;
            }

            if (this.Operator == "/")
            {
                this.Answer = Num1 / Num2;
            }

            if (this.Operator2 == "+")
            {
                this.Answer = this.Answer + this.Num3;
            }

            if (this.Operator2 == "-")
            {
                this.Answer = this.Answer - this.Num3;
                ;
            }

            if (this.Operator2 == "x")
            {
                this.Answer = this.Answer * this.Num3;
                ;
            }

            if (this.Operator2 == "/")
            {
                this.Answer = this.Answer / this.Num3;
                ;
            }
        }

        // Overriding AskQuestion implementation
        public override void AskQuestion()
        {
            // Polymorphism - this implementation is slightly different: extending base class to reward for harder question
            base.AskQuestion();

            if (IsCorrect)
            {
                Console.WriteLine("Well done! That last question was tricky! \n");
            }

        }
    }

}