# Type: `UIconEdit.IconFileBase`

 Base class for icon and cursor files.

--------------------------------------------------
## Constructor: `IconFileBase()`

 Initializes a new instance.

--------------------------------------------------
## Method: `IconFileBase.Load(System.IO.Stream)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified stream.
* `input`: A stream containing an icon or cursor file.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `input`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `input` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `input` is closed or does not support reading.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `input` is closed.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFileBase.Load(System.IO.Stream, System.Action<UIconEdit.IconLoadException>)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified stream.
* `input`: A stream containing an icon or cursor file.
* `handler`: A delegate used to process [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
s thrown when processing individual icon frames,  or `null` to throw an exception in those cases.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `input`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `input` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `input` is closed or does not support reading.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `input` is closed.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFileBase.Load(System.String, System.Action<UIconEdit.IconLoadException>)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified path.
* `path`: The path to a cursor or icon file.
* `handler`: A delegate used to process [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
s thrown when processing individual icon frames,  or `null` to throw an exception in those cases.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `path`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `path` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`System.IO.Path.GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx)
.

* [`System.IO.PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx) -  The specified path, filename, or both contain the system-defined maximum length.

* [`System.IO.FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx) -  The specified path was not found.

* [`System.IO.DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx) -  The specified path was invalid.

* [`System.UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx) - 
`path` specified a directory.

-OR-

The caller does not have the required permission.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) - `path` is in an invalid format.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFileBase.Load(System.String)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified path.
* `path`: The path to a cursor or icon file.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `path`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `path` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`System.IO.Path.GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx)
.

* [`System.IO.PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx) -  The specified path, filename, or both contain the system-defined maximum length.

* [`System.IO.FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx) -  The specified path was not found.

* [`System.IO.DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx) -  The specified path was invalid.

* [`System.UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx) - 
`path` specified a directory.

-OR-

The caller does not have the required permission.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) - `path` is in an invalid format.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFileBase.Clone()`

 Returns a duplicate of the current instance.

**Returns:** A duplicate of the current instance, with copies of every icon frame and clones of each  frame's [`UIconEdit.IconFrame.BaseImage`](#property-uiconediticonframebaseimage)
 in [`UIconEdit.IconFileBase.Frames`](#property-uiconediticonfilebaseframes)
.


--------------------------------------------------
## Method: `IconFileBase.IsValid(UIconEdit.IconFrame)`

 When overridden in a derived class, gets a value indicating whether the specified value may be added to [`UIconEdit.IconFileBase.Frames`](#property-uiconediticonfilebaseframes)
.
* `frame`: The frame to check.

**Returns:** `true` if `frame` is not `null`; `false` otherwise.


--------------------------------------------------
## Method: `IconFileBase.GetImgX(UIconEdit.IconFrame)`

 When overridden in a derived class, computes the 16-bit X component.
* `frame`: The image frame to calculate.

**Returns:** In icon files, the color panes. In cursor files, the horizontal offset of the hotspot from the left in pixels.


--------------------------------------------------
## Method: `IconFileBase.GetImgY(UIconEdit.IconFrame)`

 When overridden in a derived class, computes the 16-bit Y component.
* `frame`: The image frame to calculate.

**Returns:** In icon files, the number of bits per pixel. In cursor files, the vertical offset of the hotspot from the top, in pixels.


--------------------------------------------------
## Method: `IconFileBase.Save(System.IO.Stream)`

 Saves the file to the specified stream.
* `output`: The stream to which the file will be written.

### Exceptions

* [`System.InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx) - [`UIconEdit.IconFileBase.Frames`](#property-uiconediticonfilebaseframes)
 contains zero elements.

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `output` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `output` is closed or does not support writing.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `output` is closed.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFileBase.Save(System.String)`

 Saves the file to the specified file.
* `path`: The file to which the file will be written.

### Exceptions

* [`System.InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx) - [`UIconEdit.IconFileBase.Frames`](#property-uiconediticonfilebaseframes)
 contains zero elements.

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `path` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`System.IO.Path.GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx)
.

* [`System.IO.PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx) -  The specified path, filename, or both contain the system-defined maximum length.

