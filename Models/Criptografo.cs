using System;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public static class Criptografo
    {
        
        public static string TextoCriptografado(string TextoClaro){

            MD5 MD5Hasher = MD5.Create();

            byte[] By = Encoding.Default.GetBytes(TextoClaro);
            byte[] bytesCriptografo = MD5Hasher.ComputeHash(By);

            StringBuilder SB =  new StringBuilder();

            foreach (byte b in bytesCriptografo)
            {
                string DebugB = b.ToString("2x");
                SB.Append(DebugB);
            }

            return SB.ToString();

        }

    }
}