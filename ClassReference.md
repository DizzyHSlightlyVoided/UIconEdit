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
## Method: `public static UIconEdit.IconFileBase Load(System.IO.Stream input, System.Action<UIconEdit.IconLoadException> handler)`

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
## Method: `public static UIconEdit.IconFileBase Load(System.String path, System.Action<UIconEdit.IconLoadException> handler)`

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

Returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): A duplicate of the current instance, with copies of every icon entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) in [`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-).


--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

When overridden in a derived class, gets the 16-bit identifier for the file type.

--------------------------------------------------
## Property: `public UIconEdit.IconFileBase.EntryList Entries { get; }`

Gets a collection containing all entries in the icon file.

--------------------------------------------------
## Method: `protected virtual System.Boolean IsValid(UIconEdit.IconEntry entry)`

When overridden in a derived class, gets a value indicating whether the specified value may be added to [`IconFileBase.Entries`](#property-public-uiconediticonfilebaseentrylist-entries--get-).
* `entry`: The entry to check.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `entry` is not `null`; `false` otherwise.


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgX(UIconEdit.IconEntry entry)`

When overridden in a derived class, computes the 16-bit X component.
* `entry`: The image entry to calculate.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): In icon files, the color panes. In cursor files, the horizontal offset of the hotspot from the left in pixels.


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgY(UIconEdit.IconEntry entry)`

When overridden in a derived class, computes the 16-bit Y component.
* `entry`: The image entry to calculate.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): In icon files, the number of bits per pixel. In cursor files, the vertical offset of the hotspot from the top, in pixels.


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

Represents a list of entries. This collection treats [`IconEntry`](#type-public-class-uiconediticonentry) objects with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as though they were equal.

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
In a set operation, an element with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) already exists in the list at a different index, or the specified value is already associated with a different icon file.

--------------------------------------------------
## Property: `public virtual System.Int32 Count { get; }`

Gets the number of elements in the list.

--------------------------------------------------
## Method: `public System.Boolean Add(UIconEdit.IconEntry item)`

Adds the specified icon entry to the list.
* `item`: The icon entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get-) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) already exists in the list.


--------------------------------------------------
## Method: `public System.Boolean Insert(System.Int32 index, UIconEdit.IconEntry item)`

Adds the specified icon entry to the list at the specified index.
* `index`: The index at which to insert the icon entry.
* `item`: The icon entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get-) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) already exists in the list.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public System.Boolean SetValue(System.Int32 index, UIconEdit.IconEntry item)`

Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The item to set at the specified index.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully set; `false` if `item` is `null`, is already associated with a different icon file, or if an element with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) already exists at a different index.


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

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `item` was successfully found and removed; `false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconEdit.EntryKey key)`

Removes an icon entry similar to the specified value from the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `key` was successfully found and removed; `false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Removes an icon entry similar to the specified values from the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint16-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `bitDepth` was successfully found and removed;`false` if no such icon entry was found in the list.


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

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconEdit.EntryKey key)`

Determines if an element similar to the specified value exists in the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `key` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Determines if an element similar to the specified values exists in the list.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint16-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `bitDepth` was found;`false` if no such icon entry was found in the list.


--------------------------------------------------
## Method: `public virtual System.Int32 IndexOf(UIconEdit.IconEntry item)`

Gets the index of the specified item.
* `item`: The icon entry to search for in the list.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.IconEntry item)`

Gets the index of an element similar to the specified item.
* `item`: The icon entry to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.EntryKey key)`

