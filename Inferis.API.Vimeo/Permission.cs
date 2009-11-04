using System;

namespace Inferis.API.Vimeo
{
    public enum Permission
    {
        None = 0,
        Read,
        Write,
        Delete
    }

    public static class PermissionExtensions
    {
        public static Permission ToPermission(this string permission)
        {
            return (Permission)Enum.Parse(typeof(Permission), permission, true);
        }
    }
}
