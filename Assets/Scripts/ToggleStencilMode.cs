using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleStencilMode : MonoBehaviour
{
    public bool stencilMode = false;
    private bool wasStencilModeOn = false;

    public Shader stencilShader;
    public Shader standardShader;

    // Update is called once per frame
    void Update()
    {
        if (stencilMode != wasStencilModeOn)
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                if (stencilMode)
                {
                    renderer.material.shader = stencilShader;
                }
                else
                {
                    renderer.material.shader = standardShader;
                }
            }

            // Handle special cases
            foreach (var stencilToggle in GetComponentsInChildren<ToggleStencilMode>())
            {
                stencilToggle.stencilMode = this.stencilMode;
            }

            wasStencilModeOn = stencilMode;
        }
    }
}
