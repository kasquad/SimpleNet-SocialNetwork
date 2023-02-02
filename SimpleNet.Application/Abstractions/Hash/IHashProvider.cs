namespace SimpleNet.Application.Abstractions.Hash;

public interface IHashProvider
{
    public string ComputeHash(string password);
}