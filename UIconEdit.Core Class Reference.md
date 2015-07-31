# Type: `public abstract class UIconEdit.IconFileBase`

Base class for icon and cursor files.

--------------------------------------------------
## Constructor: `public IconFileBase()`

Initializes a new instance.

--------------------------------------------------
## Method: `public static UIconEdit.IconFileBase Load(System.IO.Stream input)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified stream.
* `input`: A stream containing an icon or cursor file.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFileBase Load(System.IO.Stream input, UIconEdit.IconLoadExceptionHandler handler)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified stream.
* `input`: A stream containing an icon or cursor file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFileBase Load(System.String path, UIconEdit.IconLoadExceptionHandler handler)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified path.
* `path`: The path to a cursor or icon file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFileBase Load(System.String path)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified path.
* `path`: The path to a cursor or icon file.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public virtual UIconEdit.IconFileBase Clone()`

When overridden in a derived class, returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): A duplicate of the current instance, with copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-).


--------------------------------------------------
## Method: `public virtual UIconEdit.IconFile CloneAsIconFile()`

Returns a duplicate of the current instance as an [`IconFile`](#type-public-class-uiconediticonfile)

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): An [`IconFile`](#type-public-class-uiconediticonfile) containing copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-), and with each entry's [`IconEntry.HotspotX`](#property-public-systemint32-hotspotx--get-set-) and [`IconEntry.HotspotY`](#property-public-systemint32-hotspoty--get-set-) values set to 0.


--------------------------------------------------
## Method: `public virtual UIconEdit.CursorFile CloneAsCursorFile()`

Returns a duplicate of the current instance as an [`IconFile`](#type-public-class-uiconediticonfile)

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): An [`IconFile`](#type-public-class-uiconediticonfile) containing copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-).


--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

When overridden in a derived class, gets the 16-bit identifier for the file type.

--------------------------------------------------
## Property: `public UIconEdit.IconFileBase.EntryList Entries { get; }`

Gets a collection containing all entries in the icon file.

--------------------------------------------------
## Method: `public void Save(System.IO.Stream output)`

Saves the file to the specified stream.
* `output`: The stream to which the file will be written.

### Exceptions

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)
[`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-) contains zero elements.

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`output` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`output` is closed or does not support writing.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`output` is closed.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public void Save(System.String path)`

Saves the file to the specified file.
* `path`: The file to which the file will be written.

### Exceptions

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)
[`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-) contains zero elements.

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
# Type: `class UIconEdit.IconFileBase.EntryList`

Represents a list of icon entries. Entries with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) cannot be added to the list; however, there may be duplicates if an icon loaded from an external icon file contained them.

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
In a set operation, an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) already exists in the list at a different index, or the specified value is already associated with a different icon file.

--------------------------------------------------
## Property: `public virtual System.Int32 Count { get; }`

Gets the number of elements in the list.

--------------------------------------------------
## Method: `public System.Boolean Add(UIconEdit.IconEntry item)`

Adds the specified icon entry to the list.
* `item`: The icon entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get-) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) already exists in the list.


--------------------------------------------------
## Method: `public System.Boolean Insert(System.Int32 index, UIconEdit.IconEntry item)`

Adds the specified icon entry to the list at the specified index.
* `index`: The index at which to insert the icon entry.
* `item`: The icon entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get-) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) already exists in the list.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public System.Boolean SetValue(System.Int32 index, UIconEdit.IconEntry item)`

Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The item to set at the specified index.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully set; `false` if `item` is `null`, is already associated with a different icon file, or if an element with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) already exists at a different index.


--------------------------------------------------
## Method: `public virtual void RemoveAt(System.Int32 index)`

Removes the element at the specified index.
* `index`: The element at the specified index.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public virtual System.Boolean Remove(UIconEdit.IconEntry item)`

Removes the specified icon entry from the list.
* `item`: The icon entry to remove from the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconEdit.IconEntry item)`

Removes an icon entry similar to the specified value from the list.
* `item`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `item` was successfully found and removed; `false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconEdit.IconEntryKey key)`

Removes an icon entry similar to the specified value from the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `key` was successfully found and removed; `false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(System.Int16 width, System.Int16 height, UIconEdit.IconBitDepth bitDepth)`

Removes an icon entry similar to the specified values from the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `bitDepth` was successfully found and removed;`false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public virtual void Clear()`

Removes all elements from the list.

--------------------------------------------------
## Method: `public virtual System.Boolean Contains(UIconEdit.IconEntry item)`

Determines if the specified element exists in the list.
* `item`: The icon entry to search for in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconEdit.IconEntry item)`

Determines if an element similar to the specified icon entry exists in the list.
* `item`: The icon entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconEdit.IconEntryKey key)`

Determines if an element similar to the specified value exists in the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `key` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(System.Int16 width, System.Int16 height, UIconEdit.IconBitDepth bitDepth)`

Determines if an element similar to the specified values exists in the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `bitDepth` was found;`false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public virtual System.Int32 IndexOf(UIconEdit.IconEntry item)`

Gets the index of the specified item.
* `item`: The icon entry to search for in the list.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.IconEntry item)`

Gets the index of an element similar to the specified item.
* `item`: The icon entry to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.IconEntryKey key)`