Gets the index of an element similar to the specified value.
* `key`: The entry key to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `key`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Gets the index of an element similar to the specified values.
* `width`: The width of the icon entry to search for.
* `height`: The height of the icon entry to search for.
* `bitDepth`: The bit depth of the icon entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint16-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `bitDepth`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public void CopyTo(UIconEdit.IconEntry[] array)`

Copies all elements in the list to the specified array.
* `array`: The array to which all elements in the list will be copied.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` is less than [`EntryList.Count`](#property-public-virtual-systemint32-count--get-).

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
## Method: `public void CopyTo(System.Int32 index, UIconEdit.IconEntry[] array, System.Int32 arrayIndex, System.Int32 count)`

Copies a range of elements in the list to the specified array.
* `index`: The index of the first item to copy.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.
* `count`: The number of elements to copy.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index`, `arrayIndex`, or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)

`index` and `count` do not indicate a valid range of elements in the current instance.

-OR-

`arrayIndex` and `count` do not indicate a valid range of elements in `array`.


--------------------------------------------------
## Method: `public UIconEdit.IconFileBase.EntryList.Enumerator GetEnumerator()`

Returns an enumerator which iterates through the list.

**Returns:** Type [`Enumerator`](#type-struct-uiconediticonfilebaseentrylistenumerator): An enumerator which iterates through the list.


--------------------------------------------------
## Method: `public UIconEdit.IconEntry[] ToArray()`

Returns an array containing all elements in the current list.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconEntry`](#type-public-class-uiconediticonentry): An array containing elements copied from the current list.


--------------------------------------------------
## Method: `public void RemoveRange(System.Int32 index, System.Int32 count)`

Removes a range of elements from the list.
* `index`: The zero-based starting index of the elements to remove.
* `count`: The number of elements to remove.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public System.Int32 RemoveWhere(System.Predicate<UIconEdit.IconEntry> match)`

Removes all elements matching the specified predicate.
* `match`: A predicate used to define the elements to remove.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of elements which were removed.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

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
## Method: `public void Sort()`

Sorts all elements in the list according to their [`IconEntry.EntryKey`](#property-public-uiconeditentrykey-entrykey--get-) value.

--------------------------------------------------
## Method: `public void Sort(System.Collections.Generic.IComparer<UIconEdit.IconEntry> comparer)`

Sorts all elements in the list according to the specified comparer.
* `comparer`: The comparer used to compare each [`IconEntry`](#type-public-class-uiconediticonentry), or `null` to their [`IconEntry.EntryKey`](#property-public-uiconeditentrykey-entrykey--get-) value.

--------------------------------------------------
## Method: `public void Sort(System.Comparison<UIconEdit.IconEntry> comparison)`

Sorts all elements in the list according to the specified delegate.
* `comparison`: The delegate used to compare each [`IconEntry`](#type-public-class-uiconediticonentry).

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`comparison` is `null`.

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
## Field: `IconTypeCode.Icon = 1`

Indicates an icon (.ICO) file.

--------------------------------------------------
## Field: `IconTypeCode.Cursor = 2`

Indicates a cursor (.CUR) file.

--------------------------------------------------
# Type: `public class UIconEdit.IconLoadException`

The exception that is thrown when an icon file contains invalid data.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, System.Object value, System.Int32 index)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.
* `index`: The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, System.Object value)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, System.Int32 index)`

Creates a new instance using a message which describes the error and the specified error code.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `index`: The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, System.Object value, System.Int32 index)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.
* `index`: The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, System.Int32 index)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `index`: The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, System.Object value)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, UIconEdit.IconErrorCode code, System.Int32 index, System.Exception innerException)`

Creates a new instance with the specified message and error code and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `code`: The error code used to identify the cause of the error.
* `index`: The index of the entry in the icon file which caused this error, or -1 if it occurred before processing the icon entries.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.

--------------------------------------------------
## Constructor: `public IconLoadException(System.String message, System.Exception innerException)`

Creates a new instance with the specified message and a reference to the exception which caused this error.
* `message`: A message describing the error.
* `innerException`: The exception that is the cause of the current exception. If the `innerException` parameter is not `null`, the current exception should be raised in a `catch` block which handles the inner exception.

--------------------------------------------------
## Property: `public virtual System.String Message { get; }`

Gets a message describing the error.

--------------------------------------------------
## Property: `public UIconEdit.IconErrorCode Code { get; }`

Gets an error code describing the icon error.

--------------------------------------------------
## Property: `public System.Int32 Index { get; }`

Gets the index in the icon file of the icon entry which caused this exception, or -1 if it occurred before the icon entries were processed.

--------------------------------------------------
## Property: `public System.Object Value { get; }`

Gets an object whose value caused the error, or `null` if there was no such value.

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
## Field: `IconErrorCode.ResourceTooSmall = 3`

Code 0x3: One of the icon directory entries has a length less than or equal to 40 bytes, which is logically too small for either a BMP or a PNG file. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the length. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.ResourceTooEarly = 4`

Code 0x4: One of the icon directory entries has a starting offset which would overlap with the list of entries. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the offset. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.ResourceOverlap = 5`

Code 0x5: One or more of the icon directory entries have overlapping offset/length combinations. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.InvalidEntryType = 4096`

Code 0x1000: the file type of an entry is invalid.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBitDepth = 4097`

Code 0x1001: the file is an icon, and an icon directory entry has a bit depth with any value other than 0, 1, 4, 8, 24, or 32. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the bit depth.

--------------------------------------------------
## Field: `IconErrorCode.ZeroValidEntries = 4098`

There are no remaining valid entries after processing. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `IconErrorCode.InvalidPngFile = 4352`

Code 0x1100: an error occurred when attempting to load a PNG entry. The inner exception may contain more information.

--------------------------------------------------
## Field: `IconErrorCode.InvalidPngSize = 4354`

Code 0x1102: the width or height of a PNG entry is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768). [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the size of the image.

--------------------------------------------------
## Field: `IconErrorCode.PngSizeMismatch = 4355`

Code 0x1105: the width or height of a PNG entry does not match the width or height listed in the icon directory entry.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpFile = 4608`

