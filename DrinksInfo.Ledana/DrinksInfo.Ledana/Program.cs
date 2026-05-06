using DrinksInfo.Ledana;

class Program
{
    static async Task Main()
    {
        UserInput userInput = new();
        while(true)
        {
            await userInput.GetCategoriesInput();
            Console.WriteLine("Press 'x' to exit or any other key to look into another drink");
            var input = Console.ReadLine();
            if (input is not null && input.ToLower() == "x")
            {
                Console.WriteLine("Good bye!");
                break;
            }
        }
        
    }
}

