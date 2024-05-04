using System.Runtime.InteropServices;

namespace SharpGDX.OpenGL;

internal sealed class GetProcAddressOSX : IGetProcAddress
{
	private const string Library = "libdl.dylib";

	public IntPtr GetProcAddress(string function)
	{
		var functionName = "_" + function;

		if (!NSIsSymbolNameDefined(functionName))
		{
			return IntPtr.Zero;
		}

		var symbol = NSLookupAndBindSymbol(functionName);

		if (symbol != IntPtr.Zero)
		{
			symbol = NSAddressOfSymbol(symbol);
		}

		return symbol;
	}

	[DllImport(Library, EntryPoint = "NSAddressOfSymbol")]
	private static extern IntPtr NSAddressOfSymbol(IntPtr symbol);

	[DllImport(Library, EntryPoint = "NSIsSymbolNameDefined")]
	private static extern bool NSIsSymbolNameDefined(string s);

	[DllImport(Library, EntryPoint = "NSLookupAndBindSymbol")]
	private static extern IntPtr NSLookupAndBindSymbol(string s);
}