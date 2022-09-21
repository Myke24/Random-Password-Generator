using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace RandomPasswordGenerator.Models
{
    public class PasswordGeneratorViewModel
    {
        public string password { get; set; } = String.Empty;
        public char randomChar { get; set; }
        public int randomCharType { get; set; } = 0;
        public char newRandomChar { get; set; }


        [DisplayName("Password Length")]
        public int pWordLength { get; set; }

        [DisplayName("Use Upper Case Characters")]
        public bool useUpper { get; set; }
        [DisplayName("Use Lower Case Characters")]
        public bool useLower { get; set; }
        [DisplayName("Use Special Characters")]
        public bool useSpecial { get; set; }
        [DisplayName("Use Numbers")]
        public bool useNumber { get; set; }
        [DisplayName("Exclude Similar Characters")]
        public bool excludeSimilar { get; set; }
        [DisplayName("Fill Password")]
        public bool fillPassword { get; set; }
        public int fillLength { get; set; }
        [DisplayName("Fill Password Input")]
        public string passwordInput { get; set; } = String.Empty;
        public List<string> charTypes = new List<string>() { "upper", "lower", "special", "number" };
        public List<string> charTypesInUse = new List<string>();
        public List<char> upper = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public List<char> special = new List<char>() { '!', '#', '$', '%', '^', '&', '*', '(', ')', '`', '~', '-', '_', '+', '=', '[', '{', ']', '}', '|', ';', ':', ',', '.', '?' };
        public Random random = new Random();
    }
}