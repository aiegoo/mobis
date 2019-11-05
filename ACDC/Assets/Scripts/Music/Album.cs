using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Album : DisplayObject
{
	public static string prefabPath = "Music/Album";

	public int idx;
	public int posIdx;



	public void SetTexture(string path)
	{
		Renderer renderer = gameObject.transform.Find("back").GetComponent<Renderer>();
		renderer.material.mainTexture = Resources.Load<Texture>("Textures/" + path);
	}

	void Start()
	{
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].material.SetVector("_PlanePosition", new Vector3(0, 1.42f, 0));
		}
	}

	void OnMouseUp()
	{
		if(MusicMenu.isMoving || y<0) return;

		MusicPlayer.instance.Stop();
		
		AlbumGroup group = parent as AlbumGroup;
		group.Slide(idx);
	}

}