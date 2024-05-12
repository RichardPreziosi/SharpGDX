# SharpGDX

C# Port of LibGDX

## Current State

This is a work in progress that will be broken up into several phases.

1. Port of Java code
    1. Most things will be left named as is
    2. Lots of shimming of Java functionality
    3. Leave the surface (public) API as close to Java as possible
1. Conform port to C# best practices
    1. Fix casing of fields and method names
    2. Change fields to properties where appropriate
    3. Unnest classes where appropriate
    4. Documentation (round 1)
3. Replace or solidify shimmed classes
    1. Cull shim classes and replace with C# functionality
    2. Documentation (round 2)

Please see the `CONVERSIONS` file for the current state of each file.

## Thoughts

I think that I would eventually like to get rid of everything in the `Shims` namespace. It is just a lot less code changes to leave it for now, and I would like to think carefully about changing the API.

I would like to eventually remove and/or reduce the usage of the buffer classes (IntBuffer, FloatBuffer, ByteBuffer, etc.). While these classes are first-class citizens in Java and the JNI knows how to convert them, they are shims in SharpGDX and there are quite a lot of hoops that I've had to jump through to get their data passed to externs.

## Special Notes

I have had to shim quite a bit of functionality in an effort to keep the API the same. Some of these shims may or may not go away in phase 2 as I solidify the purpose of this project.

`equals` and `hashCode` methods have been converted to `Equals` and `GetHashCode` respectively so that they can work as appropriate overrides for base C# functionality.

`Files.internal` has been renamed to `Files.@internal` as `internal` is a C# keyword and cannot be used as a method name.

Very few methods have had their casing fixed, all casing will be fixed eventually, but the current fixes are only to accomodate conflicting variable/field names. Other casing fixes will come as part of a future phase.

The collection types work (Array\<T\>, CharArray, FloatArray, etc.), however I would not use them in your own code unless absolutely necessary. They are technically unnecessary in C# for what their stated purpose is. For now I am leaving them in to keep the API the same, but they very well may be removed in future phases. When in doubt, default to using .Net collection types.

The `Poolable` interface is no longer nested inside of `Pool` since `Pool` has been changed to `Pool<T>`.

## Conversion Notes

- SharpGDX.Headless is ported from gdx-backend-headless
- SharpGDX.Desktop is ported from gdx-backend-lwjgl3 that uses:
    - OpenTK.Windowing.GraphicsLibraryFramework (GLFW)
    - OpenTK.Audio.OpenAL (OpenAL)
    - OpenTK.Graphics.OpenGL (OpenGL)

## Major To Dos

- Audio needs to be in it's own thread (moving the window will pause audio).