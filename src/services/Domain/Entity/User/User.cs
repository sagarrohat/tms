namespace Domain;

public class User : EntityBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Pd { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

