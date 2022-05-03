using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;

using UnityEngine.Rendering.Universal;
public class ZoomBlurPass : ScriptableRenderPass
{
    static readonly string k_RenderTag = "Render ZoomBlur Pass";
    static readonly int MainTexId = Shader.PropertyToID("_MainTex");
    static readonly int TempTargeId = Shader.PropertyToID("_TempTargetBlur");
    static readonly int FocusPowerId = Shader.PropertyToID("_FocusPower");
    static readonly int FocusDetailId = Shader.PropertyToID("_FocusDetail");
    static readonly int FocusScreenPositionId = Shader.PropertyToID("_FocusScreenPosition");
    static readonly int ReferenceResolutionXId = Shader.PropertyToID("_ReferenceResolutionX");

    ZoomBlurVolume volume;
    Material blurMaterial;
    RenderTargetIdentifier source;
    public ZoomBlurPass(RenderPassEvent evt)
    {
        renderPassEvent = evt;
        var shader = Shader.Find("PostEffect/Zoomblur");
        if(shader == null)
        {
            Debug.LogError("ZoomBlur shader not found");
            return;
        }
        blurMaterial = CoreUtils.CreateEngineMaterial(shader);

        
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if(blurMaterial == null)
        {
            Debug.LogError("Material not found");
            return;
        }

        var stack = VolumeManager.instance.stack;
        volume = stack.GetComponent<ZoomBlurVolume>();
        if(volume == null)
        {
            Debug.LogError("ZoomBlurVolume not found");
            return;
        }
        if(!volume.IsActive())
        {
            return;
        }
        var cmd = CommandBufferPool.Get(k_RenderTag);

        Render(cmd, ref renderingData);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    void  Render(CommandBuffer cmd,ref RenderingData rendereringData)
    {
        ref var cameraData = ref rendereringData.cameraData;
        var w = cameraData.cameraTargetDescriptor.width;
        var h = cameraData.cameraTargetDescriptor.height;

        blurMaterial.SetFloat(FocusPowerId, volume.focusPower.value);
        blurMaterial.SetInt(FocusDetailId, volume.focusDetail.value);   
        blurMaterial.SetVector(FocusScreenPositionId, volume.focusScreenPosition.value);
        blurMaterial.SetInt(ReferenceResolutionXId, w);
        
        
        int shaderPass = 0;

        cmd.SetGlobalTexture(MainTexId, source);
        cmd.GetTemporaryRT(TempTargeId, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
        cmd.Blit(source, TempTargeId);
        cmd.Blit(TempTargeId, source, blurMaterial, shaderPass);

    }
    public void Setup(in RenderTargetIdentifier source)
    {
        this.source = source;
        
    }
}
