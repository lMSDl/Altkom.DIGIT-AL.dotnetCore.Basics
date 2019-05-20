using System;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Program.Services
{
    public class ConsoleWriteFiggleLineService : IConsoleWriteLineService{
        public void Execute(string line) {
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render(line));
        }
        
    }
}