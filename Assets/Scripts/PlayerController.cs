using UnityEngine;
using System.Collections;
using Pathfinding;

public class PlayerController : MonoBehaviour
{
	public float move_speed;
	public PlayerPowerEnum currentPower = PlayerPowerEnum.Audio;
	private bool prevTogglePower;
	public Flash flash;
	
	// Use this for initialization
	void Start ()
	{
		prevTogglePower = false;
		SetCurrentPower(PlayerPowerEnum.Audio);
		this.gameObject.GetComponent<OTAnimatingSprite>().flipHorizontal = true;
		this.gameObject.GetComponent<OTAnimatingSprite>().flipHorizontal = false;
	}	
	
	// Update is called once per frame
	void Update ()
	{
		var input = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
		var togglePower = Input.GetButtonDown ("TogglePower");
		
		if (togglePower && !prevTogglePower) {
			SetCurrentPower (
				(PlayerPowerEnum)(((int)currentPower + 1) % ((int)PlayerPowerEnum.End)));
		}
		
		this.rigidbody.velocity = (Vector3.Normalize (input) * move_speed);
		
		
		if (input.x >= 0){
			this.gameObject.GetComponent<OTAnimatingSprite>().flipHorizontal = true;	
		} else {
			this.gameObject.GetComponent<OTAnimatingSprite>().flipHorizontal = false;	
		}
		
		prevTogglePower = togglePower;
	}
	
	IEnumerator FadeVisual (float start, float end)
	{
		const float fadeSpeed = 1.2f;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		OTSprite vignette = player.transform.GetChild (0).GetComponent<OTSprite> ();
		float t = 0f;
		yield return 1;
		while (t<1f) {
			t += fadeSpeed * Time.deltaTime;
			vignette.alpha = Mathf.Lerp (start, end, t);
			yield return 1;
		}
		vignette.alpha = end;
	}
	
	void SetCurrentPower (PlayerPowerEnum power)
	{
		if(power == PlayerPowerEnum.Audio)
			power = PlayerPowerEnum.Time;
		//This state is now false (rising edge)
		var enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		switch (currentPower) {
		case PlayerPowerEnum.Audio:
			DarknessAudioManager.Instance.muteLayers (1f);
			foreach (var enemy in enemies) {
				enemy.GetComponent<AudioSource> ().pitch = .8f;	
			}
			break;
		case PlayerPowerEnum.Time:
			var times = GameObject.FindObjectsOfType (typeof(TimeToggleThingy));
			foreach (TimeToggleThingy t in times) {
				t.setTimeSlow (false);
			}		
			foreach (var enemy in enemies) {
				enemy.GetComponent<SFXVolumeControl> ().sfxMultiplier = 1.2f;
				var aipath = enemy.GetComponent<AIPath> ();
				if (aipath != null){	aipath.speed *= 3; }
				var lmc = enemy.GetComponent<LargeMovementControl>();
				if (lmc != null) {  	lmc.speed *= 3; }
				
			}
			break;
		case PlayerPowerEnum.Visual:
			var visuals = GameObject.FindObjectsOfType (typeof(VisibleToggleSpritesThingy));
			foreach (VisibleToggleSpritesThingy vis in visuals) {
				vis.SetVisibleMode (false);
			}
			StartCoroutine (FadeVisual (0f, 1f));
			break;
		}
		
		currentPower = power;
		//This state is now true (rising edge)
		switch (currentPower) {
		case PlayerPowerEnum.Audio:
			DarknessAudioManager.Instance.muteLayers (.1f);
			foreach (var enemy in enemies) {
				enemy.GetComponent<AudioSource> ().pitch = 1f;
			}
			break;
		case PlayerPowerEnum.Time:
			var times = GameObject.FindObjectsOfType (typeof(TimeToggleThingy));
			foreach (TimeToggleThingy t in times) {
				t.setTimeSlow (true);
			}
			foreach (var enemy in enemies) {
				enemy.GetComponent<SFXVolumeControl> ().sfxMultiplier = 2.5f;
				var aipath = enemy.GetComponent<AIPath> ();
				if (aipath != null){	aipath.speed /= 3; }
				var lmc = enemy.GetComponent<LargeMovementControl>();
				if (lmc != null) {  	lmc.speed /= 3; }
			}
			break;
		case PlayerPowerEnum.Visual:
			var visuals = GameObject.FindObjectsOfType (typeof(VisibleToggleSpritesThingy));
			foreach (VisibleToggleSpritesThingy vis in visuals) {
				vis.SetVisibleMode (true);
			}
			StartCoroutine (FadeVisual (1f, 0f));
			break;
		}
	}
	
	public enum PlayerPowerEnum
	{
		
		Visual,
		Audio,
		Time,
		End
	};
		
}
