namespace Common
{
    public interface IPasswordHashProvider
    {
        string CreateHash(string password);

        bool ValidatePassword(string password, string correctHash);
    }
}