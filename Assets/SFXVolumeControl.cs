using UnityEngine;
using System.Collections;

public class SFXVolumeControl : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	var player = GameObject.FindGameObjectWithTag("Player");
			
			if (player != null) {
					
				var distance = Vector3.Distance(player.transform.position,this.transform.position) / 1.2f;
				var volume = (1/distance);
				this.gameObject.GetComponent<AudioSource>().volume = Mathf.Clamp(volume,0f,1f);
			}
		}
		
	
}
