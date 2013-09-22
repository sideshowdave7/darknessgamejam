using UnityEngine;
using System.Collections;

public class LargeMovementControl : MonoBehaviour {
	
	public Vector3 dir;
	public float speed = 1f;
	public string alignment;
	private bool change = false;
	
	// Use this for initialization
	void Start () {
		if (alignment == "horizontal") {
			dir = Vector3.right;
		}
		else if (alignment == "vertical"){
			dir = Vector3.back;			
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(dir * Time.deltaTime * speed);
		
		if (change){
			change = false;
			if(alignment == "horizontal"){
						if (dir == Vector3.right){
							dir = Vector3.left;
						} else {
							dir = Vector3.right;
						}
			} else {
						if (dir == Vector3.back){
							dir = Vector3.forward;
						} else {
							dir = Vector3.back;
						}
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {		
		change = true;
	}
}
