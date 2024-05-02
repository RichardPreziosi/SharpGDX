namespace SharpGDX.Desktop;

public interface IFunctionProvider
{
	public T Get<T>()
		where T : class;
}