namespace Inferis.API.Vimeo.Advanced.Callers
{
    public class AnonymousCaller : CallerImplBase, IAnonymousCaller
    {
        public AnonymousCaller(IApiSettings settings)
            : base(settings)
        {

        }
    }
}
