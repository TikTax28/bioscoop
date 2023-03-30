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
        bool ?EmailInUse = false;
        Clear();
        while (true) // Volledige naam van gebruiker vragen.
        {
            WriteLine("Wat is uw volledige naam? (bijv. Hans Berend Gans)");
            FullName = ReadLine();
            if (FullName == "")
            {
                WriteLine("Uw naam moet tekst bevatten, vul uw volledige naam in.");
            }
            else if (FullName.Length >= 33)
            {
                WriteLine("Naam kan niet meer dan 33 karakters zijn. Vul uw naam opnieuw in.");
            }
            else
            {
                break;
            }
        }
        while (true) // email opvragen van gebruiker.
        {
            WriteLine("Vul hier uw email in:");
            EmailAddress = ReadLine();
            EmailInUse = false;
            foreach (AccountModel allAccounts in accountsLogic.GetAllAccounts())
            {
                string email = allAccounts.EmailAddress;
                if (email == EmailAddress)
                {
                    WriteLine("Emailadres is al in gebruik kies een andere.");
                    EmailInUse = true;
                    break;
                }
            }
            if (EmailAddress == "")
            {
                WriteLine("Uw email moet tekst bevatten, vul uw email opnieuw in.");
                continue;
            }
            else if (EmailAddress.Length > 64)
            {
                WriteLine("Uw email mag niet meer dan 64 karakters bevatten, vul uw email opnieuw in.");
                continue;
            }
            else if (EmailAddress.Contains("@") && EmailInUse == false)
            {
                int atSymbolIndex = EmailAddress.IndexOf("@");

                if (atSymbolIndex > 0 && atSymbolIndex < EmailAddress.Length - 1)
                {
                    break;
                }
                else
                {
                    WriteLine("Ongeldig emailadres formaat, u moet minimaal een karakter voor de '@' en na de '@', vul uw email opnieuw in.");
                    continue;
                }
            }
            else if (EmailAddress.Contains("@") == false && EmailInUse == false)
            {
                WriteLine("Uw emailadres moet een '@' symbool bevatten, vul uw email opnieuw in.");
                continue;
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