Gets the index of an element similar to the specified value.
* `key`: The entry key to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `key`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(System.Int16 width, System.Int16 height, UIconEdit.IconBitDepth bitDepth)`

Gets the index of an element similar to the specified values.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint32-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `bitDepth`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public virtual void CopyTo(UIconEdit.IconEntry[] array, System.Int32 arrayIndex)`

Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`arrayIndex` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` minus `arrayIndex` is less than [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public System.Int32 BinarySearch(UIconEdit.IconEntry entry)`

Performs a binary search for the specified entry within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
* `entry`: The icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `entry`, if found; otherwise, the bitwise complement of the index where `entry` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearch(System.Int32 index, System.Int32 count, UIconEdit.IconEntry entry)`

Performs a binary search for the specified entry within the specified range of elements. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
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
## Method: `public System.Int32 BinarySearchSimilar(UIconEdit.IconEntry entry)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as the specified entry within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
* `entry`: The icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `entry`, if found; otherwise, the bitwise complement of the index where `entry` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(System.Int32 index, System.Int32 count, UIconEdit.IconEntry entry)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as the specified entry within the specified range of elements. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
* `index`: The index in the list at which the search begins.
* `count`: The number of elements to search.
* `entry`: The icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `entry`, if found; otherwise, the bitwise complement of the index where `entry` would be.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(UIconEdit.IconEntryKey key)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as the specified key within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
* `key`: The entry key to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `key`, if found; otherwise, the bitwise complement of the index where `key` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(System.Int32 index, System.Int32 count, UIconEdit.IconEntryKey key)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as the specified key within the specified range of elements. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
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
## Method: `public System.Int32 BinarySearchSimilar(System.Int16 width, System.Int16 height, UIconEdit.IconBitDepth bitDepth)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as the specified key within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `width`, `height`, and `bitDepth`, if found; otherwise, the bitwise complement of the index of where `width`, `height`, and `bitDepth` would be.


--------------------------------------------------
## Method: `public System.Int32 BinarySearchSimilar(System.Int32 index, System.Int32 count, System.Int16 width, System.Int16 height, UIconEdit.IconBitDepth bitDepth)`

