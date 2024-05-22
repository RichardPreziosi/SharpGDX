using System.Collections;
using SharpGDX.Maps.Tiled.Objects;
using SharpGDX.Maps.Objects;
using SharpGDX.Maps.Tiled;
using SharpGDX.Maps.Tiled.Tiles;
using static SharpGDX.Maps.Tiled.TiledMapTileLayer;
using static SharpGDX.Utils.XmlReader;
using SharpGDX.Utils.Reflect;
using SharpGDX.Shims;
using SharpGDX.Assets;
using SharpGDX.Assets.Loaders;
using SharpGDX.Utils;
using SharpGDX.Graphics;
using static SharpGDX.Graphics.Texture;
using SharpGDX.Graphics.G2D;
using SharpGDX.Graphics.GLUtils;
using SharpGDX.Mathematics;

namespace SharpGDX.Maps.Tiled;

public abstract class BaseTmxMapLoader<P> : AsynchronousAssetLoader<TiledMap, P> 
where P: BaseTmxMapLoader<P>.Parameters
{

	public class Parameters : AssetLoaderParameters<TiledMap> {
		/** generate mipmaps? **/
		public bool generateMipMaps = false;
		/** The TextureFilter to use for minification **/
		public TextureFilter textureMinFilter = TextureFilter.Nearest;
		/** The TextureFilter to use for magnification **/
		public TextureFilter textureMagFilter = TextureFilter.Nearest;
		/** Whether to convert the objects' pixel position and size to the equivalent in tile space. **/
		public bool convertObjectToTileSpace = false;
		/** Whether to flip all Y coordinates so that Y positive is up. All libGDX renderers require flipped Y coordinates, and thus
		 * flipY set to true. This parameter is included for non-rendering related purposes of TMX files, or custom renderers. */
		public bool flipY = true;
	}

	protected static readonly int FLAG_FLIP_HORIZONTALLY = unchecked((int)0x80000000);
	protected static readonly int FLAG_FLIP_VERTICALLY = 0x40000000;
	protected static readonly int FLAG_FLIP_DIAGONALLY = 0x20000000;
	protected static readonly int MASK_CLEAR = unchecked((int)0xE0000000);

	protected XmlReader xml = new XmlReader();
	protected Element root;
	protected bool convertObjectToTileSpace;
	protected bool flipY = true;

	protected int mapTileWidth;
	protected int mapTileHeight;
	protected int mapWidthInPixels;
	protected int mapHeightInPixels;

	protected TiledMap map;
	protected IntMap<MapObject> idToObject;
	protected Array<Runnable> runOnEndOfLoadTiled;

	public BaseTmxMapLoader (FileHandleResolver resolver) 
	: base(resolver)
	{
		
	}

	public override Array<AssetDescriptor<TiledMap>> getDependencies (String fileName, FileHandle tmxFile, P parameter)
	{
		throw new NotImplementedException();
		//this.root = xml.parse(tmxFile);

		//TextureLoader.TextureParameter textureParameter = new TextureLoader.TextureParameter();
		//if (parameter != null) {
		//	textureParameter.genMipMaps = parameter.generateMipMaps;
		//	textureParameter.minFilter = parameter.textureMinFilter;
		//	textureParameter.magFilter = parameter.textureMagFilter;
		//}

		//return getDependencyAssetDescriptors(tmxFile, textureParameter);
	}

	/** Gets a map of the object ids to the {@link MapObject} instances. Returns null if
	 * {@link #loadTiledMap(FileHandle, Parameters, ImageResolver)} has not been called yet.
	 *
	 * @return the map of the ids to {@link MapObject}, or null if {@link #loadTiledMap(FileHandle, Parameters, ImageResolver)}
	 *         method has not been called yet. */
	public  IntMap<MapObject>? getIdToObject () {
		return idToObject;
	}

	protected abstract Array<AssetDescriptor> getDependencyAssetDescriptors (FileHandle tmxFile,
		TextureLoader.TextureParameter textureParameter);

	/** Loads the map data, given the XML root element
	 *
	 * @param tmxFile the Filehandle of the tmx file
	 * @param parameter
	 * @param imageResolver
	 * @return the {@link TiledMap} */
	protected TiledMap loadTiledMap (FileHandle tmxFile, P parameter, ImageResolver imageResolver)
	{
		throw new NotImplementedException();
		//this.map = new TiledMap();
		//this.idToObject = new ();
		//this.runOnEndOfLoadTiled = new ();

		//if (parameter != null) {
		//	this.convertObjectToTileSpace = parameter.convertObjectToTileSpace;
		//	this.flipY = parameter.flipY;
		//} else {
		//	this.convertObjectToTileSpace = false;
		//	this.flipY = true;
		//}

		//String mapOrientation = root.getAttribute("orientation", null);
		//int mapWidth = root.getIntAttribute("width", 0);
		//int mapHeight = root.getIntAttribute("height", 0);
		//int tileWidth = root.getIntAttribute("tilewidth", 0);
		//int tileHeight = root.getIntAttribute("tileheight", 0);
		//int hexSideLength = root.getIntAttribute("hexsidelength", 0);
		//String staggerAxis = root.getAttribute("staggeraxis", null);
		//String staggerIndex = root.getAttribute("staggerindex", null);
		//String mapBackgroundColor = root.getAttribute("backgroundcolor", null);

		//MapProperties mapProperties = map.getProperties();
		//if (mapOrientation != null) {
		//	mapProperties.put("orientation", mapOrientation);
		//}
		//mapProperties.put("width", mapWidth);
		//mapProperties.put("height", mapHeight);
		//mapProperties.put("tilewidth", tileWidth);
		//mapProperties.put("tileheight", tileHeight);
		//mapProperties.put("hexsidelength", hexSideLength);
		//if (staggerAxis != null) {
		//	mapProperties.put("staggeraxis", staggerAxis);
		//}
		//if (staggerIndex != null) {
		//	mapProperties.put("staggerindex", staggerIndex);
		//}
		//if (mapBackgroundColor != null) {
		//	mapProperties.put("backgroundcolor", mapBackgroundColor);
		//}
		//this.mapTileWidth = tileWidth;
		//this.mapTileHeight = tileHeight;
		//this.mapWidthInPixels = mapWidth * tileWidth;
		//this.mapHeightInPixels = mapHeight * tileHeight;

		//if (mapOrientation != null) {
		//	if ("staggered".Equals(mapOrientation)) {
		//		if (mapHeight > 1) {
		//			this.mapWidthInPixels += tileWidth / 2;
		//			this.mapHeightInPixels = mapHeightInPixels / 2 + tileHeight / 2;
		//		}
		//	}
		//}

		//Element properties = root.getChildByName("properties");
		//if (properties != null) {
		//	loadProperties(map.getProperties(), properties);
		//}

		//Array<Element> tilesets = root.getChildrenByName("tileset");
		//foreach (Element element in tilesets) {
		//	loadTileSet(element, tmxFile, imageResolver);
		//	root.removeChild(element);
		//}

		//for (int i = 0, j = root.getChildCount(); i < j; i++) {
		//	Element element = root.getChild(i);
		//	loadLayer(map, map.getLayers(), element, tmxFile, imageResolver);
		//}

		//// update hierarchical parallax scrolling factors
		//// in Tiled the final parallax scrolling factor of a layer is the multiplication of its factor with all its parents
		//// 1) get top level groups
		//Array<MapGroupLayer> groups = map.getLayers().getByType(typeof(MapGroupLayer));
		//while (groups.notEmpty()) {
		//	MapGroupLayer group = groups.first();
		//	groups.removeIndex(0);

		//	foreach (MapLayer child in group.getLayers()) {
		//		child.setParallaxX(child.getParallaxX() * group.getParallaxX());
		//		child.setParallaxY(child.getParallaxY() * group.getParallaxY());
		//		if (child is MapGroupLayer) {
		//			// 2) handle any child groups
		//			groups.add((MapGroupLayer)child);
		//		}
		//	}
		//}

		//foreach (Runnable runnable in runOnEndOfLoadTiled) {
		//	runnable.Invoke();
		//}
		//runOnEndOfLoadTiled = null;

		//return map;
	}

	protected void loadLayer (TiledMap map, MapLayers parentLayers, Element element, FileHandle tmxFile,
		ImageResolver imageResolver) {
		String name = element.getName();
		if (name.Equals("group")) {
			loadLayerGroup(map, parentLayers, element, tmxFile, imageResolver);
		} else if (name.Equals("layer")) {
			loadTileLayer(map, parentLayers, element);
		} else if (name.Equals("objectgroup")) {
			loadObjectGroup(map, parentLayers, element);
		} else if (name.Equals("imagelayer")) {
			loadImageLayer(map, parentLayers, element, tmxFile, imageResolver);
		}
	}

	protected void loadLayerGroup (TiledMap map, MapLayers parentLayers, Element element, FileHandle tmxFile,
		ImageResolver imageResolver) {
		if (element.getName().Equals("group")) {
			MapGroupLayer groupLayer = new MapGroupLayer();
			loadBasicLayerInfo(groupLayer, element);

			Element properties = element.getChildByName("properties");
			if (properties != null) {
				loadProperties(groupLayer.getProperties(), properties);
			}

			for (int i = 0, j = element.getChildCount(); i < j; i++) {
				Element child = element.getChild(i);
				loadLayer(map, groupLayer.getLayers(), child, tmxFile, imageResolver);
			}

			foreach (MapLayer layer in groupLayer.getLayers()) {
				layer.setParent(groupLayer);
			}

			parentLayers.add(groupLayer);
		}
	}

	protected void loadTileLayer (TiledMap map, MapLayers parentLayers, Element element)
	{
		throw new NotImplementedException();
		//if (element.getName().Equals("layer")) {
		//	int width = element.getIntAttribute("width", 0);
		//	int height = element.getIntAttribute("height", 0);
		//	int tileWidth = map.getProperties().get("tilewidth", typeof(int));
		//	int tileHeight = map.getProperties().get("tileheight", typeof(int));
		//	TiledMapTileLayer layer = new TiledMapTileLayer(width, height, tileWidth, tileHeight);

		//	loadBasicLayerInfo(layer, element);

		//	int[] ids = getTileIds(element, width, height);
		//	TiledMapTileSets tilesets = map.getTileSets();
		//	for (int y = 0; y < height; y++) {
		//		for (int x = 0; x < width; x++) {
		//			int id = ids[y * width + x];
		//			bool flipHorizontally = ((id & FLAG_FLIP_HORIZONTALLY) != 0);
		//			bool flipVertically = ((id & FLAG_FLIP_VERTICALLY) != 0);
		//			bool flipDiagonally = ((id & FLAG_FLIP_DIAGONALLY) != 0);

		//			TiledMapTile tile = tilesets.getTile(id & ~MASK_CLEAR);
		//			if (tile != null) {
		//				Cell cell = createTileLayerCell(flipHorizontally, flipVertically, flipDiagonally);
		//				cell.setTile(tile);
		//				layer.setCell(x, flipY ? height - 1 - y : y, cell);
		//			}
		//		}
		//	}

		//	Element properties = element.getChildByName("properties");
		//	if (properties != null) {
		//		loadProperties(layer.getProperties(), properties);
		//	}
		//	parentLayers.add(layer);
		//}
	}

	protected void loadObjectGroup (TiledMap map, MapLayers parentLayers, Element element) {
		if (element.getName().Equals("objectgroup")) {
			MapLayer layer = new MapLayer();
			loadBasicLayerInfo(layer, element);
			Element properties = element.getChildByName("properties");
			if (properties != null) {
				loadProperties(layer.getProperties(), properties);
			}

			foreach (Element objectElement in element.getChildrenByName("object")) {
				loadObject(map, layer, objectElement);
			}

			parentLayers.add(layer);
		}
	}

	protected void loadImageLayer (TiledMap map, MapLayers parentLayers, Element element, FileHandle tmxFile,
		ImageResolver imageResolver) {
		if (element.getName().Equals("imagelayer")) {
			float x = 0;
			float y = 0;
			if (element.hasAttribute("offsetx")) {
				x = float.Parse(element.getAttribute("offsetx", "0"));
			} else {
				x = float.Parse(element.getAttribute("x", "0"));
			}
			if (element.hasAttribute("offsety")) {
				y = float.Parse(element.getAttribute("offsety", "0"));
			} else {
				y = float.Parse(element.getAttribute("y", "0"));
			}
			if (flipY) y = mapHeightInPixels - y;

			TextureRegion texture = null;

			Element image = element.getChildByName("image");

			if (image != null) {
				String source = image.getAttribute("source");
				FileHandle handle = getRelativeFileHandle(tmxFile, source);
				texture = imageResolver.getImage(handle.path());
				y -= texture.getRegionHeight();
			}

			TiledMapImageLayer layer = new TiledMapImageLayer(texture, x, y);

			loadBasicLayerInfo(layer, element);

			Element properties = element.getChildByName("properties");
			if (properties != null) {
				loadProperties(layer.getProperties(), properties);
			}

			parentLayers.add(layer);
		}
	}

	protected void loadBasicLayerInfo (MapLayer layer, Element element) {
		String name = element.getAttribute("name", null);
		float opacity = float.Parse(element.getAttribute("opacity", "1.0"));
		bool visible = element.getIntAttribute("visible", 1) == 1;
		float offsetX = element.getFloatAttribute("offsetx", 0);
		float offsetY = element.getFloatAttribute("offsety", 0);
		float parallaxX = element.getFloatAttribute("parallaxx", 1f);
		float parallaxY = element.getFloatAttribute("parallaxy", 1f);

		layer.setName(name);
		layer.setOpacity(opacity);
		layer.setVisible(visible);
		layer.setOffsetX(offsetX);
		layer.setOffsetY(offsetY);
		layer.setParallaxX(parallaxX);
		layer.setParallaxY(parallaxY);
	}

	protected void loadObject (TiledMap map, MapLayer layer, Element element) {
		loadObject(map, layer.getObjects(), element, mapHeightInPixels);
	}

	protected void loadObject (TiledMap map, TiledMapTile tile, Element element) {
		loadObject(map, tile.getObjects(), element, tile.getTextureRegion().getRegionHeight());
	}

	protected void loadObject (TiledMap map, MapObjects objects, Element element, float heightInPixels) {
		if (element.getName().Equals("object"))
		{
			MapObject obj = null;

			float scaleX = convertObjectToTileSpace ? 1.0f / mapTileWidth : 1.0f;
			float scaleY = convertObjectToTileSpace ? 1.0f / mapTileHeight : 1.0f;

			float x = element.getFloatAttribute("x", 0) * scaleX;
			float y =
				(flipY ? (heightInPixels - element.getFloatAttribute("y", 0)) : element.getFloatAttribute("y", 0)) *
				scaleY;

			float width = element.getFloatAttribute("width", 0) * scaleX;
			float height = element.getFloatAttribute("height", 0) * scaleY;

			if (element.getChildCount() > 0)
			{
				Element child = null;
				if ((child = element.getChildByName("polygon")) != null)
				{
					String[] points = child.getAttribute("points").Split(" ");
					float[] vertices = new float[points.Length * 2];
					for (int i = 0; i < points.Length; i++)
					{
						String[] point = points[i].Split(",");
						vertices[i * 2] = float.Parse(point[0]) * scaleX;
						vertices[i * 2 + 1] = float.Parse(point[1]) * scaleY * (flipY ? -1 : 1);
					}

					Polygon polygon = new Polygon(vertices);
					polygon.setPosition(x, y);
					obj = new PolygonMapObject(polygon);
				}
				else if ((child = element.getChildByName("polyline")) != null)
				{
					String[] points = child.getAttribute("points").Split(" ");
					float[] vertices = new float[points.Length * 2];
					for (int i = 0; i < points.Length; i++)
					{
						String[] point = points[i].Split(",");
						vertices[i * 2] = float.Parse(point[0]) * scaleX;
						vertices[i * 2 + 1] = float.Parse(point[1]) * scaleY * (flipY ? -1 : 1);
					}

					Polyline polyline = new Polyline(vertices);
					polyline.setPosition(x, y);
					obj = new PolylineMapObject(polyline);
				}
				else if ((child = element.getChildByName("ellipse")) != null)
				{
					obj = new EllipseMapObject(x, flipY ? y - height : y, width, height);
				}
			}

			if (obj == null)
			{
				String gid = null;
				if ((gid = element.getAttribute("gid", null)) != null)
				{
					int id = (int)long.Parse(gid);
					bool flipHorizontally = ((id & FLAG_FLIP_HORIZONTALLY) != 0);
					bool flipVertically = ((id & FLAG_FLIP_VERTICALLY) != 0);

					TiledMapTile tile = map.getTileSets().getTile(id & ~MASK_CLEAR);
					TiledMapTileMapObject tiledMapTileMapObject =
						new TiledMapTileMapObject(tile, flipHorizontally, flipVertically);
					TextureRegion textureRegion = tiledMapTileMapObject.getTextureRegion();
					tiledMapTileMapObject.getProperties().put("gid", id);
					tiledMapTileMapObject.setX(x);
					tiledMapTileMapObject.setY(flipY ? y : y - height);
					float objectWidth = element.getFloatAttribute("width", textureRegion.getRegionWidth());
					float objectHeight = element.getFloatAttribute("height", textureRegion.getRegionHeight());
					tiledMapTileMapObject.setScaleX(scaleX * (objectWidth / textureRegion.getRegionWidth()));
					tiledMapTileMapObject.setScaleY(scaleY * (objectHeight / textureRegion.getRegionHeight()));
					tiledMapTileMapObject.setRotation(element.getFloatAttribute("rotation", 0));
					obj = tiledMapTileMapObject;
				}
				else
				{
					obj = new RectangleMapObject(x, flipY ? y - height : y, width, height);
				}
			}

			{
				obj.setName(element.getAttribute("name", null));
				String rotation = element.getAttribute("rotation", null);
				if (rotation != null)
				{
					obj.getProperties().put("rotation", float.Parse(rotation));
				}

				String type = element.getAttribute("type", null);
				if (type != null)
				{
					obj.getProperties().put("type", type);
				}

				int id = element.getIntAttribute("id", 0);
				if (id != 0)
				{
					obj.getProperties().put("id", id);
				}

				obj.getProperties().put("x", x);

				if (obj is TiledMapTileMapObject)
				{
					obj.getProperties().put("y", y);
				}
				else
				{
					obj.getProperties().put("y", (flipY ? y - height : y));
				}

				obj.getProperties().put("width", width);
				obj.getProperties().put("height", height);
				obj.setVisible(element.getIntAttribute("visible", 1) == 1);
				Element properties = element.getChildByName("properties");
				if (properties != null)
				{
					loadProperties(obj.getProperties(), properties);
				}

				idToObject.put(id, obj);
				objects.add(obj);
			}
		}
	}

	protected void loadProperties ( MapProperties properties, Element element) {
		if (element == null) return;
		if (element.getName().Equals("properties")) {
		foreach (Element property in element.getChildrenByName("property")) {
				 String name = property.getAttribute("name", null);
				String value = property.getAttribute("value", null);
				String type = property.getAttribute("type", null);
				if (value == null) {
					value = property.getText();
				}
				if (type != null && type.Equals("object")) {
					// Wait until the end of [loadTiledMap] to fetch the object
					try {
						// Value should be the id of the object being pointed to
						int id = int.Parse(value);
						// Create [Runnable] to fetch object and add it to props
						Runnable fetch = () => {
							
								MapObject obj = idToObject.get(id);
								properties.put(name, obj);
						};
						// [Runnable] should not run until the end of [loadTiledMap]
						runOnEndOfLoadTiled.add(fetch);
					} catch (Exception exception) {
						throw new GdxRuntimeException(
							"Error parsing property [\" + name + \"] of type \"object\" with value: [" + value + "]", exception);
					}
				} else {
					Object castValue = castProperty(name, value, type);
					properties.put(name, castValue);
				}
			}
		}
	}

	protected Object castProperty (String name, String value, String type)
	{
		throw new NotImplementedException();
		//if (type == null) {
		//	return value;
		//} else if (type.Equals("int")) {
		//	return Integer.valueOf(value);
		//} else if (type.Equals("float")) {
		//	return Float.valueOf(value);
		//} else if (type.Equals("bool")) {
		//	return Boolean.valueOf(value);
		//} else if (type.Equals("color")) {
		//	// Tiled uses the format #AARRGGBB
		//	String opaqueColor = value.Substring(3);
		//	String alpha = value.Substring(1, 3);
		//	return Color.valueOf(opaqueColor + alpha);
		//} else {
		//	throw new GdxRuntimeException(
		//		"Wrong type given for property " + name + ", given : " + type + ", supported : string, bool, int, float, color");
		//}
	}

	protected Cell createTileLayerCell (bool flipHorizontally, bool flipVertically, bool flipDiagonally) {
		Cell cell = new Cell();
		if (flipDiagonally) {
			if (flipHorizontally && flipVertically) {
				cell.setFlipHorizontally(true);
				cell.setRotation(Cell.ROTATE_270);
			} else if (flipHorizontally) {
				cell.setRotation(Cell.ROTATE_270);
			} else if (flipVertically) {
				cell.setRotation(Cell.ROTATE_90);
			} else {
				cell.setFlipVertically(true);
				cell.setRotation(Cell.ROTATE_270);
			}
		} else {
			cell.setFlipHorizontally(flipHorizontally);
			cell.setFlipVertically(flipVertically);
		}
		return cell;
	}

	static public int[] getTileIds (Element element, int width, int height)
	{
		throw new NotImplementedException();
		//Element data = element.getChildByName("data");
		//String encoding = data.getAttribute("encoding", null);
		//if (encoding == null) { // no 'encoding' attribute means that the encoding is XML
		//	throw new GdxRuntimeException("Unsupported encoding (XML) for TMX Layer Data");
		//}
		//int[] ids = new int[width * height];
		//if (encoding.Equals("csv")) {
		//	String[] array = data.getText().Split(",");
		//	for (int i = 0; i < array.Length; i++)
		//		ids[i] = (int)long.Parse(array[i].Trim());
		//} else {
		//	if (true) if (encoding.Equals("base64")) {
		//		InputStream @is = null;
		//		try {
		//			String compression = data.getAttribute("compression", null);
		//			var bytes = Base64Coder.decode(data.getText());
		//			if (compression == null)
		//				@is = new ByteArrayInputStream(bytes);
		//			else if (compression.Equals("gzip"))
		//				@is = new BufferedInputStream(new GZIPInputStream(new ByteArrayInputStream(bytes), bytes.Length));
		//			else if (compression.Equals("zlib"))
		//				@is = new BufferedInputStream(new InflaterInputStream(new ByteArrayInputStream(bytes)));
		//			else
		//				throw new GdxRuntimeException("Unrecognised compression (" + compression + ") for TMX Layer Data");

		//			byte[] temp = new byte[4];
		//			for (int y = 0; y < height; y++) {
		//				for (int x = 0; x < width; x++) {
		//					int read = @is.read(temp);
		//					while (read < temp.Length) {
		//						int curr = @is.read(temp, read, temp.Length - read);
		//						if (curr == -1) break;
		//						read += curr;
		//					}
		//					if (read != temp.Length)
		//						throw new GdxRuntimeException("Error Reading TMX Layer Data: Premature end of tile data");
		//					ids[y * width + x] = unsignedByteToInt(temp[0]) | unsignedByteToInt(temp[1]) << 8
		//						| unsignedByteToInt(temp[2]) << 16 | unsignedByteToInt(temp[3]) << 24;
		//				}
		//			}
		//		} catch (IOException e) {
		//			throw new GdxRuntimeException("Error Reading TMX Layer Data - IOException: " + e.Message);
		//		} finally {
		//			StreamUtils.closeQuietly(@is);
		//		}
		//	} else {
		//		// any other value of 'encoding' is one we're not aware of, probably a feature of a future version of Tiled
		//		// or another editor
		//		throw new GdxRuntimeException("Unrecognised encoding (" + encoding + ") for TMX Layer Data");
		//	}
		//}
		//return ids;
	}

	protected static int unsignedByteToInt (byte b) {
		return b & 0xFF;
	}

	protected static FileHandle getRelativeFileHandle (FileHandle file, String path)
	{
		throw new NotImplementedException();
		//StringTokenizer tokenizer = new StringTokenizer(path, "\\/");
		//FileHandle result = file.parent();
		//while (tokenizer.hasMoreElements()) {
		//	String token = tokenizer.nextToken();
		//	if (token.Equals(".."))
		//		result = result.parent();
		//	else {
		//		result = result.child(token);
		//	}
		//}
		//return result;
	}

	protected void loadTileSet (Element element, FileHandle tmxFile, ImageResolver imageResolver) {
		if (element.getName().Equals("tileset")) {
			int firstgid = element.getIntAttribute("firstgid", 1);
			String imageSource = "";
			int imageWidth = 0;
			int imageHeight = 0;
			FileHandle image = null;

			String source = element.getAttribute("source", null);
			if (source != null) {
				FileHandle tsx = getRelativeFileHandle(tmxFile, source);
				try {
					element = xml.parse(tsx);
					Element imageElement = element.getChildByName("image");
					if (imageElement != null) {
						imageSource = imageElement.getAttribute("source");
						imageWidth = imageElement.getIntAttribute("width", 0);
						imageHeight = imageElement.getIntAttribute("height", 0);
						image = getRelativeFileHandle(tsx, imageSource);
					}
				} catch (SerializationException e) {
					throw new GdxRuntimeException("Error parsing external tileset.");
				}
			} else {
				Element imageElement = element.getChildByName("image");
				if (imageElement != null) {
					imageSource = imageElement.getAttribute("source");
					imageWidth = imageElement.getIntAttribute("width", 0);
					imageHeight = imageElement.getIntAttribute("height", 0);
					image = getRelativeFileHandle(tmxFile, imageSource);
				}
			}
			String name = element.get("name", null);
			int tilewidth = element.getIntAttribute("tilewidth", 0);
			int tileheight = element.getIntAttribute("tileheight", 0);
			int spacing = element.getIntAttribute("spacing", 0);
			int margin = element.getIntAttribute("margin", 0);

			Element offset = element.getChildByName("tileoffset");
			int offsetX = 0;
			int offsetY = 0;
			if (offset != null) {
				offsetX = offset.getIntAttribute("x", 0);
				offsetY = offset.getIntAttribute("y", 0);
			}
			TiledMapTileSet tileSet = new TiledMapTileSet();

			// TileSet
			tileSet.setName(name);
			MapProperties tileSetProperties = tileSet.getProperties();
			Element properties = element.getChildByName("properties");
			if (properties != null) {
				loadProperties(tileSetProperties, properties);
			}
			tileSetProperties.put("firstgid", firstgid);

			// Tiles
			Array<Element> tileElements = element.getChildrenByName("tile");

			addStaticTiles(tmxFile, imageResolver, tileSet, element, tileElements, name, firstgid, tilewidth, tileheight, spacing,
				margin, source, offsetX, offsetY, imageSource, imageWidth, imageHeight, image);

			Array<AnimatedTiledMapTile> animatedTiles = new Array<AnimatedTiledMapTile>();

			foreach (Element tileElement in tileElements) {
				int localtid = tileElement.getIntAttribute("id", 0);
				TiledMapTile tile = tileSet.getTile(firstgid + localtid);
				if (tile != null) {
					AnimatedTiledMapTile animatedTile = createAnimatedTile(tileSet, tile, tileElement, firstgid);
					if (animatedTile != null) {
						animatedTiles.add(animatedTile);
						tile = animatedTile;
					}
					addTileProperties(tile, tileElement);
					addTileObjectGroup(tile, tileElement);
				}
			}

			// replace original static tiles by animated tiles
			foreach (AnimatedTiledMapTile animatedTile in animatedTiles) {
				tileSet.putTile(animatedTile.getId(), animatedTile);
			}

			map.getTileSets().addTileSet(tileSet);
		}
	}

	protected abstract void addStaticTiles (FileHandle tmxFile, ImageResolver imageResolver, TiledMapTileSet tileset,
		Element element, Array<Element> tileElements, String name, int firstgid, int tilewidth, int tileheight, int spacing,
		int margin, String source, int offsetX, int offsetY, String imageSource, int imageWidth, int imageHeight, FileHandle image);

	protected void addTileProperties (TiledMapTile tile, Element tileElement) {
		String terrain = tileElement.getAttribute("terrain", null);
		if (terrain != null) {
			tile.getProperties().put("terrain", terrain);
		}
		String probability = tileElement.getAttribute("probability", null);
		if (probability != null) {
			tile.getProperties().put("probability", probability);
		}
		String type = tileElement.getAttribute("type", null);
		if (type != null) {
			tile.getProperties().put("type", type);
		}
		Element properties = tileElement.getChildByName("properties");
		if (properties != null) {
			loadProperties(tile.getProperties(), properties);
		}
	}

	protected void addTileObjectGroup (TiledMapTile tile, Element tileElement) {
		Element objectgroupElement = tileElement.getChildByName("objectgroup");
		if (objectgroupElement != null) {
			foreach (Element objectElement in objectgroupElement.getChildrenByName("object")) {
				loadObject(map, tile, objectElement);
			}
		}
	}

	protected AnimatedTiledMapTile createAnimatedTile (TiledMapTileSet tileSet, TiledMapTile tile, Element tileElement,
		int firstgid) {
		Element animationElement = tileElement.getChildByName("animation");
		if (animationElement != null) {
			Array<StaticTiledMapTile> staticTiles = new Array<StaticTiledMapTile>();
			IntArray intervals = new IntArray();
			foreach (Element frameElement in animationElement.getChildrenByName("frame")) {
				staticTiles.add((StaticTiledMapTile)tileSet.getTile(firstgid + frameElement.getIntAttribute("tileid")));
				intervals.add(frameElement.getIntAttribute("duration"));
			}

			AnimatedTiledMapTile animatedTile = new AnimatedTiledMapTile(intervals, staticTiles);
			animatedTile.setId(tile.getId());
			return animatedTile;
		}
		return null;
	}

	protected void addStaticTiledMapTile (TiledMapTileSet tileSet, TextureRegion textureRegion, int tileId, float offsetX,
		float offsetY) {
		TiledMapTile tile = new StaticTiledMapTile(textureRegion);
		tile.setId(tileId);
		tile.setOffsetX(offsetX);
		tile.setOffsetY(flipY ? -offsetY : offsetY);
		tileSet.putTile(tileId, tile);
	}
}