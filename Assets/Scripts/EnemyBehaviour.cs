using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{	
	protected GameObject target;
	public float speed;
	public float attackRange;
	
	void Awake ()
	{
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{		
		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player");
		if (target != null) {
			OTAnimatingSprite anim = GetComponent<OTAnimatingSprite> ();
			var diff = target.transform.position - transform.position;
			string frameSet;
			if (Mathf.Abs (diff.x) > Mathf.Abs (diff.z)) {
				if (diff.x > 0f)
					frameSet = "right";
				else {
					frameSet = "left";
				}
			} else {
				if (diff.z > 0f)
					frameSet = "up";
				else
					frameSet = "down";
			}
			if (frameSet != anim.animationFrameset)
				anim.animationFrameset = frameSet;
			transform.position = Vector3.MoveTowards (transform.position,
				target.transform.position,
				speed * Time.deltaTime);
		}
	}
}
