using SharpGDX.net;

namespace SharpGDX.Headless
{
	/** Headless implementation of the {@link com.badlogic.gdx.Net} API, based on LWJGL implementation
 * @author acoppes
 * @author Jon Renner */
	public class HeadlessNet : Net
	{

	// TODO: NetJavaImpl netJavaImpl;

	public HeadlessNet(HeadlessApplicationConfiguration configuration)
	{
		// TODO: netJavaImpl = new NetJavaImpl(configuration.maxNetThreads);
		}

		public void sendHttpRequest(SharpGDX.Net.HttpRequest httpRequest, Net.HttpResponseListener httpResponseListener)
	{
		// TODO: netJavaImpl.sendHttpRequest(httpRequest, httpResponseListener);
		}

		public void cancelHttpRequest(Net.HttpRequest httpRequest)
	{
		// TODO: netJavaImpl.cancelHttpRequest(httpRequest);
		}

		public ServerSocket newServerSocket(Net.Protocol protocol, String hostname, int port, ServerSocketHints hints)
	{
		// TODO: return new NetJavaServerSocketImpl(protocol, hostname, port, hints);
		throw new NotImplementedException();
	}

		public ServerSocket newServerSocket(Net.Protocol protocol, int port, ServerSocketHints hints)
	{
		// TODO: return new NetJavaServerSocketImpl(protocol, port, hints);
		throw new NotImplementedException();
	}

		public Socket newClientSocket(Net.Protocol protocol, String host, int port, SocketHints hints)
	{
		// TODO: return new NetJavaSocketImpl(protocol, host, port, hints);
		throw new NotImplementedException();
	}

		public bool openURI(String URI)
	{
		// TODO: Port
		//bool result = false;
		//try
		//{
		//	if (!GraphicsEnvironment.isHeadless() && Desktop.isDesktopSupported())
		//	{
		//		if (Desktop.getDesktop().isSupported(Action.BROWSE))
		//		{
		//			Desktop.getDesktop().browse(java.net.URI.create(URI));
		//			result = true;
		//		}
		//	}
		//	else
		//	{
		//		Gdx.app.error("HeadlessNet", "Opening URIs on this environment is not supported. Ignoring.");
		//	}
		//}
		//catch (Exception t)
		//{
		//	Gdx.app.error("HeadlessNet", "Failed to open URI. ", t);
		//}
		//return result;
		throw new NotImplementedException();
	}
	}
}
