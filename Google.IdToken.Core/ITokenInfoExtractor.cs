namespace Google.IdToken.Core
{
    public interface ITokenInfoExtractor
    {
        string Extract(string idToken);
    }
}