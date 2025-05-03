using System;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace EN2DE.Services
{
    public class RSACryptService
    {
        private readonly Random _random = new Random();

        public object GenerateKeyPair()
        {
            int p = GeneratePrimeNumber(100, 300);  // Slightly larger for better `n`
            int q = GeneratePrimeNumber(100, 300);
            while (p == q)
            {
                q = GeneratePrimeNumber(100, 300);
            }

            int n = p * q;
            int totient = (p - 1) * (q - 1);
            int e = FindCoprime(totient);
            int d = ModInverse(e, totient);

            return new
            {
                PublicKey = $"{n},{e}",
                PrivateKey = $"{n},{d}"
            };
        }

        public string Encrypt(string plaintext, string keyPair)
        {
            try
            {
                var parts = keyPair.Split(',');
                if (parts.Length != 2)
                    return "Invalid key pair format";

                int n = int.Parse(parts[0].Trim());
                int e = int.Parse(parts[1].Trim());

                List<BigInteger> cipherList = new List<BigInteger>();
                foreach (char c in plaintext)
                {
                    int m = (int)c;
                    BigInteger encrypted = BigInteger.ModPow(m, e, n);
                    cipherList.Add(encrypted);
                }

                return string.Join(",", cipherList);
            }
            catch (Exception ex)
            {
                return $"Encryption error: {ex.Message}";
            }
        }

        public string Decrypt(string cipherText, string keyPair)
        {
            try
            {
                var parts = keyPair.Split(',');
                if (parts.Length != 2)
                    return "Invalid key pair format";

                int n = int.Parse(parts[0].Trim());
                int d = int.Parse(parts[1].Trim());

                string[] cipherArray = cipherText.Split(',');
                StringBuilder decryptedText = new StringBuilder();
                foreach (string part in cipherArray)
                {
                    if (BigInteger.TryParse(part.Trim(), out BigInteger encrypted))
                    {
                        BigInteger decrypted = BigInteger.ModPow(encrypted, d, n);
                        decryptedText.Append((char)(int)decrypted);
                    }
                    else
                    {
                        return "Invalid encrypted value format.";
                    }
                }

                return decryptedText.ToString();
            }
            catch (Exception ex)
            {
                return $"Decryption error: {ex.Message}";
            }
        }

        private int GeneratePrimeNumber(int min, int max)
        {
            int num;
            do
            {
                num = _random.Next(min, max);
            } while (!IsPrime(num));
            return num;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }

        private int FindCoprime(int n)
        {
            int e = 2;
            while (e < n)
            {
                if (Gcd(e, n) == 1)
                    return e;
                e++;
            }
            return -1;
        }

        private int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private int ModInverse(int a, int m)
        {
            int m0 = m, t, q;
            int x0 = 0, x1 = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                q = a / m;
                t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
                x1 += m0;

            return x1;
        }
    }
}
