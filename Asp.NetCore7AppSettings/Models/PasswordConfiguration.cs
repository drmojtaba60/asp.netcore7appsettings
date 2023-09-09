namespace Asp.NetCore7AppSettings.Models
{
    public class PasswordConfiguration
    {
        public int MinLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
    }
}
