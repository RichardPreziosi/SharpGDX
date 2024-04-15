﻿using SharpGDX.Shims;

namespace SharpGDX
{
	/**
 * <p>
 * A Preference instance is a hash map holding different values. It is stored alongside your application (SharedPreferences on
 * Android, LocalStorage on GWT, on the desktop a Java Preferences file in a ".prefs" directory will be created, and on iOS an
 * NSMutableDictionary will be written to the given file). CAUTION: On the desktop platform, all libGDX applications share the
 * same ".prefs" directory. To avoid collisions use specific names like "com.myname.game1.settings" instead of "settings".
 * </p>
 * 
 * <p>
 * To persist changes made to a preferences instance {@link #flush()} has to be invoked. With the exception of Android, changes
 * are cached in memory prior to flushing. On iOS changes are not synchronized between different preferences instances.
 * </p>
 * 
 * <p>
 * Use {@link Application#getPreferences(String)} to look up a specific preferences instance. Note that on several backends the
 * preferences name will be used as the filename, so make sure the name is valid for a filename.
 * </p>
 * 
 * @author mzechner */
	public interface Preferences
	{
		public Preferences putBoolean(String key, bool val);

		public Preferences putInteger(String key, int val);

		public Preferences putLong(String key, long val);

		public Preferences putFloat(String key, float val);

		public Preferences putString(String key, String val);

		public Preferences put(Map<String, object> vals);

		public bool getBoolean(String key);

		public int getInteger(String key);

		public long getLong(String key);

		public float getFloat(String key);
		public String getString(String key);

		public bool getBoolean(String key, bool defValue);

		public int getInteger(String key, int defValue);

		public long getLong(String key, long defValue);

		public float getFloat(String key, float defValue);

		public String getString(String key, String defValue);

		/** Returns a read only Map<String, Object> with all the key, objects of the preferences. */
		public Map<String, object> get();

		public bool contains(String key);

		public void clear();

		public void remove(String key);

		/** Makes sure the preferences are persisted. */
		public void flush();
	}
}
