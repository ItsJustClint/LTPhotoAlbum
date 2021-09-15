using System;
using System.Collections.Generic;
using System.Text;

namespace LTPhotoAlbum.Services.Abstractions
{
    public interface IConsoleWrapper
    {
        public void ConsoleClear() { }

        public void ConsoleWriteLine() { }

        public void ConsoleReadLine() { }
    }
}
