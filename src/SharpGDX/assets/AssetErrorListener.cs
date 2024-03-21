using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.assets
{
	public interface AssetErrorListener
	{
		// TODO: Can this be T or does it need to switch to object?
		public void error(AssetDescriptor asset, Exception throwable);
	}
}
