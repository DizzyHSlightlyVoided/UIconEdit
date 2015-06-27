UIconEdit
=========
A .Net library for reading and editing Windows cursor and animation files. Specifically, it is used to take one or more images, and resize and combine them to form a single icon or cursor file; and may also be used to extract images from an icon. (There is currently no functionality for animated cursors; however, this is planned for future updates.)

This is a basic form of creating an icon:
```C#
using (Image image = Image.FromFile(@"C:\Path\To\Input.png")) //A 256x256 PNG file.
using (IconFile iconFile = new IconFile())
{
    iconFile.Entries.Add(new IconEntry(image, BitDepth.Bit32);
    iconFile.Entries.Add(new IconEntry(image, 48, 48, BitDepth.Bit32);
    iconFile.Entries.Add(new IconEntry(image, 32, 32, BitDepth.Bit32);
    iconFile.Entries.Add(new IconEntry(image, 16, 16, BitDepth.Bit32);

    iconFile.Save("C:\Path\To\Output.ico");
}
```

And this is the most basic form for extracting an image from a cursor file:
```C#
using (IconFile iconFile = IconFile.Load(@"C:\Path\To\Input.ico"))
{
    IconEntry entry = iconFile.Entries[0];
    Console.WriteLine(entry.Width);
    Console.WriteLine(entry.Height);
    entry.Image.Save("C:\Path\To\Output.png");
}
```

UIconBuilder
------------
A command-line utilty for creating icon files, built using UIconEdit. There are two ways to use it: using a text file containing the information on every icon image, and passing the information of each image as parameters.

```
UIconBuilder.exe /cur "C:\Path\To\Output.cur" "C:\Path\To\Input.txt"
UIconBuilder.exe /ico "C:\Path\To\Output.ico" /i [enry info] /i [enry info] ...
```

Each line of a text file contains information for a different entry; lines preceded by a pound sign (`#`) are comments, which are ignored. In command line form, the information for each entry is separated by an `/i` argument.

The image data consists of the following:

* The intended **size** of the image, as two integers separated by the letter `x`, i.e. `48x48`, respectively the width and height. If the source image file is not this size, it will be resized. The minimum value is 1, and the maximum value is 768 as of Windows 10.
* The **hotspot** of a cursor image, as two integers separated by a comma (`,`), i.e. `24,24`. This is where the cursor will "point" at in a cursor. Not valid for icon files. The values must be less than or equal to the size.
* The **bit depth**, in bits per pixel, or the color count. Valid values are 1 bit per pxel (2 colors), 4 bits per pixel (16 colors), 8 bits per pixel (256 color), 24 bits per pixel (16777216 colors), and 32 bits per pixel (24-bit with full transparency). Can take various forms:
 * `32-Bit`
 * `24BitsPerPixel`
 * `256-color`
 * `16` (there is no overlap or abiguity between bit count and color count, so a literal number will be interpreted correctly.)
* The **alpha threshold**, as an integer between 0 and 255 preceded by `a=`. In non-32-bit images, opacity (or **alpha**) is stored separately from the colors, as either fully transparent or fully opaque. In many other image formats (PNG, etc), the opacity can be anything from 0 (fully transparent) to 255 (fully opaque). When downsampling from 32 bits, any pixel with an opacity less than the alpha threshold will become fully transparent, and any pixel with an opacity greater than or equal to the alpha threshold will become fully opaque. The default, if no value is ever given, is `96`.
* The path to the source image file. Everything after `f=` is treated as part of the path.

If any of the above information is ommitted, the information from the previous entry is used.

Example contents of a text file for a cursor:

```
48x48 24,24 32Bit a=96 f=C:\Path\To\Input48x48.png
48x48 24,24 24Bit f=C:\Path\To\Input48x48.png
#Everything is the same for these next two entries except the bit depth.
32x32 32Bit 16,16 f=C:\Path\To\Input32x32.png
24-Bit
16x16 32Bit 8,8
24-Bit
```
