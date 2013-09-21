using UnityEngine;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
	public string[] keys;
	public GameObject[] values;
	
	public void LoadLevel (OgmoLevel level)
	{
		foreach (var layer in level.layers.Values) {
			LoadLayer (layer);
		}
	}
	
	private void LoadLayer (OgmoLayer layer)
	{
		if (layer.tiles != null) {
		}
		if (layer.entities != null) {
		}
	}
	
	private void LoadTile (OgmoTile tile)
	{
		
	}
}