* [`System.IO.DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx) -  The specified path is invalid.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFileBase.Dispose()`

 Immediately releases all resources used by the current instance.

--------------------------------------------------
## Method: `IconFileBase.Dispose(System.Boolean)`

 Releases all unmanaged resources used by the current instance, and optionally releases all managed resources.
* `disposing`: `true` to release both managed and unmanaged resources; `false` to release only unmanaged resources.

--------------------------------------------------
## Property: `IconFileBase.ID`

 When overridden in a derived class, gets the 16-bit identifier for the file type.

--------------------------------------------------
## Property: `IconFileBase.Frames`

 Gets a collection containing all frames in the icon file. 

--------------------------------------------------
# Type: `UIconEdit.IconFileBase.FrameList`

 Represents a hash list of frames. This collection treats [`UIconEdit.IconFrame`](#type-uiconediticonframe)
 objects with the same[`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as though they were equal.

--------------------------------------------------
## Method: `IconFileBase.FrameList.Add(UIconEdit.IconFrame)`

 Adds the specified icon frame to the list.
* `item`: The icon frame to add to the list.

**Returns:** `true` if `item` was successfully added; `false` if `item` is `null`,  is already associated with a different icon file, [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
 is equal to [`System.UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx)
, or if an element with the same[`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 already exists in the list.


--------------------------------------------------
## Method: `IconFileBase.FrameList.Insert(System.Int32, UIconEdit.IconFrame)`

 Adds the specified icon frame to the list at the specified index.
* `index`: The index at which to insert the icon frame.
* `item`: The icon frame to add to the list.

**Returns:** `true` if `item` was successfully added; `false` if `item` is `null`,  is already associated with a different icon file, [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
 is equal to [`System.UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx)
, or if an element with the same[`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 already exists in the list.


### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `index` is less than 0 or is greater than [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
.

--------------------------------------------------
## Method: `IconFileBase.FrameList.SetValue(System.Int32, UIconEdit.IconFrame)`

 Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The item to set at the specified index.

**Returns:** `true` if `item` was successfully set; `false` if `item` is `null`,  is already associated with a different icon file, or if an element with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
,  and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 already exists at a different index.


--------------------------------------------------
## Method: `IconFileBase.FrameList.RemoveAt(System.Int32)`

 Removes the element at the specified index.
* `index`: The element at the specified index.

### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `index` is less than 0 or is greater than or equal to [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
.

--------------------------------------------------
## Method: `IconFileBase.FrameList.RemoveAndDisposeAt(System.Int32)`

 Removes the element at the specified index and immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `index`: The element at the specified index.

### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `index` is less than 0 or is greater than or equal to [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
.

--------------------------------------------------
## Method: `IconFileBase.FrameList.Remove(UIconEdit.IconFrame)`

 Removes the specified icon frame from the list.
* `item`: The icon frame to remove from the list.

**Returns:** `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `IconFileBase.FrameList.RemoveAndDispose(UIconEdit.IconFrame)`

 Removes the specified icon frame from the list and immediately callse [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `item`: The icon frame to remove from the list.

**Returns:** `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `IconFileBase.FrameList.RemoveSimilar(UIconEdit.IconFrame)`

 Removes an icon frame similar to the specified value from the list.
* `item`: The icon frame compare.

**Returns:** `true` if an icon frame with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `IconFileBase.FrameList.RemoveAndDisposeSimilar(UIconEdit.IconFrame)`

 Removes an icon frame similar to the specified value from the list  and immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `item`: The icon frame to search for.

**Returns:** `true` if an icon frame with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `IconFileBase.FrameList.Clear()`

 Removes all elements from the list.

--------------------------------------------------
## Method: `IconFileBase.FrameList.ClearAndDispose()`

 Removes all elements from the list and immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
 on each one.

--------------------------------------------------
## Method: `IconFileBase.FrameList.Contains(UIconEdit.IconFrame)`

 Determines if the specified element exists in the list.
* `item`: The icon frame to search for in the list.

**Returns:** `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `IconFileBase.FrameList.ContainsSimilar(UIconEdit.IconFrame)`

 Determines if an element similar to the specified icon frame exists in the list.
* `item`: The icon frame to compare.

**Returns:** `true` if an icon frame with the same with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `IconFileBase.FrameList.IndexOf(UIconEdit.IconFrame)`

 Gets the index of the specified item.
* `item`: The icon frame to search for in the list.

**Returns:** The index of `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `IconFileBase.FrameList.IndexOfSimilar(UIconEdit.IconFrame)`

 Gets the index of an element similar to the specified item.
* `item`: The icon frame to compare.

**Returns:** The index of an icon frame with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `IconFileBase.FrameList.CopyTo(UIconEdit.IconFrame[])`

 Copies all elements in the list to the specified array.
* `array`: The array to which all elements in the list will be copied.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `array` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The length of `array` is less than [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
.

--------------------------------------------------
## Method: `IconFileBase.FrameList.CopyTo(UIconEdit.IconFrame[], System.Int32)`

 Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `array` is `null`.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `arrayIndex` is less than 0.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The length of `array` minus `arrayIndex` is less than [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
.

--------------------------------------------------
## Method: `IconFileBase.FrameList.CopyTo(System.Int32, UIconEdit.IconFrame[], System.Int32, System.Int32)`

 Copies a range of elements in the list to the specified array.
* `index`: The index of the first item to copy.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.
* `count`: The number of elements to copy.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `array` is `null`.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `index`, `arrayIndex`, or `count` is less than 0.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - 
`index` and `count` do not indicate a valid range of elements in the current instance.

-OR-

`arrayIndex` and `count` do not indicate a valid range of elements in `array`.


--------------------------------------------------
## Method: `IconFileBase.FrameList.GetEnumerator()`

 Returns an enumerator which iterates through the list.

**Returns:** An enumerator which iterates through the list.


--------------------------------------------------
## Method: `IconFileBase.FrameList.ToArray()`

 Returns an array containing all elements in the current list.

**Returns:** An array containing elements copied from the current list.


--------------------------------------------------
## Method: `IconFileBase.FrameList.RemoveWhere(System.Predicate<UIconEdit.IconFrame>)`

 Removes all elements matching the specified predicate.
* `match`: A predicate used to define the elements to remove.

**Returns:** The number of elements which were removed.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `match` is `null`.

--------------------------------------------------
## Method: `IconFileBase.FrameList.RemoveAndDisposeWhere(System.Predicate<UIconEdit.IconFrame>)`

 Removes all elements matching the specified predicate and immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `match`: A predicate used to define the elements to remove.

**Returns:** The number of elements which were removed.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `match` is `null`.

--------------------------------------------------
## Method: `IconFileBase.FrameList.Sort()`

 Sorts all elements in the list. Items are sorted by [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 (highest bit-depth to lowest),[`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
 (largest to smallest), and [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
 (largest to smallest).

--------------------------------------------------
## Method: `IconFileBase.FrameList.Sort(System.Collections.Generic.IComparer<UIconEdit.IconFrame>)`

 Sorts all elements in the list according to the specified comparer.
* `comparer`: The comparer used to compare each [`UIconEdit.IconFrame`](#type-uiconediticonframe)
, or `null` to follow the rules of [`UIconEdit.IconFileBase.FrameList.Sort`](#method-uiconediticonfilebaseframelistsort)
.

--------------------------------------------------
## Method: `IconFileBase.FrameList.Sort(System.Comparison<UIconEdit.IconFrame>)`

 Sorts all elements in the list according to the specified delegate.
* `comparison`: The delegate used to compare each [`UIconEdit.IconFrame`](#type-uiconediticonframe)
.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `comparison` is `null`.

--------------------------------------------------
## Property: `IconFileBase.FrameList.Item(System.Int32)`

 Gets and sets the element at the specified index.
* `index`: The index of the element to get or set.

### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - 
`index` is less than 0 or is greater than or equal to [`UIconEdit.IconFileBase.FrameList.Count`](#property-uiconediticonfilebaseframelistcount)
.

-OR-

In a set operation, the specified value is `null`.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) -  In a set operation, an element with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 already exists in the list at a different index, or the specified value is already associated with a different icon file.

--------------------------------------------------
## Property: `IconFileBase.FrameList.Count`

 Gets the number of elements in the list.

--------------------------------------------------
# Type: `UIconEdit.IconFileBase.FrameList.Enumerator`

 An enumerator which iterates through the list.

--------------------------------------------------
## Method: `IconFileBase.FrameList.Enumerator.Dispose()`

 Disposes of the current instance.

--------------------------------------------------
## Method: `IconFileBase.FrameList.Enumerator.MoveNext()`

 Advances the enumerator to the next position in the list.

**Returns:** `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
## Property: `IconFileBase.FrameList.Enumerator.Current`

 Gets the element at the current position in the enumerator.

--------------------------------------------------
# Type: `UIconEdit.IconTypeCode`

 The type code for an icon file.

--------------------------------------------------
## Field: `IconTypeCode.Icon`

 Indicates an icon (.ICO) file.

--------------------------------------------------
## Field: `IconTypeCode.Cursor`

 Indicates a cursor (.CUR) file.

--------------------------------------------------
# Type: `UIconEdit.IconLoadException`

 The exception that is thrown when an icon file contains invalid data.

--------------------------------------------------
## Constructor: `IconLoadException(System.String, UIconEdit.IconErrorCode, System.Object, System.Int32)`

 Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

--------------------------------------------------
## Constructor: `IconLoadException(System.String, UIconEdit.IconErrorCode, System.Object)`

 Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `IconLoadException(System.String, UIconEdit.IconErrorCode, System.Int32)`

 Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

--------------------------------------------------
## Constructor: `IconLoadException(UIconEdit.IconErrorCode, System.Object, System.Int32)`

 Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

--------------------------------------------------
## Constructor: `IconLoadException(UIconEdit.IconErrorCode, System.Int32)`

 Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

--------------------------------------------------
## Constructor: `IconLoadException(UIconEdit.IconErrorCode, System.Object)`

 Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `IconLoadException(UIconEdit.IconErrorCode)`

 Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.

--------------------------------------------------
## Constructor: `IconLoadException(System.String, UIconEdit.IconErrorCode, System.Int32, System.Exception)`

 Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter  is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.

--------------------------------------------------
## Constructor: `IconLoadException(System.String, System.Exception)`

 Creates a new instance with the specified message and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter  is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.

--------------------------------------------------
## Property: `IconLoadException.Message`

 Gets a message describing the error.

--------------------------------------------------
## Property: `IconLoadException.Code`

 Gets an error code describing the icon error.

--------------------------------------------------
## Property: `IconLoadException.Index`

 Gets the index in the icon file of the icon frame which caused this exception,  or -1 if it occurred before the icon frames were processed.

--------------------------------------------------
## Property: `IconLoadException.Value`

 Gets an object whose value caused the error, or `null` if there was no such value.

--------------------------------------------------
# Type: `UIconEdit.IconErrorCode`

 Indicates the cause of an [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
.

--------------------------------------------------
## Field: `IconErrorCode.Unknown`

 Code 0: the cause of the error is unknown.

--------------------------------------------------
## Field: `IconErrorCode.InvalidFormat`

 Code 0x1: The file is not a valid cursor or icon format.  This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.ZeroFrames`

 Code 0x2: The icon contains zero frames.  This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.ResourceTooSmall`

 Code 0x3: One of the icon directory entries has a length less than or equal to 40 bytes, which is logically too small for either a BMP or a PNG file.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains the length.  This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.ResourceTooEarly`

 Code 0x4: One of the icon directory entries has a starting offset which would overlap with the list of entries.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains the offset.  This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.ResourceOverlap`

 Code 0x5: One or more of the icon directory entries have overlapping offset/length combinations.  This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.InvalidFrameType`

 Code 0x1000: the file type of a frame is invalid.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBitDepth`

 Code 0x1001: the file is an icon, and an icon directory entry has a bit depth with any value other than 0, 1, 4, 8, 24, or 32.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains the bit depth.

--------------------------------------------------
## Field: `IconErrorCode.ZeroValidFrames`

 There are no remaining valid frames after processing.  This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.InvalidPngFile`

 Code 0x1100: an error occurred when attempting to load a PNG frame. The inner exception may contain more information.

--------------------------------------------------
## Field: `IconErrorCode.InvalidPngSize`

 Code 0x1102: the width or height of a PNG frame is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains the [`System.Drawing.Image.Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx)
 of the image.

--------------------------------------------------
## Field: `IconErrorCode.PngSizeMismatch`

 Code 0x1105: the width or height of a PNG frame does not match the width or height listed in the icon directory entry.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpFile`

 Code 0x1204: an error occurred when attempting to process a BMP frame. The inner exception may contain more information.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains a [`System.Tuple`2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.aspx)
 in which the [`System.Tuple`2.Item1`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item1.aspx)
 is the [`System.Drawing.Image.Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx)
 listed in the icon directory entry, and [`System.Tuple`2.Item2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item2.aspx)
 is the actual size.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpBitDepth`

 Code 0x1201 the bit depth of a BMP frame is not supported.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains the bit depth.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpSize`

 Code 0x1202: the width or height of a BMP frame is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.  The maximum height is doubled in images with a bit depth less than 32.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains the [`System.Drawing.Image.Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx)
 of the image.

--------------------------------------------------
## Field: `IconErrorCode.BmpHeightMismatch`

 Code 0x1203: the width or height of a BMP frame does not match the width or height listed in the icon directory entry.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains a [`System.Tuple`2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.aspx)
 in which the [`System.Tuple`2.Item1`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item1.aspx)
 is the [`System.Drawing.Image.Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx)
 listed in the icon directory entry, and [`System.Tuple`2.Item2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item2.aspx)
 is the actual size.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpHeightOdd`

 Code 0x1204: the height of a BMP frame is an odd number, indicating that there is no AND (transparency) mask.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains the [`System.Drawing.Image.Height`](https://msdn.microsoft.com/en-us/library/system.drawing.image.height.aspx)
 of the image.

--------------------------------------------------
## Field: `IconErrorCode.BmpBitDepthMismatch`

 Code 0x1205: there is a mismatch between the bit depth of a BMP frame and the expected bit depth of the file.[`UIconEdit.IconLoadException.Value`](#property-uiconediticonloadexceptionvalue)
 contains a [`System.Tuple`2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.aspx)
 in which the [`System.Tuple`2.Item1`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item1.aspx)
 is the bit depth  listed in the icon directory entry, and [`System.Tuple`2.Item2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item2.aspx)
 is the bit depth listed in the BMP frame.

--------------------------------------------------
# Type: `UIconEdit.IconLoadExceptionHandler`

 A delegate function for handling [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
 errors.
* `e`: An [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
 containing information about the error.

--------------------------------------------------
# Type: `UIconEdit.IconFrame`

 Represents a single frame in an icon.

--------------------------------------------------
## Field: `IconFrame.MinDimension`

 The minimum dimensions of an icon. 1 pixel in size.

--------------------------------------------------
## Field: `IconFrame.MaxDimension`

 The maximum dimensions of an icon. 768 as of Windows 10.

--------------------------------------------------
## Constructor: `IconFrame(System.Drawing.Image, System.Int16, System.Int16, UIconEdit.BitDepth, System.Byte)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent.  If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `width` or `height` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `IconFrame(System.Drawing.Image, System.Int16, System.Int16, UIconEdit.BitDepth)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `width` or `height` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `IconFrame(System.Drawing.Image, UIconEdit.BitDepth, System.Byte)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent.  If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The width or height of `baseImage` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `IconFrame(System.Drawing.Image, UIconEdit.BitDepth)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The width or height of `baseImage` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Method: `IconFrame.Clone()`

 Returns a duplicate of the current instance.

**Returns:** A duplicate of the current instance, with its own clone of [`UIconEdit.IconFrame.BaseImage`](#property-uiconediticonframebaseimage)
.


--------------------------------------------------
## Method: `IconFrame.Dispose()`

 Releases all resources used by the current instance.

--------------------------------------------------
## Method: `IconFrame.Dispose(System.Boolean)`

 Releases all unmanaged resources used by the current instance, and optionally releases all managed resources.
* `disposing`: `true` to release both managed and unmanaged resources; `false` to release only unmanaged resources.

--------------------------------------------------
## Property: `IconFrame.BaseImage`

 Gets and sets the image associated with the current instance.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) -  In a set operation, the specified value is `null`.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) -  In a set operation, the specified value is disposed.

--------------------------------------------------
## Property: `IconFrame.Width`

 Gets and sets the resampled width of the icon.

### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) -  In a set operation, the specified value is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

--------------------------------------------------
## Property: `IconFrame.Height`

 Gets and sets the resampled height of the icon.

### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) -  In a set operation, the specified value is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

--------------------------------------------------
## Property: `IconFrame.BitDepth`

 Gets and sets the bit depth of the current instance. This property is ignored if the width or height of the image is greater than 255.

### Exceptions

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) -  In a set operation, the specified value is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

--------------------------------------------------
## Property: `IconFrame.PixelFormat`

 Gets the pixel format of the resulting image file.

--------------------------------------------------
## Property: `IconFrame.AlphaThreshold`

 Gets and sets a value indicating the threshold of alpha values at [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
s below [`UIconEdit.BitDepth.Bit32`](#field-uiconeditbitdepthbit32)
.  Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.

--------------------------------------------------
## Property: `IconFrame.DrawInterpolationMode`

 Gets and sets the interpolation mode used by graphics objects when scaling.

### Exceptions

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) -  In a set operation, the specified value is not a valid [`System.Drawing.Drawing2D.InterpolationMode`](https://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.interpolationmode.aspx)
 value.

--------------------------------------------------
## Property: `IconFrame.DrawPixelOffsetMode`

 Gets and sets the pixel offset mode used by graphics objects when rescaling the image.

### Exceptions

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) -  In a set operation, the specified value is not a valid [`System.Drawing.Drawing2D.PixelOffsetMode`](https://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.pixeloffsetmode.aspx)
 value.

--------------------------------------------------
# Type: `UIconEdit.CursorFrame`

 Represents a single frame of a cursor.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, System.Int16, System.Int16, UIconEdit.BitDepth, System.UInt16, System.UInt16, System.Byte)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent.  If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `width` or `height` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, System.Int16, System.Int16, UIconEdit.BitDepth, System.UInt16, System.UInt16)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `width` or `height` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, System.Int16, System.Int16, UIconEdit.BitDepth, System.Byte)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent.  If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `width` or `height` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, System.Int16, System.Int16, UIconEdit.BitDepth)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `width` or `height` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, UIconEdit.BitDepth, System.UInt16, System.UInt16, System.Byte)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent.  If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The width or height of `baseImage` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, UIconEdit.BitDepth, System.UInt16, System.UInt16)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The width or height of `baseImage` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, UIconEdit.BitDepth, System.Byte)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent.  If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The width or height of `baseImage` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Constructor: `CursorFrame(System.Drawing.Image, UIconEdit.BitDepth)`

 Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `baseImage` is `null`.

* [`System.ComponentModel.InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx) - `bitDepth` is not a valid [`UIconEdit.BitDepth`](#type-uiconeditbitdepth)
 value.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The width or height of `baseImage` is less than [`UIconEdit.IconFrame.MinDimension`](#field-uiconediticonframemindimension)
 or is greater than [`UIconEdit.IconFrame.MaxDimension`](#field-uiconediticonframemaxdimension)
.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `baseImage` is disposed.

--------------------------------------------------
## Property: `CursorFrame.HotspotX`

 Gets and sets the horizontal offset of the cursor's hotspot from the left of the cursor in pixels.

--------------------------------------------------
## Property: `CursorFrame.HotspotY`

 Gets and sets the vertical offset of the cursor's hotspot from the top of the cursor in pixels.

--------------------------------------------------
## Property: `CursorFrame.Hotspot`

 Gets the offset of the cursor's hotspot from the upper-left corner of the cursor in pixels.

--------------------------------------------------
# Type: `UIconEdit.BitDepth`

 Indicates the bit depth of an icon frame.

--------------------------------------------------
## Field: `BitDepth.Bit32`

 Indicates that the frame is full color with alpha (32 bits per pixel).

--------------------------------------------------
## Field: `BitDepth.Bit24`

 Indicates that the frame is full color without alpha (24 bits per pixel plus 1 bit per pixel alpha mask).

--------------------------------------------------
## Field: `BitDepth.Color256`

 Indicates that the frame is 256-color (8 bits per pixel plus 1 bit per pixel alpha mask)

--------------------------------------------------
## Field: `BitDepth.Color16`

 Indicates that the frame is 16-color (4 bits per pixel plus 1 bit per pixel alpha mask).

--------------------------------------------------
## Field: `BitDepth.Color2`

 Indicates that the frame is 2-color (1 bit per pixel plus 1 bit per pixel alpha mask).

--------------------------------------------------
# Type: `UIconEdit.IconFile`

 Represents an icon file.

--------------------------------------------------
## Constructor: `IconFile()`

 Creates a new [`UIconEdit.IconFile`](#type-uiconediticonfile)
 instance.

--------------------------------------------------
## Method: `IconFile.Load(System.IO.Stream)`

 Loads a [`UIconEdit.IconFile`](#type-uiconediticonfile)
 from the specified stream.
* `input`: A stream containing an icon file.

**Returns:** A [`UIconEdit.IconFile`](#type-uiconediticonfile)
 loaded from `input`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `input` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `input` is closed or does not support reading.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `input` is closed.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFile.Load(System.IO.Stream, System.Action<UIconEdit.IconLoadException>)`

 Loads a [`UIconEdit.IconFile`](#type-uiconediticonfile)
 from the specified stream.
* `input`: A stream containing an icon file.
* `handler`: A delegate used to process [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
s thrown when processing individual icon frames,  or `null` to throw an exception in those cases.

**Returns:** A [`UIconEdit.IconFile`](#type-uiconediticonfile)
 loaded from `input`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `input` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `input` is closed or does not support reading.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `input` is closed.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFile.Load(System.String, System.Action<UIconEdit.IconLoadException>)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified path.
* `path`: The path to a icon file.
* `handler`: A delegate used to process [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
s thrown when processing individual icon frames,  or `null` to throw an exception in those cases.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `path`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `path` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`System.IO.Path.GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx)
.

* [`System.IO.PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx) -  The specified path, filename, or both contain the system-defined maximum length.

* [`System.IO.FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx) -  The specified path was not found.

* [`System.IO.DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx) -  The specified path was invalid.

* [`System.UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx) - 
`path` specified a directory.

-OR-

The caller does not have the required permission.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) - `path` is in an invalid format.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFile.Load(System.String)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified path.
* `path`: The path to a icon file.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `path`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `path` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`System.IO.Path.GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx)
.

* [`System.IO.PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx) -  The specified path, filename, or both contain the system-defined maximum length.

* [`System.IO.FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx) -  The specified path was not found.

* [`System.IO.DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx) -  The specified path was invalid.

* [`System.UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx) - 
`path` specified a directory.

-OR-

The caller does not have the required permission.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) - `path` is in an invalid format.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the icon file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `IconFile.GetIcon(UIconEdit.IconFrame)`

 Gets an [`System.Drawing.Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx)
 from a single frame.
* `frame`: The icon frame from which to get an [`System.Drawing.Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx)
.

**Returns:** An [`System.Drawing.Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx)
 created from `frame`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `frame` is `null`.

--------------------------------------------------
## Method: `IconFile.GetImgX(UIconEdit.IconFrame)`

 Returns the color panes.
* `frame`: This parameter is ignored.

--------------------------------------------------
## Method: `IconFile.GetImgY(UIconEdit.IconFrame)`

 Returns the number of bits per pixel in the specified frame.
* `frame`: The frame for which to get the bits-per-pixel.

**Returns:** The number of bits per pixel in `frame`.


--------------------------------------------------
## Property: `IconFile.ID`

 Gets the 16-bit type code for the current instance.

--------------------------------------------------
# Type: `UIconEdit.CursorFile`

 Represents a cursor file.

--------------------------------------------------
## Constructor: `CursorFile()`

 Creates a new [`UIconEdit.CursorFile`](#type-uiconeditcursorfile)
 instance.

--------------------------------------------------
## Method: `CursorFile.Load(System.IO.Stream)`

 Loads a [`UIconEdit.CursorFile`](#type-uiconeditcursorfile)
 from the specified stream.
* `input`: A stream containing an cursor file.

**Returns:** A [`UIconEdit.CursorFile`](#type-uiconeditcursorfile)
 loaded from `input`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `input` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `input` is closed or does not support reading.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `input` is closed.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the cursor file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `CursorFile.Load(System.IO.Stream, System.Action<UIconEdit.IconLoadException>)`

 Loads a [`UIconEdit.CursorFile`](#type-uiconeditcursorfile)
 from the specified stream.
* `input`: A stream containing an cursor file.
* `handler`: A delegate used to process [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
s thrown when processing individual icon frames,  or `null` to throw an exception in those cases.

**Returns:** A [`UIconEdit.CursorFile`](#type-uiconeditcursorfile)
 loaded from `input`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `input` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `input` is closed or does not support reading.

* [`System.ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx) - `input` is closed.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the cursor file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `CursorFile.Load(System.String, System.Action<UIconEdit.IconLoadException>)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified path.
* `path`: The path to a cursor file.
* `handler`: A delegate used to process [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception)
s thrown when processing individual icon frames,  or `null` to throw an exception in those cases.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `path`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `path` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`System.IO.Path.GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx)
.

* [`System.IO.PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx) -  The specified path, filename, or both contain the system-defined maximum length.

* [`System.IO.FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx) -  The specified path was not found.

* [`System.IO.DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx) -  The specified path was invalid.

* [`System.UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx) - 
`path` specified a directory.

-OR-

The caller does not have the required permission.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) - `path` is in an invalid format.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the cursor file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `CursorFile.Load(System.String)`

 Loads an [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation from the specified path.
* `path`: The path to a cursor file.

**Returns:** An [`UIconEdit.IconFileBase`](#type-uiconediticonfilebase)
 implementation loaded from `path`.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `path` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - `path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`System.IO.Path.GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx)
.

* [`System.IO.PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx) -  The specified path, filename, or both contain the system-defined maximum length.

* [`System.IO.FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx) -  The specified path was not found.

* [`System.IO.DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx) -  The specified path was invalid.

* [`System.UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx) - 
`path` specified a directory.

-OR-

The caller does not have the required permission.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) - `path` is in an invalid format.

* [`UIconEdit.IconLoadException`](#type-uiconediticonloadexception) -  An error occurred when processing the cursor file's format.

* [`System.IO.IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx) -  An I/O error occurred.

--------------------------------------------------
## Method: `CursorFile.IsValid(UIconEdit.IconFrame)`

 Gets a valid indicating whether the specified instance is a valid [`UIconEdit.CursorFrame`](#type-uiconeditcursorframe)
 object.
* `frame`: The icon frame to test.

**Returns:** `true` if `frame` is a [`UIconEdit.CursorFrame`](#type-uiconeditcursorframe)
 instance; `false` otherwise.


--------------------------------------------------
## Method: `CursorFile.Clone()`

 Returns a duplicate of the current instance.

**Returns:** A duplicate of the current instance, with copies of every icon frame and clones of each  frame's [`UIconEdit.IconFrame.BaseImage`](#property-uiconediticonframebaseimage)
 in [`UIconEdit.CursorFile.Frames`](#property-uiconeditcursorfileframes)
.


--------------------------------------------------
## Method: `CursorFile.GetImgX(UIconEdit.IconFrame)`

 Gets the horizontal offset of the hotspot in the specified frame from the left of the specified cursor frame.
* `frame`: The frame from which to get the horizontal offset.

**Returns:** The horizontal offset of the hotspot from the left in pixels.


--------------------------------------------------
## Method: `CursorFile.GetImgY(UIconEdit.IconFrame)`

 Gets the vertical offset of the hotspot in the specified frame from the top of the specified cursor frame.
* `frame`: The frame from which to get the horizontal offset.

**Returns:** The vertical offset of the hotspot from the top in pixels.


--------------------------------------------------
## Property: `CursorFile.ID`

 Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Property: `CursorFile.Frames`

 Gets a collection containing all frames in the cursor file.

--------------------------------------------------
# Type: `UIconEdit.CursorFile.CursorFrameList`

 Represents a list of cursor frames.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.SetValue(System.Int32, UIconEdit.CursorFrame)`

 Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The value to set.

**Returns:** `true` if `item` was successfully set; `false` if `item` is `null`,  is already associated with a different icon file, or if an element with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
,  and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 already exists in the list at a different index.


### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `index` is less than 0 or is greater than [`UIconEdit.CursorFile.CursorFrameList.Count`](#property-uiconeditcursorfilecursorframelistcount)
.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Add(UIconEdit.CursorFrame)`

 Adds the specified cursor frame to the list.
* `item`: The cursor frame to add to the list.

**Returns:** `true` if `item` was successfully added; `false` if `item` is `null`,  is already associated with a different icon file, [`UIconEdit.CursorFile.CursorFrameList.Count`](#property-uiconeditcursorfilecursorframelistcount)
 is equal to [`System.UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx)
, or if an element with the same[`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 already exists in the list.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Insert(System.Int32, UIconEdit.CursorFrame)`

 Inserts the specified cursor frame into the list at the specified index.
* `index`: The index at which the cursor frame will be inserted.
* `item`: The cursor frame to add to the list.

**Returns:** `true` if `item` was successfully added; `false` if `item` is `null`,  is already associated with a different icon file, [`UIconEdit.CursorFile.CursorFrameList.Count`](#property-uiconeditcursorfilecursorframelistcount)
 is equal to [`System.UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx)
, or if an element with the same[`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 already exists in the list.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.RemoveAt(System.Int32)`

 Removes the element at the specified index.
* `index`: The index of the icon frame to remove.

### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `index` is less than 0 or is greater than or equal to [`UIconEdit.CursorFile.CursorFrameList.Count`](#property-uiconeditcursorfilecursorframelistcount)
.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.RemoveAndDisposeAt(System.Int32)`

 Removes the element at the specified index and, if it does not exist elsewhere in the file, immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `index`: The index of the icon frame to remove.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Remove(UIconEdit.CursorFrame)`

 Removes the specified cursor frame from the list.
* `item`: The cursor frame to to remove from the list.

**Returns:** `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.RemoveAndDispose(UIconEdit.CursorFrame)`

 Removes the specified cursor frame from the list and immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `item`: The cursor frame to to remove from the list.

**Returns:** `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.RemoveSimilar(UIconEdit.CursorFrame)`

 Removes an element similar to the specified cursor frame from the list.
* `item`: The cursor frame to to compare.

**Returns:** `true` if an icon frame with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.RemoveAndDisposeSimilar(UIconEdit.CursorFrame)`

 Removes the specified cursor frame from the list, immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `item`: The cursor frame to compare.

**Returns:** `true` if an icon frame with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.CopyTo(UIconEdit.CursorFrame[])`

 Copies all elements in the list to the specified array.
* `array`: The array to which all elements in the list will be copied.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `array` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The length of `array` is less than [`UIconEdit.CursorFile.CursorFrameList.Count`](#property-uiconeditcursorfilecursorframelistcount)
.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.CopyTo(UIconEdit.CursorFrame[], System.Int32)`

 Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `array` is `null`.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `arrayIndex` is less than 0.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The length of `array` minus `arrayIndex` is less than [`UIconEdit.CursorFile.CursorFrameList.Count`](#property-uiconeditcursorfilecursorframelistcount)
.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.CopyTo(System.Int32, UIconEdit.CursorFrame[], System.Int32, System.Int32)`

 Copies a range of elements in the list to the specified array.
* `index`: The index of the first item to copy.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.
* `count`: The number of elements to copy.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `array` is `null`.

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - `index`, `arrayIndex`, or `count` is less than 0.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) - 
`index` and `count` do not indicate a valid range of elements in the current instance.

-OR-

`arrayIndex` and `count` do not indicate a valid range of elements in `array`.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Clear()`

 Removes all elements from the collection.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.ClearAndDispose()`

 Removes all elements from the collection and immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
 on each one.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Contains(UIconEdit.CursorFrame)`

 Determines if the specified cursor frame exists in the list.
* `item`: The cursor frame to search for in the list.

**Returns:** `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.ContainsSimilar(UIconEdit.CursorFrame)`

 Determines if an element similar to the specified cursor frame exists in the list.
* `item`: The cursor frame to compare.

**Returns:** `true` if a cursor frame with the same with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.IndexOf(UIconEdit.CursorFrame)`

 Gets the index of the specified cursor frame.
* `item`: The cursor frame to search for in the list.

**Returns:** `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.IndexOfSimilar(UIconEdit.CursorFrame)`

 Gets the index of an element similar to the specified cursor frame.
* `item`: The cursor frame to compare.

**Returns:** The index of a cursor frame with the same [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
, [`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
, and [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.GetEnumerator()`

 Returns an enumerator which iterates through the list.

**Returns:** An enumerator which iterates through the list.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.RemoveWhere(System.Predicate<UIconEdit.CursorFrame>)`

 Removes all elements matching the specified predicate.
* `match`: A predicate used to define the elements to remove.

**Returns:** The number of elements which were removed.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `match` is `null`.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.RemoveAndDisposeWhere(System.Predicate<UIconEdit.CursorFrame>)`

 Removes all elements matching the specified predicate and immediately calls [`UIconEdit.IconFrame.Dispose`](#method-uiconediticonframedispose)
.
* `match`: A predicate used to define the elements to remove.

**Returns:** The number of elements which were removed.


### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `match` is `null`.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.ToArray()`

 Returns an array containing all elements in the current list.

**Returns:** An array containing elements copied from the current list.


--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Sort()`

 Sorts all elements in the list. Items are sorted by [`UIconEdit.IconFrame.BitDepth`](#property-uiconediticonframebitdepth)
 (highest bit-depth to lowest),[`UIconEdit.IconFrame.Height`](#property-uiconediticonframeheight)
 (largest to smallest), and [`UIconEdit.IconFrame.Width`](#property-uiconediticonframewidth)
 (largest to smallest).

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Sort(System.Collections.Generic.IComparer<UIconEdit.CursorFrame>)`

 Sorts all elements in the list according to the specified comparer.
* `comparer`: The comparer used to compare each [`UIconEdit.CursorFrame`](#type-uiconeditcursorframe)
, or `null` to follow the rules of [`UIconEdit.CursorFile.CursorFrameList.Sort`](#method-uiconeditcursorfilecursorframelistsort)
.

### Exceptions

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The implementation of `comparer` caused an error during the sort. For example, `comparer` might not return 0  when comparing an item with itself.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Sort(System.Comparison<UIconEdit.CursorFrame>)`

 Sorts all elements in the list according to the specified delegate.
* `comparison`: The delegate used to compare each [`UIconEdit.IconFrame`](#type-uiconediticonframe)
.

### Exceptions

* [`System.ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx) - `comparison` is `null`.

* [`System.ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx) -  The implementation of `comparison` caused an error during the sort. For example, `comparison` might not return 0  when comparing an item with itself.

--------------------------------------------------
## Property: `CursorFile.CursorFrameList.Count`

 Gets the number of frames contained in the list.

--------------------------------------------------
## Property: `CursorFile.CursorFrameList.Item(System.Int32)`

 Gets and sets the cursor frame at the specified index.
* `index`: The cursor frame at the specified index.

### Exceptions

* [`System.ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - 
`index` is less than 0 or is greater than or equal to [`UIconEdit.CursorFile.CursorFrameList.Count`](#property-uiconeditcursorfilecursorframelistcount)
.

-OR-

In a set operation, the specified value is `null`.


* [`System.NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx) -  In a set operation, the specified value is `null` or is already associated with a different cursor file.

--------------------------------------------------
# Type: `UIconEdit.CursorFile.CursorFrameList.Enumerator`

 An enumerator which iterates through the list.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Enumerator.Dispose()`

 Disposes of the current instance.

--------------------------------------------------
## Method: `CursorFile.CursorFrameList.Enumerator.MoveNext()`

 Advances the enumerator to the next position in the list.

**Returns:** `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
## Property: `CursorFile.CursorFrameList.Enumerator.Current`

 Gets the element at the current position in the enumerator.