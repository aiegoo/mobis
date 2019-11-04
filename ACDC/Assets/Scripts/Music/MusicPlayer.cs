using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayer : DisplayObject
{
	public static string prefabPath = "Music/MusicPlayer";

	public List<AudioClip> audioClips;
	private AudioSource _audio;

	private static MusicPlayer _instance;
	public static MusicPlayer instance
	{
		get {
			if(_instance == null)
			{
				_instance = Create<MusicPlayer>();
				_instance.name = "motionCurve";				
			}
			return _instance;
		}
	}

	void Start()
	{
		// myAudio = GetComponent<AudioSource>();
	}

	public void Play()
	{
		// myAudio.PlayOneShot(soundExplosion);
	}

	public void Pause()
	{

	}

}
