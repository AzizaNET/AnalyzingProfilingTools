using System.Security.Cryptography;

namespace Task1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("****** Task 1 ******");

            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            byte[] salt = GenerateSalt();

            Console.WriteLine(GeneratePasswordHashUsingSalt(password, salt)); 
            
        }

        static byte[] GenerateSalt() 
        {
            byte[] salt = new byte[32];
            new Random().NextBytes(salt);
            return salt;
        
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt) 
        {
            var iterate = 10000;

            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);

            Array.Copy(hash, 0, hashBytes, 16, 20);
            
            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        public static string stringGeneratePasswordHashUsingSaltOptimized(string passwordText, byte[] salt) 
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, 1000)) 
            {
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[salt.Length + hash.Length];

                Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length);
                Buffer.BlockCopy(hash, 0, hashBytes, salt, 20);


            }
        
        }
    }
}