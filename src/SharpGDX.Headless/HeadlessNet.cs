namespace SharpGDX.Headless;

public class HeadlessNet : Net
{
	public HeadlessNet(HeadlessApplicationConfiguration configuration){}
	public void sendHttpRequest(Net.HttpRequest httpRequest, Net.HttpResponseListener? httpResponseListener)
	{
		throw new NotImplementedException();
	}

	public void cancelHttpRequest(Net.HttpRequest httpRequest)
	{
		throw new NotImplementedException();
	}

	public ServerSocket newServerSocket(Net.Protocol protocol, string hostname, int port, ServerSocketHints hints)
	{
		throw new NotImplementedException();
	}

	public ServerSocket newServerSocket(Net.Protocol protocol, int port, ServerSocketHints hints)
	{
		throw new NotImplementedException();
	}

	public Socket newClientSocket(Net.Protocol protocol, string host, int port, SocketHints hints)
	{
		throw new NotImplementedException();
	}

	public bool openURI(string URI)
	{
		throw new NotImplementedException();
	}
}