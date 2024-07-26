using System.Text;
using System.Security.Cryptography;


public class HashHelper
{
     public static string Hashinfo(string input)
     {
          using (var hash = SHA256.Create())
          {
               var by = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
               return BitConverter.ToString(by).Replace("-" , "").ToLower();
          }
     }
}