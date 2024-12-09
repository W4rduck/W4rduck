/*
	This is a personal project. Only develop by my-self.
	It is a Math Game in witch you'll be able to interact with a menu and then try to answer a few math question (like 21 + 14)
	For each good answer you'll can pass to the next question and so on.
	For each bad answer you'll lose a life. After many bad answer (depend on difficulty level) you lose the game and go back to the main menu
	The difficulty level will change magnitude of numbers and/or numbers of life
*/

/*
	TODO: 
		- Finir la boucle principale avec vérification que les entrée sont valides
		- Implemnter les options (nombre partie + niveau de difficulté)
		- Implementer un compteur de vie
		- Implementer un score
		- Implementer un mode "Time attack"
*/
using System;
	
public class Program
{
	public static void Main()
	{
		//Start of the game
		Options options = new Options();
		//Main loop
		while(true)
		{
			Console.WriteLine("Hello, what game would you like to play ? Choose from the options below:");
			Console.WriteLine("A - Addition");
			Console.WriteLine("S - Subtraction");
			Console.WriteLine("M - Multiplication");
			Console.WriteLine("D - Division");
			Console.WriteLine("O - Options");
			Console.WriteLine("Q - Quit the game");
			options.DisplayOptions();
			Console.WriteLine("----------------------------------");

			string answer = Console.ReadLine().ToUpper();
			string operation = "";
			
			switch(answer)
			{
				case "A": operation = "+";break;
				case "S": operation = "-";break;
				case "M": operation = "*";break;
				case "D": operation = "/";break;
				case "O": options = OptionsStatement();break;
				case "Q": return;
				default: 
					Console.WriteLine("Please enter a valid choise, press a key to continue");
					Console.ReadLine();
					break;
			}
			
			if(operation != "")
			{
				//Loop's Length define by NumberOfGames in options
				for(int i = 0; i < options.NumberOfGames; i++)
				{
					if(AnswerQuestionProcess(operation, options))
					{
						Console.WriteLine("Correct, press a key to continue");
						Console.ReadLine();
					}
					else
					{
						Console.WriteLine("False, press a key to continue");
						Console.ReadLine();
					}
				}
			}
		}
	}
	
	public delegate int Calculation(int a, int b);	
	
	public static bool AnswerQuestionProcess(string operation, Options options)
	{
		int numAnswer;
		Calculation calc = null;
		int min = 0;
		int max = 0;
		
		if(options.DifficultyLevel == Options.Difficulty.Easy)
		{
			min = 1;
			max = 11;
		}
		else if(options.DifficultyLevel == Options.Difficulty.Normal)
		{
			min = 10;
			max = 31;
		}
		
		else
		{
			min = 30;
			max = 100;
		}
		
		
		//Generate a new question based on operation signe
		Random rand = new Random();
		
		int a = rand.Next(min, max);
		int b = rand.Next(min, max);
		
		//Be sure result of division is a whole number like 8 / 4 = 2
		if(operation == "/")
		{
			while(a % b != 0)
			{
				a = rand.Next(101);
				b = rand.Next(101);
			}
		}
		
		string question = $"{a} {operation} {b}";
		Console.WriteLine(question);
		
		while(true)
		{
			//Ask answer and parse it into number
			string answer = Console.ReadLine();

			if(int.TryParse(answer, out numAnswer))
			{
				//Assign method in delegate Calculation then return result 
				if(question.Contains("+")) calc = Addition;
				if(question.Contains("-")) calc = Subtraction;
				if(question.Contains("*")) calc = Multiplication;
				if(question.Contains("/")) calc = Division;

				return calc(a,b) == numAnswer;
			}
			else
			{
				Console.WriteLine("Please type a valid number, press a key to continue");
				Console.ReadLine();
			}
		}
	}
	
	public static Options OptionsStatement()
	{
		while(true)
		{
			Console.Clear();
			Console.Write("Number of game: ");
			string answer = Console.ReadLine();
			int numberOfGame;


			if(int.TryParse(answer, out numberOfGame))
			{
				while(true)
				{
					Console.Write("Dificulty Level (E,N,H): ");
					answer = Console.ReadLine().ToUpper();

					switch(answer)
					{
						case "E": return new Options(numberOfGame, Options.Difficulty.Easy);	
						case "N": return new Options(numberOfGame, Options.Difficulty.Normal);
						case "H": return new Options(numberOfGame, Options.Difficulty.Hard);
						default: 
							Console.WriteLine("Please enter a valid choise, press a key to continue");
							Console.ReadLine();
							break;
					}
				}
			}
			else
			{
				Console.WriteLine("Please type a valid number, press a key to continue");
				Console.ReadLine();
			}
		}
	}
	
	// Delegate Methode for Calculation
	public static int Addition(int a, int b) => a + b;	
	public static int Subtraction(int a, int b) => a - b;	
	public static int Multiplication(int a, int b) => a * b;
	public static int Division(int a, int b) => a / b;

}

public class Options
{
	public enum Difficulty
	{
		Easy, Normal, Hard
	};
	
	private int _numberOfGames;
	private Difficulty _difficultyLevel;
	
	public Options(int numberOfGames = 5, Difficulty difficultyLevel = Difficulty.Normal)
	{
		_numberOfGames = numberOfGames;
		_difficultyLevel = difficultyLevel;
	}
	
	public int NumberOfGames 
	{
		get => _numberOfGames;
	}
	
	public Difficulty DifficultyLevel
	{
		get => _difficultyLevel;
	}
	
	public void DisplayOptions()
	{
		Console.WriteLine($"number of games: {_numberOfGames}\nDificulty level: {_difficultyLevel}");
	}
}