Code 0x1204: an error occurred when attempting to process a BMP entry. The inner exception may contain more information. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains a [`Tuple<T1,T2>`](https://msdn.microsoft.com/en-us/library/dd268536.aspx) in which the [`System.Tuple<T1,T2>.Item1`](https://msdn.microsoft.com/en-us/library/dd386940.aspx) is the size listed in the icon directory entry, and [`System.Tuple<T1,T2>.Item2`](https://msdn.microsoft.com/en-us/library/dd386892.aspx) is the actual size.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpBitDepth = 4609`

Code 0x1201 the bit depth of a BMP entry is not supported. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the bit depth.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpSize = 4610`

Code 0x1202: the width or height of a BMP entry is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768). The maximum height is doubled in images with a bit depth less than 32. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the size of the image.

--------------------------------------------------
## Field: `IconErrorCode.BmpHeightMismatch = 4611`

Code 0x1203: the width or height of a BMP entry does not match the width or height listed in the icon directory entry. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains a [`Tuple<T1,T2>`](https://msdn.microsoft.com/en-us/library/dd268536.aspx) in which the [`System.Tuple<T1,T2>.Item1`](https://msdn.microsoft.com/en-us/library/dd386940.aspx) is the size listed in the icon directory entry, and [`System.Tuple<T1,T2>.Item2`](https://msdn.microsoft.com/en-us/library/dd386892.aspx) is the actual size.

--------------------------------------------------
## Field: `IconErrorCode.InvalidBmpHeightOdd = 4612`

Code 0x1204: the height of a BMP entry is an odd number, indicating that there is no AND (transparency) mask. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains the [`Image.Height`](https://msdn.microsoft.com/en-us/library/system.drawing.image.height.aspx) of the image.

--------------------------------------------------
## Field: `IconErrorCode.BmpBitDepthMismatch = 4613`

Code 0x1205: there is a mismatch between the bit depth of a BMP entry and the expected bit depth of the file. [`IconLoadException.Value`](#property-public-systemobject-value--get-) contains a [`Tuple<T1,T2>`](https://msdn.microsoft.com/en-us/library/dd268536.aspx) in which the [`System.Tuple<T1,T2>.Item1`](https://msdn.microsoft.com/en-us/library/dd386940.aspx) is the bit depth listed in the icon directory entry, and [`System.Tuple<T1,T2>.Item2`](https://msdn.microsoft.com/en-us/library/dd386892.aspx) is the bit depth listed in the BMP entry.

--------------------------------------------------
# Type: `public class UIconEdit.IconLoadExceptionHandler`

A delegate function for handling [`IconLoadException`](#type-public-class-uiconediticonloadexception) errors.
* `e`: An [`IconLoadException`](#type-public-class-uiconediticonloadexception) containing information about the error.

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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.CursorFile Load(System.IO.Stream input, System.Action<UIconEdit.IconLoadException> handler)`

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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.CursorFile Load(System.String path, System.Action<UIconEdit.IconLoadException> handler)`

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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the cursor file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Property: `public UIconEdit.CursorFile.EntryList Entries { get; }`

Gets a collection containing all entries in the cursor file.

--------------------------------------------------
## Method: `protected virtual System.Boolean IsValid(UIconEdit.IconEntry entry)`

Gets a valid indicating whether the specified instance is a valid [`CursorEntry`](#type-public-class-uiconeditcursorentry) object.
* `entry`: The cursor entry to test.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `entry` is a [`CursorEntry`](#type-public-class-uiconeditcursorentry) instance; `false` otherwise.


--------------------------------------------------
## Method: `public virtual UIconEdit.IconFileBase Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): A duplicate of the current instance, with copies of every cursor entry and clones of each entry's [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-) in [`CursorFile.Entries`](#property-public-uiconeditcursorfileentrylist-entries--get-).


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgX(UIconEdit.IconEntry entry)`

Gets the horizontal offset of the hotspot in the specified entry from the left of the specified cursor entry.
* `entry`: The entry from which to get the horizontal offset.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): The horizontal offset of the hotspot from the left in pixels.


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgY(UIconEdit.IconEntry entry)`

Gets the vertical offset of the hotspot in the specified entry from the top of the specified cursor entry.
* `entry`: The entry from which to get the horizontal offset.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): The vertical offset of the hotspot from the top in pixels.


--------------------------------------------------
# Type: `class UIconEdit.CursorFile.EntryList`

Represents a list of cursor entries.

--------------------------------------------------
## Property: `public virtual System.Int32 Count { get; }`

Gets the number of entries contained in the list.

--------------------------------------------------
## Property: `CursorFile.EntryList.Item(System.Int32 index)`

Gets and sets the cursor entry at the specified index.
* `index`: The cursor entry at the specified index.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

