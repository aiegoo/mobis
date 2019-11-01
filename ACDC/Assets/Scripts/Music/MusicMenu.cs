using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicMenu : MonoBehaviour
{
	public AnimationCurve ac;
	const float DISTANCE_X = 1.12f;
	const float DISTANCE_Z = 0.7f;
	
	GameObject mask;
	
	List<GameObject> _albumGroups = new List<GameObject>();

    void Start()
    {

        CreateAlbumGroup();
		
    }

    // Update is called once per frame
    void Update()
    {
    }

	void CreateAlbumGroup()
	{
		GameObject group;
		for (int i = 0; i < 10; i++)
		{
			group = new GameObject("albumGroup" + i);
			group.AddComponent<AlbumGroup>();
			group.transform.parent = transform;

			group.transform.localPosition = new Vector3((i+4) * DISTANCE_X, 0, 0);

			group.GetComponent<AlbumGroup>().Create(i);
			
			_albumGroups.Add(group);
		}

		mask = Util.CreateGameObject("Music/ClippingPlane");
		mask.transform.parent = transform;
		mask.transform.localPosition = new Vector3(0, -0.01f, 0);
	}

	public void Display()
	{
		// GameObject header;

		for (int i = 0; i < 10; i++)
		{
			GameObject group = _albumGroups[i];
			GameObject header = group.GetComponent<AlbumGroup>().header;
			group.transform.DOLocalMoveX((i-3)*DISTANCE_X, 2f)
			.SetEase(Ease.OutCubic)
			.OnUpdate(() => OnUpdateTween(group, header))
			.OnComplete(() => _albumGroups[3].GetComponent<AlbumGroup>().Open());
		}
	}

	void OnUpdateTween(GameObject group, GameObject header)
	{
		float t = group.transform.localPosition.x / (DISTANCE_X*4);
		t = Mathf.Abs(t);
		t = Mathf.Clamp(t, 0, 1);
		float y = ac.Evaluate(t)-1;
		// if(group.name == "albumGroup3") Debug.Log(t + "__" + y);
		
		header.transform.DOLocalMoveY(y, 0);
	}


}
