using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Util : MonoBehaviour
{
    public static void SetAlpha() 
	{

	}

	public static GameObject CreateGameObject(string path, float x=0, float y=0, float z=0)
	{
		GameObject gameObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + path), new Vector3(x, y, z), Quaternion.identity);
		return gameObject;
	}
}