`index` is less than 0 or is greater than or equal to [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

-OR-

In a set operation, the specified value is `null`.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
In a set operation, the specified value is `null` or is already associated with a different cursor file.

--------------------------------------------------
## Method: `public System.Boolean SetValue(System.Int32 index, UIconEdit.CursorEntry item)`

Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The value to set.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully set; `false` if `item` is `null`, is already associated with a different icon file, or if an element with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) already exists in the list at a different index.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public System.Boolean Add(UIconEdit.CursorEntry item)`

Adds the specified cursor entry to the list.
* `item`: The cursor entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) already exists in the list.


--------------------------------------------------
## Method: `public System.Boolean Insert(System.Int32 index, UIconEdit.CursorEntry item)`

Inserts the specified cursor entry into the list at the specified index.
* `index`: The index at which the cursor entry will be inserted.
* `item`: The cursor entry to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1) is equal to [`UInt16.MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) already exists in the list.


--------------------------------------------------
## Method: `public virtual void RemoveAt(System.Int32 index)`

Removes the element at the specified index.
* `index`: The index of the cursor entry to remove.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public virtual System.Boolean Remove(UIconEdit.CursorEntry item)`

Removes the specified cursor entry from the list.
* `item`: The cursor entry to to remove from the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconEdit.CursorEntry item)`

Removes an element similar to the specified cursor entry from the list.
* `item`: The cursor entry to to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if a cursor entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `item` was successfully found and removed; `false` if no such cursor entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconEdit.EntryKey key)`

Removes a cursor entry similar to the specified value from the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if a cursor entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `key` was successfully found and removed; `false` if no such cursor entry was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Removes a cursor entry similar to the specified values from the list.
* `width`: The width of the cursor entry to search for.
* `height`: The height of the cursor entry to search for.
* `bitDepth`: The bit depth of the cursor entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if a cursor entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint16-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `bitDepth` was successfully found and removed;`false` if no such cursor entry was found in the list.


--------------------------------------------------
## Method: `public void CopyTo(UIconEdit.CursorEntry[] array)`

Copies all elements in the list to the specified array.
* `array`: The array to which all elements in the list will be copied.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` is less than [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public virtual void CopyTo(UIconEdit.CursorEntry[] array, System.Int32 arrayIndex)`

Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`arrayIndex` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` minus `arrayIndex` is less than [`EntryList.Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public void CopyTo(System.Int32 index, UIconEdit.CursorEntry[] array, System.Int32 arrayIndex, System.Int32 count)`

Copies a range of elements in the list to the specified array.
* `index`: The index of the first item to copy.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.
* `count`: The number of elements to copy.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index`, `arrayIndex`, or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)

`index` and `count` do not indicate a valid range of elements in the current instance.

-OR-

`arrayIndex` and `count` do not indicate a valid range of elements in `array`.


--------------------------------------------------
## Method: `public virtual void Clear()`

Removes all elements from the collection.

--------------------------------------------------
## Method: `public virtual System.Boolean Contains(UIconEdit.CursorEntry item)`

Determines if the specified cursor entry exists in the list.
* `item`: The cursor entry to search for in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconEdit.CursorEntry item)`

Determines if an element similar to the specified cursor entry exists in the list.
* `item`: The cursor entry to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if a cursor entry with the same with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconEdit.EntryKey key)`

Determines if an element similar to the specified value exists in the list.
* `key`: The entry key to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if a cursor entry with the same with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `key` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Determines if an element similar to the specified values exists in the list.
* `width`: The width of the cursor entry to search for.
* `height`: The height of the cursor entry to search for.
* `bitDepth`: The bit depth of the cursor entry to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if a cursor entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint16-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `bitDepth` was found;`false` if no such cursor entry was found in the list.


--------------------------------------------------
## Method: `public virtual System.Int32 IndexOf(UIconEdit.CursorEntry item)`

Gets the index of the specified cursor entry.
* `item`: The cursor entry to search for in the list.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.CursorEntry item)`

Gets the index of an element similar to the specified cursor entry.
* `item`: The cursor entry to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of a cursor entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.EntryKey key)`

Gets the index of an element similar to the specified value.
* `key`: The entry key to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of a cursor entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-), [`IconEntry.Height`](#property-public-systemint16-height--get-), and [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `key`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Gets the index of an element similar to the specified values.
* `width`: The width of the cursor entry to search for.
* `height`: The height of the cursor entry to search for.
* `bitDepth`: The bit depth of the cursor entry to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of a cursor entry with the same [`IconEntry.Width`](#property-public-systemint16-width--get-) as `width`, the same [`IconEntry.Height`](#property-public-systemint16-height--get-) as `height`, and the same [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) as `bitDepth`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public UIconEdit.CursorFile.EntryList.Enumerator GetEnumerator()`

Returns an enumerator which iterates through the list.

