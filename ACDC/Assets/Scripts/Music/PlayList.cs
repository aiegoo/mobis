using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[System.Serializable]
public class Music
{
	public int id;
	public string singer;
	public string song;
	public string album;
	public string genre;
}

[System.Serializable]
public class PlayList
{
	public string name;
	public string dest;
	public List<Music> musiclist;
}

[System.Serializable]
public class PlayInfo
{
	public static List<PlayList> list;
	public List<PlayList> items;
}




