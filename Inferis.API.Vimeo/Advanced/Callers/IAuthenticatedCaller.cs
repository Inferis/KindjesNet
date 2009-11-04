namespace Inferis.API.Vimeo.Advanced.Callers
{
    public interface IAuthenticatedCaller
    {
        ICallerImpl WithToken(string token); 
    }
}
