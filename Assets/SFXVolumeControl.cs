using UnityEngine;
using System.Collections;

public class SFXVolumeControl : MonoBehaviour {
	
	public float sfxMultiplier = 2.5f;
	
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<AudioSource>().pitch = .8f;
	}
	
	// Update is called once per frame
	void Update () {
	
	var player = GameObject.FindGameObjectWithTag("Player");
			
			if (player != null) {
					
				var distance = Vector3.Distance(player.transform.position,this.transform.position) / sfxMultiplier;
				var volume = (1/distance);
				this.gameObject.GetComponent<AudioSource>().volume = Mathf.Clamp(volume,0f,1f);
			}
	}
	
		
	IEnumerator FadePitchEnum(float target)
	{
		AudioSource _as = this.gameObject.GetComponent<AudioSource>();
		
		if (_as != null) {
		
			if (target < _as.volume){
			
			    while(_as.volume > target + .1f)
			    {
			        _as.pitch = Mathf.Lerp(_as.volume,target,Time.deltaTime*3);
			        yield return 0;
			    }
			} else {
				while(_as.volume < target - .1f)
			    {
			        _as.pitch = Mathf.Lerp(_as.volume,target,Time.deltaTime*3);
			        yield return 0;
			    }
			}
		    _as.pitch = target;
		   
			}
	}
	
	public void FadePitch(float target)
	{
	    StartCoroutine(FadePitchEnum(target));
	}
		
	
}
