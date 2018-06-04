namespace Common
{
    public class PasswordHashProvider : IPasswordHashProvider
    {
        public string CreateHash(string password)
        {
            return PasswordHash.CreateHash(password);
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            return PasswordHash.ValidatePassword(password, correctHash);
        }
    }
}