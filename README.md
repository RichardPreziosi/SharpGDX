# SharpGDX

C# Port of LibGDX

## Current State

This is a major work in progress that will be broken up into several phases.

1. Simple port of code
1. Conform port to C# best practices

Please see the `CONVERSIONS` file for the current state of each file.

## Thoughts

I was hoping to be able to wait longer until getting rid of the Java Buffer classes (IntBuffer, FloatBuffer, CharBuffer, ByteBuffer), but it is taking more time to fake that functionality than to just replace it with arrays and lists. These buffer classes make sense in Java, but don't offer much in C#. Plus this project is about porting libGDX not Java.

I think that I would eventually like to get rid of everything in the `Shims` namespace. It is just a lot less code changes to leave it for now, and I would like to think carefully about changing the API.

## Special Notes

I have had to shim quite a bit of functionality in an effort to keep the API the same. Some of these shims may or may not go away in phase 2 as I solidify the purpose of this project.

`equals` and `hashCode` methods have been converted to `Equals` and `GetHashCode` respectively so that they can work as appropriate overrides for base C# functionality.

`Files.internal` has been renamed to `Files.Internal` as `internal` is a C# keyword and cannot be used as a method name.

Very few methods have had their casing fixed, all casing will be fixed eventually, but the current fixes are only to accomodate conflicting variable/field names. Other casing fixes will come as part of a future phase.

The collection types work (Array\<T\>, CharArray, FloatArray, etc.), however I would not use them in your own code unless absolutely necessary. They are technically unnecessary in C# for what their stated purpose is. For now I am leaving them in to keep the API the same, but they very well may be removed in future phases. When in doubt, default to using .Net collection types.

The `Poolable` interface is no longer nested inside of `Pool` since `Pool` has been changed to `Pool<T>`.

## Conversion Chart

- IllegalArgumentException => ArgumentException
- IndexOutOfBoundsException => IndexOutOfRangeException
- RuntimeException => SystemException
- Throwable => Exception
- NoSuchElementException => InvalidOperationException
- IllegalStateException => InvalidOperationException
- NumberFormatException => FormatException
- ArrayIndexOutOfBoundsException => IndexOutOfRangeException
- NullPointerException => NullReferenceException
- InterruptedException => ThreadInterruptedException
- Long.numberOfLeadingZeros() => BitOperations.LeadingZeroCount((ulong)mask)
- CharSequence => string