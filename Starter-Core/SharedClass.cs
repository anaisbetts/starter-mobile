using System;

namespace Starter.Core
{
    public static class SharedClass
    {
        public static void PrintSomeStuff()
        {
            // This class is shared between both iOS and Android, 
            // Starter-Core-Android and Starter-Core-iOS both live in the same
            // directory, so put code that you can share here.
            //
            // All of your HTTP'y and SaveyLoady and Model'y code would be great
            // in this project
            Console.WriteLine("Foo");
        }
    }
}

