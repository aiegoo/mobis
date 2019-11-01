using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Album : MonoBehaviour
{
    public void SetTexture(string path)
	{
		Renderer renderer = gameObject.transform.Find("back").GetComponent<Renderer>();
		renderer.material.mainTexture = Resources.Load<Texture>("Textures/" + path);
	}
}
