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
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon frames, or `null` to throw an exception in those cases.

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
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon frames, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

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
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

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

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): A duplicate of the current instance, with copies of every icon frame and clones of each frame's [`BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) in [`Frames`](#property-public-uiconediticonfilebaseframelist-frames--get-).


--------------------------------------------------
## Method: `protected virtual System.Boolean IsValid(UIconEdit.IconFrame frame)`

When overridden in a derived class, gets a value indicating whether the specified value may be added to [`Frames`](#property-public-uiconediticonfilebaseframelist-frames--get-).
* `frame`: The frame to check.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `frame` is not `null`; `false` otherwise.


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgX(UIconEdit.IconFrame frame)`

When overridden in a derived class, computes the 16-bit X component.
* `frame`: The image frame to calculate.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): In icon files, the color panes. In cursor files, the horizontal offset of the hotspot from the left in pixels.


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgY(UIconEdit.IconFrame frame)`

When overridden in a derived class, computes the 16-bit Y component.
* `frame`: The image frame to calculate.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): In icon files, the number of bits per pixel. In cursor files, the vertical offset of the hotspot from the top, in pixels.


--------------------------------------------------
## Method: `public void Save(System.IO.Stream output)`

Saves the file to the specified stream.
* `output`: The stream to which the file will be written.

### Exceptions

