using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelLoader : MonoBehaviour
{	
	private int levelIndex = -1;
	public TextAsset[] levels;
	public string zoneFolder;
	public GameObject lvl;
	
	void Awake ()
	{
		if (levels != null && levels.Length > 0) {
			LoadNextLevel ();
		}
	}
	
	private GameObject LoadLevel (int index)
	{
		if (lvl == null)
			lvl = GameObject.Find ("TheLevelStuff");
		if (lvl != null)
			GameObject.Destroy (lvl);		
		levelIndex = index;
		var level = new OgmoLevel (levels [index]);
		lvl = new GameObject ("TheLevelStuff");
		foreach (var layer in level.layers.Values) {
			LoadLayer (layer);
		}
		return lvl;
	}
	
	public GameObject LoadNextLevel ()
	{
		return LoadLevel (++levelIndex);
	}
	
	public GameObject ReloadLevel ()
	{
		return LoadLevel (levelIndex);
	}
	
	private void LoadLayer (OgmoLayer layer)
	{
		if (layer.tiles != null) {
			float height = 0f;
			switch (layer.name) {
			case "Foreground":
				height = 5f;
				break;
			case "Background":
				height = 0f;
				break;
			}
			foreach (var tile in layer.tiles) {
				LoadTile (tile, height);
			}
		}
		if (layer.entities != null) {
			foreach (var entity in layer.entities) {
				LoadEntity (entity);
			}
		}
	}
	
	private void LoadTile (OgmoTile tile, float height)
	{
		string tileName = string.Format ("frame{0}", tile.id);
		string tilePath = (zoneFolder ?? "") + tileName;
		var go = GetPrefab (tilePath);
		if (go != null) {
			go = (GameObject)GameObject.Instantiate (go);
			go.transform.parent = lvl.transform;
			OTObject ot = go.GetComponent<OTObject> ();
			if (ot != null)
				ot.position = new Vector2 (tile.x, 15 - tile.y);
			else
				go.transform.position = new Vector3 (tile.x, height, -tile.y);
		}
	}
	
	private void LoadEntity (OgmoEntity entity)
	{
		var go = GetPrefab (entity.name);
		if (go != null) {
			go = (GameObject)GameObject.Instantiate (go);
			go.transform.parent = lvl.transform;
			OTObject ot = go.GetComponent<OTObject> ();
			go.transform.position = new Vector3 (entity.x * 0.0625f, 0.25f, -entity.y * 0.0625f);
			if (ot != null)
				ot.position = new Vector2 (entity.x * 0.0625f, entity.y * 0.0625f);
		}
	}
	
	private GameObject GetPrefab (string path)
	{
		GameObject go;
		go = (GameObject)Resources.Load (path);
		return go;
	}
}
