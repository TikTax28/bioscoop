using static System.Console;
using Newtonsoft.Json;

static class User
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void LogIn()
    {
        WriteLine("Login");
        WriteLine("Vul uw emailadres in: ");
        string ?email = ReadLine();
        WriteLine("Vul uw paswoord in: ");
        string ?password = ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {            
            var temp = new CreateMenus();
            temp.LoggedInMenu();
            
        }
        else
        {
            WriteLine("No account found with that email and password");
            Clear();
            var temp = new CreateMenus();
            temp.LogIn();
        }
    }

    public static void AddAccToJson(string EmailAddress, string PassWord, string FullName)// gegevens toevoegen aan json lijst.
    {
        int _numberAccounts;
        _numberAccounts = accountsLogic._accounts.Count;
        AccountModel NewUser = new AccountModel(_numberAccounts + 1, EmailAddress, PassWord, FullName);
        accountsLogic.UpdateList(NewUser);
    }

    

    public static void CreateAcc()
    {
        string ?FullName;
        string ?EmailAddress;
        string ?PassWord;
        Clear();
        while (true) // Volledige naam van gebruiker vragen.
        {
            WriteLine("Wat is uw volledige naam? (bijv. Hans Berend Gans)");
            FullName = ReadLine();
            if (FullName != "")
            {
                break;
            }
            else
            {
                WriteLine("Uw naam moet tekst bevatten, vul uw volledige naam in.");
            }
        }
        while (true) // email opvragen van gebruiker.
        {
            WriteLine("Vul hier uw email in:");
            EmailAddress = ReadLine();
            if (EmailAddress != "")
            {
                break;
            }
            else
            {
                WriteLine("Uw email moet tekst bevatten, vul uw email opnieuw in.");
            }
        }
        while (true)
        {
            WriteLine("Vul hier uw gewenste wachtwoord in:");
            PassWord = ReadLine();
            if (PassWord != "")
            {
                WriteLine("Vul hier uw gewenste wachtwoord nog een keer in:");
                string ?PassWordCopy = ReadLine();
                if (PassWord == PassWordCopy)
                {
                    WriteLine("Uw gegevens zijn opgeslagen, dankuwel voor het registreren.");
                    break;
                }
                else
                {
                    WriteLine("Wachtwoorden komen niet overeen vul uw gewenste wachtwoord opnieuw in.");
                }
            }
            else
            {
                WriteLine("Uw wachtwoord moet tekst bevatten, vul uw wachtwoord opnieuw in.");
            }
        }
        AddAccToJson(EmailAddress, PassWord, FullName);
        Clear();
        AccountModel acc = accountsLogic.CheckLogin(EmailAddress, PassWord);
        var temp = new CreateMenus();
        temp.LoggedInMenu();
    }
}