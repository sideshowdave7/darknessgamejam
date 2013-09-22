using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float move_speed;
	public PlayerPowerEnum currentPower = PlayerPowerEnum.Visual;
	private bool prevTogglePower;
	public Flash flash;
	
	// Use this for initialization
	void Start ()
	{
		prevTogglePower = false;
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		var input = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
		var togglePower = Input.GetButtonDown ("TogglePower");
		
		if (togglePower && !prevTogglePower) {
			
			switch (currentPower) {
			case PlayerPowerEnum.Audio:
				DarknessAudioManager.Instance.muteLayers(1f);

				break;
			case PlayerPowerEnum.Time:
				var times = GameObject.FindObjectsOfType(typeof(TimeToggleThingy));
				foreach(TimeToggleThingy t in times){
					t.setTimeSlow(false);
				}		
				var enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (var enemy in enemies){
					enemy.GetComponent<AudioSource>().pitch = .8f;	
					enemy.GetComponent<SFXVolumeControl>().sfxMultiplier = 1.2f;	
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
				var enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (var enemy in enemies){
					enemy.GetComponent<AudioSource>().pitch = 1f;
					enemy.GetComponent<SFXVolumeControl>().sfxMultiplier = 2.5f;
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
		
		Visual,
		Audio,
		Time,
		End
	};
		
}
