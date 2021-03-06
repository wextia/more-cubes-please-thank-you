﻿using UnityEngine;
using System.Collections;
using System;

public abstract class AnimateUI : MonoBehaviour {

    [Header("Animate")]
    [SerializeField]
    float timeToStart = 1f;
    [SerializeField]
    float duration = 3f;
    [SerializeField]
    bool executeOnEnable = true;

    float currentTime;


    public float TimeToStart
    {
        get
        {
            return timeToStart;
        }

        set
        {
            timeToStart = value;
        }
    }

    public float Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    public void StartAnimation()
    {
        StopCoroutine("Animate");
        InitValues();
        StartCoroutine("Animate");
    }

	void OnEnable () {
        if (!executeOnEnable)
            return;
        StartAnimation();
    }

    void OnDisable()
    {
        StopCoroutine("Animate");
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(timeToStart);
        currentTime = 0;
        bool finished = false;

        //Magic happens here
        while (!finished)
        {
            ApplyAnimation(currentTime / duration);
            currentTime += Time.deltaTime;
            if(currentTime >= duration)
            {
                finished = true;
                FinishAnimation();
            }
            yield return null;
        }
    }

    /// <summary>
    /// Reset the values used in ApplyAnimation
    /// </summary>
    protected abstract void InitValues();

    protected abstract void ApplyAnimation(float progress);

    /// <summary>
    /// Force the animation to finish
    /// </summary>
    protected abstract void FinishAnimation();

}
