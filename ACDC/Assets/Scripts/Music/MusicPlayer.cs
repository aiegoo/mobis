using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayer : DisplayObject
{
	public static string prefabPath = "Music/MusicPlayer";

	public List<AudioClip> audioClips;
	private AudioSource _audio;

	public static MusicPlayer instance;
	public static void Init()
	{
		instance = Create<MusicPlayer>(true);
		instance.name = "musicPlayer";	

	}

	void Awake()
	{
		_audio = gameObject.AddComponent<AudioSource>();
	}
	
	public void Play(int idx)
	{
		_audio.clip = audioClips[idx];
		_audio.Play();
	}

	public void Stop()
	{
		_audio.Stop();
	}

}
