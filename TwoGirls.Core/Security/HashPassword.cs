using System.Security.Cryptography;
namespace TwoGirls.Core.Security
{
    public static class HashPassword
    {
        private const int SaltSize = 16; // 128-bit salt
        private const int HashSize = 32; // 256-bit hash
        private const int Iterations = 10000; // recommended iterations as of 2023
        private const string HashPrefix = "$HASH$V1$"; // prefix to identify hash type and version

        public static string Hash(string password)
        {
            // Generate a random salt
            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt and other parameters
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine the salt and hash together and add a prefix to indicate the hash type and version
            var combined = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, combined, 0, SaltSize);
            Array.Copy(hash, 0, combined, SaltSize, HashSize);
            var saltString = Convert.ToBase64String(salt);
            var hashString = Convert.ToBase64String(hash);
            return $"{HashPrefix}{saltString}${hashString}";
        }

        public static bool Verify(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
            {
                return false;
            }

            var hashParts = hash.Split('$');

            if (hashParts.Length != 5 || hashParts[1] != "HASH" || hashParts[2] != "V1")
            {
                return false;
            }

            var salt = Convert.FromBase64String(hashParts[3]);
            var expectedHash = Convert.FromBase64String(hashParts[4]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                var actualHash = pbkdf2.GetBytes(HashSize);

                if (actualHash.Length != expectedHash.Length)
                {
                    return false;
                }

                for (int i = 0; i < actualHash.Length; i++)
                {
                    if (actualHash[i] != expectedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}