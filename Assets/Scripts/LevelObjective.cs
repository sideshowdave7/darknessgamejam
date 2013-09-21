using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class LevelObjective : MonoBehaviour {
	private Collider collider;
	
	// Use this for initialization
	void Awake () {
		collider = GetComponent<Collider>();
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
			Application.LoadLevel(Application.loadedLevel + 1);
    }
}
