using System;
using System.Windows.Forms;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for LibraryMenuItem.
    /// </summary>
    public class LibraryMenuItem : MenuItem
    {
        public LibraryMenuItem(string filename, string text, EventHandler handler) :
            base(text, handler)
        {
            Filename = filename;
        }

        public string Filename { get; }
    }
}