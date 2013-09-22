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
 
	public OgmoLayer (XmlNode layerNode, int width, int height)
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
			ParseTileLayer (layerNode, width, height);
			break;
		case LayerType.GRID:
			ParseGridLayer (layerNode, width, height);
			break;
		}
	}
 
	protected void ParseEntityLayer (XmlNode layerNode)
	{
		tiles = null;
		tileWidth = 0;
		tileHeight = 0;
  
		for (int ii=0; ii < layerNode.ChildNodes.Count; ii++) {
			entities.Add (new OgmoEntity (layerNode.ChildNodes [ii]));
		}
	}
 
	protected void ParseGridLayer (XmlNode layerNode, int width, int height)
	{
		tiles = new List<OgmoTile> (layerNode.ChildNodes.Count);
		foreach (XmlElement element in layerNode) {
			tiles.Add (new OgmoTile{
				id = int.Parse(element.GetAttribute("id")),
				x = int.Parse (element.GetAttribute ("x")),
				y = int.Parse (element.GetAttribute ("y"))
			});
		}
	}
 
	protected void ParseTileLayer (XmlNode layerNode, int width, int height)
	{/*
		tiles = new List<OgmoTile> (layerNode.ChildNodes.Count);
		foreach (XmlElement element in layerNode) {
			tiles.Add (new OgmoTile{
				id = int.Parse(element.GetAttribute("id")),
				x = int.Parse (element.GetAttribute ("x")),
				y = int.Parse (element.GetAttribute ("y"))
			});
		}
		*/
	}
}