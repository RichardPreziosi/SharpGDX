using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.net;

namespace SharpGDX.Desktop
{
	public class DesktopNet : Net
	{
		public DesktopNet(DesktopApplicationConfiguration config)
		{

		}
		public void sendHttpRequest(Net.HttpRequest httpRequest, Net.HttpResponseListener httpResponseListener)
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
}
