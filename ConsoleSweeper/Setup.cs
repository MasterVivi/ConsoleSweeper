using System;
using ConsoleSweeper.Logic;
using ConsoleSweeper.Model;
using ConsoleSweeper.Wrapper;
using SimpleInjector;

namespace ConsoleSweeper
{
    public static class Setup
    {
        public static Container Container;

        public static void Launch()
        {
            // Create a new Simple Injector container
            Container = new Container();

            // Configure the container
            Container.Register<IPlayer, Player>(Lifestyle.Singleton);
            Container.Register<IBoard, Board>(Lifestyle.Singleton);
            Container.Register<IConsoleWrapper, ConsoleWrapper>(Lifestyle.Singleton);

            // Verify configuration
            Container.Verify();
        }
    }
}