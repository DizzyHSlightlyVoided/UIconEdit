# Type: `public abstract class UIconDrawing.IconFileBase`

Base class for icon and cursor files.

--------------------------------------------------
## Constructor: `public IconFileBase()`

Initializes a new instance.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFileBase Load(System.IO.Stream input)`

Loads an [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation from the specified stream.
* `input`: A stream containing an icon or cursor file.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFileBase Load(System.IO.Stream input, UIconDrawing.IconLoadExceptionHandler handler)`

Loads an [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation from the specified stream.
* `input`: A stream containing an icon or cursor file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFileBase Load(System.String path, UIconDrawing.IconLoadExceptionHandler handler)`

Loads an [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation from the specified path.
* `path`: The path to a cursor or icon file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFileBase Load(System.String path)`

Loads an [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation from the specified path.
* `path`: The path to a cursor or icon file.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public abstract UIconDrawing.IconFileBase Clone()`

When overridden in a derived class, returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase): A duplicate of the current instance, with copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uicondrawingiconfilebaseentrylist-entries--get-).


--------------------------------------------------
## Method: `public override UIconDrawing.IconFile CloneAsIconFile()`

Returns a duplicate of the current instance as an [`IconFile`](#type-public-class-uicondrawingiconfile)

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): An [`IconFile`](#type-public-class-uicondrawingiconfile) containing copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uicondrawingiconfilebaseentrylist-entries--get-), and with each entry's [`IconEntry.HotspotX`](#property-public-systemint32-hotspotx--get-set-) and [`IconEntry.HotspotY`](#property-public-systemint32-hotspoty--get-set-) values set to 0.


--------------------------------------------------
## Method: `public override UIconDrawing.CursorFile CloneAsCursorFile()`

Returns a duplicate of the current instance as an [`IconFile`](#type-public-class-uicondrawingiconfile)

**Returns:** Type [`CursorFile`](#type-public-class-uicondrawingcursorfile): An [`IconFile`](#type-public-class-uicondrawingiconfile) containing copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uicondrawingiconfilebaseentrylist-entries--get-).


--------------------------------------------------
## Property: `public abstract UIconDrawing.IconTypeCode ID { get; }`

When overridden in a derived class, gets the 16-bit identifier for the file type.

--------------------------------------------------
## Property: `public UIconDrawing.IconFileBase.EntryList Entries { get; }`

Gets a collection containing all entries in the icon file.

--------------------------------------------------
## Method: `public void Save(System.IO.Stream output)`

Saves the file to the specified stream.
* `output`: The stream to which the file will be written.

### Exceptions

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)
[`IconFileBase.Entries`](#property-public-uicondrawingiconfilebaseentrylist-entries--get-) contains zero elements.

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`output` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`output` is closed or does not support writing.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)

The current instance is disposed.

-OR-

`output` is closed.


##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public void Save(System.String path)`

Saves the file to the specified file.
* `path`: The file to which the file will be written.

### Exceptions

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)
[`IconFileBase.Entries`](#property-public-uicondrawingiconfilebaseentrylist-entries--get-) contains zero elements.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path is invalid.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public System.Boolean IsDisposed { get; }`

Gets a value indicating whether the current instance has been disposed.

--------------------------------------------------
## Method: `public override void Dispose()`

Immediately releases all resources used by the current instance.

--------------------------------------------------
## Disposed`

Raised when the current instance is disposed.

--------------------------------------------------
# Type: `class UIconDrawing.IconFileBase.EntryList`

Represents a list of icon entries. Entries with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) cannot be added to the list; however, there may be duplicates if an icon loaded from an external icon file contained them.

--------------------------------------------------
## CollectionChanged`

Raised when elements are added to or removed from the list.

--------------------------------------------------
## PropertyChanged`

Raised when a property changes in the current instance.

--------------------------------------------------
## Property: `IconFileBase.EntryList.Item(System.Int32 index)`

Gets and sets the element at the specified index.
* `index`: The index of the element to get or set.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

`index` is less than 0 or is greater than or equal to [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

-OR-

In a set operation, the specified value is `null`.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
In a set operation, an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) already exists in the list at a different index, the specified value is already associated with a different icon file, or the specified value is disposed.

--------------------------------------------------
## Property: `public virtual System.Int32 Count { get; }`

Gets the number of elements in the list.

--------------------------------------------------
## Method: `public System.Boolean Add(UIconDrawing.IconEntry item)`

Adds the specified icon entry to the list.
* `item`: The icon entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is disposed, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get-) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), the current instance is disposed, or if an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) already exists in the list.


--------------------------------------------------
## Method: `public System.Boolean Insert(System.Int32 index, UIconDrawing.IconEntry item)`

Adds the specified icon entry to the list at the specified index.
* `index`: The index at which to insert the icon entry.
* `item`: The icon entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is disposed, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get-) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), the current instance is disposed, or if an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) already exists in the list.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public System.Boolean SetValue(System.Int32 index, UIconDrawing.IconEntry item)`

Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The item to set at the specified index.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully set; `false` if `item` is `null`, is disposed, is already associated with a different icon file, an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) already exists at a different index, or if the current is disposed.


--------------------------------------------------
## Method: `public void RemoveAt(System.Int32 index)`

Removes the element at the specified index.
* `index`: The element at the specified index.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public System.Boolean Remove(UIconDrawing.IconEntry item)`

Removes the specified icon entry from the list.
* `item`: The icon entry to remove from the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconDrawing.IconEntry item)`

Removes an icon entry similar to the specified value from the list.
* `item`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `item` was successfully found and removed; `false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconDrawing.IconEntryKey key)`

Removes an icon entry similar to the specified value from the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `key` was successfully found and removed; `false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Removes an icon entry similar to the specified values from the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `bitDepth` was successfully found and removed;`false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public void Clear()`

Removes all elements from the list.

--------------------------------------------------
## Method: `public System.Boolean Contains(UIconDrawing.IconEntry item)`

Determines if the specified element exists in the list.
* `item`: The icon entry to search for in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconDrawing.IconEntry item)`

Determines if an element similar to the specified icon entry exists in the list.
* `item`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconDrawing.IconEntryKey key)`

Determines if an element similar to the specified value exists in the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `key` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Determines if an element similar to the specified values exists in the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `bitDepth` was found;`false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Int32 IndexOf(UIconDrawing.IconEntry item)`

Gets the index of the specified item.
* `item`: The icon entry to search for in the list.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconDrawing.IconEntry item)`

Gets the index of an element similar to the specified item.
* `item`: The icon entry to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconDrawing.IconEntryKey key)`

