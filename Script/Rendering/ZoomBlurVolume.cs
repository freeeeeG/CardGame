using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class ZoomBlurVolume : VolumeComponent,IPostProcessComponent
{
    [Range(0f,100f)]
    public FloatParameter focusPower = new FloatParameter(20f);

    public Vector2Parameter focusScreenPosition = new Vector2Parameter(Vector2.zero); 
    public IntParameter focusDetail = new IntParameter(5);
    public IntParameter referenceResolutionX = new IntParameter(1080);
    public bool IsActive() => focusPower.value > 0; 
    public bool IsTileCompatible() => false;
}
