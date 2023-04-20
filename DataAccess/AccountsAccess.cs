using System.Text.Json;

static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));


    public static List<AccountModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        List<AccountModel> accounts = JsonSerializer.Deserialize<List<AccountModel>>(json);
        
        if (accounts.Count > 0)
        {
            AccountModel.CurrentId = accounts.Max(f => f.Id) + 1;
        }
        else
        {
            AccountModel.CurrentId = 1;
        }

        return accounts;
    }


    public static void WriteAll(List<AccountModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }



}