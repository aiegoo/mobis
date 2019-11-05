using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCurve : DisplayObject
{
	public static string prefabPath = "Motion/MotionCurve";

	public AnimationCurve albumHeaderCurve;
	public AnimationCurve defaultCurve;

	public static MotionCurve instance;
	public static void Init()
	{
		instance = Create<MotionCurve>(true);
		instance.name = "motionCurve";	
	}
}
