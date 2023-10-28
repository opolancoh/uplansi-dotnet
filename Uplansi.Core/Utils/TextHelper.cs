namespace Uplansi.Core.Utils;

public class TextHelper
{
    public static class RegExp
    {
        public static string PersonNameRegExp => @"^[a-zA-Z0-9 .,`'()ÁÉÍÓÚáéíóúñÑ\/\-']*$";
        
        public static string UserNameRegExp => @"^[a-zA-Z0-9_@.\-']*$";
        
        public static string EmailRegExp => @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
    }
}