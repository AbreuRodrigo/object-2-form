using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TweenType
{
	Linear = LeanTweenType.linear,
	QuadraticIn = LeanTweenType.easeInQuad,
	QuadraticOut = LeanTweenType.easeOutQuad,
	QuadraticInOut = LeanTweenType.easeInOutQuad,
	CubicIn = LeanTweenType.easeInCubic,
	CubicOut = LeanTweenType.easeOutCubic,
	CubicInOut = LeanTweenType.easeInOutCubic,
	QuarticIn = LeanTweenType.easeInQuart,
	QuarticOut = LeanTweenType.easeOutQuart,
	QuarticInOut = LeanTweenType.easeInOutQuart,
	QuinticIn = LeanTweenType.easeInQuint,
	QuinticOut = LeanTweenType.easeOutQuint,
	QuinticInOut = LeanTweenType.easeInOutQuint,
	SinusoidalIn = LeanTweenType.easeInSine,
	SinusoidalOut = LeanTweenType.easeOutSine,
	SinusoidalInOut = LeanTweenType.easeInOutSine,
	ExponentialIn = LeanTweenType.easeInExpo,
	ExponentialOut = LeanTweenType.easeOutExpo,
	ExponentialInOut = LeanTweenType.easeInOutExpo,
	CircularIn = LeanTweenType.easeInCirc,
	CircularOut = LeanTweenType.easeOutCirc,
	CircularInOut = LeanTweenType.easeInOutCirc,
	ElasticIn = LeanTweenType.easeInElastic,
	ElasticOut = LeanTweenType.easeOutElastic,
	ElasticInOut = LeanTweenType.easeInOutElastic,
	BackIn = LeanTweenType.easeInBack,
	BackOut = LeanTweenType.easeOutBack,
	BackInOut = LeanTweenType.easeInOutBack,
	BounceIn = LeanTweenType.easeInBounce,
	BounceOut = LeanTweenType.easeOutBounce,
	BounceInOut = LeanTweenType.easeInOutBounce
}

public class TweenManager : MonoBehaviourSingleton<TweenManager>
{
    public void ResetTweening()
    {
        LeanTween.cancelAll();
    }

    public void Alpha(RectTransform rect, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, float delay = 0, Action onComplete = null)
    {
        LeanTween.alpha(rect, to, time).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnComplete(onComplete).setEase((LeanTweenType)type);
    }

    public void Alpha(RectTransform rect, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, float delay = 0, Action onComplete = null, Action<float> onUpdate = null)
    {
        LeanTween.alpha(rect, to, time).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnComplete(onComplete).setOnUpdate(onUpdate).setEase((LeanTweenType)type);
    }

    public void Alpha(GameObject obj, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, float delay = 0, Action onComplete = null, Action<float> onUpdate = null)
    {
		LeanTween.alpha(obj, to, time).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnComplete(onComplete).setOnUpdate(onUpdate).setEase((LeanTweenType)type);
    }