Performs a binary search for an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as the specified key within the entire list. This method presumes that the list is already sorted according to each [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
* `index`: The index in the list at which the search begins.
* `count`: The number of elements to search.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an entry with the same [`IconEntry.Width`](#property-public-systemint32-width--get-), [`IconEntry.Height`](#property-public-systemint32-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) as `width`, `height`, and `bitDepth`, if found; otherwise, the bitwise complement of the index of where `width`, `height`, and `bitDepth` would be.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public void Sort()`

Sorts all elements in the list according to their [`IconEntry.EntryKey`](#property-public-uiconediticonentrykey-entrykey--get-) value.
### Remarks

This method raises the [`EntryList.CollectionChanged`](#collectionchanged) event using [`NotifyCollectionChangedAction.Reset`](https://msdn.microsoft.com/en-us/library/system.collections.specialized.notifycollectionchangedaction.reset.aspx) if the list contains more than 2 elements.

--------------------------------------------------
## Method: `public UIconEdit.IconFileBase.EntryList.Enumerator GetEnumerator()`

Returns an enumerator which iterates through the list.

**Returns:** Type [`Enumerator`](#type-struct-uiconediticonfilebaseentrylistenumerator): An enumerator which iterates through the list.


--------------------------------------------------
## Method: `public void Move(System.Int32 oldIndex, System.Int32 newIndex)`

Moves an element from one index to another.
* `oldIndex`: The index of the element to move.
* `newIndex`: The destination index.

--------------------------------------------------
## Method: `public UIconEdit.IconEntry[] ToArray()`

Returns an array containing all elements in the current list.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconEntry`](#type-public-class-uiconediticonentry): An array containing elements copied from the current list.


--------------------------------------------------
## Method: `public UIconEdit.IconEntry Find(System.Predicate<UIconEdit.IconEntry> match)`

Searches for an element which matches the specified predicate, and returns the first matching icon entry in the list.
* `match`: A predicate used to define the element to search for.

**Returns:** Type [`IconEntry`](#type-public-class-uiconediticonentry): An icon entry matching the specified predicate, or `null` if no such icon entry was found.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Int32 FindIndex(System.Predicate<UIconEdit.IconEntry> match)`

Searches for an element which matches the specified predicate, and returns the index of the first matching icon entry in the list.
* `match`: A predicate used to define the element to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of the icon entry matching the specified predicate, or -1 if no such icon entry was found.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Boolean Exists(System.Predicate<UIconEdit.IconEntry> match)`

Determines whether any element matching the specified predicate exists in the list.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if at least one element matches the specified predicate; `false` otherwise.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Boolean TrueForAll(System.Predicate<UIconEdit.IconEntry> match)`

Determines whether every element in the list matches the specified predicate.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if every element in the list matches the specified predicate; `false` otherwise.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Collections.Generic.List<T> FindAll(System.Predicate<UIconEdit.IconEntry> match)`

Returns a list containing all icon entries which match the specified predicate.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`List`](https://msdn.microsoft.com/en-us/library/6sh2ey19.aspx): A list containing all elements matching `match`.


--------------------------------------------------
# Type: `struct UIconEdit.IconFileBase.EntryList.Enumerator`

An enumerator which iterates through the list.

--------------------------------------------------
## Property: `public UIconEdit.IconEntry Current { get; }`

Gets the element at the current position in the enumerator.

--------------------------------------------------
## Method: `public void Dispose()`

Disposes of the current instance.

--------------------------------------------------
## Method: `public System.Boolean MoveNext()`

Advances the enumerator to the next position in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
# Type: `public enum UIconEdit.IconTypeCode`

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
# Type: `public class UIconEdit.CursorFile`

Represents a cursor file.

--------------------------------------------------
## Constructor: `public CursorFile()`

Creates a new [`CursorFile`](#type-public-class-uiconeditcursorfile) instance.

--------------------------------------------------
## Method: `public static UIconEdit.CursorFile Load(System.IO.Stream input)`

Loads a [`CursorFile`](#type-public-class-uiconeditcursorfile) from the specified stream.
* `input`: A stream containing an cursor file.

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): A [`CursorFile`](#type-public-class-uiconeditcursorfile) loaded from `input`.


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
## Method: `public static UIconEdit.CursorFile Load(System.IO.Stream input, UIconEdit.IconLoadExceptionHandler handler)`

Loads a [`CursorFile`](#type-public-class-uiconeditcursorfile) from the specified stream.
* `input`: A stream containing an cursor file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual cursor entries, or `null` to throw an exception in those cases.

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): A [`CursorFile`](#type-public-class-uiconeditcursorfile) loaded from `input`.


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
## Method: `public static UIconEdit.CursorFile Load(System.String path, UIconEdit.IconLoadExceptionHandler handler)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified path.
* `path`: The path to a cursor file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual cursor entries, or `null` to throw an exception in those cases.

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


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

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.CursorFile Load(System.String path)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified path.
* `path`: The path to a cursor file.

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


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

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Method: `public virtual UIconEdit.IconFileBase Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): A duplicate of the current instance, with copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-).


--------------------------------------------------
# Type: `public class UIconEdit.IconFile`

Represents an icon file.

--------------------------------------------------
## Constructor: `public IconFile()`

Creates a new [`IconFile`](#type-public-class-uiconediticonfile) instance.

--------------------------------------------------
## Method: `public static UIconEdit.IconFile Load(System.IO.Stream input)`

Loads a [`IconFile`](#type-public-class-uiconediticonfile) from the specified stream.
* `input`: A stream containing an icon file.

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): A [`IconFile`](#type-public-class-uiconediticonfile) loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFile Load(System.IO.Stream input, UIconEdit.IconLoadExceptionHandler handler)`

Loads a [`IconFile`](#type-public-class-uiconediticonfile) from the specified stream.
* `input`: A stream containing an icon file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): A [`IconFile`](#type-public-class-uiconediticonfile) loaded from `input`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`input` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`input` is closed or does not support reading.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`input` is closed.

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFile Load(System.String path, UIconEdit.IconLoadExceptionHandler handler)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified path.
* `path`: The path to a icon file.
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon entries, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


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

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFile Load(System.String path)`

Loads an [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation from the specified path.
* `path`: The path to a icon file.

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


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

##### [`FileFormatException`](https://msdn.microsoft.com/en-us/library/system.io.fileformatexception.aspx)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Method: `public virtual UIconEdit.IconFileBase Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): A duplicate of the current instance, with copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-).


--------------------------------------------------
# Type: `public class UIconEdit.IconEntry`

Represents a single entry in an icon.

--------------------------------------------------
## Field: `public const System.Byte DefaultAlphaThreshold = 96`

The default [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-) value.

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int32 width, System.Int32 height, UIconEdit.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY, System.Byte alphaThreshold)`

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
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int32 width, System.Int32 height, UIconEdit.IconBitDepth bitDepth, System.Byte alphaThreshold)`

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
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int32 width, System.Int32 height, UIconEdit.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY)`

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
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int32 width, System.Int32 height, UIconEdit.IconBitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY, System.Byte alphaThreshold)`

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
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.IconBitDepth bitDepth, System.Int32 hotspotX, System.Int32 hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the left side. Constrained to be less than the width of `baseImage`.
* `hotspotY`: In a cursor file, the horizontal offset in pixels of the cursor's hotspot from the top. Constrained to be less than the height of `baseImage`.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.IconBitDepth bitDepth, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.IconBitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Method: `protected virtual System.Windows.Freezable CreateInstanceCore()`

Creates a new default [`IconEntry`](#type-public-class-uiconediticonentry) instance.

**Returns:** Type [`Freezable`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.aspx): A default [`IconEntry`](#type-public-class-uiconediticonentry) instance.

### See Also
* [`Freezable.CreateInstanceCore()`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.createinstancecore.aspx)


--------------------------------------------------
## Method: `protected virtual void CloneCore(System.Windows.Freezable sourceFreezable)`

Makes the current instance a deep copy of the specified other object.
* `sourceFreezable`: The object to clone.

--------------------------------------------------
## Method: `protected virtual void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)`

Makes the current instance a deep copy of the specified other object's value.
* `sourceFreezable`: The object to clone.

--------------------------------------------------
## Method: `protected virtual void GetAsFrozenCore(System.Windows.Freezable sourceFreezable)`

Makes the current instance a frozen copy of the specified other object.
* `sourceFreezable`: The object to clone.

--------------------------------------------------
## Method: `protected virtual void GetCurrentValueAsFrozenCore(System.Windows.Freezable sourceFreezable)`

Makes the current instance a frozen copy of the specified other object's value.
* `sourceFreezable`: The object to clone.

--------------------------------------------------
## Field: `public const System.Int16 MinDimension = 1`

The minimum dimensions of an icon. 1 pixels.

--------------------------------------------------
## Field: `public const System.Int16 MaxDimension = 768`

The maximum dimensions of an icon. 768 pixels as of Windows 10.

--------------------------------------------------
## Field: `public const System.Int16 MaxBmp32 = 96`

Gets and sets the maximum width or height at which an icon entry will be saved as a BMP file when [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) is [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0); all entries with a width or height greater than this will be saved as PNG. 96 pixels.

--------------------------------------------------
## Field: `public const System.Int16 MaxBmp = 255`

Gets and sets the maximum width or height at which an icon entry will be saved as a BMP file when [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) is any value except [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0); all entries with a width or height greater than this will be saved as PNG. 255 pixels.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty IsQuantizedProperty`

The dependency property for the read-only [`IconEntry.IsQuantized`](#property-public-systemboolean-isquantized--get-) property.

--------------------------------------------------
## Property: `public System.Boolean IsQuantized { get; }`

Gets a value indicating whether [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) are known to be already quantized.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty BaseImageProperty`

The dependency property for the [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) property.

--------------------------------------------------
## Property: `public System.Windows.Media.Imaging.BitmapSource BaseImage { get; set; }`

Gets and sets the image associated with the current instance.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
In a set operation, the specified value is `null`.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty AlphaImageProperty`

The dependency property for the [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) property.

--------------------------------------------------
## Property: `public System.Windows.Media.Imaging.BitmapSource AlphaImage { get; set; }`

Gets and sets an image to be used as the alpha mask, or `null` to derive the alpha mask from [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-). Black pixels are transparent; white pixels are opaque.

--------------------------------------------------
## Property: `public System.Boolean IsPng { get; }`

Gets a value indicating whether the current instance will be saved as a PNG image within the icon structure by default.

--------------------------------------------------
## Property: `public UIconEdit.IconEntryKey EntryKey { get; }`

Gets a key for the icon entry.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty WidthProperty`

The dependency property for the read-only [`IconEntry.Width`](#property-public-systemint32-width--get-) property.

--------------------------------------------------
## Property: `public System.Int32 Width { get; }`

Gets the resampled width of the icon.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty HeightProperty`

The dependency property for the read-only [`IconEntry.Height`](#property-public-systemint32-height--get-) property.

--------------------------------------------------
## Property: `public System.Int32 Height { get; }`

Gets the resampled height of the icon.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty BitDepthProperty`

The dependency property for the read-only [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-) property.

--------------------------------------------------
## Property: `public UIconEdit.IconBitDepth BitDepth { get; }`

Gets the bit depth of the current instance.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty AlphaThresholdProperty`

The dependency property for the [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-) property.

--------------------------------------------------
## Property: `public System.Byte AlphaThreshold { get; set; }`

Gets and sets a value indicating the threshold of alpha values at [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-)s below [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0). Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty HotspotXProperty`

The dependency property for the [`IconEntry.HotspotX`](#property-public-systemint32-hotspotx--get-set-) property.

--------------------------------------------------
## Property: `public System.Int32 HotspotX { get; set; }`

In a cursor, gets the horizontal offset in pixels of the cursor's hotspot from the left side. Constrained to greater than or equal to 0 and less than or equal to [`IconEntry.Width`](#property-public-systemint32-width--get-).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty HotspotYProperty`

The dependency property for the [`IconEntry.HotspotY`](#property-public-systemint32-hotspoty--get-set-) property.

--------------------------------------------------
## Property: `public System.Int32 HotspotY { get; set; }`

In a cursor, gets the vertical offset in pixels of the cursor's hotspot from the top side. Constrained to greater than or equal to 0 and less than or equal to [`IconEntry.Height`](#property-public-systemint32-height--get-).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty IconScalingFilterProperty`

The dependency property for the [`IconScalingFilter`](#type-public-enum-uiconediticonscalingfilter) property.

--------------------------------------------------
## Property: `public UIconEdit.IconScalingFilter ScalingFilter { get; set; }`

Gets and sets the scaling mode used to resize [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) when quantizing.

### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
In a set operation, the specified value is not a valid [`IconScalingFilter`](#type-public-enum-uiconediticonscalingfilter) value.

--------------------------------------------------
## Property: `public System.Int32 BitsPerPixel { get; }`

Gets the number of bits per pixel specified by [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-).

--------------------------------------------------
## Method: `public static System.Int32 GetBitsPerPixel(UIconEdit.IconBitDepth bitDepth)`

Returns the number of bits per pixel associated with the specified [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.
* `bitDepth`: The [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) to check.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): 1 for [`IconBitDepth.Depth1BitsPerPixel`](#field-iconbitdepthdepth1bitsperpixel--4); 4 for [`IconBitDepth.Depth4BitsPerPixel`](#field-iconbitdepthdepth4bitsperpixel--3); 8 for [`IconBitDepth.Depth8BitsPerPixel`](#field-iconbitdepthdepth8bitsperpixel--2); 24 for [`IconBitDepth.Depth24BitsPerPixel`](#field-iconbitdepthdepth24bitsperpixel--1); or 32 for [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0).


### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

--------------------------------------------------
## Property: `public System.Int64 ColorCount { get; }`

Gets the maximum color count specified by [`IconEntry.BitDepth`](#property-public-uiconediticonbitdepth-bitdepth--get-).

--------------------------------------------------
## Method: `public static System.Int64 GetColorCount(UIconEdit.IconBitDepth bitDepth)`

Gets the maximum color count associated with the specified [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth).
* `bitDepth`: The [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) to check.

**Returns:** Type [`Int64`](https://msdn.microsoft.com/en-us/library/system.int64.aspx): 21 for [`IconBitDepth.Depth2Color`](#field-iconbitdepthdepth2color--4); 16 for [`IconBitDepth.Depth16Color`](#field-iconbitdepthdepth16color--3); 256 for [`IconBitDepth.Depth256Color`](#field-iconbitdepthdepth256color--2); 16777216 for [`IconBitDepth.Depth24BitsPerPixel`](#field-iconbitdepthdepth24bitsperpixel--1); or 4294967296 for [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0).


### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

--------------------------------------------------
## Method: `public static UIconEdit.IconBitDepth GetBitDepth(System.Int64 value)`

Returns the [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) associated with the specified numeric value.
* `value`: The color count or number of bits per pixel to use.

**Returns:** Type [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth): [`IconBitDepth.Depth1BitPerPixel`](#field-iconbitdepthdepth1bitperpixel--4) if `value` is 1 or 2; [`IconBitDepth.Depth4BitsPerPixel`](#field-iconbitdepthdepth4bitsperpixel--3) if `value` is 4 or 16; [`IconBitDepth.Depth8BitsPerPixel`](#field-iconbitdepthdepth8bitsperpixel--2) if `value` is 8 or 256; [`IconBitDepth.Depth24BitsPerPixel`](#field-iconbitdepthdepth24bitsperpixel--1) if `value` is 24 or 16777216; or [`IconBitDepth.Depth32BitsPerPixel`](#field-iconbitdepthdepth32bitsperpixel--0) if `value` is 32 or 4294967296.


### Exceptions

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`value` is not one of the specified parameter values.

--------------------------------------------------
## Method: `public static System.Windows.Media.PixelFormat GetPixelFormat(UIconEdit.IconBitDepth depth)`

Returns the [`PixelFormat`](https://msdn.microsoft.com/en-us/library/system.windows.media.pixelformat.aspx) associated with the specified [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth).
* `depth`: The bit depth from which to get the pixel format.

**Returns:** Type [`PixelFormat`](https://msdn.microsoft.com/en-us/library/system.windows.media.pixelformat.aspx): The pixel format associated with `depth`.


### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`depth` is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

--------------------------------------------------
## Method: `public System.Windows.Media.Imaging.BitmapSource CombineAlpha()`

Applies [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) to [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-).

**Returns:** Type [`BitmapSource`](https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmapsource.aspx): A new [`BitmapSource`](https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmapsource.aspx), sized according to [`IconEntry.Width`](#property-public-systemint32-width--get-) and [`IconEntry.Height`](#property-public-systemint32-height--get-), consisting of [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) applied to [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) and with pixel format [`PixelFormats.Bgra32`](https://msdn.microsoft.com/en-us/library/system.windows.media.pixelformats.bgra32.aspx)


--------------------------------------------------
## Method: `public UIconEdit.IconEntry Clone()`

Returns a modifiable copy of the current instance. When copying this object's dependency properties, this method copies expressions (which might no longer resolve), but not animations or their current values.

**Returns:** Type [`IconEntry`](#type-public-class-uiconediticonentry): A modifiable copy of the current instance.

### See Also
* [`Freezable`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.aspx)
* [`Freezable.Clone()`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.clone.aspx)


--------------------------------------------------
## Method: `public UIconEdit.IconEntry CloneCurrentValue()`

Returns a modifiable copy of the current instance using its curent values.

**Returns:** Type [`IconEntry`](#type-public-class-uiconediticonentry): A modifiable copy of the current instance.

### See Also
* [`Freezable`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.aspx)
* [`Freezable.CloneCurrentValue()`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.clonecurrentvalue.aspx)


--------------------------------------------------
## Method: `public UIconEdit.IconEntry GetAsFrozen()`

Creates a frozen copy of the current instance, using base (non-animated) property values.

**Returns:** Type [`IconEntry`](#type-public-class-uiconediticonentry): A frozen copy of the current instance.

### See Also
* [`Freezable`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.aspx)
* [`Freezable.GetAsFrozen()`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.getasfrozen.aspx)


--------------------------------------------------
## Method: `public UIconEdit.IconEntry GetCurrentValueAsFrozen()`

Creates a frozen copy of the current instance, using current property values.

**Returns:** Type [`IconEntry`](#type-public-class-uiconediticonentry): A frozen copy of the current instance.

### See Also
* [`Freezable`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.aspx)
* [`Freezable.GetCurrentValueAsFrozen()`](https://msdn.microsoft.com/en-us/library/system.windows.freezable.getcurrentvalueasfrozen.aspx)


--------------------------------------------------
## Method: `public System.Windows.Media.Imaging.BitmapSource GetQuantizedPng()`

Returns color quantization of the current instance as it would appear for a PNG entry.

**Returns:** Type [`BitmapSource`](https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmapsource.aspx): A [`BitmapSource`](https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmapsource.aspx) containing the quantized image.


--------------------------------------------------
## Method: `public void SetQuantized(System.Boolean isPng)`

Sets [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) equal to their quantized equivalent, in a form indicated by the specified value.
* `isPng`: If `true`, [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) will be set `null` and [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) will be quantized as if it was a PNG icon entry. If `false`, [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) will be quantized as if for a BMP entry.

### Exceptions

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)
The current instance is frozen.

--------------------------------------------------
## Method: `public void SetQuantized()`

Sets [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) equal to their quantized equivalent, in a form indicated by [`IconEntry.IsPng`](#property-public-systemboolean-ispng--get-).
### Remarks

Performs the same action as [`IconEntry.SetQuantized(System.Boolean)`](#method-public-void-setquantizedsystemboolean-ispng), with [`IconEntry.IsPng`](#property-public-systemboolean-ispng--get-) passed as the parameter.

### Exceptions

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)
The current instance is frozen.

--------------------------------------------------
## Method: `IconEntry.GetQuantized(out System.Windows.Media.Imaging.BitmapSource alphaMask)`

Returns color quantization of the current instance as it would appear for a BMP entry.
* `alphaMask`: When this method returns, contains the quantized alpha mask generated using [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-). Black pixels are transparent; white pixels are opaque. This parameter is passed uninitialized.

**Returns:** A [`BitmapSource`](https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmapsource.aspx) containing the quantized image without the alpha mask.


--------------------------------------------------
## Method: `public virtual System.String ToString()`

Returns a string representation of the current instance.

**Returns:** Type [`String`](https://msdn.microsoft.com/en-us/library/system.string.aspx): A string representation of the current instance.


--------------------------------------------------
## Method: `public static UIconEdit.IconBitDepth ParseBitDepth(System.String value)`

Parses the specified string as a [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.
* `value`: The value to parse.

**Returns:** Type [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth): The parsed [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`value` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)

`value` is an empty string or contains only whitespace.

-OR-

`value` does not translate to a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

### Remarks


`value` is parsed in a case-insensitive manner which works differently from [`Enum.Parse()`](https://msdn.microsoft.com/en-us/library/kxydatf9.aspx).

First of all, all non-alphanumeric characters are stripped. If `value` is entirely numeric, or begins with "Depth" followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the integer [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.

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
## Method: `IconEntry.TryParseBitDepth(System.String value, out UIconEdit.IconBitDepth result)`

Parses the specified string as a [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.
* `value`: The value to parse.
* `result`: When this method returns, contains the parsed [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) result, or the default value for type [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) if `value` could not be parsed. This parameter is passed uninitialized.

**Returns:** `true` if `value` was successfully parsed; `false` otherwise.

### Remarks


`value` is parsed in a case-insensitive manner which works differently from [`Enum.TryParse()`](https://msdn.microsoft.com/en-us/library/dd991317.aspx).

First of all, all non-alphanumeric characters are stripped. If `value` is entirely numeric, or begins with "Depth" followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the integer [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.

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
# Type: `public struct UIconEdit.IconEntryKey`

Represents a simplified key for an icon entry.

--------------------------------------------------
## Constructor: `public IconEntryKey(System.Int32 width, System.Int32 height, UIconEdit.IconBitDepth bitDepth)`

Creates a new instance.
* `width`: The width of the icon entry.
* `height`: The height of the icon entry.
* `bitDepth`: The bit depth of the icon entry.

--------------------------------------------------
## Field: `public System.Int32 Width`

Indicates the width of the icon entry.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is less than or equal to 0.

--------------------------------------------------
## Field: `public System.Int32 Height`

Indicates the height of the icon entry.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is less than or equal to 0.

--------------------------------------------------
## Field: `public UIconEdit.IconBitDepth BitDepth`

Indicates the bit depth of the icon entry.

### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
In a set operation, the specified value is not a valid [`IconBitDepth`](#type-public-enum-uiconediticonbitdepth) value.

--------------------------------------------------
## Method: `public System.Int32 CompareTo(UIconEdit.IconEntryKey other)`

Compares the current instance to the specified other [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) object. First [`IconEntryKey.BitDepth`](#field-public-uiconediticonbitdepth-bitdepth) is compared, then [`IconEntryKey.Height`](#field-public-systemint32-height), then [`IconEntryKey.Width`](#field-public-systemint32-width) (with higher color-counts and larger elements first).
* `other`: The other [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): A value less than 0 if the current value comes before `other`; a value greater than 0 if the current value comes after `other`; or 0 if the current instance is equal to `other`.


--------------------------------------------------
## Field: `public static readonly UIconEdit.IconEntry Empty`

Returns an invalid [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) with all values equal to 0.

--------------------------------------------------
## Method: `public System.String ToString()`

Returns a string representation of the current value.

**Returns:** Type [`String`](https://msdn.microsoft.com/en-us/library/system.string.aspx): A string representation of the current value.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThan(UIconEdit.IconEntryKey f1, UIconEdit.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThan(UIconEdit.IconEntryKey f1, UIconEdit.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is greater than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThanOrEqual(UIconEdit.IconEntryKey f1, UIconEdit.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThanOrEqual(UIconEdit.IconEntryKey f1, UIconEdit.IconEntryKey f2)`

Compares two [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean Equals(UIconEdit.IconEntryKey other)`

Determines if the current instance is equal to the specified other [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) value.
* `other`: The other [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

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
## Operator: `public static System.Boolean op_Equality(UIconEdit.IconEntryKey f1, UIconEdit.IconEntryKey f2)`

Determines equality of two [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_Inequality(UIconEdit.IconEntryKey f1, UIconEdit.IconEntryKey f2)`

Determines inequality of two [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) objects.
* `f1`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.
* `f2`: An [`IconEntryKey`](#type-public-struct-uiconediticonentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is not equal to `f2`; `false` otherwise.


--------------------------------------------------
# Type: `public enum UIconEdit.IconBitDepth`

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
# Type: `public enum UIconEdit.IconScalingFilter`

Indicates options for resizing [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) and [`IconEntry.AlphaImage`](#property-public-systemwindowsmediaimagingbitmapsource-alphaimage--get-set-) when quantizing.

--------------------------------------------------
## Field: `IconScalingFilter.Matrix = 0`

Resizes using a transformation matrix.

--------------------------------------------------
## Field: `IconScalingFilter.Bilinear = 1`

Specifies bilinear interpolation.

--------------------------------------------------
## Field: `IconScalingFilter.Bicubic = 2`

Specifies bicubic interpolation.

--------------------------------------------------
## Field: `IconScalingFilter.NearestNeighbor = 3`

Specifies nearest-neighbor interpolation.

--------------------------------------------------
## Field: `IconScalingFilter.HighQualityBilinear = 4`

Specifies high-quality bilinear interpolation. Prefiltering is performed to ensure high-quality transformation.

--------------------------------------------------
## Field: `IconScalingFilter.HighQualityBicubic = 5`

Specifies high-quality bicubic interpolation. Prefiltering is performed to ensure high-quality transformation.

--------------------------------------------------
# Type: `public class UIconEdit.IconLoadException`

The exception that is thrown when an icon file contains invalid data.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode, System.Object value, System.Int32 entryIndex)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode, System.Object value)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode, System.Int32 entryIndex)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode, System.Int32 entryIndex)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode, System.Object value, System.Int32 entryIndex)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode, System.Object value)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode typeCode, System.Int32 entryIndex, System.Exception innerException)`

Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `typeCode`: The type code of the file which caused the error.
* `entryIndex`: The index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, UIconEdit.IconTypeCode innerException, System.Exception typeCode)`

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
## Property: `public virtual System.String Message { get; }`

Gets a message describing the error.

--------------------------------------------------
## Property: `public System.Int32 EntryIndex { get; }`

Gets the index in the icon file of the entry in the icon directory which caused the exception, or a value less than 0 if the error was not caused by an icon entry.
### Remarks

This refers to the index of the entry within the list of icon directory entries; it may not refer to the position of the image within the rest of the icon file.

--------------------------------------------------
## Property: `public UIconEdit.IconErrorCode Code { get; }`

Gets an error code describing the icon error.

--------------------------------------------------
## Property: `public System.Object Value { get; }`

Gets an object whose value caused the error, or `null` if there was no such value.

--------------------------------------------------
## Property: `public UIconEdit.IconTypeCode TypeCode { get; }`

Gets a value indicating the type of the icon file.

--------------------------------------------------
# Type: `public class UIconEdit.IconExtractException`

The exception that is thrown when an icon file extracted from an EXE or DLL file contains invalid data.

--------------------------------------------------
## Property: `public virtual System.String Message { get; }`

Gets a message describing the error.

--------------------------------------------------
## Property: `public System.Int32 ExtractIndex { get; }`

Gets the index in the DLL or EXE file of the icon or cursor which caused the error.

--------------------------------------------------
# Type: `public enum UIconEdit.IconErrorCode`

Indicates the cause of an [`IconLoadException`](#type-public-class-uiconediticonloadexception).

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

Code 0x3: An icon was expected but a cursor was loaded, or vice versa. [`IconLoadException.TypeCode`](#property-public-uiconediticontypecode-typecode--get-) contains the expected value, and [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the actual value. This is a fatal error, and the icon file cannot continue processing when it occurs.

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
# Type: `public class UIconEdit.IconLoadExceptionHandler`

A delegate function for handling [`IconLoadException`](#type-public-class-uiconediticonloadexception) errors.
* `e`: An [`IconLoadException`](#type-public-class-uiconediticonloadexception) containing information about the error.

--------------------------------------------------
# Type: `public class UIconEdit.IconExtractExceptionHandler`

A delegate function for handling [`IconExtractException`](#type-public-class-uiconediticonextractexception) errors.
* `e`: An [`IconExtractException`](#type-public-class-uiconediticonextractexception) containing information about the error.

--------------------------------------------------
# Type: `public abstract class UIconEdit.IconExtraction`

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
## Method: `public static UIconEdit.IconFile ExtractIconSingle(System.String path, System.Int32 index, UIconEdit.IconLoadExceptionHandler handler)`

Extracts a single icon from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The zero-based index of the icon in `path`.
* `handler`: A delegate used to handle non-fatal [`IconLoadException`](#type-public-class-uiconediticonloadexception) errors, or `null` to always throw an exception.

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): The icon with the specified key in `path`.


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
## Method: `public static UIconEdit.IconFile ExtractIconSingle(System.String path, System.Int32 index)`

Extracts a single icon from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The index of the icon in `path`.

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): The icon with the specified key in `path`.


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
## Method: `public static UIconEdit.CursorFile ExtractCursorSingle(System.String path, System.Int32 index, UIconEdit.IconLoadExceptionHandler handler)`

Extracts a single cursor from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The index of the cursor in `path`.
* `handler`: A delegate used to handle non-fatal [`IconLoadException`](#type-public-class-uiconediticonloadexception) errors, or `null` to always throw an exception.

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): The cursor with the specified key in `path`.


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
## Method: `public static UIconEdit.CursorFile ExtractCursorSingle(System.String path, System.Int32 index)`

Extracts a single cursor from the specified EXE or DLL file.
* `path`: The path to the file to load.
* `index`: The [`IntPtr`](https://msdn.microsoft.com/en-us/library/system.intptr.aspx) key of the cursor in `path`.

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): The cursor with the specified key in `path`.


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
## Method: `public static UIconEdit.IconFile[] ExtractAllIcons(System.String path, UIconEdit.IconExtractExceptionHandler singleHandler, UIconEdit.IconExtractExceptionHandler allHandler)`

Extracts all icons from the specified EXE or DLL file.
* `path`: The path to the file from which to load all icons.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown by a single icon entry in a single icon file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single icon entry in an icon file, or `null` to always throw an exception regardless.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconFile`](#type-public-class-uiconediticonfile): An array containing all icon files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFile[] ExtractAllIcons(System.String path)`

Extracts all icons from the specified EXE or DLL file.
* `path`: The path to the file from which to load all icons.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconFile`](#type-public-class-uiconediticonfile): An array containing all icon files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.CursorFile[] ExtractAllCursors(System.String path, UIconEdit.IconExtractExceptionHandler singleHandler, UIconEdit.IconExtractExceptionHandler allHandler)`

Extracts all cursors from the specified EXE or DLL file.
* `path`: The path to the file from which to load all cursors.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown by a single cursor entry in a single cursor file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single cursor entry in a cursor file, or `null` to always throw an exception regardless.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`CursorFile`](#type-public-class-uiconeditcursorfile): An array containing all cursor files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.CursorFile[] ExtractAllCursors(System.String path)`

Extracts all cursors from the specified EXE or DLL file.
* `path`: The path to the file from which to load all cursors.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`CursorFile`](#type-public-class-uiconeditcursorfile): An array containing all cursor files that could be loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractIconsForEach(System.String path, UIconEdit.IconExtractCallback<UIconEdit.IconFile> callback, UIconEdit.IconExtractExceptionHandler singleHandler, UIconEdit.IconExtractExceptionHandler allHandler)`

Iterates through each icon in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all icons.
* `callback`: An action to perform on each icon.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown by a single icon entry in a single cursor file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single icon entry in an icon file, or `null` to always throw an exception regardless.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractIconsForEach(System.String path, UIconEdit.IconExtractCallback<UIconEdit.IconFile> callback)`

Iterates through each icon in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all icons.
* `callback`: An action to perform on each icon.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading an icon.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractCursorsForEach(System.String path, UIconEdit.IconExtractCallback<UIconEdit.CursorFile> callback, UIconEdit.IconExtractExceptionHandler singleHandler, UIconEdit.IconExtractExceptionHandler allHandler)`

Iterates through each cursor in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all cursors.
* `callback`: An action to perform on each cursor.
* `singleHandler`: A delegate used to handle [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown by a single icon entry in a single cursor file, or `null` to always throw an exception regardless.
* `allHandler`: A delegate used to handle all other excpetions thrown by a single icon entry in a cursor file, or `null` to always throw an exception regardless.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static void ExtractCursorsForEach(System.String path, UIconEdit.IconExtractCallback<UIconEdit.CursorFile> callback)`

Iterates through each cursor in the specified EXE or DLL file, and performs the specified action on each one.
* `path`: The path to the file from which to load all cursors.
* `callback`: An action to perform on each cursor.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` or `callback` is `null`.

##### [`Win32Exception`](https://msdn.microsoft.com/en-us/library/system.componentmodel.win32exception.aspx)
An error occurred when attempting to load resources from `path`.

##### [`IconExtractException`](#type-public-class-uiconediticonextractexception)
An error occurred when loading a cursor.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
# Type: `public class UIconEdit.IconExtractCallback`1`

A delegate function to perform on each cursor or icon extracted from a DLL or EXE file.The type of the [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation.
* `index`: The index of the current cursor or icon to process.
* `iconFile`: The cursor or icon which was extracted.
* `e`: A [`CancelEventArgs`](https://msdn.microsoft.com/en-us/library/system.componentmodel.canceleventargs.aspx) object which is used to cancel
