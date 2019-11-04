using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicMenu : DisplayObject
{
	public static string prefabPath = "Music/MusicMenu";
	public static MusicMenu instance;

	public static bool isMoving = false;

	const float DISTANCE_X = 1.12f;
	const float DISTANCE_Z = 0.7f;

	DisplayObject container;
	DisplayObject mask;

	public List<AlbumGroup> albumGroups = new List<AlbumGroup>();
	int direction = -1;
	int focusIdx;

	public void Start()
	{
		instance = this;

		TextMesh[] meshes = gameObject.GetComponentsInChildren<TextMesh>();
		TextMesh mesh;
		for (int i = 0; i < meshes.Length; i++)
		{
			mesh = meshes[i];
			mesh.color = new Color(1, 1, 1, 0);
		}

		container = CreateEmptyObject();
		AddChild(container);
		container.transform.localPosition = new Vector3(4 * DISTANCE_X, 0, 0);

		CreateAlbumGroup();
	}

	void CreateAlbumGroup()
	{
		AlbumGroup group;
		for (int i = 0; i < 10; i++)
		{
			group = Create<AlbumGroup>();
			group.name = "albumGroup" + i;
			group.idx = i;
			container.AddChild(group);

			group.transform.localPosition = new Vector3(i * DISTANCE_X, 0, 0);

			group.CreateGroup(i);

			albumGroups.Add(group);
		}

		// mask = CreateObject("Music/ClippingPlane");
		// AddChild(mask);
		// mask.transform.localPosition = new Vector3(0, -0.01f, 0);
	}

	public void Appear()
	{
		isMoving = true;
		Slide(3, 2.0f);
		DOVirtual.DelayedCall(2f, () => albumGroups[3].Open());
	}

	public void Slide(int idx, float duration = -1)
	{
		isMoving = true;
		AlbumGroup group = albumGroups[idx];
		float tx = container.x + group.x;
		direction = tx > 0 ? -1 : 1;

		if (duration == -1)
		{
			tx = Mathf.Abs(tx);
			if (tx <= DISTANCE_X + 0.3) duration = 0.6f;
			else if (tx >= DISTANCE_X * 3 - 0.3) duration = 1.2f;
			else duration = 0.8f;

			container.TweenX(-group.x, duration).OnUpdate(UpdateHeaderPos)
			.OnComplete(() => group.Open());
		}
		else
		{
			container.TweenX(-group.x, duration, Ease.OutCubic).OnUpdate(UpdateHeaderPos)
			.OnComplete(() => group.Open());
		}

		focusIdx = idx;
	}

	void UpdateHeaderPos()
	{
		AlbumGroup group;
		float dx;
		for (int i = 0; i < 10; i++)
		{
			group = albumGroups[i];
			dx = container.x + group.x;
			float t = dx / (DISTANCE_X * 4);
			t = Mathf.Abs(t);
			t = Mathf.Clamp(t, 0, 1);
			float y = MotionCurve.instance.albumHeaderCurve.Evaluate(t) - 1;
			group.header.y = y;

			if (direction == 1)
			{
				if (dx > DISTANCE_X * 6)
				{
					group.x -= DISTANCE_X * 10;
				}
			}
			else
			{
				if (dx < -DISTANCE_X * 6)
				{
					group.x += DISTANCE_X * 10;
				}
			}
		}

	}

	public void UpdateMenu(int idx)
	{
		AlbumGroup group = albumGroups[focusIdx];
		DisplayTitle(false);
		DOVirtual.DelayedCall(0.0f, () => group.Close(idx));
	}

	public void DisplayTitle(bool willShow = true)
	{
		float value = willShow ? 1 : 0;
		Ease ease = willShow ? Ease.Linear : Ease.OutQuad;
		int rnd = Random.Range(0, 5);
		for (int i = 0; i < 4; i++)
		{
			float delay = willShow ? i * 0.1f : 0f;

			TextMesh m1 = GameObject.Find("song" + i).GetComponent<TextMesh>();
			TextMesh m2 = GameObject.Find("singer" + i).GetComponent<TextMesh>();
			

			float a1 = m1.color.a;
			DOTween.To(() => a1, v => a1 = v, value, 0.5f).SetDelay(delay).SetEase(ease)
			.OnUpdate(() => m1.color = new Color(1, 1, 1, a1));

			float a2 = m1.color.a;
			DOTween.To(() => a2, v => a2 = v, value, 0.5f).SetDelay(delay).SetEase(ease)
			.OnUpdate(() => m2.color = new Color(1, 1, 1, a2));

			if (i == 0)
			{
				TextMesh m3 = GameObject.Find("album").GetComponent<TextMesh>();
				float a3 = m3.color.a;
				DOTween.To(() => a3, v => a3 = v, value, 0.5f).SetDelay(delay).SetEase(ease)
				.OnUpdate(() => m3.color = new Color(1, 1, 1, a3));


			}

			if (willShow)
			{
				
				m1.text = PlayInfo.list[rnd].musiclist[i].song;
				m2.text = PlayInfo.list[rnd].musiclist[i].singer;
				if (i == 0) 
				{
					TextMesh m3 = GameObject.Find("album").GetComponent<TextMesh>();
					m3.text = PlayInfo.list[rnd].musiclist[i].album;
				}
			}

		}
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{

		}
	}
}
