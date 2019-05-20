using System;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Program.Services
{
    public class ConsoleWriteLineService : IConsoleWriteLineService{
        public void Execute(string line) {
            Console.WriteLine(line);
        }
        
    }
}