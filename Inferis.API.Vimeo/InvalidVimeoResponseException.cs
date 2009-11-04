using System;

namespace Inferis.API.Vimeo
{
    public class InvalidVimeoResponseException : Exception
    {
        public InvalidVimeoResponseException() 
            : base("An invalid response was received from Vimeo. The REST response could not be parsed.")
        {
        }
    }
}
