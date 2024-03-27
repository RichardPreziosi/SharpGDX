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
		private readonly Dictionary<string, string> properties = new();
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
			properties[key] = (val).ToString();
			return this;
		}

		public Preferences putInteger(String key, int val)
		{
			properties[key]= (val).ToString();
			return this;
		}

		public Preferences putLong(String key, long val)
		{
			properties[key] = val.ToString();
			return this;
		}

		public Preferences putFloat(String key, float val)
		{
			properties[key] = (val).ToString(CultureInfo.InvariantCulture);
			return this;
		}

		public Preferences putString(String key, String val)
		{
			properties[key] = val;
			return this;
		}

		public Preferences put(Dictionary<String, object> vals)
		{
			foreach (var val in vals)
			{
				if (val.Value is Boolean) putBoolean(val.Key, (Boolean)val.Value);
				if (val.Value is int) putInteger(val.Key, (int)val.Value);
				if (val.Value is long) putLong(val.Key, (long)val.Value);
				if (val.Value is String) putString(val.Key, (String)val.Value);
				if (val.Value is float) putFloat(val.Key, (float)val.Value);
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
			return Boolean.Parse(properties.GetValueOrDefault(key, (defValue).ToString()));
		}

		public int getInteger(String key, int defValue)
		{
			return int.Parse(properties.GetValueOrDefault(key, (defValue).ToString()));
		}

		public long getLong(String key, long defValue)
		{
			return long.Parse(properties.GetValueOrDefault(key, (defValue).ToString()));
		}

		public float getFloat(String key, float defValue)
		{
			return float.Parse(properties.GetValueOrDefault(key, (defValue).ToString(CultureInfo.InvariantCulture)));
		}

		public String getString(String key, String defValue)
		{
			return properties.GetValueOrDefault(key, defValue);
		}

		public Dictionary<String, object> get()
		{
			Dictionary<String, Object> map = new Dictionary<String, Object>();

			// TODO: This only 'mostly' works.
			foreach (var val in properties)
			{
				if (bool.TryParse(val.Value, out var b))
					map[val.Key] = b;
				if (int.TryParse(val.Value, out var i)) map[val.Key] = i;
				if (long.TryParse(val.Value, out var l)) map[val.Key] = l;
				if (float.TryParse(val.Value, out var f)) map[val.Key] = f;
				map[val.Key] = val.Value;
			}

			return map;
		}

		public bool contains(String key)
		{
			return properties.ContainsKey(key);
		}

		public void clear()
		{
			properties.Clear();
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
			throw new NotImplementedException();
		}

		public void remove(String key)
		{
			properties.Remove(key);
		}
	}
}