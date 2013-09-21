using UnityEngine;
using System.Collections;

public class LevelObjective : MonoBehaviour {
	// Use this for initialization
	void Awake () {
	}
	
	void OnTriggerEnter(Collider other) {	
        if(other.gameObject.tag == "Player"){
			var ll = GameObject.FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
			if(ll!=null)
				ll.LoadNextLevel();
		}
    }
}
