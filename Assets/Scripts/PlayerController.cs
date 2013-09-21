using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float move_speed;
	public PlayerPowerEnum currentPower = PlayerPowerEnum.Normal;
	private bool prevTogglePower;
	
	// Use this for initialization
	void Start ()
	{
		prevTogglePower = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var input = new Vector3(Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical"), 0f);
		var togglePower = Input.GetButtonDown("TogglePower");
		
		if (togglePower && !prevTogglePower){
			currentPower = (PlayerPowerEnum)(((int)currentPower + 1) % (int)PlayerPowerEnum.End);
		}
		
		this.rigidbody.velocity = (Vector3.Normalize(input) * move_speed);
		
		prevTogglePower = togglePower;
	}
	
	
	
	public enum PlayerPowerEnum {
		
		Normal,
		Visual,
		Audio,
		Time,
		End
	};
		
}
