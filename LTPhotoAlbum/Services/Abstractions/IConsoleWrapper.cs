using System;
using System.Collections.Generic;
using System.Text;

namespace LTPhotoAlbum.Services.Abstractions
{
    // Do we need this interface for something?
    public interface IConsoleWrapper
    {
        public void ConsoleClear() { }

        public void ConsoleWriteLine() { }

        public void ConsoleReadLine() { }
    }
}
