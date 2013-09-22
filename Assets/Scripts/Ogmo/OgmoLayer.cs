using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public enum LayerType
{
	TILES,
	GRID,
	ENTITIES
}

public class OgmoLayer
{
	public string name;
	public int tileWidth;
	public int tileHeight;
	public List<OgmoEntity> entities;
	public List<OgmoTile> tiles;
 
	public OgmoLayer (XmlElement layerNode, int width, int height)
	{  
		entities = new List<OgmoEntity> ();
		LayerType type = LayerType.ENTITIES;
		name = layerNode.Name;
		if (layerNode.Attributes.GetNamedItem ("tileset") != null) {
			type = LayerType.TILES;
		} else if (layerNode.Attributes.GetNamedItem ("exportMode") != null) {
			type = LayerType.GRID;
		}
  
		switch (type) {
		case LayerType.ENTITIES:
			ParseEntityLayer (layerNode);
			break;
		case LayerType.TILES:
			ParseGridLayer (layerNode, width, height);
			break;
		case LayerType.GRID:
			ParseGridLayer (layerNode, width, height);
			break;
		}
	}
 
	protected void ParseEntityLayer (XmlElement layerNode)
	{
		tiles = null;
		tileWidth = 0;
		tileHeight = 0;
		
		foreach (XmlElement child in layerNode) {
			entities.Add (new OgmoEntity (child));
		}
	}
 
	protected void ParseGridLayer (XmlElement layerNode, int width, int height)
	{
		string attr = layerNode.Attributes ["exportMode"].Value;
		switch (attr) {
		case "XML":
			ParseXmlLayer (layerNode, width, height);
			break;
		case "XMLCoords":
			ParseXmlCoordsLayer (layerNode, width, height);
			break;
		case "CSV":
			ParseCsvLayer (layerNode, width, height);
			break;
		}
	}
 
	protected void ParseXmlLayer (XmlElement layerNode, int width, int height)
	{
		tiles = new List<OgmoTile> (layerNode.ChildNodes.Count);
		foreach (XmlElement element in layerNode) {
			tiles.Add (new OgmoTile{
				id = int.Parse (element.GetAttribute ("id")),
				x = int.Parse (element.GetAttribute ("x")),
				y = int.Parse (element.GetAttribute ("y"))
			});
		}
	}
	
	protected void ParseBitLayer (XmlElement layerNode, int width, int height)
	{
		tiles = new List<OgmoTile> (width * height);
		var rows = layerNode.InnerText.Split (new string[]{"\r\n","\n"}, System.StringSplitOptions.RemoveEmptyEntries);
		for (int y=0; y<rows.Length; y++) {
			string row = rows [y];
			for (int x=0; x<row.Length; x++) {
				tiles.Add (new OgmoTile{
					id = short.Parse (rows [x]),
					x = x,
					y = y
				});
			}
		}
	}
	
	protected void ParseXmlCoordsLayer (XmlElement layerNode, int width, int height)
	{
		tiles = new List<OgmoTile> (layerNode.ChildNodes.Count);
		foreach (XmlElement element in layerNode) {
			tiles.Add (new OgmoTile{
				id = short.Parse (element.GetAttribute ("ty")) * 10 +
					short.Parse (element.GetAttribute ("tx")),
				x = int.Parse (element.GetAttribute ("x")),
				y = int.Parse (element.GetAttribute ("y"))
			});
		}
	}
	
	protected void ParseCsvLayer (XmlElement layerNode, int width, int height)
	{
		tiles = new List<OgmoTile> (width * height);
		var rows = layerNode.InnerText.Split (new string[]{"\r\n","\n"}, System.StringSplitOptions.RemoveEmptyEntries);
		for (int y=0; y<rows.Length; y++) {
			string row = rows [y];
			var cols = row.Split (new string[]{","," ","\t"}, System.StringSplitOptions.RemoveEmptyEntries);
			for (int x=0; x<cols.Length; x++) {
				tiles.Add (new OgmoTile{
				id = short.Parse (cols [x]),
				x = x,
				y = y
			});
			}
		}
	}
}