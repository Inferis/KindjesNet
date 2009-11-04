namespace Inferis.API.Vimeo.Advanced.Callers
{
    public interface ISignedCaller
    {
        ISignedCallerImpl WithSecret(string secret);
    }
}
