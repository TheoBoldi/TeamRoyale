using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionController : MonoBehaviour
{
    public static TransitionController instance = null;

    public Image transitionImage = null;
    public float timeToTransition = 1;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void FadeIn(Action callback)
    {
        transitionImage.DOFade(1, timeToTransition / 2).SetUpdate(true).OnComplete(()=> callback());
    }

    public void FadeOut(Action callback = null)
    {
        transitionImage.DOFade(0, timeToTransition / 2).SetUpdate(true).OnComplete(() => callback?.Invoke());
    }
}
