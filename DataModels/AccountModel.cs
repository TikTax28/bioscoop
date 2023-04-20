using System.Text.Json.Serialization;


class AccountModel
{
    private static int _nextId;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool isAdmin { get; set; }

    public AccountModel()
    {
        Id = NextId();
    }
    public AccountModel(string emailAddress, string password, string fullName, bool isadmin)
    {
        Id = CurrentId;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        isAdmin = isadmin;

        _nextId = Id + 1;
    }
    private static int NextId()
    {
        return ++_nextId;
    }

    public static int CurrentId
    {
        get { return _nextId; }
        set { _nextId = value; }
    }
}




