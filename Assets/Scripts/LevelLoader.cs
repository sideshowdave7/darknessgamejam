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
	
	public void LoadLevel (OgmoLevel level)
	{
		if (lvl == null)
			lvl = GameObject.Find ("TheLevelStuff");
		if (lvl != null)
			GameObject.Destroy (lvl);		
		levelIndex = System.Array.IndexOf (levels, level);
		lvl = new GameObject ("TheLevelStuff");
		foreach (var layer in level.layers.Values) {
			LoadLayer (layer);
		}
	}
	
	public void LoadNextLevel ()
	{
		LoadLevel (new OgmoLevel (levels [++levelIndex]));
	}
	
	public void ReloadLevel ()
	{
		LoadLevel (new OgmoLevel (levels [levelIndex]));
	}
	
	private void LoadLayer (OgmoLayer layer)
	{
		if (layer.tiles != null) {
			foreach (var tile in layer.tiles) {
				LoadTile (tile);
			}
		}
		if (layer.entities != null) {
			foreach (var entity in layer.entities) {
				LoadEntity (entity);
			}
		}
	}
	
	private void LoadTile (OgmoTile tile)
	{
		string tileName = string.Format ("{0}_{1}", tile.tx, tile.ty);
		string tilePath = (zoneFolder ?? "") + tileName;
		var go = GetPrefab (tilePath);
		if (go != null) {
			go = (GameObject)GameObject.Instantiate (go);
			go.transform.parent = lvl.transform;
			OTObject ot = go.GetComponent<OTObject> ();
			if (ot != null)
				ot.position = new Vector2 (tile.x, tile.y);
			else
				go.transform.position = new Vector3 (tile.x, tile.y, 0f);
		}
	}
	
	private void LoadEntity (OgmoEntity entity)
	{
		var go = GetPrefab (entity.name);
		if (go != null) {
			go = (GameObject)GameObject.Instantiate (go);
			go.transform.parent = lvl.transform;
			OTObject ot = go.GetComponent<OTObject> ();
			if (ot != null)
				ot.position = new Vector2 (entity.x * 0.0625f, entity.y * 0.0625f);
			else
				go.transform.position = new Vector3 (entity.x * 0.0625f, entity.y * 0.0625f, 0f);
		}
	}
	
	private GameObject GetPrefab (string path)
	{
		GameObject go;
		go = (GameObject)Resources.Load (path);
		return go;
	}
}
