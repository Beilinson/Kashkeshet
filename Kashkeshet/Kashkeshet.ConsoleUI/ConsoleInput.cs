using Kashkeshet.Common.UI;
using System;

namespace Kashkeshet.ConsoleUI
{
    public class ConsoleInput : IInput
    {
        public object Input()
        {
            var input = Console.ReadLine();
            // Magic to remove the previously written line !!!
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            return input;
        }
    }
}
