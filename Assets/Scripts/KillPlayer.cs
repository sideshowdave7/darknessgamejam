﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class KillPlayer : MonoBehaviour {
	// Use this for initialization
	void Awake () {
	}
	
	void OnTriggerEnter(Collider other) {		
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "player2" || other.name == "Player(Clone)"){
			var ll = GameObject.FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
			if(ll!=null)
				ll.ReloadLevel();
		}
    }
}
