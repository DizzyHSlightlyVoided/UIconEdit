#region BSD license
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
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace UIconEdit.Builder
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            args = args.Where(s => !string.IsNullOrWhiteSpace(s)).Select(delegate (string s)
            {
                s = s.Trim();
                if (s == "/I") return "/i";
                return s;
            }).ToArray();

            if (args.Length == 0)
            {
                HelpMessage();
                return Finisher(1);
            }

            const string noWait1 = "/nowait", noWait2 = "-nowait";
            switch (args[0].ToLower())
            {
                case "/ico":
                case "-ico":
                case "/icon":
                case "-icon":
                    file = new IconFile();
                    break;
                case "/cur":
                case "-cur":
                case "/cursor":
                case "-cursor":
                    file = new CursorFile();
                    break;
                case noWait1:
                case noWait2:
                    nowait = true;
                    goto default;
                default:
                    HelpMessage();
                    return Finisher(2);
            }
            int resultCode = 0;

            try
            {
                for (int i = 1; i < args.Length; i++)
                {
                    string curArg = args[i];

                    if (outputFile == null)
                    {
                        if (curArg.Equals(noWait1, StringComparison.OrdinalIgnoreCase) || curArg.Equals(noWait2, StringComparison.OrdinalIgnoreCase))
                        {
                            nowait = true;
                            continue;
                        }
                        outputFile = curArg;

                        Console.WriteLine("Writing {0} file to {1} ...", file.ID, outputFile);

                        continue;
                    }

                    if (curArg.Equals("/i", StringComparison.OrdinalIgnoreCase))
                    {
                        i++;
                        if (i == args.Length)
                            break;

                        int nextDex = Array.IndexOf(args, "/i", i);

                        if (nextDex < 0) nextDex = args.Length;

                        if (imageParams == null) imageParams = new List<string>();

                        string line = string.Join(" ", new ArraySegment<string>(args, i, nextDex - i));

                        imageParams.Add(line);

                        i = nextDex - 1;
                        continue;
                    }

                    Console.WriteLine("Loading params from {0} ...", curArg);

                    imageParams = File.ReadAllLines(curArg);
                }

                if (outputFile == null || imageParams == null)
                    throw new ArgumentException("Missing parameters!");

                EntryItem item = new EntryItem(IconEntry.DefaultAlphaThreshold);
                _images = new Dictionary<string, BitmapSource>();

                foreach (string[] curLine in imageParams.Select(s => s.Trim().Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)))
                {
                    if (curLine.Length == 0)
                        continue;

                    if (curLine[0][0] == '#') continue;

                    Seen seen = Seen.None;
                    for (int i = 0; i < curLine.Length; i++)
                    {
                        string curWord = curLine[i];

                        if (curWord.StartsWith("-f=", StringComparison.OrdinalIgnoreCase) || curWord.StartsWith("/f=", StringComparison.OrdinalIgnoreCase)
                            || curWord.StartsWith("f=", StringComparison.OrdinalIgnoreCase))
                        {
                            curWord = curWord.Substring(curWord.IndexOf('=') + 1);
                            i++;
                            if (i < curLine.Length)
                                curWord += ' ' + string.Join(" ", new ArraySegment<string>(curLine, i, curLine.Length - i));

                            string curPath = Path.GetFullPath(curWord);

                            BitmapSource img;
                            if (!_images.TryGetValue(curPath, out img))
                            {
                                img = BitmapFrame.Create(new Uri(curPath));
                                _images[curPath] = img;
                            }
                            item.Img = img;
                            break;
                        }

                        BitDepth depth;
                        if (IconEntry.TryParseBitDepth(curWord, out depth))
                        {
                            if (seen.HasFlag(Seen.Depth))
                                throw new ArgumentException("Duplicate bit-depth parameter: " + depth);

                            item.Depth = depth;
                            seen |= Seen.Depth;
                            continue;
                        }

                        string getWord = curWord.ToLower(); //Case only matters with filename, so treat everything else as lowercase.

                        if (getWord.StartsWith("-a=") || getWord.StartsWith("/a=") || getWord.StartsWith("a="))
                        {
                            if (seen.HasFlag(Seen.Alpha))
                                throw new ArgumentException("Alpha parameter given twice: " + curWord);

                            getWord = getWord.Substring(getWord.IndexOf('=') + 1);

                            byte alpha;
                            if (!byte.TryParse(getWord, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out alpha))
                                throw new ArgumentException("Invalid alpha value:" + curWord);

                            item.Alpha = alpha;

                            seen |= Seen.Alpha;

                            continue;
                        }

                        int dex = getWord.IndexOf(',');

                        if (dex >= 0)
                        {
                            if (seen.HasFlag(Seen.Hotspot))
                                throw new ArgumentException("Duplicate hotspot parameter: " + curWord);

                            if (file.ID != IconTypeCode.Cursor)
                                throw new ArgumentException("Hotspots are not supported in icon files: " + curWord);

                            string first = getWord.Substring(0, dex);
                            string second = getWord.Substring(dex + 1);

                            ushort x, y;
                            if (!ushort.TryParse(first, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out x) ||
                                !ushort.TryParse(second, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out y))
                                throw new ArgumentException("Invalid hotspot parameter: " + curWord);

                            item.SeenHotspot = true;
                            seen |= Seen.Hotspot;
                            continue;
                        }

                        dex = getWord.IndexOf('x');

                        if (dex >= 0)
                        {
                            if (seen.HasFlag(Seen.Size))
                                throw new ArgumentException("Duplicate size parameter: " + curWord);

                            string first = getWord.Substring(0, dex);
                            string second = getWord.Substring(dex + 1);

                            short width, height;
                            if (!short.TryParse(first, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out width) ||
                                !short.TryParse(second, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out height) ||
                                width < IconEntry.MinDimension || height < IconEntry.MinDimension ||
                                width > IconEntry.MaxDimension || height > IconEntry.MaxDimension)
                                throw new ArgumentException("Invalid size parameter: " + curWord);

                            item.Width = width;
                            item.Height = height;

                            seen |= Seen.Size;
                            continue;
                        }

                        throw new ArgumentException("Invalid parameter: " + curWord);
                    }

                    if (item.Img == null || item.Width == 0 || item.Height == 0)
                        throw new ArgumentException("Missing parameters!");

                    if (item.Width < item.X || item.Height < item.Y)
                        throw new ArgumentException(string.Format("Hotspot {0},{1} is larger than item size {2}x{3}.", item.X, item.Y, item.Width, item.Height));

                    IconEntry entry;

                    if (file.ID == IconTypeCode.Cursor)
                        entry = item.GetCursorEntry();
                    else
                        entry = item.GetIconEntry();

                    if (!file.Entries.Add(entry))
                    {
                        file.Entries.RemoveSimilar(entry);
                        file.Entries.Add(entry);
                    }
                }

                if (file.Entries.Count == 0)
                    throw new ArgumentException("Missing parameters!");

                file.Save(outputFile);

                Console.WriteLine("Completed!");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine();
                HelpMessage();
                resultCode = e.HResult;
            }

            return Finisher(resultCode);
        }

        [Flags]
        private enum Seen
        {
            None,
            Size = 1,
            Hotspot = 2,
            Depth = 4,
            Alpha = 8,
        }

        private struct EntryItem
        {
            public EntryItem(byte alpha)
                : this(null, 0, 0, 0, 0, 0, alpha)
            {
            }

            public EntryItem(BitmapSource img, short width, short height, BitDepth depth, ushort x, ushort y, byte alpha)
            {
                Img = img;
                Width = width;
                Height = height;
                Depth = depth;
                Alpha = alpha;
                X = x;
                Y = y;
                SeenHotspot = false;
            }

            public EntryItem(BitmapSource img, short width, short height, BitDepth depth, byte alpha)
                : this(img, width, height, depth, 0, 0, alpha)
            {
            }

            public BitmapSource Img;
            public ushort X, Y;
            public byte Alpha;
            public short Width, Height;
            public BitDepth Depth;
            internal bool SeenHotspot;

            public IconEntry GetIconEntry()
            {
                return new IconEntry(Img, Width, Height, Depth, Alpha);
            }

            public IconEntry GetCursorEntry()
            {
                return new IconEntry(Img, Width, Height, Depth, X, Y, Alpha);
            }
        }

        static IconFileBase file;
        static Dictionary<string, BitmapSource> _images;
        static IList<string> imageParams;
        static string outputFile;

        static bool nowait;
        static int Finisher(int result)
        {
            if (!nowait)
                Wait();
            return result;
        }

        static void Wait()
        {
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void HelpMessage()
        {
            string filename = Path.GetFileNameWithoutExtension(typeof(Program).Assembly.Location);
            Console.Write(filename);
            Console.WriteLine(" [/ico or /cur] (/nowait) [path to output] /i [image options] /i [image options] ...");
            Console.WriteLine("  -OR-");
            Console.Write(filename);
            Console.WriteLine(" [/ico or /cur] (/nowait) [path to output] [path to text file]");
            Console.WriteLine();
            Console.WriteLine("Use /ico as the first argument to make an icon, and /cur to make a cursor.");
            Console.WriteLine("Use /nowait to end immediately after processing instead of asking for input.");
            Console.WriteLine("[path to output] - the path to the output file.");
            Console.WriteLine("[path to text file] - A text file containing image options, one on each line.");
            Console.WriteLine();
            Console.WriteLine("[Image options] consists of: ");
            Console.WriteLine("    /i [width]x[height] [HotspotX],[HotspotY] [Depth] a=[Alpha] f=[image path]");
            Console.WriteLine("* /i - separates each image entry from the next. Not mandatory in text-file");
            Console.WriteLine(" form.");
            Console.WriteLine("* [width]x[height] - two integers between {0} and {1}, separated by the letter X.", IconEntry.MinDimension, IconEntry.MaxDimension);
            Console.WriteLine(" Specifies the size of the icon entry.");
            Console.WriteLine("* [HotspotX],[HotspotY] - Two integers between 0 and the width and height,");
            Console.WriteLine(" separated by a comma. Cursors only. Specifies the offset of the cursor's");
            Console.WriteLine(" hotspot from the upper-left corner.");
            Console.WriteLine("* [Bit Depth] - indicates bits-per-pixel/color count. Valid formats are");
            Console.WriteLine(" 32-bit, 24-bit, 8-bit (256-color), 4-bit (16-color), and 1-bit (2-color).");
            Console.WriteLine(" Can take various forms:");
            Console.WriteLine("  24-Bit");
            Console.WriteLine("  32BitsPerPixel");
            Console.WriteLine("  16_Color");
            Console.WriteLine("  256");
            Console.WriteLine(" (There's no overlap between the two sets of numbers; 256=256 colors, etc.)");
            Console.WriteLine(" Defaults to 32 bits per pixel.");
            Console.WriteLine("* a=[Alpha] - an integer between 0 and 255, preceded by the letter A. Indicates");
            Console.WriteLine(" alpha threshold; opacity values less than this will be rendered fully");
            Console.WriteLine(" transparent below 32-bit colors, and values greater than this will be fully");
            Console.WriteLine(" opaque. This parameter is optional and defaults to {0}.", IconEntry.DefaultAlphaThreshold);
            Console.WriteLine("* f=[image path] - The path to the image file used. Everything between this");
            Console.WriteLine(" and the next /i are used.");
            Console.WriteLine();
            Console.WriteLine("If any arguments are ommitted, the values from the previous entry are");
            Console.WriteLine("used. However, size and bit-depth combinations must be unique.");
            Console.WriteLine();
        }
    }
}
