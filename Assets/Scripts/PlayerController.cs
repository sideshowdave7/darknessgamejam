using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float move_speed;
	public PlayerPowerEnum currentPower = PlayerPowerEnum.Normal;
	private bool prevTogglePower;
	public Flash flash;
	
	// Use this for initialization
	void Start ()
	{
		prevTogglePower = false;
		flash = gameObject.AddComponent<Flash>();
	}
	
	void Flash (float duration) {
     flash.enabled = true;
     Invoke("Cancel", duration);
	}
 
	void Cancel () {
	    flash.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var input = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
		var togglePower = Input.GetButtonDown ("TogglePower");
		
		if (togglePower && !prevTogglePower) {
			
			Flash(1.0f);
			
			switch (currentPower) {
			case PlayerPowerEnum.Audio:
				DarknessAudioManager.Instance.muteLayers(1f);
				break;
			case PlayerPowerEnum.Time:
				var times = GameObject.FindObjectsOfType(typeof(TimeToggleThingy));
				foreach(TimeToggleThingy t in times){
					t.setTimeSlow(false);
				}
				break;
			case PlayerPowerEnum.Visual:
				var visuals = GameObject.FindObjectsOfType(typeof(VisibleToggleSpritesThingy));
				foreach(VisibleToggleSpritesThingy vis in visuals){
					vis.SetVisibleMode(false);
				}
				break;
			}
			currentPower = (PlayerPowerEnum)(((int)currentPower + 1) % (int)PlayerPowerEnum.End);
			switch (currentPower) {
			case PlayerPowerEnum.Audio:
				DarknessAudioManager.Instance.muteLayers(.1f);
				break;
			case PlayerPowerEnum.Time:
				var times = GameObject.FindObjectsOfType(typeof(TimeToggleThingy));
				foreach(TimeToggleThingy t in times){
					t.setTimeSlow(true);
				}
				break;
			case PlayerPowerEnum.Visual:
				var visuals = GameObject.FindObjectsOfType(typeof(VisibleToggleSpritesThingy));
				foreach(VisibleToggleSpritesThingy vis in visuals){
					vis.SetVisibleMode(true);
				}
				break;
			}
		}
		
		this.rigidbody.velocity = (Vector3.Normalize (input) * move_speed);
		
		prevTogglePower = togglePower;
	}
	
	
	
	public enum PlayerPowerEnum
	{
		
		Normal,
		Visual,
		Audio,
		Time,
		End
	};
		
}
