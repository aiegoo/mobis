using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicView : MonoBehaviour
{
    public GameObject musicMenu;
    void Start()
    {
		transform.localPosition = new Vector3(0, 1.42f, 0.15f);
        musicMenu = Util.CreateGameObject("Music/MusicMenu");
		musicMenu.name = "musicMenu";
		
		musicMenu.transform.parent = transform;
		musicMenu.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
