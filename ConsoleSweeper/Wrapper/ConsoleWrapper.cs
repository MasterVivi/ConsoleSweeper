using System;
namespace ConsoleSweeper.Wrapper
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void Clear()
        {
            Console.Clear();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void Write(string data)
        {
            Console.Write(data);
        }

        public void WriteLine(string data)
        {
            Console.WriteLine(data);
        }

        public void WriteLine(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
        }
    }
}