Gets the index of an element similar to the specified value.
* `key`: The entry key to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `key`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Gets the index of an element similar to the specified values.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `bitDepth`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public void CopyTo(UIconDrawing.IconEntry[] array, System.Int32 index)`

Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `index`: The index in `array` at which copying begins.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` minus `index` is less than [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public System.Int32 BinarySearch(UIconDrawing.IconEntry entry)`

Performs a binary search for the specified entry within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `entry`: The icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `entry`, if found; otherwise, the bitwise complement of the index where `entry` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearch(System.Int32 index, System.Int32 count, UIconDrawing.IconEntry entry)`

Performs a binary search for the specified entry within the specified range of elements. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `index`: The index in the list at which the search begins.
* `count`: The number of elements to search.
* `entry`: The icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `entry`, if found; otherwise, the bitwise complement of the index where `entry` would be.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(UIconDrawing.IconEntry entry)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as the specified entry within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `entry`: The icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `entry`, if found; otherwise, the bitwise complement of the index where `entry` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(System.Int32 index, System.Int32 count, UIconDrawing.IconEntry entry)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as the specified entry within the specified range of elements. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `index`: The index in the list at which the search begins.
* `count`: The number of elements to search.
* `entry`: The icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `entry`, if found; otherwise, the bitwise complement of the index where `entry` would be.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(UIconDrawing.IconEntryKey key)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as the specified key within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `key`: The entry key to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `key`, if found; otherwise, the bitwise complement of the index where `key` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(System.Int32 index, System.Int32 count, UIconDrawing.IconEntryKey key)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as the specified key within the specified range of elements. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `index`: The index in the list at which the search begins.
* `count`: The number of elements to search.
* `key`: The entry key to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `key`, if found; otherwise, the bitwise complement of the index where `key` would be.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as the specified key within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `width`, `height`, and `bitDepth`, if found; otherwise, the bitwise complement of the index of where `width`, `height`, and `bitDepth` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(System.Int32 index, System.Int32 count, System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as the specified key within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
* `index`: The index in the list at which the search begins.
* `count`: The number of elements to search.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `width`, `height`, and `bitDepth`, if found; otherwise, the bitwise complement of the index of where `width`, `height`, and `bitDepth` would be.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public void Sort()`

Sorts all elements in the list according to their [`IconEntry.EntryKey`](#property-public-uicondrawingiconentrykey-entrykey--get-) value.
### Remarks

This method raises the [`EntryList.CollectionChanged`](#collectionchanged) event using [`NotifyCollectionChangedAction.Reset`](https://msdn.microsoft.com/en-us/library/system.collections.specialized.notifycollectionchangedaction.reset.aspx) if the list contains more than 2 elements.

--------------------------------------------------
## Method: `public UIconDrawing.IconFileBase.EntryList.Enumerator GetEnumerator()`

Returns an enumerator which iterates through the list.

**Returns:** Type [`Enumerator`](#type-struct-uicondrawingiconfilebaseentrylistenumerator): An enumerator which iterates through the list.


--------------------------------------------------
## Method: `public void Move(System.Int32 oldIndex, System.Int32 newIndex)`

Moves an element from one index to another.
* `oldIndex`: The index of the element to move.
* `newIndex`: The destination index.

--------------------------------------------------
## Method: `public UIconDrawing.IconEntry[] ToArray()`

Returns an array containing all elements in the current list.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconEntry`](#type-public-class-uicondrawingiconentry): An array containing elements copied from the current list.


--------------------------------------------------
## Method: `public UIconDrawing.IconEntry Find(System.Predicate<UIconDrawing.IconEntry> match)`

Searches for an element which matches the specified predicate, and returns the first matching icon entry in the list.
* `match`: A predicate used to define the element to search for.

**Returns:** Type [`IconEntry`](#type-public-class-uicondrawingiconentry): An icon entry matching the specified predicate, or `null` if no such icon entry was found.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Int32 FindIndex(System.Predicate<UIconDrawing.IconEntry> match)`

Searches for an element which matches the specified predicate, and returns the index of the first matching icon entry in the list.
* `match`: A predicate used to define the element to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of the icon entry matching the specified predicate, or -1 if no such icon entry was found.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Boolean Exists(System.Predicate<UIconDrawing.IconEntry> match)`

Determines whether any element matching the specified predicate exists in the list.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if at least one element matches the specified predicate; `false` otherwise.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Boolean TrueForAll(System.Predicate<UIconDrawing.IconEntry> match)`

Determines whether every element in the list matches the specified predicate.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if every element in the list matches the specified predicate; `false` otherwise.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Collections.Generic.List<T> FindAll(System.Predicate<UIconDrawing.IconEntry> match)`

Returns a list containing all icon entries which match the specified predicate.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`List<T>`](https://msdn.microsoft.com/en-us/library/6sh2ey19.aspx): A list containing all elements matching `match`.


--------------------------------------------------
# Type: `struct UIconDrawing.IconFileBase.EntryList.Enumerator`

An enumerator which iterates through the list.

--------------------------------------------------
## Property: `public UIconDrawing.IconEntry Current { get; }`

Gets the element at the current position in the enumerator.

--------------------------------------------------
## Method: `public void Dispose()`

Disposes of the current instance.

--------------------------------------------------
## Method: `public System.Boolean MoveNext()`

Advances the enumerator to the next position in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
# Type: `public enum UIconDrawing.IconTypeCode`

The type code for an icon file.

--------------------------------------------------
## Field: `IconTypeCode.Unknown = 0`

Indicates an unknown or invalid file.

--------------------------------------------------
## Field: `IconTypeCode.Icon = 1`

Indicates an icon (.ICO) file.

--------------------------------------------------
## Field: `IconTypeCode.Cursor = 2`

Indicates a cursor (.CUR) file.

--------------------------------------------------
# Type: `public class UIconDrawing.CursorFile`

Represents a cursor file.

--------------------------------------------------
## Constructor: `public CursorFile()`

Creates a new [`CursorFile`](#type-public-class-uicondrawingcursorfile) instance.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile Load(System.IO.Stream input)`

Loads a [`CursorFile`](#type-public-class-uicondrawingcursorfile) from the specified stream.
* `input`: A stream containing an cursor file.

**Returns:** Type [`CursorFile`](#type-public-class-uicondrawingcursorfile): A [`CursorFile`](#type-public-class-uicondrawingcursorfile) loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile Load(System.IO.Stream input, UIconDrawing.IconLoadExceptionHandler handler)`

Loads a [`CursorFile`](#type-public-class-uicondrawingcursorfile) from the specified stream.
* `input`: A stream containing an cursor file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown when processing individual cursor entries, or `null` to throw an exception in those cases.

**Returns:** Type [`CursorFile`](#type-public-class-uicondrawingcursorfile): A [`CursorFile`](#type-public-class-uicondrawingcursorfile) loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the cursor file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile Load(System.String path, UIconDrawing.IconLoadExceptionHandler handler)`

Loads a [`CursorFile`](#type-public-class-uicondrawingcursorfile) from the specified path.
* `path`: The path to a cursor file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown when processing individual cursor entries, or `null` to throw an exception in those cases.

**Returns:** Type [`CursorFile`](#type-public-class-uicondrawingcursorfile): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the cursor file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile Load(System.String path)`

Loads a [`CursorFile`](#type-public-class-uicondrawingcursorfile) from the specified path.
* `path`: The path to a cursor file.

**Returns:** Type [`CursorFile`](#type-public-class-uicondrawingcursorfile): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the cursor file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public override UIconDrawing.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Method: `public override UIconDrawing.IconFileBase Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase): A duplicate of the current instance, with copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uicondrawingiconfilebaseentrylist-entries--get-).


--------------------------------------------------
# Type: `public class UIconDrawing.IconFile`

Represents an icon file.

--------------------------------------------------
## Constructor: `public IconFile()`

Creates a new [`IconFile`](#type-public-class-uicondrawingiconfile) instance.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile FromHandle(System.IntPtr handle)`

Loads an icon from the specified handle.
* `handle`: A Windows handle to an icon.

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): A loaded [`IconFile`](#type-public-class-uicondrawingiconfile).

### Remarks

When using this method, you must dispose of the original icon by using the `DestroyIcon` method in the Win32 API to ensure that the resources are released.

--------------------------------------------------
## Constructor: `public IconFile(System.Drawing.Icon icon)`

Creates a new instance using the specified [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx).
* `icon`: An [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) to decode.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`icon` is `null`.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
`icon` contains invalid values.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile Load(System.IO.Stream input)`

Loads a [`IconFile`](#type-public-class-uicondrawingiconfile) from the specified stream.
* `input`: A stream containing an icon file.

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): A [`IconFile`](#type-public-class-uicondrawingiconfile) loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile Load(System.IO.Stream input, UIconDrawing.IconLoadExceptionHandler handler)`

Loads a [`IconFile`](#type-public-class-uicondrawingiconfile) from the specified stream.
* `input`: A stream containing an icon file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): A [`IconFile`](#type-public-class-uicondrawingiconfile) loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile Load(System.String path, UIconDrawing.IconLoadExceptionHandler handler)`

Loads an [`IconFile`](#type-public-class-uicondrawingiconfile) from the specified path.
* `path`: The path to a icon file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile Load(System.String path)`

Loads an [`IconFile`](#type-public-class-uicondrawingiconfile) from the specified path.
* `path`: The path to a icon file.

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the icon file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public override UIconDrawing.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Method: `public override UIconDrawing.IconFileBase Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase): A duplicate of the current instance, with copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uicondrawingiconfilebaseentrylist-entries--get-).


--------------------------------------------------
# Type: `public class UIconDrawing.IconEntry`

Represents a single entry in an icon.

--------------------------------------------------
## Field: `public const System.Byte DefaultAlphaThreshold = 96`

The default [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-) value.

--------------------------------------------------
## Field: `public const System.Byte DefaultAlphaThreshold32 = 1`

The default [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-) value when [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) is [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side. Constrained to be less than `width`.
* `hotspotY`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top. Constrained to be less than `height`.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side. Constrained to be less than `width`.
* `hotspotY`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top. Constrained to be less than `height`.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, UIconDrawing.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side. Constrained to be less than the width of `baseImage`.
* `hotspotY`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top. Constrained to be less than the height of `baseImage`.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, UIconDrawing.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side. Constrained to be less than the width of `baseImage`.
* `hotspotY`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top. Constrained to be less than the height of `baseImage`.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, UIconDrawing.IconBitDepth bitDepth, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Drawing.Image baseImage, UIconDrawing.IconBitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Method: `public UIconDrawing.IconEntry Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconEntry`](#type-public-class-uicondrawingiconentry): A duplicate of the current instance.


--------------------------------------------------
## Property: `public System.Boolean IsDisposed { get; }`

Gets a value indicating whether the current instance has been disposed.

--------------------------------------------------
## Method: `public void Dispose()`

Immediately releases all resources used by the current instance.

--------------------------------------------------
## Disposed`

Raised when the current instance is disposed.

--------------------------------------------------
## Field: `public const System.Int16 MinDimension = 1`

The minimum dimensions of an icon. 1 pixels.

--------------------------------------------------
## Field: `public const System.Int16 MaxDimension = 768`

The maximum dimensions of an icon. 768 pixels as of Windows 10.

--------------------------------------------------
## Field: `public const System.Int16 MaxBmp32 = 96`

Gets and sets the maximum width or height at which an icon entry will be saved as a BMP file when [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) is [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0); all entries with a width or height greater than this will be saved as PNG. 96 pixels.

--------------------------------------------------
## Field: `public const System.Int16 MaxBmp = 255`

Gets and sets the maximum width or height at which an icon entry will be saved as a BMP file when [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) is any value except [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0); all entries with a width or height greater than this will be saved as PNG. 255 pixels.

--------------------------------------------------
## Property: `public System.Boolean IsQuantized { get; }`

Gets a value indicating whether [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) are both known to be already quantized.

--------------------------------------------------
## Property: `public System.Drawing.Image BaseImage { get; set; }`

Gets and sets the image associated with the current instance.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
In a set operation, the specified value is `null`.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.

--------------------------------------------------
## Property: `public System.Drawing.Image AlphaImage { get; set; }`

Gets and sets an image to be used as the alpha mask, or `null` to derive the alpha mask from [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-). Black pixels are transparent; white pixels are opaque.

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current image is disposed.

--------------------------------------------------
## Property: `public System.Boolean IsPngByDefault { get; }`

Gets a value indicating whether the current instance will be saved as a PNG image within the icon structure by default.

--------------------------------------------------
## Property: `public System.Boolean IsPng { get; set; }`

Gets and sets a value indicating whether the current instance will be saved as a PNG image.`true` is not recommended for [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) values other than [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0), and `false` is not recommended for [`IconEntry.Width`](#property-public-systemint32-width--get-) and [`IconEntry.Height`](#property-public-systemint32-height--get-) greater than or equal to 256.

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
In a set operation, the current instance is disposed.

--------------------------------------------------
## Property: `public UIconDrawing.IconEntryKey EntryKey { get; }`

Gets a key for the icon entry.

--------------------------------------------------
## Property: `public System.Int32 Width { get; }`

Gets the resampled width of the icon.

--------------------------------------------------
## Property: `public System.Int32 Height { get; }`

Gets the resampled height of the icon.

--------------------------------------------------
## Property: `public System.Drawing.Size Size { get; }`

Gets the size of the current instance.

--------------------------------------------------
## Property: `public UIconDrawing.IconBitDepth BitDepth { get; }`

Gets the bit depth of the current instance.

--------------------------------------------------
## Property: `public System.Byte AlphaThreshold { get; set; }`

Gets and sets a value indicating the threshold of alpha values at [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-)s below [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0). Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
In a set operation, the current instance is disposed.

--------------------------------------------------
## Property: `public System.Int32 HotspotX { get; set; }`

In a cursor, gets the horizontal offset in pixels of the cursor's hotspot from the left side. Constrained to greater than or equal to 0 and less than or equal to [`IconEntry.Width`](#property-public-systemint32-width--get-).

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
In a set operation, the current instance is disposed.

--------------------------------------------------
## Property: `public System.Int32 HotspotY { get; set; }`

In a cursor, gets the vertical offset in pixels of the cursor's hotspot from the top side. Constrained to greater than or equal to 0 and less than or equal to [`IconEntry.Height`](#property-public-systemint32-height--get-).

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
In a set operation, the current instance is disposed.

--------------------------------------------------
## Property: `public UIconDrawing.IconScalingFilter ScalingFilter { get; set; }`

Gets and sets the scaling mode used to resize [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) when quantizing.

### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
In a set operation, the specified value is not a valid [`IconScalingFilter`](#type-public-enum-uicondrawingiconscalingfilter) value.

--------------------------------------------------
## Property: `public System.Int32 BitsPerPixel { get; }`

Gets the number of bits per pixel specified by [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-).

--------------------------------------------------
## Method: `public static System.Int32 GetBitsPerPixel(UIconDrawing.IconBitDepth bitDepth)`

Returns the number of bits per pixel associated with the specified [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.
* `bitDepth`: The [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) to check.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): 1 for [`IconBitDepth.Depth1BitsPerPixel`](#field-iconbitdepthdepth1bitsperpixel--4); 4 for [`IconBitDepth.Depth4BitsPerPixel`](#field-iconbitdepthdepth4bitsperpixel--3); 8 for [`IconBitDepth.Depth8BitsPerPixel`](#field-iconbitdepthdepth8bitsperpixel--2); 24 for [`IconBitDepth.Depth24BitsPerPixel`](#field-iconbitdepthdepth24bitsperpixel--1); or 32 for [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0).


### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

--------------------------------------------------
## Property: `public System.Int64 ColorCount { get; }`

Gets the maximum color count specified by [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-).

--------------------------------------------------
## Method: `public static System.Int64 GetColorCount(UIconDrawing.IconBitDepth bitDepth)`

Gets the maximum color count associated with the specified [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth).
* `bitDepth`: The [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) to check.

**Returns:** Type [`Int64`](https://msdn.microsoft.com/en-us/library/system.int64.aspx): 21 for [`IconBitDepth.Depth2Color`](#field-iconbitdepthdepth2color--4); 16 for [`IconBitDepth.Depth16Color`](#field-iconbitdepthdepth16color--3); 256 for [`IconBitDepth.Depth256Color`](#field-iconbitdepthdepth256color--2); 16777216 for [`IconBitDepth.Depth24BitsPerPixel`](#field-iconbitdepthdepth24bitsperpixel--1); or 4294967296 for [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0).


### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

--------------------------------------------------
## Method: `public static UIconDrawing.IconBitDepth GetBitDepth(System.Int64 value)`

Returns the [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) associated with the specified numeric value.
* `value`: The color count or number of bits per pixel to use.

**Returns:** Type [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth): [`IconBitDepth.Depth1BitPerPixel`](#field-iconbitdepthdepth1bitperpixel--4) if `value` is 1 or 2; [`IconBitDepth.Depth4BitsPerPixel`](#field-iconbitdepthdepth4bitsperpixel--3) if `value` is 4 or 16; [`IconBitDepth.Depth8BitsPerPixel`](#field-iconbitdepthdepth8bitsperpixel--2) if `value` is 8 or 256; [`IconBitDepth.Depth24BitsPerPixel`](#field-iconbitdepthdepth24bitsperpixel--1) if `value` is 24 or 16777216; or [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0) if `value` is 32 or 4294967296.


### Exceptions

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`value` is not one of the specified parameter values.

--------------------------------------------------
## Method: `public static UIconDrawing.IconBitDepth GetBitDepth(System.Drawing.Imaging.PixelFormat pFormat)`

Returns the [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) associated with the specified [`PixelFormat`](https://msdn.microsoft.com/en-us/library/system.drawing.imaging.pixelformat.aspx) value.
* `pFormat`: A [`PixelFormat`](https://msdn.microsoft.com/en-us/library/system.drawing.imaging.pixelformat.aspx) from which to derive the [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

**Returns:** Type [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth): The [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) associated with `pFormat`, based on [`Image.GetPixelFormatSize(Imaging.PixelFormat)`](https://msdn.microsoft.com/en-us/library/system.drawing.image.getpixelformatsize(system.drawing.imaging.pixelformat).aspx); [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0) if the number of bits per pixel is not 1, 4, 8, 24, or 32; or [`IconBitDepth.Depth24BitsPerPixel`](#field-iconbitdepthdepth24bitsperpixel--1) if the pixel format is [`PixelFormat.Format32bppRgb`](https://msdn.microsoft.com/en-us/library/system.drawing.imaging.pixelformat.format32bpprgb.aspx) (because only RGB data is stored in this pixel format, just as with [`PixelFormat.Format24bppRgb`](https://msdn.microsoft.com/en-us/library/system.drawing.imaging.pixelformat.format24bpprgb.aspx)).


--------------------------------------------------
## Method: `public static System.Drawing.Imaging.PixelFormat GetPixelFormat(UIconDrawing.IconBitDepth depth)`

Returns the [`PixelFormat`](https://msdn.microsoft.com/en-us/library/system.drawing.imaging.pixelformat.aspx) associated with the specified [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth).
* `depth`: The bit depth from which to get the pixel format.

**Returns:** Type [`PixelFormat`](https://msdn.microsoft.com/en-us/library/system.drawing.imaging.pixelformat.aspx): The pixel format associated with `depth`.


### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`depth` is not a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

--------------------------------------------------
## Method: `public System.Drawing.Bitmap GetCombinedAlpha()`

Applies [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) to [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-).

**Returns:** Type [`Bitmap`](https://msdn.microsoft.com/en-us/library/system.drawing.bitmap.aspx): A new [`Bitmap`](https://msdn.microsoft.com/en-us/library/system.drawing.bitmap.aspx), sized according to [`IconEntry.Width`](#property-public-systemint32-width--get-) and [`IconEntry.Height`](#property-public-systemint32-height--get-), consisting of [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) applied to [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and with a 32-bit pixel format.


### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.

--------------------------------------------------
## Method: `public void SetQuantized(System.Boolean isPng)`

Sets [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) equal to their quantized equivalent, in a form indicated by the specified value.
* `isPng`: If `true`, [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) will be set `null` and [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) will be quantized as if it was a PNG icon entry. If `false`, [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) will be quantized as if for a BMP entry.

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.

--------------------------------------------------
## Method: `public void SetQuantized()`

Sets [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) equal to their quantized equivalent, in a form indicated by [`IconEntry.IsPng`](#property-public-systemboolean-ispng--get-set-).
### Remarks

Performs the same action as [`IconEntry.SetQuantized(System.Boolean)`](#method-public-void-setquantizedsystemboolean-ispng), with [`IconEntry.IsPng`](#property-public-systemboolean-ispng--get-set-) passed as the parameter.

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.

--------------------------------------------------
## Method: `public System.Drawing.Bitmap GetQuantizedPng()`

Returns color quantization of the current instance as it would appear for a PNG entry.

**Returns:** Type [`Bitmap`](https://msdn.microsoft.com/en-us/library/system.drawing.bitmap.aspx): A [`Bitmap`](https://msdn.microsoft.com/en-us/library/system.drawing.bitmap.aspx) containing the quantized image.


### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.
### Remarks

If [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) is already quantized and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) is `null`, this method returns a clone of [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) which must be disposed when you're done with it.

--------------------------------------------------
## Method: `IconEntry.GetQuantized(out System.Drawing.Bitmap alphaMask)`

Returns color quantization of the current instance as it would appear for a BMP entry.
* `alphaMask`: When this method returns, contains the quantized alpha mask generated using [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-). Black pixels are transparent; white pixels are opaque. This parameter is passed uninitialized.

**Returns:** A [`Bitmap`](https://msdn.microsoft.com/en-us/library/system.drawing.bitmap.aspx) containing the quantized image without the alpha mask.


### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.
### Remarks

If [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) are already quantized, this method returns clones of both objects which must be disposed after you're done with them.

--------------------------------------------------
## Method: `public override System.String ToString()`

Returns a string representation of the current instance.

**Returns:** Type [`String`](https://msdn.microsoft.com/en-us/library/system.string.aspx): A string representation of the current instance.


--------------------------------------------------
## Method: `public static UIconDrawing.IconBitDepth ParseBitDepth(System.String value)`

Parses the specified string as a [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.
* `value`: The value to parse.

**Returns:** Type [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth): The parsed [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`value` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)

`value` is an empty string or contains only whitespace.

-OR-

`value` does not translate to a valid [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.

### Remarks


`value` is parsed in a case-insensitive manner which works differently from [`Enum.Parse(System.Type,System.String,System.Boolean)`](https://msdn.microsoft.com/en-us/library/kxydatf9.aspx).

First of all, all non-alphanumeric characters are stripped. If `value` is entirely numeric, or begins with "Depth" followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the integer [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.

Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel" (or "BitPerPixel" in the case of [`IconBitDepth.Depth1BitPerPixel`](#field-iconbitdepthdepth1bitperpixel--4)).

### Example


```C#
BitDepth result;
if (IconEntry.TryParse("32", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed!");
//Succeeded: BitDepth.Depth32BitsPerPixel
if (IconEntry.TryParse("32Bit", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed");
//Succeeded: BitDepth.Depth32BitsPerPixel
if (IconEntry.TryParse("32Color", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed");
//Failed
if (IconEntry.TryParse("Depth256", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed");
//Succeeded: BitDepth.Depth256Color
```



--------------------------------------------------
## Method: `IconEntry.TryParseBitDepth(System.String value, out UIconDrawing.IconBitDepth result)`

Parses the specified string as a [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value.
* `value`: The value to parse.
* `result`: When this method returns, contains the parsed [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) result, or the default value for type [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) if `value` could not be parsed. This parameter is passed uninitialized.

**Returns:** `true` if `value` was successfully parsed; `false` otherwise.

### Remarks


`value` is parsed in a case-insensitive manner which works differently from [`Enum.TryParse<T>(System.String,System.Boolean,<>@)`](https://msdn.microsoft.com/en-us/library/dd991317.aspx).

First of all, all non-alphanumeric characters are stripped. If `value` is entirely numeric, or begins with "Depth" followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the integer [`IconBitDepth`](#type-public-enum-uicondrawingiconbitdepth) value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.

Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel" (or "BitPerPixel" in the case of [`IconBitDepth.Depth1BitPerPixel`](#field-iconbitdepthdepth1bitperpixel--4)).

### Example


```C#
BitDepth result;
if (IconEntry.TryParse("32", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed!");
//Succeeded: BitDepth.Depth32BitsPerPixel
if (IconEntry.TryParse("32Bit", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed");
//Succeeded: BitDepth.Depth32BitsPerPixel
if (IconEntry.TryParse("32Color", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed");
//Failed
if (IconEntry.TryParse("Depth256", out result)) Console.WriteLine("Succeeded: " + result);
else Console.WriteLine("Failed");
//Succeeded: BitDepth.Depth256Color
```



--------------------------------------------------
# Type: `public struct UIconDrawing.IconEntryKey`

Represents a simplified key for an icon entry.

--------------------------------------------------
## Constructor: `public IconEntryKey(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Creates a new instance.
* `width`: The width of the icon entry.
* `height`: The height of the icon entry.
* `bitDepth`: The bit depth of the icon entry.

--------------------------------------------------
## Field: `public System.Int32 Width`

Indicates the width of the icon entry.

--------------------------------------------------
## Field: `public System.Int32 Height`

Indicates the height of the icon entry.

--------------------------------------------------
## Field: `public UIconDrawing.IconBitDepth BitDepth`

Indicates the bit depth of the icon entry.

--------------------------------------------------
## Method: `public System.Int32 CompareTo(UIconDrawing.IconEntryKey other)`

Compares the current instance to the specified other [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) object. First [`IconEntryKey.BitDepth`](#field-public-uicondrawingiconbitdepth-bitdepth) is compared, then [`IconEntryKey.Height`](#field-public-systemint32-height), then [`IconEntryKey.Width`](#field-public-systemint32-width) (with higher color-counts and larger elements first).
* `other`: The other [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): A value less than 0 if the current value comes before `other`; a value greater than 0 if the current value comes after `other`; or 0 if the current instance is equal to `other`.


--------------------------------------------------
## Field: `public static readonly UIconDrawing.IconEntry Empty`

Returns an invalid [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) with all values equal to 0.

--------------------------------------------------
## Property: `public System.Boolean IsValid { get; }`

Gets a value indicating whether the current value would actually appear in an icon entry.

--------------------------------------------------
## Method: `public System.String ToString()`

Returns a string representation of the current value.

**Returns:** Type [`String`](https://msdn.microsoft.com/en-us/library/system.string.aspx): A string representation of the current value.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThan(UIconDrawing.IconEntryKey f1, UIconDrawing.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThan(UIconDrawing.IconEntryKey f1, UIconDrawing.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is greater than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThanOrEqual(UIconDrawing.IconEntryKey f1, UIconDrawing.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThanOrEqual(UIconDrawing.IconEntryKey f1, UIconDrawing.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean Equals(UIconDrawing.IconEntryKey other)`

Determines if the current instance is equal to the specified other [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) value.
* `other`: The other [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the current instance is equal to `other`; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean Equals(System.Object obj)`

Determines if the current instance is equal to the specified other object.
* `obj`: The other object to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the current instance is equal to `obj`; `false` otherwise.


--------------------------------------------------
## Method: `public System.Int32 GetHashCode()`

Returns a hash code for the current value.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): A hash code for the current value.


--------------------------------------------------
## Operator: `public static System.Boolean op_Equality(UIconDrawing.IconEntryKey f1, UIconDrawing.IconEntryKey f2)`

Determines equality of two [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_Inequality(UIconDrawing.IconEntryKey f1, UIconDrawing.IconEntryKey f2)`

Determines inequality of two [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uicondrawingiconentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is not equal to `f2`; `false` otherwise.


--------------------------------------------------
# Type: `public enum UIconDrawing.IconBitDepth`

Indicates the bit depth of an icon entry.

--------------------------------------------------
## Field: `IconBitDepth.Depth32BitsPerPixel = 0`

Indicates that the entry is full color with alpha (32 bits per pixel).

--------------------------------------------------
## Field: `IconBitDepth.Depth24BitsPerPixel = 1`

Indicates that the entry is full color without alpha (24 bits per pixel).

--------------------------------------------------
## Field: `IconBitDepth.Depth256Color = 2`

Indicates that the entry is 256-color (8 bits per pixel). Same value as [`IconBitDepth.Depth8BitsPerPixel`](#field-iconbitdepthdepth8bitsperpixel--2).

--------------------------------------------------
## Field: `IconBitDepth.Depth16Color = 3`

Indicates that the entry is 16-color (4 bits per pixel). Same value as [`IconBitDepth.Depth4BitsPerPixel`](#field-iconbitdepthdepth4bitsperpixel--3).

--------------------------------------------------
## Field: `IconBitDepth.Depth2Color = 4`

Indicates that the entry is 2-color (1 bit per pixel). Same value as [`IconBitDepth.Depth1BitPerPixel`](#field-iconbitdepthdepth1bitperpixel--4).

--------------------------------------------------
## Field: `IconBitDepth.Depth8BitsPerPixel = 2`

Indicates that the entry is 256-color (8 bits per pixel). Same value as [`IconBitDepth.Depth256Color`](#field-iconbitdepthdepth256color--2).

--------------------------------------------------
## Field: `IconBitDepth.Depth4BitsPerPixel = 3`

Indicates that the entry is 16-color (4 bits per pixel). Same value as [`IconBitDepth.Depth16Color`](#field-iconbitdepthdepth16color--3).

--------------------------------------------------
## Field: `IconBitDepth.Depth1BitPerPixel = 4`

Indicates that the entry is 2-color (1 bit per pixel). Same value as [`IconBitDepth.Depth2Color`](#field-iconbitdepthdepth2color--4).

--------------------------------------------------
## Field: `IconBitDepth.Depth1BitsPerPixel = 4`

Indicates that the entry is 2-color (1 bit per pixel). Same value as [`IconBitDepth.Depth2Color`](#field-iconbitdepthdepth2color--4).

--------------------------------------------------
# Type: `public enum UIconDrawing.IconScalingFilter`

Indicates options for resizing [`IconEntry.BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemdrawingimage-alphaimage--get-set-) when quantizing.

--------------------------------------------------
## Field: `IconScalingFilter.Bilinear = 0`

Specifies bilinear interpolation.

--------------------------------------------------
## Field: `IconScalingFilter.Bicubic = 1`

Specifies bicubic interpolation.

--------------------------------------------------
## Field: `IconScalingFilter.NearestNeighbor = 2`

Specifies nearest-neighbor interpolation.

--------------------------------------------------
## Field: `IconScalingFilter.HighQualityBilinear = 3`

Specifies high-quality bilinear interpolation. Prefiltering is performed to ensure high-quality transformation.

--------------------------------------------------
## Field: `IconScalingFilter.HighQualityBicubic = 4`

Specifies high-quality bicubic interpolation. Prefiltering is performed to ensure high-quality transformation.

--------------------------------------------------
# Type: `public class UIconDrawing.IconLoadException`

The exception that is thrown when an icon file contains invalid data.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode, System.Object value, System.Int32 entryIndex)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode, System.Object value)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode, System.Int32 entryIndex)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode, System.Int32 entryIndex)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode, System.Object value, System.Int32 entryIndex)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode, System.Object value)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode typeCode, System.Int32 entryIndex, System.Exception innerException)`

Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconDrawing.IconErrorCode code, UIconDrawing.IconTypeCode innerException, System.Exception typeCode)`

Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.
* `typeCode`: The type code of the file which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, System.Exception innerException)`

Creates a new instance with the specified message and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.

--------------------------------------------------
## Property: `public override System.String Message { get; }`

Gets a message describing the error.

--------------------------------------------------
## Property: `public System.Int32 EntryIndex { get; }`

Gets the index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.
### Remarks

This refers to the index of the entry within the list of icon directory entries; it may not refer to the position of the image within the rest of the icon file.

--------------------------------------------------
## Property: `public UIconDrawing.IconErrorCode Code { get; }`

Gets an error code describing the icon error.

--------------------------------------------------
## Property: `public System.Object Value { get; }`

Gets an object whose value caused the error, or `null` if there was no such value.

--------------------------------------------------
## Property: `public UIconDrawing.IconTypeCode TypeCode { get; }`

Gets a value indicating the type of the icon file.

--------------------------------------------------
# Type: `public class UIconDrawing.IconExtractException`

The exception that is thrown when an icon file extracted from an EXE or DLL file contains invalid data.

--------------------------------------------------
## Property: `public override System.String Message { get; }`

Gets a message describing the error.

--------------------------------------------------
## Property: `public System.Int32 ExtractIndex { get; }`

Gets the index in the DLL or EXE file of the icon or cursor which caused the error.

--------------------------------------------------
# Type: `public enum UIconDrawing.IconErrorCode`

Indicates the cause of an [`IconLoadException`](#type-public-class-uicondrawingiconloadexception).

--------------------------------------------------
## Field: `IconErrorCode.Unknown = 0`

Code 0: the cause of the error is unknown.

--------------------------------------------------
## Field: `IconErrorCode.InvalidFormat = 1`

Code 0x1: The file is not a valid cursor or icon format. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.ZeroEntries = 2`

Code 0x2: The icon contains zero entries. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.WrongType = 3`

Code 0x3: An icon was expected but a cursor was loaded, or vice versa. [`IconLoadException.TypeCode`](#property-public-uicondrawingicontypecode-typecode--get-) contains the expected value, and [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the actual value. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.EntryParseError = 4096`

Code 0x1000: an error occurred when attempting to parse an icon frame.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBitDepth = 4097`

Code 0x1001: The bit depth of an individual entry is not 1, 4, 8, 24, or 32. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the bit depth.

--------------------------------------------------
## Field: `IconErrorCode.ZeroValidEntries = 4098`

Code 0x1002: There are no remaining valid entries after processing. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
# Type: `public class UIconDrawing.IconLoadExceptionHandler`

A delegate function for handling [`IconLoadException`](#type-public-class-uicondrawingiconloadexception) errors.
* `e`: An [`IconLoadException`](#type-public-class-uicondrawingiconloadexception) containing information about the error.

--------------------------------------------------
# Type: `public class UIconDrawing.IconExtractExceptionHandler`

A delegate function for handling [`IconExtractException`](#type-public-class-uicondrawingiconextractexception) errors.
* `e`: An [`IconExtractException`](#type-public-class-uicondrawingiconextractexception) containing information about the error.

--------------------------------------------------
# Type: `public interface UIconDrawing.IEntryList`

A base class for [`IconEntry`](#type-public-class-uicondrawingiconentry) collections.

--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconDrawing.IconEntry item)`

Determines if an element similar to the specified icon entry exists in the list.
* `item`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconDrawing.IconEntryKey key)`

Determines if an element similar to the specified icon entry exists in the list.
* `key`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `key` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Determines if an element similar to the specified values exists in the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `bitDepth` was found;`false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconDrawing.IconEntry item)`

Gets the index of an element similar to the specified item.
* `item`: The icon entry to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconDrawing.IconEntryKey key)`

Gets the index of an element similar to the specified item.
* `key`: The icon entry to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `key`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Gets the index of an element similar to the specified values.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `bitDepth`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconDrawing.IconEntry item)`

Removes an icon entry similar to the specified value from the list.
* `item`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `item` was successfully found and removed; `false` if no such icon entry was found in the list.


### Exceptions

##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
The collection is read-only.

--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconDrawing.IconEntryKey key)`

Removes an icon entry similar to the specified value from the list.
* `key`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `key` was successfully found and removed; `false` if no such icon entry was found in the list.


### Exceptions

##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
The collection is read-only.

--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(System.Int32 width, System.Int32 height, UIconDrawing.IconBitDepth bitDepth)`

Removes an icon entry similar to the specified values from the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) as `bitDepth` was successfully found and removed;`false` if no such icon entry was found in the list.


### Exceptions

##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
The collection is read-only.

--------------------------------------------------
# Type: `public abstract class UIconDrawing.IconExtraction`

Provides methods for extracting icons from EXE and DLL files.

--------------------------------------------------
## Method: `public static System.Int32 ExtractIconCount(System.String path)`

Determines the number of icons in the specified EXE or DLL file.
* `path`: The path to the file to load.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of icons in the specified file.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

--------------------------------------------------
## Method: `public static System.Int32 ExtractCursorCount(System.String path)`

Determines the number of cursors in the specified EXE or DLL file.
* `path`: The path to the file to load.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of cursors in the specified file.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

--------------------------------------------------
## Method: `public static System.Drawing.Icon ExtractIconObjSingle(System.String path, System.Int32 index)`

Loads a single [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) from the specified collection EXE or DLL file with the default size.
* `path`: The path to the file to load.
* `index`: The zero-based index of the icon in `path`.

**Returns:** Type [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx): The icon with the specified key in `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than the number of icons in `path`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when loading the icon.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
An error occurred when loading the icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static System.Drawing.Icon ExtractIconObjSingle(System.String path, System.Int32 index, System.Drawing.Size size)`

Loads a single [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) from the specified collection EXE or DLL file with the specified size.
* `path`: The path to the file to load.
* `index`: The zero-based index of the icon in `path`.
* `size`: The indended width of the icon.

**Returns:** Type [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx): The icon with the specified key in `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than the number of icons in `path`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when loading the icon.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
An error occurred when loading the icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static System.Drawing.Icon ExtractIconObjSingle(System.String path, System.Int32 index, System.Int32 width, System.Int32 height)`

Loads a single [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) from the specified collection EXE or DLL file with the specified size.
* `path`: The path to the file to load.
* `index`: The zero-based index of the icon in `path`.
* `width`: The indended width of the icon.
* `height`: The intended height of the icon.

**Returns:** Type [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx): The icon with the specified key in `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than the number of icons in `path`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when loading the icon.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
An error occurred when loading the icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile ExtractIconSingle(System.String path, System.Int32 index, UIconDrawing.IconLoadExceptionHandler handler)`

Extracts a single icon from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The zero-based index of the icon in `path`.
* `handler`: A delegate used to handle non-fatal [`IconLoadException`](#type-public-class-uicondrawingiconloadexception) errors, or `null` to always throw an exception.

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): The icon with the specified key in `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than the number of icons in `path`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when loading the icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile ExtractIconSingle(System.String path, System.Int32 index)`

Extracts a single icon from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The index of the icon in `path`.

**Returns:** Type [`IconFile`](#type-public-class-uicondrawingiconfile): The icon with the specified key in `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than the number of icons in `path`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when loading the icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile ExtractCursorSingle(System.String path, System.Int32 index, UIconDrawing.IconLoadExceptionHandler handler)`

Extracts a single cursor from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The index of the cursor in `path`.
* `handler`: A delegate used to handle non-fatal [`IconLoadException`](#type-public-class-uicondrawingiconloadexception) errors, or `null` to always throw an exception.

**Returns:** Type [`CursorFile`](#type-public-class-uicondrawingcursorfile): The cursor with the specified key in `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than the number of cursors in `path`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when loading the icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile ExtractCursorSingle(System.String path, System.Int32 index)`

Extracts a single cursor from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The [`IntPtr`](https://msdn.microsoft.com/en-us/library/system.intptr.aspx) key of the cursor in `path`.

**Returns:** Type [`CursorFile`](#type-public-class-uicondrawingcursorfile): The cursor with the specified key in `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than the number of cursors in `path`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when loading the icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile[] ExtractAllIcons(System.String path, UIconDrawing.IconExtractExceptionHandler singleHandler, UIconDrawing.IconExtractExceptionHandler allHandler)`

Extracts all icons from the specified EXE or DLL file.
* `path`: The path to the file from which to load all icons.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown by a single icon entry in a single icon file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single icon entry in an icon file, or `null` to always throw an exception regardless.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconFile`](#type-public-class-uicondrawingiconfile): An array containing all icon files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.IconFile[] ExtractAllIcons(System.String path)`

Extracts all icons from the specified EXE or DLL file.
* `path`: The path to the file from which to load all icons.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconFile`](#type-public-class-uicondrawingiconfile): An array containing all icon files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile[] ExtractAllCursors(System.String path, UIconDrawing.IconExtractExceptionHandler singleHandler, UIconDrawing.IconExtractExceptionHandler allHandler)`

Extracts all cursors from the specified EXE or DLL file.
* `path`: The path to the file from which to load all cursors.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown by a single cursor entry in a single cursor file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single cursor entry in a cursor file, or `null` to always throw an exception regardless.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`CursorFile`](#type-public-class-uicondrawingcursorfile): An array containing all cursor files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.CursorFile[] ExtractAllCursors(System.String path)`

Extracts all cursors from the specified EXE or DLL file.
* `path`: The path to the file from which to load all cursors.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`CursorFile`](#type-public-class-uicondrawingcursorfile): An array containing all cursor files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractIconsForEach(System.String path, UIconDrawing.IconExtractCallback<UIconDrawing.IconFile> callback, UIconDrawing.IconExtractExceptionHandler singleHandler, UIconDrawing.IconExtractExceptionHandler allHandler)`

Iterates through each icon in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all icons.
* `callback`: An action to perform on each icon.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown by a single icon entry in a single cursor file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single icon entry in an icon file, or `null` to always throw an exception regardless.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractIconsForEach(System.String path, UIconDrawing.IconExtractCallback<UIconDrawing.IconFile> callback)`

Iterates through each icon in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all icons.
* `callback`: An action to perform on each icon.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractCursorsForEach(System.String path, UIconDrawing.IconExtractCallback<UIconDrawing.CursorFile> callback, UIconDrawing.IconExtractExceptionHandler singleHandler, UIconDrawing.IconExtractExceptionHandler allHandler)`

Iterates through each cursor in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all cursors.
* `callback`: An action to perform on each cursor.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)s thrown by a single icon entry in a single cursor file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single icon entry in a cursor file, or `null` to always throw an exception regardless.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractCursorsForEach(System.String path, UIconDrawing.IconExtractCallback<UIconDrawing.CursorFile> callback)`

Iterates through each cursor in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all cursors.
* `callback`: An action to perform on each cursor.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
# Type: `public class UIconDrawing.IconExtractCallback`1`

A delegate function to perform on each cursor or icon extracted from a DLL or EXE file.The type of the [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation.
* `index`: The index of the current cursor or icon to process.
* `iconFile`: The cursor or icon which was extracted.
* `e`: A [`CancelEventArgs`](https://msdn.microsoft.com/en-us/library/system.componentmodel.canceleventargs.aspx) object which is used to cancel

--------------------------------------------------
# Type: `public class UIconDrawing.AnimatedCursorFile`

Represents an animated cursor file.

--------------------------------------------------
## Constructor: `public AnimatedCursorFile()`

Initializes a new [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile) instance.

--------------------------------------------------
## Method: `public static UIconDrawing.AnimatedCursorFile Load(System.IO.Stream input, UIconDrawing.IconExtractExceptionHandler handler)`

Loads an animated cursor file from the specified stream.
* `input`: The stream containing the animated cursor file to load.
* `handler`: An error handler for loading the individual cursor files, or `null` to always throw an exception.

**Returns:** Type [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile): A loaded [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile).


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading the animated cursor file.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when loading the animated cursor file.

--------------------------------------------------
## Method: `public static UIconDrawing.AnimatedCursorFile Load(System.IO.Stream input)`

Loads an animated cursor file from the specified stream.
* `input`: The stream containing the animated cursor file to load.

**Returns:** Type [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile): A loaded [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile).


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading the animated cursor file.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when loading the animated cursor file.

--------------------------------------------------
## Method: `public static UIconDrawing.AnimatedCursorFile Load(System.String path, UIconDrawing.IconExtractExceptionHandler handler)`

Loads an [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile) from the specified path.
* `path`: The path to a cursor file.
* `handler`: An error handler for loading the individual cursor files, or `null` to always throw an exception.

**Returns:** Type [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconExtractException`](#type-public-class-uicondrawingiconextractexception)
An error occurred when loading the animated cursor file.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when loading the animated cursor file.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconDrawing.AnimatedCursorFile Load(System.String path)`

Loads an [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile) from the specified path.
* `path`: The path to a cursor file.

**Returns:** Type [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile): An [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`FileNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.filenotfoundexception.aspx)
The specified path was not found.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path was invalid.

##### [`UnauthorizedAccessException`](https://msdn.microsoft.com/en-us/library/system.unauthorizedaccessexception.aspx)

`path` specified a directory.

-OR-

The caller does not have the required permission.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
`path` is in an invalid format.

##### [`IconLoadException`](#type-public-class-uicondrawingiconloadexception)
An error occurred when processing the cursor file's format.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public void Save(System.IO.Stream output)`

Saves the current instance to the specified stream.
* `output`: The stream to which the current instance is written.

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)

The current instance is disposed.

-OR-

`output` is closed.


##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)

[`AnimatedCursorFile.Entries`](#property-public-uicondrawinganimatedcursorfileentrylist-entries--get-) is `null`.

-OR-

The elements in [`AnimatedCursorFile.Entries`](#property-public-uicondrawinganimatedcursorfileentrylist-entries--get-) do not all have the same number of [`IconEntry`](#type-public-class-uicondrawingiconentry) objects with the same combination of [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-).

-OR-

[`AnimatedCursorFile.FrameIndices`](#property-public-systemcollectionsobjectmodelobservablecollectiont-frameindices--get-) contains elements which are less than 0, or are greater than or equal to the number of elements in [`AnimatedCursorFile.Entries`](#property-public-uicondrawinganimatedcursorfileentrylist-entries--get-).


##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`output` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`output` does not support writing.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public void Save(System.String path)`

Saves the current instance to the specified path.
* `path`: The path to the file to which the current instance will be saved.

### Exceptions

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
The current instance is disposed.

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)

[`AnimatedCursorFile.Entries`](#property-public-uicondrawinganimatedcursorfileentrylist-entries--get-) is `null`.

-OR-

The elements in [`AnimatedCursorFile.Entries`](#property-public-uicondrawinganimatedcursorfileentrylist-entries--get-) do not all have the same number of [`IconEntry`](#type-public-class-uicondrawingiconentry) objects with the same combination of [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-).

-OR-

[`AnimatedCursorFile.FrameIndices`](#property-public-systemcollectionsobjectmodelobservablecollectiont-frameindices--get-) contains elements which are less than 0, or are greater than or equal to the number of elements in [`AnimatedCursorFile.Entries`](#property-public-uicondrawinganimatedcursorfileentrylist-entries--get-).


##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`Path.GetInvalidPathChars()`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path is invalid.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public UIconDrawing.AnimatedCursorFile.EntryList Entries { get; }`

Gets a list of [`IconFileBase`](#type-public-abstract-class-uicondrawingiconfilebase) objects containing all entries in the animated cursor file.

--------------------------------------------------
## Property: `public System.Collections.ObjectModel.ObservableCollection<T> FrameIndices { get; }`

Gets the ordering of the frames, as indices within [`AnimatedCursorFile.Entries`](#property-public-uicondrawinganimatedcursorfileentrylist-entries--get-).

--------------------------------------------------
## Method: `public static System.TimeSpan JiffiesToTime(System.Int32 jiffies)`

Converts the specified number of "jiffies" (1/60 of a second) to its corresponding [`TimeSpan`](https://msdn.microsoft.com/en-us/library/system.timespan.aspx) value.
* `jiffies`: The number of jiffies to convert.

**Returns:** Type [`TimeSpan`](https://msdn.microsoft.com/en-us/library/system.timespan.aspx): A [`TimeSpan`](https://msdn.microsoft.com/en-us/library/system.timespan.aspx) with a [`TimeSpan.TotalSeconds`](https://msdn.microsoft.com/en-us/library/system.timespan.totalseconds.aspx) value equal to `jiffies` divided by 60.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`jiffies` is less than or equal to 0.

--------------------------------------------------
## Method: `public static System.Int32 TimeToJiffies(System.TimeSpan value)`

Converts the specified [`TimeSpan`](https://msdn.microsoft.com/en-us/library/system.timespan.aspx) to its equivalent number of "jiffies" (1/60 of a second).
* `value`: The [`TimeSpan`](https://msdn.microsoft.com/en-us/library/system.timespan.aspx) to convert.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): A number of jiffies equal to `value`'s [`TimeSpan.TotalSeconds`](https://msdn.microsoft.com/en-us/library/system.timespan.totalseconds.aspx) multiplied by 60.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`value` translates to a number of jiffies less than or equal to 0, or greater than [`Int32.MaxValue`](https://msdn.microsoft.com/en-us/library/system.int32.maxvalue.aspx).

--------------------------------------------------
## Property: `public System.Int32 DisplayRateJiffies { get; set; }`

Gets and sets the default delay before displaying the next frame, in "jiffies" (1/60 of a second).

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is less than or equal to 0.

--------------------------------------------------
## Property: `public System.TimeSpan DisplayRateTime { get; set; }`

Gets and sets the default delay before displaying the next frame. Fitted to the nearest "jiffy" (1/60 of a second).

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value translates to a number of jiffies less than or equal to 0 or greater than [`Int32.MaxValue`](https://msdn.microsoft.com/en-us/library/system.int32.maxvalue.aspx)

--------------------------------------------------
## Property: `public System.String CursorName { get; set; }`

Gets and sets the name of the animated cursor file. This value is null-terminated when written to the file.

--------------------------------------------------
## Property: `public System.String CursorAuthor { get; set; }`

Gets and sets the author of the animated cursor file. This value is null-terminated when written to the file.

--------------------------------------------------
## PropertyChanged`

Raised when a property on the current instance changes.

--------------------------------------------------
## Method: `protected void OnPropertyChanged(System.String propertyName)`

Raises the [`AnimatedCursorFile.PropertyChanged`](#propertychanged-1) event.
* `propertyName`: The name of the property which was changed.

--------------------------------------------------
## Disposed`

Raised when the current instance is disposed.

--------------------------------------------------
## Property: `public System.Boolean IsDisposed { get; }`

Gets a value indicating whether the current instance has been disposed.

--------------------------------------------------
## Method: `public void Dispose()`

Immediately releases all resources used by the current instance.

--------------------------------------------------
# Type: `class UIconDrawing.AnimatedCursorFile.EntryList`

Represents a list of [`CursorFile`](#type-public-class-uicondrawingcursorfile) objects for use in an animated cursor file.

--------------------------------------------------
## CollectionChanged`

Raised when the collection changes.

--------------------------------------------------
## Property: `AnimatedCursorFile.EntryList.Item(System.Int32 index)`

Gets and sets the element at the specified index.
* `index`: The index of the element to get or set.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
In a set operation, the specified value is `null`.

--------------------------------------------------
## Property: `public virtual System.Int32 Count { get; }`

Gets the number of elements contained in the list.

--------------------------------------------------
## Method: `public System.Boolean Insert(System.Int32 index, UIconDrawing.AnimatedCursorFrame frame)`

Adds the specified [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe) to the list at the specified index.
* `index`: The index at which the frame will be inserted.
* `frame`: The [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe) to add.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `frame` was successfully added; `false` if `frame` is `null`, already exists in the list, or is already associated with a different [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile).


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public System.Boolean Add(UIconDrawing.AnimatedCursorFrame frame)`

Adds the specified [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe) to the list.
* `frame`: The [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe) to add.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `frame` was successfully added; `false` if `frame` is `null`, already exists in the list, or is already associated with a different [`AnimatedCursorFile`](#type-public-class-uicondrawinganimatedcursorfile).


--------------------------------------------------
## Method: `public void RemoveAt(System.Int32 index)`

Removes the element at the specified index.
* `index`: The index of the element to remove.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public System.Boolean Remove(UIconDrawing.AnimatedCursorFrame frame)`

Removes the specified frame from the list.
* `frame`: The frame to remove.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `frame` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Int32 IndexOf(UIconDrawing.AnimatedCursorFrame frame)`

Returns the index of the specified frame.
* `frame`: The frame to search for in the list.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `frame`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Boolean Contains(UIconDrawing.AnimatedCursorFrame frame)`

Determines whether the specified frame exists in the list.
* `frame`: The frame to search for in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `frame` was found; `false` otherwise.


--------------------------------------------------
## Method: `public void Clear()`

Removes all elements from the list.

--------------------------------------------------
## Method: `public void CopyTo(UIconDrawing.AnimatedCursorFrame[] array, System.Int32 index)`

Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `index`: The index in `array` at which copying begins.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` minus `index` is less than [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public UIconDrawing.AnimatedCursorFrame[] ToArray()`

Returns an array containing all elements in the current list.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe): An array containing elements copied from the current list.


--------------------------------------------------
## Method: `public UIconDrawing.AnimatedCursorFile.EntryList.Enumerator GetEnumerator()`

Returns an enumerator which iterates through the list.

**Returns:** Type [`Enumerator`](#type-struct-uicondrawinganimatedcursorfileentrylistenumerator): An enumerator which iterates through the list.


--------------------------------------------------
# Type: `struct UIconDrawing.AnimatedCursorFile.EntryList.Enumerator`

An enumerator which iterates through the list.

--------------------------------------------------
## Property: `public UIconDrawing.AnimatedCursorFrame Current { get; }`

Gets the element at the current position in the enumerator.

--------------------------------------------------
## Method: `public void Dispose()`

Disposes of the current instance.

--------------------------------------------------
## Method: `public System.Boolean MoveNext()`

Advances the enumerator to the next position in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
# Type: `public class UIconDrawing.AnimatedCursorFrame`

Represents rate information for a single frame of an animated cursor.

--------------------------------------------------
## Constructor: `public AnimatedCursorFrame(UIconDrawing.CursorFile file)`

Creates a new instance with the specified cursor file.
* `file`: The cursor file associated with the current instance.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`file` is `null`.

--------------------------------------------------
## Constructor: `public AnimatedCursorFrame(UIconDrawing.CursorFile file, System.Int32 jiffies)`

Creates a new instance with the specified cursor file and delay.
* `file`: The cursor file associated with the current instance.
* `jiffies`: The delay before displaying the next frame in the animated cursor, in "jiffies" (1/60 of a second).

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`file` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`jiffies` is less than 0.

--------------------------------------------------
## Constructor: `public AnimatedCursorFrame(UIconDrawing.CursorFile file, System.TimeSpan length)`

Creates a new instance with the specified cursor file and delay.
* `file`: The cursor file associated with the current instance.
* `length`: The delay before displaying the next frame in the animated cursor. Fitted to the nearest "jiffy" (1/60 of a second).

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`file` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`length` is less than [`TimeSpan.Zero`](https://msdn.microsoft.com/en-us/library/system.timespan.zero.aspx), or represents a number of "jiffies" (1/60 of a second) greater than [`Int32.MaxValue`](https://msdn.microsoft.com/en-us/library/system.int32.maxvalue.aspx).

--------------------------------------------------
## PropertyChanged`

Raised when a property on the current instance changes.

--------------------------------------------------
## Method: `public System.Boolean SimilarListEquals(UIconDrawing.AnimatedCursorFrame other)`

Determines if the elements in [`AnimatedCursorFrame.File`](#property-public-uicondrawingcursorfile-file--get-) in the current instance are all similar to that of specified other [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe).
* `other`: The other [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the current instance contains the same number of elements with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uicondrawingiconbitdepth-bitdepth--get-) values as the specified other [`AnimatedCursorFrame`](#type-public-class-uicondrawinganimatedcursorframe);`false` otherwise.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`other` is `null`.

--------------------------------------------------
## Property: `public UIconDrawing.CursorFile File { get; }`

Gets and sets the cursor file associated with the current instance.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
In a set operation, the specified value is `null`.

--------------------------------------------------
## Property: `public System.Nullable<T> LengthJiffies { get; set; }`

Gets and sets the delay before displaying the next frame in the animated cursor, in "jiffies" (1/60 of a second), or `null` to use the animated cursor file's [`AnimatedCursorFile.DisplayRateJiffies`](#property-public-systemint32-displayratejiffies--get-set-) value.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is not `null` and is less than 0.

--------------------------------------------------
## Property: `public System.Nullable<T> LengthTime { get; set; }`

Gets and sets the delay before displaying the next frame in the animated cursor, or `null` to use the animated cursor file's [`AnimatedCursorFile.DisplayRateTime`](#property-public-systemtimespan-displayratetime--get-set-) property. Fitted to the nearest "jiffy" (1/60 of a second).

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is not `null`, and is less than [`TimeSpan.Zero`](https://msdn.microsoft.com/en-us/library/system.timespan.zero.aspx) or represents a number of [`AnimatedCursorFrame.LengthJiffies`](#property-public-systemnullablet-lengthjiffies--get-set-) greater than [`Int32.MaxValue`](https://msdn.microsoft.com/en-us/library/system.int32.maxvalue.aspx).

--------------------------------------------------
## Method: `public override System.String ToString()`

Returns a string representation of the current instance.

**Returns:** Type [`String`](https://msdn.microsoft.com/en-us/library/system.string.aspx): A string representation of the current instance.
