while (true)
{
    Console.WriteLine("Please enter a password.  Make sure it follows the following rules:\n");
    Console.WriteLine("Passwords must be at least 6 letters long and no more than 13 letters long.");
    Console.WriteLine("Passwords must contain at least one uppercase letter, one lowercase letter, and one number.");
    Console.WriteLine("Passwords cannot contain a capital T or an ampersand (&) because Ingelmar in IT has decreed it.\n");
    Console.Write("New Password: ");
    PasswordValidator password = new PasswordValidator();
    Console.WriteLine(password.PasswordResponseString);
    Console.ReadLine();
    Console.Clear();
}

class PasswordValidator
{
    public bool IsValid { get; set; }
    private string PasswordString { get; set; }
    public string PasswordResponseString { get; set; }


    public PasswordValidator()
    {
        PasswordString = PasswordMasking();
        PasswordResponseString = "";
        IsValid = ValidatePassword(PasswordString);
    }

    private string PasswordMasking()
    {
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && PasswordString.Length > 0)
            {
                Console.Write("\b \b");
                PasswordString = PasswordString[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                PasswordString += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        Console.WriteLine();
        return PasswordString;
    }

    private bool ValidatePassword(string password)
    {
        bool problem1 = false;
        bool problem2 = true;
        bool problem3 = true;
        bool problem4 = true;
        bool problem5 = false;

        foreach (char letter in password)
        {
            if (password.Length < 6 || password.Length > 13)
                problem1 = true;

            if (char.IsUpper(letter))
                problem2 = false;
            
            if (char.IsLower(letter))
                problem3 = false;

            if (char.IsDigit(letter))
                problem4 = false;
            
            if (letter == 'T' || letter == '&')
                problem5 = true;
        }
        if (problem1)
            PasswordResponseString += ">>Password needs to be between 6 and 13 characters long.\n";

        if (problem2)
            PasswordResponseString += ">>Password contains no capitalized letters.\n";

        if (problem3)
            PasswordResponseString += ">>Password contains no lowercase letters.\n";

        if (problem4)
            PasswordResponseString += ">>Password contains no numbers.\n";

        if (problem5)
            PasswordResponseString += ">>Password cannot contain a capital \"T\" or an \"&\"\n";

        if (problem1 || problem2 || problem3 || problem4 || problem5)
            return false;

        PasswordResponseString += ">>Your password is strong like Kong.\n";
        return true;
    }
}
