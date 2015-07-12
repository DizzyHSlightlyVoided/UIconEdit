Notes on .ICO files
===================

Microsoft has a [guide to designing icons](https://msdn.microsoft.com/en-us/library/windows/desktop/dn742485.aspx), which describes Windows Aero icons for Windows Vista; the same principles also seem to apply in Windows 7, Windows 8, Windows 8.1, and Windows 10 desktop apps. Windows 8 and 10's app-store apps use [a considerably different design philosophy](https://msdn.microsoft.com/en-us/library/windows/apps/hh465403.aspx). Neither article clearly explains the *structure* of icon files &mdash; that is, which sizes and bit depths you should use. Below, I list my observations of what Microsoft has done in Windows XP, Windows 7, and Windows 10 (I have not worked with Vista, 8, or 8.1).

A Windows icon file consists of [multiple images](http://en.wikipedia.org/wiki/ICO_%28file_format%29) which can either have either 1-bit, 4-bit, 8-bit, 24-bit, or 32-bit colors. Each image stored in one of two different file formats. The first and older format is a modification of the [BMP file format](http://en.wikipedia.org/wiki/BMP_file_format), omitting the file header and storing all usable data contiguously. The .ICO format has existed for longer than BMP files have allowed transparency, so in order to give them transparent backgrounds, the BMP is stored with a height double that of the icon image, and it switches to a 1-bit-per-pixel transparency map, even in 32-bit BMPs. Windows Vista introduced the second format, that of a PNG file stored in its entirety; the purpose of this appears to be the inclusion of 256-pixel images (the width and height in the icon entry are each stored as a single byte).

Icons
-----

Windows XP stored icons in 16x16, 32x32, and 48x48 BMP images, in every bit depth except 1-bit.

Windows 7 uses 32-bit, 8-bit, and badly-dithered 4-bit colors, and also includes a 32-bit 256x256 PNG file in many cases. A few icons here and there also add other icon-sizes; for example, the application icon for explorer.exe also has 128x128, 64x64, and 24x24 BMPs.

Windows 10 only uses 32-bit and 8-bit colors, and each system icon includes 20x20, 40x40, and 80x80 BMPs in addition to all of the above mentioned sizes. Also, the 128x128 32-bit image in each icon is saved as a PNG.

On that note, in Windows system icons, I have only observed PNG files in the 32-bit color range, not in other bit depths. It's technically possible to save PNGs in other color ranges (the PNG format [supports all ICO color formats and more](https://en.wikipedia.org/wiki/Portable_Network_Graphics#Pixel_format), apart from anything else), but given the lack of a *need* for lower color depths in more modern systems, it probably isn't necessary. Also, in my experiments with various icon viewing software, I've discovered that some of them got confused and interpreted *any* PNG images as 32-bit, regardless of the bit depth's listing in the icon structure.

At some point, there was a build of "Project Threshold" (Windows 10's development codename) which had 512x512 and 768x768 icons, presumably for use in 8k monitors with hundreds of pixels per linear inch, but the current Windows Technical Preview version only uses icons up to 256x256, just like Windows 7 and 8. I can only speculate on why they were removed; perhaps because with a four-inch icon on a 48-inch screen, you really aren't likely to clearly see the difference in detail between a 768-pixel square and an expanded 256-pixel square.

Cursors
-------

Windows system cursors are made with precisely one bit-depth. Windows 7 has one 32x32 and one 48x48 image; Windows 10 has 32x32, 48x48, 64x64, 96x96, and 128x128 sizes.