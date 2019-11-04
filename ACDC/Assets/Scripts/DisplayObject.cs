using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class DisplayObject : MonoBehaviour
{
	public static T Create<T>(bool hasScriptAdded = false) where T : Component
	{
		FieldInfo info = typeof(T).GetField("prefabPath");

		if (info == null)
		{
			GameObject gameObject = new GameObject();
			return gameObject.AddComponent<T>();
		}
		else
		{
			string prefabPath = (string)info.GetValue(null);
			GameObject gameObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + prefabPath));
			if (hasScriptAdded)
			{
				return gameObject.GetComponent<T>();
			}
			else
			{
				return gameObject.AddComponent<T>();
			}
		}
	}

	public static DisplayObject CreateObject(string prefabPath)
	{
		GameObject gameObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + prefabPath));
		return gameObject.AddComponent<DisplayObject>();
	}

	public static DisplayObject CreateEmptyObject()
	{
		GameObject gameObject = new GameObject();
		return gameObject.AddComponent<DisplayObject>();
	}


	public void ResetPosition()
	{
		transform.localPosition = new Vector3(0, 0, 0);
	}

	public float x
	{
		get { return transform.localPosition.x; }
		set
		{
			Vector3 p = transform.localPosition;
			p.x = value;
			transform.localPosition = p;
		}
	}

	public float y
	{
		get { return transform.localPosition.y; }
		set
		{
			Vector3 p = transform.localPosition;
			p.y = value;
			transform.localPosition = p;
		}
	}

	public float z
	{
		get { return transform.localPosition.z; }
		set
		{
			Vector3 p = transform.localPosition;
			p.z = value;
			transform.localPosition = p;
		}
	}

	public float _scale = 1;
	public float scale
	{
		get { return _scale; }
		set
		{
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(value, value, value));
			_scale = value;
		}
	}

	public float _scaleX = 1;
	public float scaleX
	{
		get { return _scaleX; }
		set
		{
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(value, 1, 1));
			_scaleX = value;
		}
	}

	public float _scaleY = 1;
	public float scaleY
	{
		get { return _scaleY; }
		set
		{
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1, value, 1));
			_scaleY = value;
		}
	}

	public float _scaleZ = 1;
	public float scaleZ
	{
		get { return _scaleZ; }
		set
		{
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1, 1, value));
			_scaleZ = value;
		}
	}

	private List<float> _alphaList;
	public float _alpha = 1;
	public float alpha
	{
		get { return _alpha; }
		set
		{
			Renderer[] list = gameObject.GetComponentsInChildren<Renderer>();
			Renderer renderer;
			Color color;

			if (_alphaList == null)
			{
				_alphaList = new List<float>();
				for (int i = 0; i < list.Length; i++)
				{
					renderer = list[i];
					_alphaList.Add(list[i].material.color.a);
				}
			}

			for (int i = 0; i < list.Length; i++)
			{
				renderer = list[i];
				color = renderer.material.color;
				color.a = _alphaList[i] * value;
				renderer.material.color = color;
			}
			_alpha = value;
		}
	}

	public DisplayObject parent;

	public void AddChild(DisplayObject child)
	{
		child.transform.parent = transform;
		child.parent = this;
	}

	public TweenerCore<Vector3, Vector3, VectorOptions> TweenX(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return transform.DOLocalMoveX(value, duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return transform.DOLocalMoveX(value, duration).SetEase(ease);
		}
	}

	public TweenerCore<Vector3, Vector3, VectorOptions> TweenY(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return transform.DOLocalMoveY(value, duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return transform.DOLocalMoveY(value, duration).SetEase(ease);
		}
	}

	public TweenerCore<Vector3, Vector3, VectorOptions> TweenZ(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return transform.DOLocalMoveZ(value, duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return transform.DOLocalMoveZ(value, duration).SetEase(ease);
		}
	}

	public TweenerCore<float, float, FloatOptions> TweenScale(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return DOTween.To(() => scale, v => scale = v, value, duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return DOTween.To(() => scale, v => scale = v, value, duration).SetEase(ease);
		}
	}

	public TweenerCore<float, float, FloatOptions> TweenScaleX(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return DOTween.To(() => scaleX, v => scaleX = v, value, duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return DOTween.To(() => scaleX, v => scaleX = v, value, duration).SetEase(ease);
		}
	}
	public TweenerCore<float, float, FloatOptions> TweenScaleY(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return DOTween.To(() => scaleY, v => scaleY = v, value, duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return DOTween.To(() => scaleY, v => scaleY = v, value, duration).SetEase(ease);
		}
	}
	public TweenerCore<float, float, FloatOptions> TweenScaleZ(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return DOTween.To(() => scaleZ, v => scaleZ = v, value, duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return DOTween.To(() => scaleZ, v => scaleZ = v, value, duration).SetEase(ease);
		}
	}

	public TweenerCore<Quaternion, Vector3, QuaternionOptions> TweenRotaionX(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return transform.DOLocalRotate(new Vector3(value, 0, 0), duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return transform.DOLocalRotate(new Vector3(value, 0, 0), duration).SetEase(ease);
		}
	}

	public TweenerCore<Quaternion, Vector3, QuaternionOptions> TweenRotaionY(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return transform.DOLocalRotate(new Vector3(0, value, 0), duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return transform.DOLocalRotate(new Vector3(0, value, 0), duration).SetEase(ease);
		}
	}

	public TweenerCore<Quaternion, Vector3, QuaternionOptions> TweenRotaionZ(float value, float duration, Ease ease = Ease.Unset)
	{
		if (ease == Ease.Unset)
		{
			return transform.DOLocalRotate(new Vector3(0, 0, value), duration).SetEase(MotionCurve.instance.defaultCurve);
		}
		else
		{
			return transform.DOLocalRotate(new Vector3(0, 0, value), duration).SetEase(ease);
		}
	}

	public TweenerCore<float, float, FloatOptions> TweenAlpha(float value, float duration)
	{
		return DOTween.To(() => alpha, v => alpha = v, value, duration).SetEase(Ease.Linear);
	}
}
