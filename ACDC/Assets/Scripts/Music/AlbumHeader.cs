using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlbumHeader : DisplayObject
{
	public static string prefabPath = "Music/AlbumHeader";

	void Start()
	{
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].material.SetVector("_PlanePosition", new Vector3(0, 1.42f, 0));
		}

		string title = PlayInfo.list[(parent as AlbumGroup).idx%5].name;
		transform.Find("title").gameObject.GetComponent<TextMesh>().text = title;
		transform.Find("titleShadow").gameObject.GetComponent<TextMesh>().text = title;
	}

	void OnMouseUp()
	{
		
		if(MusicMenu.isMoving || y<-1) return;
		
		AlbumGroup group = parent as AlbumGroup;
		MusicMenu.instance.UpdateMenu(group.idx);
	}
}

