using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VolumeController : DisplayObject
{
	int _step = -1;
	float _angle;

	GameObject container;
	RawImage flash;
	RawImage bg;
	Image gauge;
	Tween tween;
	Text volumeValue;

    void Start()
    {
		container = GameObject.Find("container");
		flash = GameObject.Find("flash").GetComponent<RawImage>();
		bg = GameObject.Find("bg").GetComponent<RawImage>();
		gauge = GameObject.Find("gauge").GetComponent<Image>();
		volumeValue = GameObject.Find("volumeValue").GetComponent<Text>();

		container.transform.DOLocalMoveY(-500f, 0f);
		bg.DOFade(0, 0);
		
		SetStep(4);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            bg.DOFade(1f, 1f);
			container.transform.DOLocalMoveY(-120f, 0.7f);

			SetStep(_step+1);

			if(tween != null) tween.Kill();
			tween = DOVirtual.DelayedCall(2, ()=>{
				bg.DOFade(0f, 1f);
				container.transform.DOLocalMoveY(-500, 0.7f);
				tween = null;

				Camera cam =  GameObject.Find("mainCamera").GetComponent<Camera>();
				DOTween.To(()=>cam.orthographicSize, (v)=>cam.orthographicSize=v, 2f, 0.7f);
				cam.transform.DOMoveY(10f, 0.7f);
			});

			Camera camera =  GameObject.Find("mainCamera").GetComponent<Camera>();
			DOTween.To(()=>camera.orthographicSize, (v)=>camera.orthographicSize=v, 2.6f, 0.7f);
			camera.transform.DOMoveY(9.2f, 0.7f);
        }

		if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            bg.DOFade(1f, 1f);
			container.transform.DOLocalMoveY(-120f, 0.7f);

			SetStep(_step-1);

			if(tween != null) tween.Kill();
			tween = DOVirtual.DelayedCall(2, ()=>{
				bg.DOFade(0f, 1f);
				container.transform.DOLocalMoveY(-500, 0.7f);
				tween = null;

				Camera cam =  GameObject.Find("mainCamera").GetComponent<Camera>();
				DOTween.To(()=>cam.orthographicSize, (v)=>cam.orthographicSize=v, 2f, 0.7f);
				cam.transform.DOMoveY(10f, 0.7f);
			});

			Camera camera =  GameObject.Find("mainCamera").GetComponent<Camera>();
			DOTween.To(()=>camera.orthographicSize, (v)=>camera.orthographicSize=v, 2.6f, 0.7f);
			camera.transform.DOMoveY(9.2f, 0.7f);
        }
    }

	public void SetStep(int step)
	{
		if(step < 0 || step > 9) return;

		float angle = 90f + 270f * step / 9f;
		float duration = _step ==-1 ? 0f : 0.5f;
		_step = step;

		// DOTween.To(()=>gauge.fillAmount, v=>gauge.fillAmount=v, angle / 360f, duration);
		DOTween.To(()=>_angle, v=>_angle=v, angle, duration).OnUpdate(()=>{
			gauge.fillAmount = _angle / 360;
			gauge.material.SetFloat("_RotationA", _angle);
			flash.transform.localRotation = Quaternion.Euler(0f, 0f, 45f - _angle);

			float curStep = 9f*((_angle-90f)%360)/270;
			volumeValue.text = Mathf.Floor(curStep * 5) + "";
			MusicPlayer.instance.SetVolume(curStep*0.2f);
		});
	}

}
