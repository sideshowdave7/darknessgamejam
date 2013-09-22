using UnityEngine;
using System.Collections;

public class AnimationStateController : MonoBehaviour {
	
	
	public OTAnimatingSprite animSprite;
	Vector3 dir;
	
	// Use this for initialization
	void Start () {
		animSprite = this.gameObject.GetComponent<OTAnimatingSprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
		dir = Vector3.Normalize(this.rigidbody.velocity);	
		
		var lmc = this.gameObject.GetComponent<LargeMovementControl>();
		if (lmc != null){
			
			dir = lmc.dir;
		}
		
		if (dir == Vector3.right){
			animSprite.animationFrameset = "right";
		} else if (dir == Vector3.left){
			animSprite.animationFrameset = "left";
		} else if (dir == Vector3.forward){
			animSprite.animationFrameset = "forward";
		} else if (dir == Vector3.back){
			animSprite.animationFrameset = "back";
		} 
		
	}
}
