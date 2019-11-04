using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCurve : DisplayObject
{
	public static string prefabPath = "Motion/MotionCurve";

	public AnimationCurve albumHeaderCurve;
	public AnimationCurve defaultCurve;

	private static MotionCurve _instance;
	public static MotionCurve instance
	{
		get {
			if(_instance == null)
			{
				_instance = Create<MotionCurve>(true);
				_instance.name = "motionCurve";				
			}
			return _instance;
		}
	}
}
