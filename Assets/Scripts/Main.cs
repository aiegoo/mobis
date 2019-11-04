using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Main : DisplayObject
{
	public static bool isOpened = false;
    // Start is called before the first frame update
	public MusicView musicView;
	bool isFirst = true;

	void Awake()
	{
		string json = File.ReadAllText(Application.streamingAssetsPath + "/playlist.json");
		PlayInfo.list = JsonUtility.FromJson<PlayInfo>(json).items;
	}

    void Start()
    {
		Global g = Global.instance;

        musicView = Create<MusicView>();
		musicView.name = "musicView";
		AddChild(musicView);
		
    }

    // Update is called once per frame
    void Update()
    {
		// transform.localScale = transform.localScale;

        if(Input.GetKeyDown(KeyCode.Space) && isFirst)
        {
            musicView.musicMenu.Appear();
			isFirst = false;
        }
    }
}
