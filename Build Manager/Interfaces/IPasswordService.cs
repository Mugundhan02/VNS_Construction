namespace BuildManager.Interfaces
{
    public interface IPasswordService
    {
        byte[] GenerateSalt();

        byte[] HashPassword(string password, byte[] salt);

        bool VerifyPassword(string password, byte[] storedHash, byte[] salt);
    }
}