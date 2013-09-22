using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelLoader : MonoBehaviour
{	
	public OTContainer[] containers;
	private int levelIndex = 0;
	public TextAsset[] levels;
	public string zoneFolder;
	public GameObject lvl;
	
	void Awake ()
	{
		if (levels != null && levels.Length > 0) {
			ReloadLevel ();
		}
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.L)) {
			GameObject.Destroy (lvl);
			LoadNextLevel (Vector3.zero);
		}
	}
	
	private GameObject LoadLevel (int index, Vector3 pos)
	{
		levelIndex = Mathf.Clamp (index, 0, levels.Length - 1);
		var level = new OgmoLevel (levels [levelIndex]);
		lvl = new GameObject ("TheLevelStuff");
		lvl.transform.position = pos;
		foreach (var layer in level.layers.Values) {
			LoadLayer (layer);
		}

		return lvl;
	}
	
	public GameObject LoadNextLevel (Vector3 pos)
	{
		return LoadLevel (++levelIndex, pos);
	}
	
	public GameObject ReloadLevel ()
	{
		GameObject.Destroy (lvl);
		return LoadLevel (levelIndex, Vector3.zero);
	}
	
	private void LoadLayer (OgmoLayer layer)
	{
		if (layer.tiles != null) {
			OTContainer container = containers [0];
			if (!string.IsNullOrEmpty (layer.tileSet)) {
				foreach (var c in containers) {
					if (c != null && 
						string.Equals (c.name, layer.tileSet, System.StringComparison.InvariantCultureIgnoreCase))
						container = c;
				}
			}
			float height = 0f;
			switch (layer.name) {
			case "Foreground":
				height = -5f;
				break;
			case "Background":
				height = 0f;
				break;
			}
			foreach (var tile in layer.tiles) {
				LoadTile (tile, container, height);
			}
		}
		if (layer.entities != null) {
			foreach (var entity in layer.entities) {
				LoadEntity (entity);
			}
		}
	}
	
	private void LoadTile (OgmoTile tile, OTContainer container, float height)
	{
		string tileName = string.Format ("frame{0}", tile.id);
		string tilePath = (zoneFolder ?? "") + tileName;
		var go = GetPrefab (tilePath);
		if (go != null) {
			go = (GameObject)GameObject.Instantiate (go);
			go.transform.parent = lvl.transform;
			OTSprite ot = go.GetComponent<OTSprite> ();
			
			go.transform.localPosition = new Vector3 (tile.x, height, 15 - tile.y);
			if (ot != null) {
				ot.position = new Vector2 (go.transform.localPosition.x, go.transform.localPosition.z);
				ot.depth = (int)height;
				int id = ot.frameIndex;
				ot.spriteContainer = container;
				ot.frameIndex = id;
				ot.otTransform = ot.transform;
				ot.transform.localScale = Vector3.one;
			}
		}
	}
	
	private Vector2 GetEntitySize (string name)
	{
		Vector2 size = Vector2.one;
		switch (name) {
		case "Cockroach":
		case "Mouse":
		case"Coyote":		
		case "Rabbit":
		case "Bat":
		case "Exit":
		case "TrashCan":
		case "Boulder":
		case "Table":
			size = new Vector2 (1, 1);
			break;
		case "Elk":
		case "ElkVert":
		case "Bear":
		case "BearVert":
		case "Gorilla":
		case "GorillaVert":
			size = new Vector2 (2, 2);
			break;	
		case "Human":
			size = new Vector2 (2, 2);
			break;
		case "Player":
			size = new Vector2 (1, 2);
			break;
		}
		return size;
	}
	
	private void LoadEntity (OgmoEntity entity)
	{
		var go = GetPrefab (entity.name);
		if (go != null) {
			go = (GameObject)GameObject.Instantiate (go);
			go.transform.parent = lvl.transform;
			OTObject ot = go.GetComponent<OTObject> ();
			Vector3 pos = new Vector3 (((float)entity.x) / 16f, 0.25f, 15f - ((float)entity.y) / 16f);
			var size = GetEntitySize (entity.name) * 0.5f;
			pos.x += size.x * 0.5f;
			pos.z -= size.y * 0.5f;
			go.transform.localPosition = pos;
			if (ot != null) {
				ot.position = new Vector2 (go.transform.localPosition.x, go.transform.localPosition.z);
				ot.depth = -1;
			}
		}
	}
	
	private GameObject GetPrefab (string path)
	{
		UnityEngine.Object go;
		go = Resources.Load (path);
		return (go as GameObject);
	}
}