    public void ScaleIn(GameObject obj, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.scale(obj, Vector3.one, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void ScaleOut(GameObject obj, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.scale(obj, Vector3.zero, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void ScaleIn(RectTransform rect, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.scale(rect, Vector3.one, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnStart(onStart).setEase((LeanTweenType)type);
    }

    public void ScaleOut(RectTransform rect, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
        LeanTween.scale(rect, Vector3.zero, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnStart(onStart).setEase((LeanTweenType)type);
    }

    public void ScaleTo(GameObject obj, Vector3 to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
        LeanTween.scale(obj, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnStart(onStart).setEase((LeanTweenType)type);
    }

    public void ScaleTo(RectTransform rect, Vector3 to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.scale(rect, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnStart(onStart).setEase((LeanTweenType)type);
    }

    public void ScalePingPong(RectTransform rect, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.scale(rect, new Vector3(1.1f, 1.1f, 1.1f), time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setLoopPingPong().setEase((LeanTweenType)type);
    }

    public void ValueTransition(float from, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action<float> onUpdate = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.value(from, to, time).setOnStart(onStart).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnUpdate(onUpdate).setEase((LeanTweenType)type);
    }	   

    public void ValueTransition(GameObject gameObject, float from, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action<float> onUpdate = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.value(gameObject, from, to, time).setOnStart(onStart).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnUpdate(onUpdate).setEase((LeanTweenType)type);
    }
	
    public void ValueTransitionPingPong(float from, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action<float> onUpdate = null, Action onComplete = null, float delay = 0)
    {
		LeanTween.value(from, to, time).setOnStart(onStart).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setOnUpdate(onUpdate).setLoopPingPong(0).setEase((LeanTweenType)type);
    }

    //SLIDE/MOVE - Animated movements in one axis

    public void SlideX(GameObject obj, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
		LeanTween.moveX(obj, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }	    

    public void SlideX(RectTransform rect, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
		LeanTween.moveX(rect, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }
	
    public void SlideY(GameObject obj, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.moveY(obj, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void SlideY(RectTransform rect, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.moveY(rect, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void SlideTo(GameObject obj, Vector3 to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.move(obj, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void SlideTo(RectTransform rect, Vector3 to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.move(rect, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void MoveOutRight(RectTransform rect, float to, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.moveX(rect, to, 0).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void MoveY(RectTransform rect, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
		LeanTween.moveY(rect, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void MoveX(RectTransform rect, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onStart = null, Action onComplete = null, float delay = 0)
    {
        LeanTween.moveX(rect, to, time).setOnComplete(onComplete).setDelay(delay).setOnStart(onStart).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void MoveX(GameObject obj, float to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.moveX(obj, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void Move(GameObject obj, Vector3 to, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.move(obj, to, time).setOnComplete(onComplete).setDelay(delay).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

	public void ShakeX(GameObject obj, float amount, float time, bool unscaledTime = true, Action onComplete = null, float delay = 0)
	{
		LeanTween.move(obj, obj.transform.position + new Vector3(amount, 0, 0), time).setOnComplete(()=> {
			LeanTween.move(obj, obj.transform.position - new Vector3(amount, 0, 0), time * 0.5f).setOnComplete(onComplete).setIgnoreTimeScale(unscaledTime).setLoopPingPong(1).setEaseInOutBack();
		}).setDelay(delay).setIgnoreTimeScale(unscaledTime).setLoopPingPong(1).setEaseInOutBack();
	}

    public void Shake(GameObject obj, float amount, float time, TweenType type, bool unscaledTime = true, Action onComplete = null, float delay = 0, int loop = 1)
    {
        LeanTween.move(obj, obj.transform.position + new Vector3(amount, amount, 0), time).setOnComplete(() => {
            LeanTween.move(obj, obj.transform.position - new Vector3(amount, amount, 0), time * 0.5f).setOnComplete(onComplete).setIgnoreTimeScale(unscaledTime).setLoopPingPong(loop).setEase((LeanTweenType)type);
		}).setDelay(delay).setIgnoreTimeScale(unscaledTime).setLoopPingPong(loop).setEase((LeanTweenType)type);
	}

    //ALPHA FADING
    public void Fade(GameObject obj, float fadeTo, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.alpha(obj, fadeTo, time).setDelay(delay).setOnComplete(onComplete).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void Fade(RectTransform rect, float fadeTo, float time, TweenType type = TweenType.Linear, bool unscaledTime = true, Action onComplete = null, float delay = 0)
    {
        LeanTween.alpha(rect, fadeTo, time).setDelay(delay).setOnComplete(onComplete).setIgnoreTimeScale(unscaledTime).setEase((LeanTweenType)type);
    }

    public void Spin(RectTransform rect, float time, TweenType type = TweenType.Linear, bool unscaledtime = true, float delay = 0)
    {
		LeanTween.rotateAround(rect, new Vector3(0, 0, 1), -360, time).setDelay(delay).setRepeat(-1).setIgnoreTimeScale(unscaledtime).setEase((LeanTweenType)type);
    }

    public void Rotate180Y(RectTransform rect, float time, TweenType type = TweenType.Linear, bool unscaledtime = true, float delay = 0)
    {
		LeanTween.rotateAround(rect, new Vector3(0, 1, 0), 180, time).setDelay(delay).setIgnoreTimeScale(unscaledtime).setEase((LeanTweenType)type);
    }

    public void Rotate(RectTransform rect, float time, float y, Action onComplete, TweenType type = TweenType.Linear, bool unscaledtime = true, float delay = 0)
    {
		LeanTween.rotateAround(rect, new Vector3(0, 1, 0), y, time).setDelay(delay).setIgnoreTimeScale(unscaledtime).setOnComplete(onComplete).setEase((LeanTweenType)type);
    }
}