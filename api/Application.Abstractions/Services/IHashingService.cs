namespace Application;

public interface IHashingService
{
    string Compute(string text);
    bool Verify(string text, string hashedText);
}