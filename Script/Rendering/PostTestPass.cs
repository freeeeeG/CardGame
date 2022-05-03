using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

using UnityEngine.Rendering.Universal;



namespace UnityEngine.Experiemntal.Rendering.Universal

{

    /// <summary>

    /// Copy the given color buffer to the given destination color buffer.

    ///

    /// You can use this pass to copy a color buffer to the destination,

    /// so you can use it later in rendering. For example, you can copy

    /// the opaque texture to use it for distortion effects.

    /// </summary>

    internal class PostTestPass : ScriptableRenderPass

    {





        public enum RenderTarget

        {

            Color,

            RenderTexture,

        }

        public Material blurMaterial = null;

        public int blurShaderPassIndex = 0;

        public FilterMode filterMode { get; set; }

        private int downSample;

        private int blurCount;

        private float indensity;

        private RenderTargetIdentifier source { get; set; }

        private RenderTargetHandle destination { get; set; }



        RenderTargetHandle m_TemporaryColorTexture01;

        RenderTargetHandle m_TemporaryColorTexture02;

        RenderTargetHandle m_TemporaryColorTexture03;

        string m_ProfilerTag;





        public PostTestPass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag, int downSample, int blurCount, float indensity)

        {

            this.renderPassEvent = renderPassEvent;

            this.blurMaterial = blitMaterial;

            this.blurShaderPassIndex = blitShaderPassIndex;

            this.downSample = downSample;

            this.blurCount = blurCount;

            this.indensity = indensity;

            m_ProfilerTag = tag;

            m_TemporaryColorTexture01.Init("_TemporaryColorTexture1");

            m_TemporaryColorTexture02.Init("_TemporaryColorTexture2");

            m_TemporaryColorTexture03.Init("_TemporaryColorTexture3");

        }



        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)

        {

            this.source = source;

            this.destination = destination;

        }







        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)

        {
            if (!renderingData.cameraData.postProcessEnabled) return;
            Debug.Log("PostTestPass");
            int width = renderingData.cameraData.cameraTargetDescriptor.width;

            int height = renderingData.cameraData.cameraTargetDescriptor.height;

            CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;

            opaqueDesc.width = opaqueDesc.width >> downSample;

            opaqueDesc.height = opaqueDesc.height >> downSample;

            opaqueDesc.depthBufferBits = 0;

            cmd.GetTemporaryRT(m_TemporaryColorTexture01.id, opaqueDesc, filterMode);

            cmd.GetTemporaryRT(m_TemporaryColorTexture02.id, opaqueDesc, filterMode);

            cmd.GetTemporaryRT(m_TemporaryColorTexture03.id, opaqueDesc, filterMode);

            cmd.BeginSample("Blur");

            cmd.Blit(source, m_TemporaryColorTexture03.Identifier());

            for (int i = 0; i < blurCount; i++)

            {

                blurMaterial.SetVector("_offsets", new Vector4(0, indensity, 0, 0));

                cmd.Blit(m_TemporaryColorTexture03.Identifier(), m_TemporaryColorTexture01.Identifier(), blurMaterial);



                blurMaterial.SetVector("_offsets", new Vector4(indensity, 0, 0, 0));

                cmd.Blit(m_TemporaryColorTexture01.Identifier(), m_TemporaryColorTexture02.Identifier(), blurMaterial);



                cmd.Blit(m_TemporaryColorTexture02.Identifier(), m_TemporaryColorTexture03.Identifier());

            }

            cmd.Blit(m_TemporaryColorTexture03.Identifier(), source);

            cmd.EndSample("Blur");

            context.ExecuteCommandBuffer(cmd);

            CommandBufferPool.Release(cmd);

        }



        public override void FrameCleanup(CommandBuffer cmd)

        {

            if (destination == RenderTargetHandle.CameraTarget)

            {

                cmd.ReleaseTemporaryRT(m_TemporaryColorTexture01.id);

                cmd.ReleaseTemporaryRT(m_TemporaryColorTexture02.id);

            }

        }

    }

}