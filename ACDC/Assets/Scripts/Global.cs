using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Global
{
	public AnimationCurve defaultMotionCurve;

	private static Global _instance;
	public static Global instance
	{
		get {
			if(_instance == null)
			{
				_instance = new Global();
			}
			return _instance;
		}
	}

	public Global()
	{
		// DOTween.useSafeMode = false;
		DOTween.useSmoothDeltaTime = true;
	}
}

