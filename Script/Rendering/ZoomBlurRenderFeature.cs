using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;

using UnityEngine.Rendering.Universal;
public class ZoomBlurRenderFeature : ScriptableRendererFeature
{
    public ZoomBlurPass zoomBlurPass;
    public override void Create()
    {
        zoomBlurPass = new ZoomBlurPass(RenderPassEvent.AfterRenderingTransparents);
    }


    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        zoomBlurPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(zoomBlurPass);
    }
}
