using UnityEngine;

namespace WindowWiper.Scripts.Window
{
    public class WipeableWindowGPU : WipeableWindow
    {
        [SerializeField] private ComputeShader _wipeShader;

        #region Shader Hashes

        private static readonly int WindowRenderTextureKeyword = Shader.PropertyToID("_WindowRenderTexture");
        private static readonly int WipeTextureCoordinatesKeyword = Shader.PropertyToID("_WipeTextureCoordinates");
        private static readonly int FullWipeRadiusKeyword = Shader.PropertyToID("_FullWipeRadius");
        private static readonly int FadeWipeRadiusKeyword = Shader.PropertyToID("_FadeWipeRadius");
        private static readonly int InteractionTimeKeyword = Shader.PropertyToID("_InteractionTime");
        private static readonly int WipeTimeKeyword = Shader.PropertyToID("_WipeTime");

        #endregion

        private int _wipeKernelId;

        private int _groupThreadsX;
        private int _groupThreadsY;

        protected override void Start()
        {
            base.Start();
            
            Graphics.Blit(_windowTexture, _windowRenderTexture);

            _wipeKernelId = _wipeShader.FindKernel("Wipe");
            
            _wipeShader.GetKernelThreadGroupSizes(_wipeKernelId, out uint x, out uint y, out uint z);
            if (z > 1)
            {
                Debug.LogError("Your shader might not work properly, please set [numthreads(x, y, 1)]");
            }

            _wipeShader.SetTexture(_wipeKernelId, WindowRenderTextureKeyword, _windowRenderTexture);
            _wipeShader.SetFloat(WipeTimeKeyword, _wipeTime);
            
            _groupThreadsX = (_windowRenderTexture.width + (int)x - 1) / (int)x;
            _groupThreadsY = (_windowRenderTexture.height + (int)y - 1) / (int)y;
        }

        public override void Wipe(WipeData wipeData)
        {
            _wipeShader.SetFloat(FullWipeRadiusKeyword, wipeData.FullWipeRadius);
            _wipeShader.SetFloat(FadeWipeRadiusKeyword, wipeData.FadeWipeRadius);
            _wipeShader.SetFloat(InteractionTimeKeyword, wipeData.InteractionTime);
            
            _wipeShader.SetFloats(WipeTextureCoordinatesKeyword,
                wipeData.TextureCoordinates.x, wipeData.TextureCoordinates.y);
            
            _wipeShader.Dispatch(_wipeKernelId, _groupThreadsX, _groupThreadsY, 1);
        }
    }
}