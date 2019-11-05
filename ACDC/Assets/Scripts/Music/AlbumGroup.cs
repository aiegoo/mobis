using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlbumGroup : DisplayObject
{


	const float DISTANCE_Z = 0.7f;
	public int idx;
	public int focusIdx = 0;
	public AlbumHeader header;
	public List<Album> albums = new List<Album>();
	Interator<Album> iter;

	public void CreateGroup(int idx)
	{
		this.idx = idx;
		header = Create<AlbumHeader>();
		header.name = "header";

		AddChild(header);
		header.transform.localPosition = new Vector3(0, -1, 0);
		header.transform.localScale = new Vector3(0.84f, 0.93f, 0.84f);

		Album album;
		for (int i = 0; i < 8; i++)
		{
			album = Create<Album>();
			AddChild(album);
			album.name = "album" + i;
			album.idx = i;
			album.posIdx = i <= 3 ? i : 4;
			album.SetTexture("album_0" + (PlayInfo.list[idx%5].musiclist[i%4].id-1));

			album.transform.parent = transform;
			album.transform.localPosition = new Vector3(0, -1f, DISTANCE_Z * ((i < 3) ? i : 2));
			album.transform.localScale = new Vector3(0.84f, 0.93f, 0.84f);
			albums.Add(album);
		}

		iter = new Interator<Album>(albums);
	}

	void Reset()
	{
		Album album;
		for (int i = 0; i < 8; i++)
		{
			album = albums[i];
			AddChild(album);
			album.posIdx = i <= 3 ? i : 4;

			album.transform.parent = transform;
			album.transform.localPosition = new Vector3(0, -1f, DISTANCE_Z * ((i < 3) ? i : 2));
			album.transform.localScale = new Vector3(0.84f, 0.93f, 0.84f);
		}

		focusIdx = 0;
		iter.index = 0;
	}

	public void Open()
	{
		
		MusicMenu.isMoving = true;
		// header.transform.DOLocalMoveY(0, 0);
		header.y = 0;

		int albumIndex = 0;
		Album album;
		Transform t = header.transform;
		header.TweenZ(DISTANCE_Z * 3, 0.7f)
		.OnUpdate(() =>
		{
			album = albums[albumIndex];
			float z = album.transform.localPosition.z;
			if (t.localPosition.z > z + 0.035f && albumIndex < 3)
			{
				if (albumIndex == 2)
				{
					album.TweenY(0, 0.4f)
					.OnComplete(() =>
					{
						albums[0].TweenZ(-DISTANCE_Z, 0.6f);
						albums[0].TweenRotaionY(45, 0.6f).SetDelay(0.2f);

						albums[1].TweenZ(0, 0.6f);
						albums[2].TweenZ(DISTANCE_Z, 0.6f);

						albums[3].TweenY(0, 0.4f);
						Debug.Log(albumIndex);
						DOVirtual.DelayedCall(0.2f, () => MusicMenu.instance.DisplayTitle(true));
					});
				}
				else
				{
					album.TweenY(0, 0.4f);
				}

				albumIndex++;
			}
		});
	}

	public void Slide(int idx)
	{
		if (idx == focusIdx) return;

		MusicMenu.isMoving = true;
		MusicPlayer.instance.Stop();

		iter.index = focusIdx;

		List<Album> list = new List<Album>();
		for (int i = 0; i < 3; i++)
		{
			list.Add(iter.item);
			iter.Next();
		}

		iter.index = focusIdx;

		if (albums[idx].posIdx == 1)
		{
			iter.item.TweenRotaionY(135, 0.4f);
			iter.item.TweenAlpha(0f, 0.4f).OnComplete(() => OnHideAlbum(list[0]));
			iter.item.posIdx = 4;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * -1, 0.6f);
			iter.item.TweenRotaionY(45, 0.6f).SetDelay(0.2f);
			iter.item.posIdx = 0;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * 0, 0.6f);
			iter.item.posIdx = 1;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * 1, 0.6f);
			iter.item.posIdx = 2;

			iter.Next();
			iter.item.TweenY(0, 0.4f).SetDelay(0.0f);
			iter.item.posIdx = 3;

		}
		else if (albums[idx].posIdx == 2)
		{
			iter.item.TweenRotaionY(135, 0.4f);
			iter.item.TweenAlpha(0f, 0.4f).OnComplete(() => OnHideAlbum(list[0]));
			iter.item.posIdx = 4;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * -1, 0.4f);
			iter.item.TweenRotaionY(135, 0.4f).SetDelay(0.2f);
			iter.item.TweenAlpha(0f, 0.4f).SetDelay(0.2f).OnComplete(() => OnHideAlbum(list[1]));
			iter.item.posIdx = 4;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * -1, 0.6f);
			iter.item.TweenRotaionY(45, 0.6f).SetDelay(0.2f);
			iter.item.posIdx = 0;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * 0, 0.6f);
			iter.item.posIdx = 1;

			iter.Next();
			iter.item.TweenY(0, 0.4f).SetDelay(0.0f);
			iter.item.TweenZ(DISTANCE_Z * 1, 0.6f).SetDelay(0.1f);
			iter.item.posIdx = 2;

			iter.Next();
			iter.item.TweenY(0, 0.4f).SetDelay(0.2f);
			iter.item.posIdx = 3;

		}
		else
		{
			iter.item.TweenRotaionY(135, 0.4f);
			iter.item.TweenAlpha(0f, 0.4f).OnComplete(() => OnHideAlbum(list[0]));
			iter.item.posIdx = 4;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * -1, 0.4f);
			iter.item.TweenRotaionY(135, 0.4f).SetDelay(0.2f);
			iter.item.TweenAlpha(0f, 0.4f).SetDelay(0.2f).OnComplete(() => OnHideAlbum(list[1]));
			iter.item.posIdx = 4;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * -1, 0.4f);
			iter.item.TweenRotaionY(135, 0.4f).SetDelay(0.2f);
			iter.item.TweenAlpha(0f, 0.4f).SetDelay(0.2f).OnComplete(() => OnHideAlbum(list[2]));
			iter.item.posIdx = 4;

			iter.Next();
			iter.item.TweenZ(DISTANCE_Z * -1, 0.6f);
			iter.item.TweenRotaionY(45, 0.6f).SetDelay(0.2f);
			iter.item.posIdx = 0;

			iter.Next();
			iter.item.TweenY(0, 0.4f).SetDelay(0.0f);
			iter.item.TweenZ(DISTANCE_Z * 0, 0.6f).SetDelay(0.1f);
			iter.item.posIdx = 1;

			iter.Next();
			iter.item.TweenY(0, 0.4f).SetDelay(0.1f);
			iter.item.TweenZ(DISTANCE_Z * 1, 0.6f).SetDelay(0.2f);
			iter.item.posIdx = 2;

			iter.Next();
			iter.item.TweenY(0, 0.4f).SetDelay(0.3f);
			iter.item.posIdx = 3;


		}
		DOVirtual.DelayedCall(0.8f, () =>
		{
			MusicMenu.isMoving = false;

			int groupIdx = MusicMenu.instance.focusIdx % 5;
			Album album = albums[focusIdx];
			int id = focusIdx % 4;
			MusicPlayer.instance.Play(PlayInfo.list[groupIdx].musiclist[id].id - 1);

		});

		focusIdx = idx;
	}

	public void OnHideAlbum(Album album)
	{

		album.transform.localRotation = Quaternion.identity;
		album.transform.localPosition = new Vector3(0, -1, DISTANCE_Z * 2);
		album.alpha = 1;
	}

	public void Close(int targetIdx)
	{
		MusicMenu.isMoving = true;
		iter.index = focusIdx;

		List<Album> list = new List<Album>();
		for (int i = 0; i < 4; i++)
		{
			list.Add(iter.item);
			iter.Next();
		}

		iter.index = focusIdx;

		iter.item.TweenRotaionY(135, 0.4f);
		iter.item.TweenAlpha(0f, 0.4f).OnComplete(() => OnHideAlbum(list[0]));
		iter.item.posIdx = 4;

		iter.Next();
		iter.item.TweenZ(DISTANCE_Z * -1, 0.2f);
		iter.item.TweenRotaionY(135, 0.4f).SetDelay(0.1f);
		iter.item.TweenAlpha(0f, 0.4f).SetDelay(0.1f).OnComplete(() => OnHideAlbum(list[1]));
		iter.item.posIdx = 4;

		iter.Next();
		iter.item.TweenZ(DISTANCE_Z * -1, 0.4f);
		iter.item.TweenRotaionY(135, 0.4f).SetDelay(0.2f);
		iter.item.TweenAlpha(0f, 0.4f).SetDelay(0.2f).OnComplete(() => OnHideAlbum(list[2]));
		iter.item.posIdx = 4;

		iter.Next();
		iter.item.TweenZ(DISTANCE_Z * -1, 0.8f);
		iter.item.TweenRotaionY(135, 0.4f).SetDelay(0.4f);
		iter.item.TweenAlpha(0f, 0.4f).SetDelay(0.4f).OnComplete(() => OnHideAlbum(list[3]));
		iter.item.posIdx = 4;

		header.TweenZ(DISTANCE_Z * 0, 1.0f);

		DOVirtual.DelayedCall(0.9f, () => {
			MusicMenu.instance.Slide(targetIdx);
			Reset();
		});
	}
}
