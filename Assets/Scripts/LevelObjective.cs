using UnityEngine;
using System.Collections;
using Pathfinding;

public class LevelObjective : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
	{
	}
	
	private enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}
	
	void OnTriggerEnter (Collider other)
	{	
		if (other.gameObject.tag == "Player") {
			var diff = this.transform.position - other.transform.position;
			diff.Normalize ();
			ExitDirection dir;
			if (diff.y < diff.x)
				dir = ExitDirection.Right;
			else if (diff.y < -diff.x)
				dir = ExitDirection.Left;
			else if (diff.y > 0f)
				dir = ExitDirection.Top;
			else
				dir = ExitDirection.Bottom;
			StartLevelTransition (dir);
		}
	}
	
	void StartLevelTransition (ExitDirection dir)
	{
		Vector3 exitDest, mid, enterSource;
		mid = Vector3.zero;
		var ll = GameObject.FindObjectOfType (typeof(LevelLoader)) as LevelLoader;
		if (ll != null) {
			switch (dir) {
			case ExitDirection.Left:
				exitDest = new Vector3 (20f, 0f, 0f);
				enterSource = new Vector3 (-20f, 0f, 0f);
				break;
			case ExitDirection.Right:
				exitDest = new Vector3 (-20f, 0f, 0f);
				enterSource = new Vector3 (20f, 0f, 0f);
				break;
			case ExitDirection.Top:
				exitDest = new Vector3 (0f, 0f, -15f);
				enterSource = new Vector3 (-20f, 0f, 15f);
				break;
			default:
				exitDest = new Vector3 (0f, 0f, 15f);
				enterSource = new Vector3 (-20f, 0f, -15f);
				break;
			}
		
			var oldLvl = ll.lvl;
			var player = GameObject.FindGameObjectWithTag("Player");
			GameObject.Destroy(player);
			var newLvl = ll.LoadNextLevel (enterSource);
			StartCoroutine (LevelTransition (oldLvl, newLvl, exitDest));
		}
	}
	
	IEnumerator LevelTransition (GameObject oldLevel, GameObject newLevel, Vector3 exitDest)
	{
		const float speed = 15f;
		while (newLevel.transform.position != Vector3.zero) {
			oldLevel.transform.position = Vector3.MoveTowards (oldLevel.transform.position, exitDest, speed * Time.deltaTime);
			newLevel.transform.position = Vector3.MoveTowards (newLevel.transform.position, Vector3.zero, speed * Time.deltaTime);
			yield return 1;
		}
		
		foreach (Progress progress in AstarPath.active.ScanLoop ()) {
				Debug.Log ("Rescanning paths");
		}
		
		GameObject.Destroy (oldLevel);
	}
	
	private enum ExitDirection
	{
		Left,
		Right,
		Top,
		Bottom
	}
}
