namespace Acorisoft.Extensions.Windows
{
    public static class Xaml
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly object False = false;

        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly object True = true;

        public static object Box(bool expression) => expression ? True : False;
    }
}