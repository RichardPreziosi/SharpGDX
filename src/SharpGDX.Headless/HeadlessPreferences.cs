using SharpGDX.files;
using SharpGDX.shims;
using SharpGDX.utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SharpGDX.Files;

namespace SharpGDX.Headless
{
	public class HeadlessPreferences : Preferences
	{
		private readonly Map<string, string> properties = new();
		private readonly FileHandle file;

		public HeadlessPreferences(String name, String directory)
		{
			// TODO: PORT
			// this(new HeadlessFileHandle(new File(directory, name), FileType.External));
		}

		public HeadlessPreferences(FileHandle file)
		{
			// TODO: Port
			//this.file = file;
			//if (!file.exists()) return;
			//InputStream in = null;
			//try
			//{
			//	in = new BufferedInputStream(file.read());
			//	properties.loadFromXML(in);
			//}
			//catch (Throwable t)
			//{
			//	t.printStackTrace();
			//}
			//finally
			//{
			//	StreamUtils.closeQuietly(in);
			//}
		}

		public Preferences putBoolean(String key, bool val)
		{
			properties.put(key, (val).ToString());
			return this;
		}

		public Preferences putInteger(String key, int val)
		{
			properties.put(key, (val).ToString());
			return this;
		}

		public Preferences putLong(String key, long val)
		{
			properties.put(key, val.ToString());
			return this;
		}

		public Preferences putFloat(String key, float val)
		{
			properties.put(key, (val).ToString(CultureInfo.InvariantCulture));
			return this;
		}

		public Preferences putString(String key, String val)
		{
			properties.put(key, val);
			return this;
		}

		public Preferences put(Map<String, object> vals)
		{
			foreach (var val in vals.entrySet())
			{
				if (val.getValue() is Boolean) putBoolean(val.getKey(), (Boolean)val.getValue());
				if (val.getValue() is int) putInteger(val.getKey(), (int)val.getValue());
				if (val.getValue() is long) putLong(val.getKey(), (long)val.getValue());
				if (val.getValue() is String) putString(val.getKey(), (String)val.getValue());
				if (val.getValue() is float) putFloat(val.getKey(), (float)val.getValue());
			}

			return this;
		}

		public bool getBoolean(String key)
		{
			return getBoolean(key, false);
		}

		public int getInteger(String key)
		{
			return getInteger(key, 0);
		}

		public long getLong(String key)
		{
			return getLong(key, 0);
		}

		public float getFloat(String key)
		{
			return getFloat(key, 0);
		}

		public String getString(String key)
		{
			return getString(key, "");
		}

		public bool getBoolean(String key, bool defValue)
		{
			return Boolean.Parse(properties.get(key, (defValue).ToString()));
		}

		public int getInteger(String key, int defValue)
		{
			return int.Parse(properties.get(key, (defValue).ToString()));
		}

		public long getLong(String key, long defValue)
		{
			return long.Parse(properties.get(key, (defValue).ToString()));
		}

		public float getFloat(String key, float defValue)
		{
			return float.Parse(properties.get(key, (defValue).ToString(CultureInfo.InvariantCulture)));
		}

		public String getString(String key, String defValue)
		{
			return properties.get(key, defValue);
		}

		public Map<String, object> get()
		{
			Map<String, Object> map = new HashMap<String, Object>();

			// TODO: This only 'mostly' works.
			foreach (Map<string, string>.Entry<string, string> val in properties.entrySet())
			{
				if (bool.TryParse(val.getValue(), out var b))
					map.put(val.getKey(), b);
				if (int.TryParse(val.getValue(), out var i)) map.put(val.getKey(), i);
				if (long.TryParse(val.getValue(), out var l)) map.put(val.getKey(), l);
				if (float.TryParse(val.getValue(), out var f)) map.put(val.getKey(), f);
				map.put(val.getKey(), val.getValue());
			}

			return map;
		}

		public bool contains(String key)
		{
			return properties.containsKey(key);
		}

		public void clear()
		{
			properties.clear();
		}

		public void flush()
		{
			// TODO: Port
			//OutputStream @out = null;
			//try
			//{
			//	@out = new BufferedOutputStream(file.write(false));
			//	properties.storeToXML(@out, null);
			//}
			//catch (Exception ex)
			//{
			//	throw new GdxRuntimeException("Error writing preferences: " + file, ex);
			//}
			//finally
			//{
			//	StreamUtils.closeQuietly(@out);
			//}
		}

		public void remove(String key)
		{
			properties.remove(key);
		}
	}
}