using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

		MotionCurve.Init();
		MusicPlayer.Init();
		
	}

    void Start()
    {
		Global g = Global.instance;

        musicView = Create<MusicView>();
		musicView.name = "musicView";
		AddChild(musicView);

		// GameObject g = GameObject.Find("light");
		// Material c = g.GetComponent<Image>().material;
		// Texture t = c.GetTexture("_BlendTex");

		// Texture2D t2 = rotateTexture(t as Texture2D, true);
		// c.SetTexture("_BlendTex", t2);
		
		// Debug.Log(c);
		
		
    }

	// Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    //  {
    //      Color32[] original = originalTexture.GetPixels32();
    //      Color32[] rotated = new Color32[original.Length];
    //      int w = originalTexture.width;
    //      int h = originalTexture.height;
 
    //      int iRotated, iOriginal;
 
    //      for (int j = 0; j < h; ++j)
    //      {
    //          for (int i = 0; i < w; ++i)
    //          {
    //              iRotated = (i + 1) * h - j - 1;
    //              iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
    //              rotated[iRotated] = original[iOriginal];
    //          }
    //      }
 
    //      Texture2D rotatedTexture = new Texture2D(h, w);
    //      rotatedTexture.SetPixels32(rotated);
    //      rotatedTexture.Apply();
    //      return rotatedTexture;
    //  }

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
