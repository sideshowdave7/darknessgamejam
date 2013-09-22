using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour {
	
	private GUITexture flash;
	Color flashColor = Color.black;
	
	// Use this for initialization
	void Start () {
		Texture2D tex= new Texture2D ( 1 , 1 );
	    tex.SetPixel( 0 , 0 , Color.white );
	    tex.Apply();
	 
	    var storageGB = new GameObject("Flash");
	    storageGB.transform.localScale = new Vector3(0 , 0 , 1);
	 
	    flash = storageGB.AddComponent<GUITexture>();
	    flash.pixelInset = new Rect(0 , 0 , Screen.width , Screen.height );
	    flash.color = flashColor;
	    flash.texture = tex;
	    flash.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}

