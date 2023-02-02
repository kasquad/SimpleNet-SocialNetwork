using SimpleNet.Application.Abstractions.Hash;

namespace SimpleNet.Services.Authentication;

public class Md5HashProvider : IHashProvider
{
    public string ComputeHash(string password)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        
        byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(password);
        byte[] hashPasswordBytes = md5.ComputeHash(passwordBytes);
            
        return Convert.ToHexString(hashPasswordBytes);
    }
}