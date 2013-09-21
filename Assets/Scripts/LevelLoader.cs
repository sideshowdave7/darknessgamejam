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
	
	private void LoadLevel (int index)
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
	}
	
	public void LoadNextLevel ()
	{
		LoadLevel (++levelIndex);
	}
	
	public void ReloadLevel ()
	{
		LoadLevel (levelIndex);
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
				go.transform.position = new Vector3 (tile.x, 0f, tile.y);
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
				go.transform.position = new Vector3 (entity.x * 0.0625f, 0f, entity.y * 0.0625f);
		}
	}
	
	private GameObject GetPrefab (string path)
	{
		GameObject go;
		go = (GameObject)Resources.Load (path);
		return go;
	}
}
