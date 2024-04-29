using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Assets
{
	public interface AssetErrorListener
	{
		public void error(AssetDescriptor asset, Exception throwable);
	}
}
