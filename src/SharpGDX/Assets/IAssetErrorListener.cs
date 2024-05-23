using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Assets
{
	public interface IAssetErrorListener
	{
		public void error(IAssetDescriptor asset, Exception throwable);
	}
}
