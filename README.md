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

NOTICE
------
UIconEdit and UIconBuilder are currently **in development**. Parts of the API and functionality may be changed or removed without warning.