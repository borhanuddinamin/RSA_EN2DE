using System.ComponentModel.DataAnnotations;

namespace EN2DE.Models
{
    public class RSACryptModel
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string PlainText { get; set; }
        public string EncryptKey { get; set; }
        public string EncryptedText { get; set; }
        public string CipherText { get; set; }
        public string DecryptKey { get; set; }
        public string DecryptedMessage { get; set; }
    }
}