**Returns:** Type [`Enumerator`](#type-struct-uiconeditcursorfileentrylistenumerator): An enumerator which iterates through the list.


--------------------------------------------------
## Method: `public void RemoveRange(System.Int32 index, System.Int32 count)`

Removes a range of elements from the list.
* `index`: The zero-based starting index of the elements to remove.
* `count`: The number of elements to remove.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` or `count` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`index` and `count` do not indicate a valid range of elements in the list.

--------------------------------------------------
## Method: `public System.Int32 RemoveWhere(System.Predicate<UIconEdit.CursorEntry> match)`

Removes all elements matching the specified predicate.
* `match`: A predicate used to define the elements to remove.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of elements which were removed.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public UIconEdit.CursorEntry Find(System.Predicate<UIconEdit.CursorEntry> match)`

Searches for an element which matches the specified predicate, and returns the first matching cursor entry in the list.
* `match`: A predicate used to define the element to search for.

**Returns:** Type [`CursorEntry`](#type-public-class-uiconeditcursorentry): A cursor entry matching the specified predicate, or `null` if no such cursor entry was found.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Int32 FindIndex(System.Predicate<UIconEdit.CursorEntry> match)`

Searches for an element which matches the specified predicate, and returns the index of the first matching cursor entry in the list.
* `match`: A predicate used to define the element to search for.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of the cursor entry matching the specified predicate, or -1 if no such cursor entry was found.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Boolean Exists(System.Predicate<UIconEdit.CursorEntry> match)`

Determines whether any element matching the specified predicate exists in the list.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if at least one element matches the specified predicate; `false` otherwise.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Boolean TrueForAll(System.Predicate<UIconEdit.CursorEntry> match)`

Determines whether every element in the list matches the specified predicate.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if every element in the list matches the specified predicate; `false` otherwise.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Collections.Generic.List<T> FindAll(System.Predicate<UIconEdit.CursorEntry> match)`

Returns a list containing all cursor entries which match the specified predicate.
* `match`: A predicate used to define the elements to search for.

**Returns:** Type [`List<T>`](https://msdn.microsoft.com/en-us/library/6sh2ey19.aspx): A list containing all elements matching `match`.


--------------------------------------------------
## Method: `public UIconEdit.CursorEntry[] ToArray()`

Returns an array containing all elements in the current list.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`CursorEntry`](#type-public-class-uiconeditcursorentry): An array containing elements copied from the current list.


--------------------------------------------------
## Method: `public void Sort()`

Sorts all elements in the list according to their [`IconEntry.EntryKey`](#property-public-uiconeditentrykey-entrykey--get-) value.

--------------------------------------------------
## Method: `public void Sort(System.Collections.Generic.IComparer<UIconEdit.CursorEntry> comparer)`

Sorts all elements in the list according to the specified comparer.
* `comparer`: The comparer used to compare each [`CursorEntry`](#type-public-class-uiconeditcursorentry), or `null` to use their [`IconEntry.EntryKey`](#property-public-uiconeditentrykey-entrykey--get-) value.

### Exceptions

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The implementation of `comparer` caused an error during the sort. For example, `comparer` might not return 0 when comparing an item with itself.

--------------------------------------------------
## Method: `public void Sort(System.Comparison<UIconEdit.CursorEntry> comparison)`

Sorts all elements in the list according to the specified delegate.
* `comparison`: The delegate used to compare each [`IconEntry`](#type-public-class-uiconediticonentry).

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`comparison` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The implementation of `comparison` caused an error during the sort. For example, `comparison` might not return 0 when comparing an item with itself.

--------------------------------------------------
# Type: `struct UIconEdit.CursorFile.EntryList.Enumerator`

An enumerator which iterates through the list.

--------------------------------------------------
## Method: `public void Dispose()`

Disposes of the current instance.

--------------------------------------------------
## Property: `public UIconEdit.CursorEntry Current { get; }`

Gets the element at the current position in the enumerator.

--------------------------------------------------
## Method: `public System.Boolean MoveNext()`

Advances the enumerator to the next position in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFile Load(System.IO.Stream input, System.Action<UIconEdit.IconLoadException> handler)`

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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public static UIconEdit.IconFile Load(System.String path, System.Action<UIconEdit.IconLoadException> handler)`

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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
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

##### [`IconLoadException`](#type-public-class-uiconediticonloadexception)
An error occurred when processing the icon file's format.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Method: `public static System.Drawing.Icon GetIcon(UIconEdit.IconEntry entry)`

Gets an [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) from a single entry.
* `entry`: The icon entry from which to get an [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx).

**Returns:** Type [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx): An [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) created from `entry`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`entry` is `null`.

--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgX(UIconEdit.IconEntry entry)`

Returns the color panes.
* `entry`: This parameter is ignored.

--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgY(UIconEdit.IconEntry entry)`

Returns the number of bits per pixel in the specified entry.
* `entry`: The entry for which to get the bits-per-pixel.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): The number of bits per pixel in `entry`.


--------------------------------------------------
# Type: `public class UIconEdit.IconEntry`

Represents a single entry in an icon.

--------------------------------------------------
## Field: `public const System.Byte DefaultAlphaThreshold = 96`

The default [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-) value.

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int16 bitDepth, System.Int16 alphaThreshold, UIconEdit.BitDepth width, System.Byte height)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int16 bitDepth, System.Int16 width, UIconEdit.BitDepth height)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.BitDepth bitDepth, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public IconEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.BitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Field: `public const System.Int16 MinDimension = 1`

The minimum dimensions of an icon. 1 pixel in size.

--------------------------------------------------
## Field: `public const System.Int16 MaxDimension = 768`

The maximum dimensions of an icon. 768 as of Windows 10.

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
## Field: `public static readonly System.Windows.DependencyProperty EntryKeyProperty`

The dependency property for the read-only [`IconEntry.EntryKey`](#property-public-uiconeditentrykey-entrykey--get-) property.

--------------------------------------------------
## Property: `public UIconEdit.EntryKey EntryKey { get; }`

Gets a key for the icon entry.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty WidthProperty`

The dependency property for the read-only [`IconEntry.Width`](#property-public-systemint16-width--get-) property.

--------------------------------------------------
## Property: `public System.Int16 Width { get; }`

Gets the resampled width of the icon.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty HeightProperty`

The dependency property for the read-only [`IconEntry.Height`](#property-public-systemint16-height--get-) property.

--------------------------------------------------
## Property: `public System.Int16 Height { get; }`

Gets the resampled height of the icon.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty BitDepthProperty`

The dependency property for the read-only [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-) property.

--------------------------------------------------
## Property: `public UIconEdit.BitDepth BitDepth { get; }`

Gets the bit depth of the current instance.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty AlphaThresholdProperty`

The dependency property for the [`IconEntry.AlphaThreshold`](#property-public-systembyte-alphathreshold--get-set-) property.

--------------------------------------------------
## Property: `public System.Byte AlphaThreshold { get; set; }`

Gets and sets a value indicating the threshold of alpha values at [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-)s below [`BitDepth.Depth32BitsPerPixel`](#field-bitdepthdepth32bitsperpixel--0). Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty BitsPerPixelProperty`

The dependency-property for the read-only [`IconEntry.BitsPerPixel`](#property-public-systemuint16-bitsperpixel--get-) property.

--------------------------------------------------
## Property: `public System.UInt16 BitsPerPixel { get; }`

Gets the number of bits per pixel specified by [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty ColorCountProperty`

The dependency property for the read-only [`IconEntry.ColorCount`](#property-public-systemint64-colorcount--get-) property.

--------------------------------------------------
## Property: `public System.Int64 ColorCount { get; }`

Gets the maximum color count specified by [`IconEntry.BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty DrawInterpolationModeProperty`

The dependency property for the [`IconEntry.DrawInterpolationMode`](#property-public-systemdrawingdrawing2dinterpolationmode-drawinterpolationmode--get-set-) property.

--------------------------------------------------
## Property: `public System.Drawing.Drawing2D.InterpolationMode DrawInterpolationMode { get; set; }`

Gets and sets the interpolation mode used by graphics objects when scaling.

### Exceptions

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
In a set operation, the specified value is not a valid [`InterpolationMode`](https://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.interpolationmode.aspx) value.

--------------------------------------------------
## Field: `public static System.Windows.DependencyProperty DrawPixelOffsetModeProperty`

The dependency property for the [`IconEntry.DrawPixelOffsetMode`](#property-public-systemdrawingdrawing2dpixeloffsetmode-drawpixeloffsetmode--get-set-) property.

--------------------------------------------------
## Property: `public System.Drawing.Drawing2D.PixelOffsetMode DrawPixelOffsetMode { get; set; }`

Gets and sets the pixel offset mode used by graphics objects when rescaling the image.

### Exceptions

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
In a set operation, the specified value is not a valid [`PixelOffsetMode`](https://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.pixeloffsetmode.aspx) value.

--------------------------------------------------
## Method: `public UIconEdit.IconEntry Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconEntry`](#type-public-class-uiconediticonentry): A duplicate of the current instance, with its own clone of [`IconEntry.BaseImage`](#property-public-systemwindowsmediaimagingbitmapsource-baseimage--get-set-).


--------------------------------------------------
## Method: `public static UIconEdit.BitDepth ParseBitDepth(System.String value)`

Parses the specified string as a [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.
* `value`: The value to parse.

**Returns:** Type [`BitDepth`](#type-public-enum-uiconeditbitdepth): The parsed [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`value` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)

`value` is an empty string or contains only whitespace.

-OR-

`value` does not translate to a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

### Remarks


`value` is parsed in a case-insensitive manner which works differently from [`Enum.Parse()`](https://msdn.microsoft.com/en-us/library/kxydatf9.aspx).

First of all, all non-alphanumeric characters are stripped. If `value` is entirely numeric, or begins with "Depth" followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the integer [`BitDepth`](#type-public-enum-uiconeditbitdepth) value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.

Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel" (or "BitPerPixel" in the case of [`BitDepth.Depth1BitPerPixel`](#field-bitdepthdepth1bitperpixel--4)).

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
## Method: `IconEntry.TryParseBitDepth(System.String value, UIconEdit.BitDepth@ result)`

Parses the specified string as a [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.
* `value`: The value to parse.
* `result`: When this method returns, contains the parsed [`BitDepth`](#type-public-enum-uiconeditbitdepth) result, or the default value for type [`BitDepth`](#type-public-enum-uiconeditbitdepth) if `value` could not be parsed. This parameter is passed uninitialized.

**Returns:** `true` if `value` was successfully parsed; `false` otherwise.

### Remarks


`value` is parsed in a case-insensitive manner which works differently from [`Enum.TryParse()`](https://msdn.microsoft.com/en-us/library/dd991317.aspx).

First of all, all non-alphanumeric characters are stripped. If `value` is entirely numeric, or begins with "Depth" followed by an entirely numeric value, it is parsed according to the number of colors or the number of bits per pixel, rather than the integer [`BitDepth`](#type-public-enum-uiconeditbitdepth) value. There is fortunately no overlap; 1, 4, 8, 24, and 32 always refer to the number of bits per pixel, whereas 2, 16, 256, 16777216, and 4294967296 always refer to the number of colors.

Otherwise, "Depth" is prepended to the beginning, and it attempts to ensure that the value ends with either "Color" or "BitsPerPixel" (or "BitPerPixel" in the case of [`BitDepth.Depth1BitPerPixel`](#field-bitdepthdepth1bitperpixel--4)).

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
# Type: `public class UIconEdit.CursorEntry`

Represents a single entry of a cursor.

--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int16 bitDepth, System.Int16 alphaThreshold, UIconEdit.BitDepth width, System.UInt16 height, System.UInt16 hotspotX, System.Byte hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

-OR-

`hotspotX` is greater than `width`.

-OR-

`hotspotY` is greater than `height`.


--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int16 bitDepth, System.Int16 width, UIconEdit.BitDepth height, System.UInt16 hotspotX, System.UInt16 hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

-OR-

`hotspotX` is greater than `width`.

-OR-

`hotspotY` is greater than `height`.


--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int16 bitDepth, System.Int16 alphaThreshold, UIconEdit.BitDepth width, System.Byte height)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, System.Int16 bitDepth, System.Int16 width, UIconEdit.BitDepth height)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.BitDepth bitDepth, System.UInt16 alphaThreshold, System.UInt16 hotspotX, System.Byte hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

-OR-

`hotspotX` is greater than the width of `baseImage`.

-OR-

`hotspotY` is greater than the height of `baseImage`.


##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.BitDepth bitDepth, System.UInt16 hotspotX, System.UInt16 hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

-OR-

`hotspotX` is greater than the width of `baseImage`.

-OR-

`hotspotY` is greater than the height of `baseImage`.


##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.BitDepth bitDepth, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Constructor: `public CursorEntry(System.Windows.Media.Imaging.BitmapSource baseImage, UIconEdit.BitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-enum-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`IconEntry.MinDimension`](#field-public-const-systemint16-mindimension--1) or is greater than [`IconEntry.MaxDimension`](#field-public-const-systemint16-maxdimension--768).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty HotspotXProperty`

The dependency property for the [`CursorEntry.HotspotX`](#property-public-systemuint16-hotspotx--get-set-) property.

--------------------------------------------------
## Property: `public System.UInt16 HotspotX { get; set; }`

Gets and sets the horizontal offset of the cursor's hotspot from the left of the cursor in pixels.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is greater than [`IconEntry.Width`](#property-public-systemint16-width--get-).

--------------------------------------------------
## Field: `public static readonly System.Windows.DependencyProperty HotspotYProperty`

The dependency property for the [`CursorEntry.HotspotY`](#property-public-systemuint16-hotspoty--get-set-) property.

--------------------------------------------------
## Property: `public System.UInt16 HotspotY { get; set; }`

Gets and sets the vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is greater than [`IconEntry.Height`](#property-public-systemint16-height--get-).

--------------------------------------------------
## Property: `public System.Windows.Point Hotspot { get; }`

Gets the offset of the cursor's hotspot from the upper-left corner of the cursor in pixels.

--------------------------------------------------
# Type: `public struct UIconEdit.EntryKey`

Represents a simplified key for an icon entry.

--------------------------------------------------
## Constructor: `public EntryKey(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Creates a new instance.
* `width`: The width of the icon entry.
* `height`: The height of the icon entry.
* `bitDepth`: The bit depth of the icon entry.

--------------------------------------------------
## Field: `public System.Int16 Width`

Indicates the width of the icon entry.

--------------------------------------------------
## Field: `public System.Int16 Height`

Indicates the height of the icon entry.

--------------------------------------------------
## Field: `public UIconEdit.BitDepth BitDepth`

Indicates the bit depth of the icon entry.

--------------------------------------------------
## Property: `public System.Boolean IsEmpty { get; }`

Gets a value indicating whether [`EntryKey.Width`](#field-public-systemint16-width), [`EntryKey.Height`](#field-public-systemint16-height), and [`EntryKey.BitDepth`](#field-public-uiconeditbitdepth-bitdepth) are all 0.

--------------------------------------------------
## Property: `public System.Boolean IsValid { get; }`

Gets a value indicating whether the current instance contains valid values which would actually occur in an [`IconEntry`](#type-public-class-uiconediticonentry).

--------------------------------------------------
## Method: `public System.Int32 CompareTo(UIconEdit.EntryKey other)`

Compares the current instance to the specified other [`EntryKey`](#type-public-struct-uiconeditentrykey) object. First [`EntryKey.BitDepth`](#field-public-uiconeditbitdepth-bitdepth) is compared, then [`EntryKey.Height`](#field-public-systemint16-height), then [`EntryKey.Width`](#field-public-systemint16-width) (with higher color-counts and larger elements first).
* `other`: The other [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): A value less than 0 if the current value comes before `other`; a value greater than 0 if the current value comes after `other`; or 0 if the current instance is equal to `other`.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThan(UIconEdit.EntryKey f1, UIconEdit.EntryKey f2)`

Compares two [`EntryKey`](#type-public-struct-uiconeditentrykey) objects.
* `f1`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.
* `f2`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThan(UIconEdit.EntryKey f1, UIconEdit.EntryKey f2)`

Compares two [`EntryKey`](#type-public-struct-uiconeditentrykey) objects.
* `f1`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.
* `f2`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is greater than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThanOrEqual(UIconEdit.EntryKey f1, UIconEdit.EntryKey f2)`

Compares two [`EntryKey`](#type-public-struct-uiconeditentrykey) objects.
* `f1`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.
* `f2`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThanOrEqual(UIconEdit.EntryKey f1, UIconEdit.EntryKey f2)`

Compares two [`EntryKey`](#type-public-struct-uiconeditentrykey) objects.
* `f1`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.
* `f2`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean Equals(UIconEdit.EntryKey other)`

Determines if the current instance is equal to the specified other [`EntryKey`](#type-public-struct-uiconeditentrykey) value.
* `other`: The other [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

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
## Operator: `public static System.Boolean op_Equality(UIconEdit.EntryKey f1, UIconEdit.EntryKey f2)`

Determines equality of two [`EntryKey`](#type-public-struct-uiconeditentrykey) objects.
* `f1`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.
* `f2`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_Inequality(UIconEdit.EntryKey f1, UIconEdit.EntryKey f2)`

Determines inequality of two [`EntryKey`](#type-public-struct-uiconeditentrykey) objects.
* `f1`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.
* `f2`: An [`EntryKey`](#type-public-struct-uiconeditentrykey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is not equal to `f2`; `false` otherwise.


--------------------------------------------------
# Type: `public enum UIconEdit.BitDepth`

Indicates the bit depth of an icon entry.

--------------------------------------------------
## Field: `BitDepth.Depth32BitsPerPixel = 0`

Indicates that the entry is full color with alpha (32 bits per pixel).

--------------------------------------------------
## Field: `BitDepth.Depth24BitsPerPixel = 1`

Indicates that the entry is full color without alpha (24 bits per pixel).

--------------------------------------------------
## Field: `BitDepth.Depth256Color = 2`

Indicates that the entry is 256-color (8 bits per pixel). Same value as [`BitDepth.Depth8BitsPerPixel`](#field-bitdepthdepth8bitsperpixel--2).

--------------------------------------------------
## Field: `BitDepth.Depth16Color = 3`

Indicates that the entry is 16-color (4 bits per pixel). Same value as [`BitDepth.Depth4BitsPerPixel`](#field-bitdepthdepth4bitsperpixel--3).

--------------------------------------------------
## Field: `BitDepth.Depth2Color = 4`

Indicates that the entry is 2-color (1 bit per pixel). Same value as [`BitDepth.Depth1BitPerPixel`](#field-bitdepthdepth1bitperpixel--4).

--------------------------------------------------
## Field: `BitDepth.Depth8BitsPerPixel = 2`

Indicates that the entry is 256-color (8 bits per pixel). Same value as [`BitDepth.Depth256Color`](#field-bitdepthdepth256color--2).

--------------------------------------------------
## Field: `BitDepth.Depth4BitsPerPixel = 3`

Indicates that the entry is 16-color (4 bits per pixel). Same value as [`BitDepth.Depth16Color`](#field-bitdepthdepth16color--3).

--------------------------------------------------
## Field: `BitDepth.Depth1BitPerPixel = 4`

Indicates that the entry is 2-color (1 bit per pixel). Same value as [`BitDepth.Depth2Color`](#field-bitdepthdepth2color--4).

--------------------------------------------------
## Field: `BitDepth.Depth1BitsPerPixel = 4`

Indicates that the entry is 2-color (1 bit per pixel). Same value as [`BitDepth.Depth2Color`](#field-bitdepthdepth2color--4).
