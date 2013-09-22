using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehaviour))]
public class TimeToggleThingy : MonoBehaviour
{
	public float timeSlowMultiplier;
	private bool isSlowed = false;
	
	public void setTimeSlow (bool enabled)
	{
		if (enabled && !isSlowed) {
			EnemyBehaviour eb = GetComponent<EnemyBehaviour> ();
			eb.speed *= timeSlowMultiplier;
			isSlowed = true;
		} else if (!enabled && isSlowed) {
			EnemyBehaviour eb = GetComponent<EnemyBehaviour> ();
			eb.speed /= timeSlowMultiplier;
			isSlowed = false;			
		}
	}
}
