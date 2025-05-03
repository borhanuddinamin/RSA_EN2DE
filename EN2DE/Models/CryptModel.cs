using System;
using System.ComponentModel.DataAnnotations;

namespace EN2DE.Models
{
    public class CryptModel
    {
        [Required(ErrorMessage = "Please enter text to encrypt/decrypt")]
        public string InputText { get; set; }
        
        public string OutputText { get; set; }
        
        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }
        
        public bool IsEncrypt { get; set; } = true;
    }
}