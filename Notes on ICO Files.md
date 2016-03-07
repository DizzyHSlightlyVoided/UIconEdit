Notes on .ICO files
===================
Microsoft has a [guide to designing icons](https://msdn.microsoft.com/en-us/library/windows/desktop/dn742485.aspx), which describes Windows Aero icons for Windows Vista; the same principles also seem to apply in Windows 7, Windows 8, Windows 8.1, and Windows 10 desktop apps. Windows 8 and 10's app-store apps use [a considerably different design philosophy](https://msdn.microsoft.com/en-us/library/windows/apps/hh465403.aspx). Neither article clearly explains the *structure* of icon files &mdash; that is, which sizes and bit depths you should use. Below, I list my observations of what Microsoft has done in Windows XP, Windows 7, and Windows 10 (I have not worked with Vista, 8, or 8.1).

A Windows icon file consists of [multiple images](http://en.wikipedia.org/wiki/ICO_%28file_format%29) which can either have either 1-bit, 4-bit, 8-bit, 24-bit, or 32-bit colors. Technically, you're only allowed to have one size- and bit-depth combination. A given image in an icon is in one of two file formats. The first and older format is a modification of the [BMP file format](http://en.wikipedia.org/wiki/BMP_file_format), omitting the file header and storing all usable image data contiguously. Windows Vista introduced the second format, that of a PNG file stored in its entirety. The two benefits of this are that 1. you can have 256-pixel images (the width and height in the icon structure are each stored as a single byte), and 2. PNG files tend to take up much less space than BMPs.

Icons
-----
Windows always stores icons as squares. It's technically possible to have rectangular icons where the width and height are different, but there's no real benefit to doing this.

Windows XP stored icons in 16x16, 32x32, and 48x48 BMP images, in every bit-depth except 1-bit.

Windows 7 tended to use 32-bit, 8-bit, and badly-dithered 4-bit colors, and also includes a 32-bit 256x256 PNG file in many cases. A few icons here and there also added other icon-sizes; for example, the application icon for explorer.exe also has 128x128, 64x64, and 24x24 BMPs.

Windows 10 only uses 32-bit icons, and each system icon includes 16x16, 20x20, 24x24, 32x32, 40x40, 48x48, 64x64, and 256x256. The Windows Technical Preview also tended to include 80x80, 96x96, and 128x128 icons, and 8-bit colors for all except the 128x128 and 256x256 versions; the 32-bit 128x128 image was also a PNG file.

In Windows system icons, I have only seen PNG files used as 32-bit images, not other bit depths. It's technically possible to save PNGs in other color ranges; the PNG format [supports all ICO color formats and more](https://en.wikipedia.org/wiki/Portable_Network_Graphics#Pixel_format). However, given the lack of a *need* for lower color depths in more modern systems (again, Windows 10 omits them entirely), it probably isn't necessary. Also, in my experiments with various icon viewing programs, I've discovered that some of them got confused and interpreted *any* PNG images as 32-bit, regardless of the bit depth's listing in the icon structure. This includese WPF's own [IconBitmapDecoder](https://msdn.microsoft.com/en-us/library/ms602492.aspx).

At some point, there was a build of "Project Threshold" (Windows 10's development codename) which had 512x512 and 768x768 icons, presumably for use in 8k monitors with hundreds of pixels per linear inch. However, the released version of Windows 10 only uses icons up to 256x256. I can only speculate on why they were removed; perhaps Microsoft felt that when we're talking about a three-inch icon on a 48-inch screen, it's going to be hard to see the difference between a 768-pixel icon and an expanded 256-pixel icon, so it wasn't worth the extra resolution.

Cursors
-------
Windows system cursors have precisely one bit-depth. Both Windows 7 and Windows 10 use 32-bit images. Windows 7 cursors have one 32x32 and one 48x48 image; Windows 10 cursors have 32x32, 48x48, 64x64, 96x96, and 128x128 images.

Windows itself supports PNG images in animated cursors; however, I have found that there is relatively poor support for PNGs in *software* for editing animated cursors.
