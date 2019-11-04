using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicView : DisplayObject
{
	public MusicMenu musicMenu;

	public void Start()
	{
		transform.localPosition = new Vector3(0, 1.42f, 0.15f);
        musicMenu = Create<MusicMenu>();
		musicMenu.name = "musicMenu";
		
		AddChild(musicMenu);
		musicMenu.ResetPosition();
	}
}
