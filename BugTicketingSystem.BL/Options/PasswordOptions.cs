namespace BugTicketingSystem.BL.Options
{
    public class PasswordOptions
    {
        public const string Key = "PasswordOptions";
        public bool RequiredDigit { get; set; } = true;
        public bool RequiredLowerCase { get; set; } = true;
        public bool RequiredUpperCase { get; set; } = true;
        public int RequiredLength { get; set; } = 8;

    }
}