##### [`InvalidOperationException`](https://msdn.microsoft.com/en-us/library/system.invalidoperationexception.aspx)
[`Frames`](#property-public-uiconediticonfilebaseframelist-frames--get-) contains zero elements.

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
[`Frames`](#property-public-uiconediticonfilebaseframelist-frames--get-) contains zero elements.

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in [`GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

##### [`PathTooLongException`](https://msdn.microsoft.com/en-us/library/system.io.pathtoolongexception.aspx)
The specified path, filename, or both contain the system-defined maximum length.

##### [`DirectoryNotFoundException`](https://msdn.microsoft.com/en-us/library/system.io.directorynotfoundexception.aspx)
The specified path is invalid.

##### [`IOException`](https://msdn.microsoft.com/en-us/library/system.io.ioexception.aspx)
An I/O error occurred.

--------------------------------------------------
## Method: `public virtual void Dispose()`

Immediately releases all resources used by the current instance.

--------------------------------------------------
## Method: `protected virtual void Dispose(System.Boolean disposing)`

Releases all unmanaged resources used by the current instance, and optionally releases all managed resources.
* `disposing`: `true` to release both managed and unmanaged resources; `false` to release only unmanaged resources.

--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

When overridden in a derived class, gets the 16-bit identifier for the file type.

--------------------------------------------------
## Property: `public UIconEdit.IconFileBase.FrameList Frames { get; }`

Gets a collection containing all frames in the icon file.

--------------------------------------------------
# Type: `class UIconEdit.IconFileBase.FrameList`

Represents a hash list of frames. This collection treats [`IconFrame`](#type-public-class-uiconediticonframe) objects with the same[`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as though they were equal.

--------------------------------------------------
## Method: `public System.Boolean Add(UIconEdit.IconFrame item)`

Adds the specified icon frame to the list.
* `item`: The icon frame to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`Count`](#property-public-virtual-systemint32-count--get-) is equal to [`MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same[`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) already exists in the list.


--------------------------------------------------
## Method: `public System.Boolean Insert(System.Int32 index, UIconEdit.IconFrame item)`

Adds the specified icon frame to the list at the specified index.
* `index`: The index at which to insert the icon frame.
* `item`: The icon frame to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`Count`](#property-public-virtual-systemint32-count--get-) is equal to [`MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same[`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) already exists in the list.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than [`Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public System.Boolean SetValue(System.Int32 index, UIconEdit.IconFrame item)`

Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The item to set at the specified index.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully set; `false` if `item` is `null`, is already associated with a different icon file, or if an element with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) already exists at a different index.


--------------------------------------------------
## Method: `public virtual void RemoveAt(System.Int32 index)`

Removes the element at the specified index.
* `index`: The element at the specified index.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public void RemoveAndDisposeAt(System.Int32 index)`

Removes the element at the specified index and immediately calls [`Dispose`](#method-public-virtual-void-dispose-1).
* `index`: The element at the specified index.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public virtual System.Boolean Remove(UIconEdit.IconFrame item)`

Removes the specified icon frame from the list.
* `item`: The icon frame to remove from the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean RemoveAndDispose(UIconEdit.IconFrame item)`

Removes the specified icon frame from the list and immediately callse [`Dispose`](#method-public-virtual-void-dispose-1).
* `item`: The icon frame to remove from the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconEdit.IconFrame item)`

Removes an icon frame similar to the specified value from the list.
* `item`: The icon frame compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon frame with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveAndDisposeSimilar(UIconEdit.IconFrame item)`

Removes an icon frame similar to the specified value from the list and immediately calls [`Dispose`](#method-public-virtual-void-dispose-1).
* `item`: The icon frame to search for.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon frame with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `public virtual void Clear()`

Removes all elements from the list.

--------------------------------------------------
## Method: `public void ClearAndDispose()`

Removes all elements from the list and immediately calls [`Dispose`](#method-public-virtual-void-dispose-1) on each one.

--------------------------------------------------
## Method: `public virtual System.Boolean Contains(UIconEdit.IconFrame item)`

Determines if the specified element exists in the list.
* `item`: The icon frame to search for in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconEdit.IconFrame item)`

Determines if an element similar to the specified icon frame exists in the list.
* `item`: The icon frame to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon frame with the same with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public virtual System.Int32 IndexOf(UIconEdit.IconFrame item)`

Gets the index of the specified item.
* `item`: The icon frame to search for in the list.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.IconFrame item)`

Gets the index of an element similar to the specified item.
* `item`: The icon frame to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of an icon frame with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public void CopyTo(UIconEdit.IconFrame[] array)`

Copies all elements in the list to the specified array.
* `array`: The array to which all elements in the list will be copied.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` is less than [`Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public virtual void CopyTo(UIconEdit.IconFrame[] array, System.Int32 arrayIndex)`

Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`arrayIndex` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` minus `arrayIndex` is less than [`Count`](#property-public-virtual-systemint32-count--get-).

--------------------------------------------------
## Method: `public void CopyTo(System.Int32 index, UIconEdit.IconFrame[] array, System.Int32 arrayIndex, System.Int32 count)`

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
## Method: `public UIconEdit.IconFileBase.FrameList.Enumerator GetEnumerator()`

Returns an enumerator which iterates through the list.

**Returns:** Type [`Enumerator`](#type-struct-uiconediticonfilebaseframelistenumerator): An enumerator which iterates through the list.


--------------------------------------------------
## Method: `public UIconEdit.IconFrame[] ToArray()`

Returns an array containing all elements in the current list.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`IconFrame`](#type-public-class-uiconediticonframe): An array containing elements copied from the current list.


--------------------------------------------------
## Method: `public System.Int32 RemoveWhere(System.Predicate<UIconEdit.IconFrame> match)`

Removes all elements matching the specified predicate.
* `match`: A predicate used to define the elements to remove.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of elements which were removed.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Int32 RemoveAndDisposeWhere(System.Predicate<UIconEdit.IconFrame> match)`

Removes all elements matching the specified predicate and immediately calls [`Dispose`](#method-public-virtual-void-dispose-1).
* `match`: A predicate used to define the elements to remove.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of elements which were removed.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public void Sort()`

Sorts all elements in the list according to their [`FrameKey`](#property-public-uiconeditframekey-framekey--get-) value.

--------------------------------------------------
## Method: `public void Sort(System.Collections.Generic.IComparer<UIconEdit.IconFrame> comparer)`

Sorts all elements in the list according to the specified comparer.
* `comparer`: The comparer used to compare each [`IconFrame`](#type-public-class-uiconediticonframe), or `null` to their [`FrameKey`](#property-public-uiconeditframekey-framekey--get-) value.

--------------------------------------------------
## Method: `public void Sort(System.Comparison<UIconEdit.IconFrame> comparison)`

Sorts all elements in the list according to the specified delegate.
* `comparison`: The delegate used to compare each [`IconFrame`](#type-public-class-uiconediticonframe).

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`comparison` is `null`.

--------------------------------------------------
## Property: `IconFileBase.FrameList.Item(System.Int32 index)`

Gets and sets the element at the specified index.
* `index`: The index of the element to get or set.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

`index` is less than 0 or is greater than or equal to [`Count`](#property-public-virtual-systemint32-count--get-).

-OR-

In a set operation, the specified value is `null`.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
In a set operation, an element with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) already exists in the list at a different index, or the specified value is already associated with a different icon file.

--------------------------------------------------
## Property: `public virtual System.Int32 Count { get; }`

Gets the number of elements in the list.

--------------------------------------------------
# Type: `struct UIconEdit.IconFileBase.FrameList.Enumerator`

An enumerator which iterates through the list.

--------------------------------------------------
## Method: `public void Dispose()`

Disposes of the current instance.

--------------------------------------------------
## Method: `public System.Boolean MoveNext()`

Advances the enumerator to the next position in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
## Property: `public UIconEdit.IconFrame Current { get; }`

Gets the element at the current position in the enumerator.

--------------------------------------------------
# Type: `public struct UIconEdit.IconTypeCode`

The type code for an icon file.

--------------------------------------------------
## Field: `public static UIconEdit.IconTypeCode Icon`

Indicates an icon (.ICO) file.

--------------------------------------------------
## Field: `public static UIconEdit.IconTypeCode Cursor`

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
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

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
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, System.Object value, System.Int32 index)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `value`: The value which caused the error.
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

--------------------------------------------------
## Constructor: `public IconLoadException(UIconEdit.IconErrorCode code, System.Int32 index)`

Creates a new instance with the default message and the specified error code.
* `code`: The error code used to identify the cause of the error.
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.

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
* `index`: The index of the frame in the icon file which caused this error, or -1 if it occurred before processing the icon frames.
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

Gets the index in the icon file of the icon frame which caused this exception, or -1 if it occurred before the icon frames were processed.

--------------------------------------------------
## Property: `public System.Object Value { get; }`

Gets an object whose value caused the error, or `null` if there was no such value.

--------------------------------------------------
# Type: `public struct UIconEdit.IconErrorCode`

Indicates the cause of an [`IconLoadException`](#type-public-class-uiconediticonloadexception).

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode Unknown`

Code 0: the cause of the error is unknown.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidFormat`

Code 0x1: The file is not a valid cursor or icon format. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode ZeroFrames`

Code 0x2: The icon contains zero frames. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode ResourceTooSmall`

Code 0x3: One of the icon directory entries has a length less than or equal to 40 bytes, which is logically too small for either a BMP or a PNG file.[`Value`](#property-public-systemobject-value--get-) contains the length. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode ResourceTooEarly`

Code 0x4: One of the icon directory entries has a starting offset which would overlap with the list of entries.[`Value`](#property-public-systemobject-value--get-) contains the offset. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode ResourceOverlap`

Code 0x5: One or more of the icon directory entries have overlapping offset/length combinations. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidFrameType`

Code 0x1000: the file type of a frame is invalid.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidBitDepth`

Code 0x1001: the file is an icon, and an icon directory entry has a bit depth with any value other than 0, 1, 4, 8, 24, or 32.[`Value`](#property-public-systemobject-value--get-) contains the bit depth.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode ZeroValidFrames`

There are no remaining valid frames after processing. This is a fatal error, and the icon file cannot continue processing when it occurs.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidPngFile`

Code 0x1100: an error occurred when attempting to load a PNG frame. The inner exception may contain more information.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidPngSize`

Code 0x1102: the width or height of a PNG frame is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).[`Value`](#property-public-systemobject-value--get-) contains the [`Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx) of the image.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode PngSizeMismatch`

Code 0x1105: the width or height of a PNG frame does not match the width or height listed in the icon directory entry.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidBmpFile`

Code 0x1204: an error occurred when attempting to process a BMP frame. The inner exception may contain more information.[`Value`](#property-public-systemobject-value--get-) contains a [`Tuple`2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.aspx) in which the [`Tuple`Item1`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item1.aspx) is the [`Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx) listed in the icon directory entry, and [`Tuple`Item2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item2.aspx) is the actual size.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidBmpBitDepth`

Code 0x1201 the bit depth of a BMP frame is not supported.[`Value`](#property-public-systemobject-value--get-) contains the bit depth.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidBmpSize`

Code 0x1202: the width or height of a BMP frame is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension). The maximum height is doubled in images with a bit depth less than 32.[`Value`](#property-public-systemobject-value--get-) contains the [`Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx) of the image.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode BmpHeightMismatch`

Code 0x1203: the width or height of a BMP frame does not match the width or height listed in the icon directory entry.[`Value`](#property-public-systemobject-value--get-) contains a [`Tuple`2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.aspx) in which the [`Tuple`Item1`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item1.aspx) is the [`Size`](https://msdn.microsoft.com/en-us/library/system.drawing.image.size.aspx) listed in the icon directory entry, and [`Tuple`Item2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item2.aspx) is the actual size.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode InvalidBmpHeightOdd`

Code 0x1204: the height of a BMP frame is an odd number, indicating that there is no AND (transparency) mask.[`Value`](#property-public-systemobject-value--get-) contains the [`Height`](https://msdn.microsoft.com/en-us/library/system.drawing.image.height.aspx) of the image.

--------------------------------------------------
## Field: `public static UIconEdit.IconErrorCode BmpBitDepthMismatch`

Code 0x1205: there is a mismatch between the bit depth of a BMP frame and the expected bit depth of the file.[`Value`](#property-public-systemobject-value--get-) contains a [`Tuple`2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.aspx) in which the [`Tuple`Item1`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item1.aspx) is the bit depth listed in the icon directory entry, and [`Tuple`Item2`](https://msdn.microsoft.com/en-us/library/system.tuple`2.item2.aspx) is the bit depth listed in the BMP frame.

--------------------------------------------------
# Type: `public class UIconEdit.IconLoadExceptionHandler`

A delegate function for handling [`IconLoadException`](#type-public-class-uiconediticonloadexception) errors.
* `e`: An [`IconLoadException`](#type-public-class-uiconediticonloadexception) containing information about the error.

--------------------------------------------------
# Type: `public class UIconEdit.IconFrame`

Represents a single frame in an icon.

--------------------------------------------------
## Field: `public static System.Int32 MinDimension`

The minimum dimensions of an icon. 1 pixel in size.

--------------------------------------------------
## Field: `public static System.Int32 MaxDimension`

The maximum dimensions of an icon. 768 as of Windows 10.

--------------------------------------------------
## Constructor: `public IconFrame(System.Drawing.Image baseImage, System.Int16 bitDepth, System.Int16 alphaThreshold, UIconEdit.BitDepth width, System.Byte height)`

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
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public IconFrame(System.Drawing.Image baseImage, System.Int16 bitDepth, System.Int16 width, UIconEdit.BitDepth height)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public IconFrame(System.Drawing.Image baseImage, UIconEdit.BitDepth bitDepth, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public IconFrame(System.Drawing.Image baseImage, UIconEdit.BitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Method: `public virtual UIconEdit.IconFrame Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconFrame`](#type-public-class-uiconediticonframe): A duplicate of the current instance, with its own clone of [`BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-).


--------------------------------------------------
## Method: `public virtual void Dispose()`

Releases all resources used by the current instance.

--------------------------------------------------
## Method: `protected virtual void Dispose(System.Boolean disposing)`

Releases all unmanaged resources used by the current instance, and optionally releases all managed resources.
* `disposing`: `true` to release both managed and unmanaged resources; `false` to release only unmanaged resources.

--------------------------------------------------
## Property: `public System.Drawing.Image BaseImage { get; set; }`

Gets and sets the image associated with the current instance.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
In a set operation, the specified value is `null`.

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
In a set operation, the specified value is disposed.

--------------------------------------------------
## Property: `public UIconEdit.FrameKey FrameKey { get; }`

Gets a key for the icon frame.

--------------------------------------------------
## Property: `public System.Int16 Width { get; set; }`

Gets and sets the resampled width of the icon.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

--------------------------------------------------
## Property: `public System.Int16 Height { get; set; }`

Gets and sets the resampled height of the icon.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
In a set operation, the specified value is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

--------------------------------------------------
## Property: `public UIconEdit.BitDepth BitDepth { get; set; }`

Gets and sets the bit depth of the current instance. This property is ignored if the width or height of the image is greater than 255.

### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
In a set operation, the specified value is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

--------------------------------------------------
## Property: `public System.Drawing.Imaging.PixelFormat PixelFormat { get; }`

Gets the pixel format of the resulting image file.

--------------------------------------------------
## Property: `public System.Byte AlphaThreshold { get; set; }`

Gets and sets a value indicating the threshold of alpha values at [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-)s below [`Bit32`](#field-public-static-uiconeditbitdepth-bit32). Alpha values less than this value will be fully transparent; alpha values greater than or equal to this value will be fully opaque.

--------------------------------------------------
## Property: `public System.Drawing.Drawing2D.InterpolationMode DrawInterpolationMode { get; set; }`

Gets and sets the interpolation mode used by graphics objects when scaling.

### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
In a set operation, the specified value is not a valid [`InterpolationMode`](https://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.interpolationmode.aspx) value.

--------------------------------------------------
## Property: `public System.Drawing.Drawing2D.PixelOffsetMode DrawPixelOffsetMode { get; set; }`

Gets and sets the pixel offset mode used by graphics objects when rescaling the image.

### Exceptions

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
In a set operation, the specified value is not a valid [`PixelOffsetMode`](https://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.pixeloffsetmode.aspx) value.

--------------------------------------------------
# Type: `public class UIconEdit.CursorFrame`

Represents a single frame of a cursor.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, System.Int16 bitDepth, System.Int16 alphaThreshold, UIconEdit.BitDepth width, System.UInt16 height, System.UInt16 hotspotX, System.Byte hotspotY)`

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
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, System.Int16 bitDepth, System.Int16 width, UIconEdit.BitDepth height, System.UInt16 hotspotX, System.UInt16 hotspotY)`

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
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, System.Int16 bitDepth, System.Int16 alphaThreshold, UIconEdit.BitDepth width, System.Byte height)`

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
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, System.Int16 bitDepth, System.Int16 width, UIconEdit.BitDepth height)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `width`: The width of the new image.
* `height`: The height of the new image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`width` or `height` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, UIconEdit.BitDepth bitDepth, System.UInt16 alphaThreshold, System.UInt16 hotspotX, System.Byte hotspotY)`

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
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, UIconEdit.BitDepth bitDepth, System.UInt16 hotspotX, System.UInt16 hotspotY)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `hotspotX`: The horizontal offset of the cursor's hotspot from the left of the cursor in pixels.
* `hotspotY`: The vertical offset of the cursor's hotspot from the top of the cursor in pixels.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, UIconEdit.BitDepth bitDepth, System.Byte alphaThreshold)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.
* `alphaThreshold`: If the alpha value of a given pixel is below this value, that pixel will be fully transparent. If the alpha value is greater than or equal to this value, the pixel will be fully opaque.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Constructor: `public CursorFrame(System.Drawing.Image baseImage, UIconEdit.BitDepth bitDepth)`

Creates a new instance with the specified image.
* `baseImage`: The image associated with the current instance.
* `bitDepth`: Indicates the bit depth of the resulting image.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`baseImage` is `null`.

##### [`InvalidEnumArgumentException`](https://msdn.microsoft.com/en-us/library/system.componentmodel.invalidenumargumentexception.aspx)
`bitDepth` is not a valid [`BitDepth`](#type-public-struct-uiconeditbitdepth) value.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The width or height of `baseImage` is less than [`MinDimension`](#field-public-static-systemint32-mindimension) or is greater than [`MaxDimension`](#field-public-static-systemint32-maxdimension).

##### [`ObjectDisposedException`](https://msdn.microsoft.com/en-us/library/system.objectdisposedexception.aspx)
`baseImage` is disposed.

--------------------------------------------------
## Property: `public System.UInt16 HotspotX { get; set; }`

Gets and sets the horizontal offset of the cursor's hotspot from the left of the cursor in pixels.

--------------------------------------------------
## Property: `public System.UInt16 HotspotY { get; set; }`

Gets and sets the vertical offset of the cursor's hotspot from the top of the cursor in pixels.

--------------------------------------------------
## Property: `public System.Drawing.Point Hotspot { get; }`

Gets the offset of the cursor's hotspot from the upper-left corner of the cursor in pixels.

--------------------------------------------------
# Type: `public struct UIconEdit.FrameKey`

Represents a simplified key for an icon frame.

--------------------------------------------------
## Constructor: `public FrameKey(System.Int16 width, System.Int16 height, UIconEdit.BitDepth bitDepth)`

Creates a new instance.
* `width`: The width of the icon frame.
* `height`: The height of the icon frame.
* `bitDepth`: The bit depth of the icon frame.

--------------------------------------------------
## Field: `public System.Int16 Width`

Indicates the width of the icon frame.

--------------------------------------------------
## Field: `public System.Int16 Height`

Indicates the height of the icon frame.

--------------------------------------------------
## Field: `public UIconEdit.BitDepth BitDepth`

Indicates the bit depth of the icon frame.

--------------------------------------------------
## Method: `public System.Int32 CompareTo(UIconEdit.FrameKey other)`

Compares the current instance to the specified other [`FrameKey`](#type-public-struct-uiconeditframekey) object. First[`BitDepth`](#field-public-uiconeditbitdepth-bitdepth) is compared, then [`Height`](#field-public-systemint16-height), then [`Width`](#field-public-systemint16-width) (with higher color-counts and larger elements first).
* `other`: The other [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): A value less than 0 if the current value comes before `other`; a value greater than 0 if the current value comes after `other`; or 0 if the current instance is equal to `other`.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThan(UIconEdit.FrameKey f1, UIconEdit.FrameKey f2)`

Compares two [`FrameKey`](#type-public-struct-uiconeditframekey) objects.
* `f1`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.
* `f2`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThan(UIconEdit.FrameKey f1, UIconEdit.FrameKey f2)`

Compares two [`FrameKey`](#type-public-struct-uiconeditframekey) objects.
* `f1`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.
* `f2`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is greater than `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_LessThanOrEqual(UIconEdit.FrameKey f1, UIconEdit.FrameKey f2)`

Compares two [`FrameKey`](#type-public-struct-uiconeditframekey) objects.
* `f1`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.
* `f2`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_GreaterThanOrEqual(UIconEdit.FrameKey f1, UIconEdit.FrameKey f2)`

Compares two [`FrameKey`](#type-public-struct-uiconeditframekey) objects.
* `f1`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.
* `f2`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is less than or equal to `f2`; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean Equals(UIconEdit.FrameKey other)`

Determines if the current instance is equal to the specified other [`FrameKey`](#type-public-struct-uiconeditframekey) value.
* `other`: The other [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

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
## Operator: `public static System.Boolean op_Equality(UIconEdit.FrameKey f1, UIconEdit.FrameKey f2)`

Determines equality of two [`FrameKey`](#type-public-struct-uiconeditframekey) objects.
* `f1`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.
* `f2`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is equal to `f2`; `false` otherwise.


--------------------------------------------------
## Operator: `public static System.Boolean op_Inequality(UIconEdit.FrameKey f1, UIconEdit.FrameKey f2)`

Determines inequality of two [`FrameKey`](#type-public-struct-uiconeditframekey) objects.
* `f1`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.
* `f2`: A [`FrameKey`](#type-public-struct-uiconeditframekey) to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `f1` is not equal to `f2`; `false` otherwise.


--------------------------------------------------
# Type: `public struct UIconEdit.BitDepth`

Indicates the bit depth of an icon frame.

--------------------------------------------------
## Field: `public static UIconEdit.BitDepth Bit32`

Indicates that the frame is full color with alpha (32 bits per pixel).

--------------------------------------------------
## Field: `public static UIconEdit.BitDepth Bit24`

Indicates that the frame is full color without alpha (24 bits per pixel plus 1 bit per pixel alpha mask).

--------------------------------------------------
## Field: `public static UIconEdit.BitDepth Color256`

Indicates that the frame is 256-color (8 bits per pixel plus 1 bit per pixel alpha mask)

--------------------------------------------------
## Field: `public static UIconEdit.BitDepth Color16`

Indicates that the frame is 16-color (4 bits per pixel plus 1 bit per pixel alpha mask).

--------------------------------------------------
## Field: `public static UIconEdit.BitDepth Color2`

Indicates that the frame is 2-color (1 bit per pixel plus 1 bit per pixel alpha mask).

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
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon frames, or `null` to throw an exception in those cases.

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
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon frames, or `null` to throw an exception in those cases.

**Returns:** Type [`IconFile`](#type-public-class-uiconediticonfile): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

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
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

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
## Method: `public static System.Drawing.Icon GetIcon(UIconEdit.IconFrame frame)`

Gets an [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) from a single frame.
* `frame`: The icon frame from which to get an [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx).

**Returns:** Type [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx): An [`Icon`](https://msdn.microsoft.com/en-us/library/system.drawing.icon.aspx) created from `frame`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`frame` is `null`.

--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgX(UIconEdit.IconFrame frame)`

Returns the color panes.
* `frame`: This parameter is ignored.

--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgY(UIconEdit.IconFrame frame)`

Returns the number of bits per pixel in the specified frame.
* `frame`: The frame for which to get the bits-per-pixel.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): The number of bits per pixel in `frame`.


--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

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
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon frames, or `null` to throw an exception in those cases.

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
* `handler`: A delegate used to process [`IconLoadException`](#type-public-class-uiconediticonloadexception)s thrown when processing individual icon frames, or `null` to throw an exception in those cases.

**Returns:** Type [`CursorFile`](#type-public-class-uiconeditcursorfile): An [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase) implementation loaded from `path`.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`path` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

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
`path` is empty, contains only whitespace, or contains one or more invalid path characters as defined in[`GetInvalidPathChars`](https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars.aspx).

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
## Method: `protected virtual System.Boolean IsValid(UIconEdit.IconFrame frame)`

Gets a valid indicating whether the specified instance is a valid [`CursorFrame`](#type-public-class-uiconeditcursorframe) object.
* `frame`: The icon frame to test.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `frame` is a [`CursorFrame`](#type-public-class-uiconeditcursorframe) instance; `false` otherwise.


--------------------------------------------------
## Method: `public virtual UIconEdit.IconFileBase Clone()`

Returns a duplicate of the current instance.

**Returns:** Type [`IconFileBase`](#type-public-abstract-class-uiconediticonfilebase): A duplicate of the current instance, with copies of every icon frame and clones of each frame's [`BaseImage`](#property-public-systemdrawingimage-baseimage--get-set-) in [`Frames`](#property-public-uiconeditcursorfilecursorframelist-frames--get-).


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgX(UIconEdit.IconFrame frame)`

Gets the horizontal offset of the hotspot in the specified frame from the left of the specified cursor frame.
* `frame`: The frame from which to get the horizontal offset.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): The horizontal offset of the hotspot from the left in pixels.


--------------------------------------------------
## Method: `protected virtual System.UInt16 GetImgY(UIconEdit.IconFrame frame)`

Gets the vertical offset of the hotspot in the specified frame from the top of the specified cursor frame.
* `frame`: The frame from which to get the horizontal offset.

**Returns:** Type [`UInt16`](https://msdn.microsoft.com/en-us/library/system.uint16.aspx): The vertical offset of the hotspot from the top in pixels.


--------------------------------------------------
## Property: `public virtual UIconEdit.IconTypeCode ID { get; }`

Gets the 16-bit type code for the current instance.

--------------------------------------------------
## Property: `public UIconEdit.CursorFile.CursorFrameList Frames { get; }`

Gets a collection containing all frames in the cursor file.

--------------------------------------------------
# Type: `class UIconEdit.CursorFile.CursorFrameList`

Represents a list of cursor frames.

--------------------------------------------------
## Method: `public System.Boolean SetValue(System.Int32 index, UIconEdit.CursorFrame item)`

Sets the value at the specified index.
* `index`: The index of the value to set.
* `item`: The value to set.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully set; `false` if `item` is `null`, is already associated with a different icon file, or if an element with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) already exists in the list at a different index.


### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than [`Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public System.Boolean Add(UIconEdit.CursorFrame item)`

Adds the specified cursor frame to the list.
* `item`: The cursor frame to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`Count`](#property-public-virtual-systemint32-count--get--1) is equal to [`MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same[`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) already exists in the list.


--------------------------------------------------
## Method: `public System.Boolean Insert(System.Int32 index, UIconEdit.CursorFrame item)`

Inserts the specified cursor frame into the list at the specified index.
* `index`: The index at which the cursor frame will be inserted.
* `item`: The cursor frame to add to the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was successfully added; `false` if `item` is `null`, is already associated with a different icon file, [`Count`](#property-public-virtual-systemint32-count--get--1) is equal to [`MaxValue`](https://msdn.microsoft.com/en-us/library/system.uint16.maxvalue.aspx), or if an element with the same[`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) already exists in the list.


--------------------------------------------------
## Method: `public virtual void RemoveAt(System.Int32 index)`

Removes the element at the specified index.
* `index`: The index of the icon frame to remove.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`index` is less than 0 or is greater than or equal to [`Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public void RemoveAndDisposeAt(System.Int32 index)`

Removes the element at the specified index and, if it does not exist elsewhere in the file, immediately calls [`Dispose`](#method-public-virtual-void-dispose-1).
* `index`: The index of the icon frame to remove.

--------------------------------------------------
## Method: `public virtual System.Boolean Remove(UIconEdit.CursorFrame item)`

Removes the specified cursor frame from the list.
* `item`: The cursor frame to to remove from the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean RemoveAndDispose(UIconEdit.CursorFrame item)`

Removes the specified cursor frame from the list and immediately calls [`Dispose`](#method-public-virtual-void-dispose-1).
* `item`: The cursor frame to to remove from the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found and successfully removed; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean RemoveSimilar(UIconEdit.CursorFrame item)`

Removes an element similar to the specified cursor frame from the list.
* `item`: The cursor frame to to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon frame with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `public System.Boolean RemoveAndDisposeSimilar(UIconEdit.CursorFrame item)`

Removes the specified cursor frame from the list, immediately calls [`Dispose`](#method-public-virtual-void-dispose-1).
* `item`: The cursor frame to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if an icon frame with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item` was successfully found and removed; `false` if no such icon frame was found in the list.


--------------------------------------------------
## Method: `public void CopyTo(UIconEdit.CursorFrame[] array)`

Copies all elements in the list to the specified array.
* `array`: The array to which all elements in the list will be copied.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` is less than [`Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public virtual void CopyTo(UIconEdit.CursorFrame[] array, System.Int32 arrayIndex)`

Copies all elements in the list to the specified array, starting at the specified index.
* `array`: The array to which all elements in the list will be copied.
* `arrayIndex`: The index in `array` at which copying begins.

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`array` is `null`.

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)
`arrayIndex` is less than 0.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The length of `array` minus `arrayIndex` is less than [`Count`](#property-public-virtual-systemint32-count--get--1).

--------------------------------------------------
## Method: `public void CopyTo(System.Int32 index, UIconEdit.CursorFrame[] array, System.Int32 arrayIndex, System.Int32 count)`

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
## Method: `public void ClearAndDispose()`

Removes all elements from the collection and immediately calls [`Dispose`](#method-public-virtual-void-dispose-1) on each one.

--------------------------------------------------
## Method: `public virtual System.Boolean Contains(UIconEdit.CursorFrame item)`

Determines if the specified cursor frame exists in the list.
* `item`: The cursor frame to search for in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `public System.Boolean ContainsSimilar(UIconEdit.CursorFrame item)`

Determines if an element similar to the specified cursor frame exists in the list.
* `item`: The cursor frame to compare.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if a cursor frame with the same with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item` exists in the list; `false` otherwise.


--------------------------------------------------
## Method: `public virtual System.Int32 IndexOf(UIconEdit.CursorFrame item)`

Gets the index of the specified cursor frame.
* `item`: The cursor frame to search for in the list.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): `true` if `item` was found; `false` otherwise.


--------------------------------------------------
## Method: `public System.Int32 IndexOfSimilar(UIconEdit.CursorFrame item)`

Gets the index of an element similar to the specified cursor frame.
* `item`: The cursor frame to compare.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The index of a cursor frame with the same [`Width`](#property-public-systemint16-width--get-set-), [`Height`](#property-public-systemint16-height--get-set-), and [`BitDepth`](#property-public-uiconeditbitdepth-bitdepth--get-set-) as `item`, if found; otherwise, -1.


--------------------------------------------------
## Method: `public UIconEdit.CursorFile.CursorFrameList.Enumerator GetEnumerator()`

Returns an enumerator which iterates through the list.

**Returns:** Type [`Enumerator`](#type-struct-uiconeditcursorfilecursorframelistenumerator): An enumerator which iterates through the list.


--------------------------------------------------
## Method: `public System.Int32 RemoveWhere(System.Predicate<UIconEdit.CursorFrame> match)`

Removes all elements matching the specified predicate.
* `match`: A predicate used to define the elements to remove.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of elements which were removed.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public System.Int32 RemoveAndDisposeWhere(System.Predicate<UIconEdit.CursorFrame> match)`

Removes all elements matching the specified predicate and immediately calls [`Dispose`](#method-public-virtual-void-dispose-1).
* `match`: A predicate used to define the elements to remove.

**Returns:** Type [`Int32`](https://msdn.microsoft.com/en-us/library/system.int32.aspx): The number of elements which were removed.


### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`match` is `null`.

--------------------------------------------------
## Method: `public UIconEdit.CursorFrame[] ToArray()`

Returns an array containing all elements in the current list.

**Returns:** Type [`Array`](https://msdn.microsoft.com/en-us/library/system.array.aspx) of type [`CursorFrame`](#type-public-class-uiconeditcursorframe): An array containing elements copied from the current list.


--------------------------------------------------
## Method: `public void Sort()`

Sorts all elements in the list according to their [`FrameKey`](#property-public-uiconeditframekey-framekey--get-) value.

--------------------------------------------------
## Method: `public void Sort(System.Collections.Generic.IComparer<UIconEdit.CursorFrame> comparer)`

Sorts all elements in the list according to the specified comparer.
* `comparer`: The comparer used to compare each [`CursorFrame`](#type-public-class-uiconeditcursorframe), or `null` to use their [`FrameKey`](#property-public-uiconeditframekey-framekey--get-) value.

### Exceptions

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The implementation of `comparer` caused an error during the sort. For example, `comparer` might not return 0 when comparing an item with itself.

--------------------------------------------------
## Method: `public void Sort(System.Comparison<UIconEdit.CursorFrame> comparison)`

Sorts all elements in the list according to the specified delegate.
* `comparison`: The delegate used to compare each [`IconFrame`](#type-public-class-uiconediticonframe).

### Exceptions

##### [`ArgumentNullException`](https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx)
`comparison` is `null`.

##### [`ArgumentException`](https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx)
The implementation of `comparison` caused an error during the sort. For example, `comparison` might not return 0 when comparing an item with itself.

--------------------------------------------------
## Property: `public virtual System.Int32 Count { get; }`

Gets the number of frames contained in the list.

--------------------------------------------------
## Property: `CursorFile.CursorFrameList.Item(System.Int32 index)`

Gets and sets the cursor frame at the specified index.
* `index`: The cursor frame at the specified index.

### Exceptions

##### [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx)

`index` is less than 0 or is greater than or equal to [`Count`](#property-public-virtual-systemint32-count--get--1).

-OR-

In a set operation, the specified value is `null`.


##### [`NotSupportedException`](https://msdn.microsoft.com/en-us/library/system.notsupportedexception.aspx)
In a set operation, the specified value is `null` or is already associated with a different cursor file.

--------------------------------------------------
# Type: `struct UIconEdit.CursorFile.CursorFrameList.Enumerator`

An enumerator which iterates through the list.

--------------------------------------------------
## Method: `public void Dispose()`

Disposes of the current instance.

--------------------------------------------------
## Method: `public System.Boolean MoveNext()`

Advances the enumerator to the next position in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
## Property: `public UIconEdit.CursorFrame Current { get; }`

Gets the element at the current position in the enumerator.
---------------------------------
##Property: `CursorFile.CursorFrameList.Item(System.Int32 index)`

Gets and sets the cursor frame at the specified index.
* `index`: The cursor frame at the specified index.

### Exceptions

* [`ArgumentOutOfRangeException`](https://msdn.microsoft.com/en-us/library/system.argumentoutofrangeexception.aspx) - 
`index` is less than 0 or is greater than or equal to [`Count`](#property-public-virtual-systemint32-count--get--1).

-OR-

In a set operation, the specified value is `null`.


* [`NotSupportedException`](https://msdn.microsoft.com/en-us--------------------------
##Method: `public System.Boolean MoveNext()`

Advances the enumerator to the next position in the list.

**Returns:** Type [`Boolean`](https://msdn.microsoft.com/en-us/library/system.boolean.aspx): `true` if the enumerator successfully advanced; `false` if the enumerator has passed the end of the list.


--------------------------------------------------
##Property: `public UIconEdit.CursorFrame Current { get; }`

Gets the element at the current position in the enumerator.