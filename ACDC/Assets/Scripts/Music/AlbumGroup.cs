using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlbumGroup : MonoBehaviour
{
	const float DISTANCE_Z = 0.7f;
	public int idx;
	public GameObject header;
	public List<GameObject> albums = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Create(int idx)
	{
		this.idx = idx;
		header = Util.CreateGameObject("Music/AlbumHeader");
		header.name = "header";
		
		header.transform.parent = transform;
		header.transform.localPosition = new Vector3(0, -1, 0);
		header.transform.localScale = new Vector3(0.84f, 0.93f, 0.84f);
 
		GameObject album;
		for (int i = 0; i < 8; i++)
		{
			album = Util.CreateGameObject("Music/Album");
			album.name = "album" + i;
			album.GetComponent<Album>().SetTexture("album_0" + (i%4+1));

			album.transform.parent = transform;
			album.transform.localPosition = new Vector3(0, -5, DISTANCE_Z * ((i<3) ? i : 2));
			album.transform.localScale = new Vector3(0.84f, 0.93f, 0.84f);
			albums.Add(album);
		}
	}

	public void Open()
	{
		header.transform.DOLocalMoveY(0, 0);

		int albumIndex = 0;
		GameObject album;
		header.transform.DOLocalMoveZ(DISTANCE_Z*3, 1.0f).SetEase(Ease.InOutCubic)
		.OnUpdate(()=>{
			album = albums[albumIndex];
			float z = album.GetComponent<Album>().transform.localPosition.z;
			if(header.transform.localPosition.z > z + 0.04f && albumIndex<3)
			{
				
				if(albumIndex==2)
				{
					album.transform.DOLocalMoveY(0, 0.6f).SetEase(Ease.OutCubic)
					.OnComplete(()=>{
						albums[0].transform.DOLocalMoveZ(-DISTANCE_Z, 0.8f).SetEase(Ease.InOutCubic);
						albums[0].transform.DOLocalRotate(new Vector3(0, 45, 0), 0.8f).SetDelay(0.2f).SetEase(Ease.InOutCubic);

						albums[1].transform.DOLocalMoveZ(0, 0.8f).SetEase(Ease.InOutCubic);
						albums[2].transform.DOLocalMoveZ(DISTANCE_Z, 0.8f).SetEase(Ease.InOutCubic);

						albums[3].transform.DOLocalMoveY(0, 0.8f).SetEase(Ease.InOutCubic).From(-2);
					});
				}
				else
				{
					album.transform.DOLocalMoveY(0, 0.6f).SetEase(Ease.OutCubic);
				}

				albumIndex++;
			}
		});
	}

	public void Close()
	{

	}
}
