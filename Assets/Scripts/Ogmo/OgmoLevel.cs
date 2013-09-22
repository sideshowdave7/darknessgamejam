using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class OgmoLevel
{
	public string name;
	public int width;
	public int height;
	public Dictionary<string,OgmoLayer> layers;

	public OgmoLevel (TextAsset xmlFile)
	{
		name = xmlFile.name;
		XmlDocument xml = new XmlDocument ();
		xml.LoadXml (xmlFile.text);
		XmlElement root = (XmlElement)xml.FirstChild;
		width = int.Parse (root.GetAttribute ("width"));
		height = int.Parse (root.GetAttribute ("height"));
		layers = new Dictionary<string,OgmoLayer> ();
		foreach (XmlElement child in root) {
			OgmoLayer layer = new OgmoLayer (child, width, height);
			layers [layer.name] = layer;
		}
	}
}
