using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class OgmoEntity
{
	public string name;
	public int x;
	public int y;
	public float width;
	public float height;
	public List<Vector2> nodes;
	public Dictionary<string, string> entityAttributes;

	public OgmoEntity (XmlElement node)
	{
		XmlAttributeCollection attributes = node.Attributes;
		name = node.Name;
		width = float.NaN;
		height = float.NaN;
		entityAttributes = new Dictionary<string, string> ();
		for (int ii = 0; ii < attributes.Count; ii++) {
			XmlNode attribute = attributes [ii];
			switch (attribute.Name) {
			case "x":
				x = int.Parse (attribute.Value);
				break;
			case "y":
				y = int.Parse (attribute.Value);
				break;
			case "width":
				width = float.Parse (attribute.Value);
				break;
			case "height":
				height = float.Parse (attribute.Value);
				break;
			default:
				entityAttributes [attribute.Name] = attribute.Value;
				break;
			}
		}
		nodes = new List<Vector2> ();
		for (int ii=0; ii < node.ChildNodes.Count; ii++) {
			XmlNode childNode = node.ChildNodes [ii];
			attributes = childNode.Attributes;
			Vector2 nodePosition;
			if (Parse (childNode, out nodePosition))
				nodes.Add (nodePosition);
		}
	}
	
	bool Parse (XmlNode childNode, out Vector2 value)
	{
		bool retval = false;
		if (childNode.Attributes ["x"] != null && childNode.Attributes ["y"] != null) {
			value = new Vector2 (float.Parse (childNode.Attributes ["x"].Value),
            	float.Parse (childNode.Attributes ["y"].Value));
			retval = true;
		} else
			value = Vector2.zero;
		return retval;
	}
}