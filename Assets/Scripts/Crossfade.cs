using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeState
{
    A,
    B
}

public class Crossfade : MonoBehaviour
{
    public FadeState TargetState = FadeState.A;
    private FadeState curState = FadeState.A;
    private bool isFading = true;

    public Renderer crossfadeRenderer;
    public float fadeTime = 1.0f;

    private float startTime = 0.0f;
    //private float endTime;

    public void TriggerCrossfade()
    {
        TargetState = TargetState == FadeState.A ? FadeState.B : FadeState.A;
    }



    // Update is called once per frame
    void Update()
    {
        if (TargetState != curState)
        {
            curState = TargetState;
            startTime = Time.realtimeSinceStartup;
            //endTime = startTime + fadeTime;
            isFading = true;
            crossfadeRenderer.enabled = true;
        }

        if (isFading)
        {
            float curTime = Time.realtimeSinceStartup;
            float fadeAmount = (curTime - startTime) / fadeTime;

            // Set shader value
            if (crossfadeRenderer != null)
            {
                crossfadeRenderer.material.SetFloat("_DissolveAmount", fadeAmount);
            }

            if (fadeAmount >= fadeTime)
            {
                isFading = false;
                crossfadeRenderer.enabled = false;
            }
        }


    }
}
