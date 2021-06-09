using System;

namespace CustomerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthConfig config = AuthConfig.ReadPropertiesFromFile("appsettings.json");

            Console.WriteLine($"Authority: {config.Authority}");
        }
    }
}
