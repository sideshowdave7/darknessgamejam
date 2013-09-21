using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class DarknessAudioManager  : MonoBehaviour
{
	
	public List<AudioClip> audioClips;
	private Dictionary<AudioClip,bool> audioClipsPlaying;
	private List<AudioSource> _audioSources = null;
	private List<AudioSource> _deleted = null;
	private static DarknessAudioManager _instance = null;
	
	// Use this for initialization
	void Start ()
	{
		_audioSources = new List<AudioSource> ();
		audioClipsPlaying = new Dictionary<AudioClip, bool>();
		
		foreach ( var clip in audioClips) {
			audioClipsPlaying[clip]  = false;	
		}
		
		_deleted = new List<AudioSource> ();
		DontDestroyOnLoad(this);
		
		
		foreach (var clip in audioClips){
		
			PlayOneShot ("Darkness-bass1","AudioManager",true);
			PlayOneShot ("Darkness-bass2","AudioManager",true);
			PlayOneShot ("Darkness-bass3","AudioManager",true);
			PlayOneShot ("Darkness-melody","AudioManager",true);
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void FixedUpdate ()
	{
		
		_deleted.Clear ();
		
		for (var x=0; x < _audioSources.Count; x++) {
			if (_audioSources [x] != null && _audioSources [x].clip != null && !_audioSources [x].isPlaying) {
				_deleted.Add (_audioSources [x]);
				Destroy (_audioSources [x]);
				audioClipsPlaying[_audioSources[x].clip] = false;
			} 
		}
		
		foreach (var _as in _deleted) {
			_audioSources.Remove (_as);	
		}
	
		
	}
	
	public static DarknessAudioManager Instance {
		get { return _instance; }
	}
	
	void Awake ()
	{
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			_instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}
	
	public void PlayOneShot (string sfxName, string targetTag, bool loop)
	{
		var clip = findAudioClip (sfxName);
		if (!audioClipsPlaying[clip]){			
			var go = GameObject.FindGameObjectWithTag (targetTag);
		
			AudioSource _as = (AudioSource)go.AddComponent ("AudioSource");
			_audioSources.Add (_as);
	
			_as.clip = clip;
			_as.loop = loop;
			_as.volume = .5f;
			_as.Play ();
			audioClipsPlaying[clip] = true;
		} 
		
	}
	
	AudioClip findAudioClip (string name)
	{
		
		foreach (var clip in audioClips) {
			if (clip != null)
			{
				if (clip.name == name) {
					return clip;	
				}
			}
		}
		return null;
	}
	
	IEnumerator FadeMusicEnum(AudioSource _as,float target)
	{
	    while(_as.volume > .1F)
	    {
	        _as.volume = Mathf.Lerp(_as.volume,target,Time.deltaTime);
	        yield return 0;
	    }
	    _as.volume = 0;
	   
	}
	
	public void FadeMusic(AudioSource _as, float target)
	{
	    StartCoroutine(FadeMusicEnum(_as,target));
	}
}
