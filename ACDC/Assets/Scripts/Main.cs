using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static bool isOpened = false;
    // Start is called before the first frame update
	public GameObject musicView;
	bool isFirst = true;
    void Start()
    {
        musicView = new GameObject("musicView");
		musicView.AddComponent<MusicView>();
		musicView.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isFirst)
        {
            musicView.GetComponent<MusicView>().musicMenu.GetComponent<MusicMenu>().Display();
			isFirst = false;
        }

		//테스트용
		if(Input.GetMouseButtonUp(0))
		{
			if(Main.isOpened)
			{
				musicView.GetComponent<MusicView>().musicMenu.GetComponent<MusicMenu>().albumGroups[3].GetComponent<AlbumGroup>().Close();
			}
		}
    }
}
