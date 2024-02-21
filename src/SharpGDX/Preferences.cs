using SharpGDX.shims;

namespace SharpGDX;

/**
 * <p>
 *     A Preference instance is a hash map holding different values. It is stored alongside your application
 *     (SharedPreferences on
 *     Android, LocalStorage on GWT, on the desktop a Java Preferences file in a ".prefs" directory will be created, and
 *     on iOS an
 *     NSMutableDictionary will be written to the given file). CAUTION: On the desktop platform, all libGDX applications
 *     share the
 *     same ".prefs" directory. To avoid collisions use specific names like "com.myname.game1.settings" instead of
 *     "settings".
 * </p>
 * <p>
 *     To persist changes made to a preferences instance {@link #flush()} has to be invoked. With the exception of
 *     Android, changes
 *     are cached in memory prior to flushing. On iOS changes are not synchronized between different preferences
 *     instances.
 * </p>
 * <p>
 *     Use {@link Application#getPreferences(String)} to look up a specific preferences instance. Note that on several
 *     backends the
 *     preferences name will be used as the filename, so make sure the name is valid for a filename.
 * </p>
 * @author mzechner
 */
public interface Preferences
{
	public void clear();

	public bool contains(string key);

	/**
	 * Makes sure the preferences are persisted.
	 */
	public void flush();

	/**
	 * Returns a read only Map
	 * <String, Object> with all the key, objects of the preferences.
	 */
	public Map<string, object> get();

	public bool getBoolean(string key);

	public bool getBoolean(string key, bool defValue);
	public float getFloat(string key);

	public float getFloat(string key, float defValue);

	public int getInteger(string key);

	public int getInteger(string key, int defValue);

	public long getLong(string key);

	public long getLong(string key, long defValue);
	public string getString(string key);

	public string getString(string key, string defValue);

	public Preferences put(Map<string, object> vals);
	public Preferences putBoolean(string key, bool val);

	public Preferences putFloat(string key, float val);

	public Preferences putInteger(string key, int val);

	public Preferences putLong(string key, long val);

	public Preferences putString(string key, string val);

	public void remove(string key);
}