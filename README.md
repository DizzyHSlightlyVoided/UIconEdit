UIconEdit
=========
A C# application for opening and editing [Windows .CUR and .ICO files](http://en.wikipedia.org/wiki/ICO_%28file_format%29), using the [Windows Presentation Foundation](http://en.wikipedia.org/wiki/Windows_Presentation_Foundation) for image processing. The project is divided into three parts:

* The UIconEdit.Core library, which allows icon creation and modification in code.
* The UIconEdit WPF graphical app.
* The UIconBuilder command-line utility for creating icon or cursor files.

UIconEdit app's functionality
-----------------------------
* Opening and saving icons and cursors.
* Importing images from external files.
* Exporting icon images as .PNG files.
* Duplicating an image with a different size and/or bit depth. You could just take a single 256x256 PNG image, and set 256x256, 48x48, 32x32, and 16x16 images, with 32-bit, 24-bit, and 8-bit color depths.
* Icon sizes up to 768x768 (the maximum as of Windows 10).
* Opening .ICO, .CUR, and image files as a command-line parameter.

Planned functionality
---------------------
### UIconEdit
* Getting and setting a cursor's hotspot.
* Extracting icons from .DLL and .EXE files.

### UIconEdit.Core
* The creation of animated cursors (.ANI files). This probably won't make it into UIconEdit itself.

UIconEdit.Core examples
-----------------------

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

NOTICE
------
UIconEdit and UIconBuilder are currently **in development**. Parts of the API and functionality may be changed or removed without warning.
