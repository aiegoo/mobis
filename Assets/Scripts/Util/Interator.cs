using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interator<T>
{
	private int _length;
	public int index;
	private List<T> _list;

	public Interator(int length, int idx = 0)
	{
		_length = length;
		index = idx;
	}

	public Interator(List<T> list, int idx = 0)
	{
		_list = list;
		_length = list.Count;
		index = idx;
	}

	public T item
	{
		get { return _list[index]; }
	}

	public T nextItem
	{
		get { return _list[Next()]; }
	}

	public int Next()
	{
		index++;
		if(index >= _length)
		{
			index = 0;
		}
		return index;
	}
	
}

