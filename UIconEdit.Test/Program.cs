#region BSD License
/*
Copyright © 2015, KimikoMuffin.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
3. The names of its contributors may not be used to endorse or promote 
   products derived from this software without specific prior written 
   permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace UIconEdit.Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            {
                string[] strings = new string[] { "32", "64", "29", "Depth32", "32Bit", "32Color", "16777216Color", "Depth4294967296Color", IconBitDepth.Depth256Color.ToString() };

                foreach (string str in strings)
                {
                    Console.Write(string.Format("Testing string \"{0}\": ", str));

                    IconBitDepth result;
                    if (IconEntry.TryParseBitDepth(str, out result))
                    {
                        Console.WriteLine("Succeeded! BitDepth." + result);
                    }
                    else Console.WriteLine("Failed!");
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey();
                Console.WriteLine();
            }
            Console.WriteLine("Loading file Gradient.ico ...");
            {
                IconFile iconFile = IconFile.Load("Gradient.ico");
                foreach (IconEntry entry in iconFile.Entries)
                    Save(entry, string.Format("Gradient{0}bit{1}x{2}.png", entry.BitsPerPixel, entry.Width, entry.Height));
                Console.WriteLine("Saving GradientOut.ico ...");
                iconFile.Save("GradientOut.ico");
            }
            Console.WriteLine("Completed!");
            Console.WriteLine("Loading file Crosshair.cur ...");
            {
                CursorFile cursorFile = CursorFile.Load("Crosshair.cur");
                foreach (IconEntry entry in cursorFile.Entries)
                    Save(entry, string.Format("Crosshair{0}bit{1}x{2}.png", entry.BitsPerPixel, entry.Width, entry.Height));

                Console.WriteLine("Saving CrosshairOut.cur ...");
                cursorFile.Save("CrosshairOut.cur");
            }
            Console.WriteLine("Completed!");
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        private static void Save(IconEntry entry, string filename)
        {
            Console.WriteLine("Saving {0} ...", filename);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(entry.CombineAlpha()));
            using (FileStream fs = File.Open(filename, FileMode.Create))
                encoder.Save(fs);
        }
    }
}
