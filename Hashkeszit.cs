using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace jatek
{
    public static class Hashkeszit
    {
        public static string KeszitHash(string jelszo)
        {
            byte[] salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            byte[] jelszoBytes = Encoding.UTF8.GetBytes(jelszo);
            byte[] combined = new byte[salt.Length + jelszoBytes.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(jelszoBytes, 0, combined, salt.Length, jelszoBytes.Length);

            using var sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(combined);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        // Ellenőrzi, hogy a megadott jelszó hash-e egyezik-e az eltárolttal
        public static bool EllenorizHash(string jelszo, string elmentett)
        {
            string[] reszek = elmentett.Split(':');
            if (reszek.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(reszek[0]);
            byte[] jelszoBytes = Encoding.UTF8.GetBytes(jelszo);
            byte[] combined = new byte[salt.Length + jelszoBytes.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(jelszoBytes, 0, combined, salt.Length, jelszoBytes.Length);

            using var sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(combined);
            string ujHashString = Convert.ToBase64String(hash);

            return ujHashString == reszek[1];
        }
    }
}
