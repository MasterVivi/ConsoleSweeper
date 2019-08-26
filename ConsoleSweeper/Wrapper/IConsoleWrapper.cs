using System;
namespace ConsoleSweeper.Wrapper
{
    public interface IConsoleWrapper
    {
        ConsoleKeyInfo ReadKey();

        void Write(string data);
        void WriteLine(string data);
        void WriteLine(string format, object arg0);

        void SetCursorPosition(int x, int y);

        void Clear();
    }
}