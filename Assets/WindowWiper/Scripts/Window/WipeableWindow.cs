using UnityEngine;

namespace WindowWiper.Scripts.Window
{
    public abstract class WipeableWindow : MonoBehaviour
    {
        [SerializeField] protected float _wipeTime;
        
        [Header("Textures")]
        [SerializeField] protected Texture2D _windowTexture;

        [Header("Materials")]
        [SerializeField] private Material _windowMaterial;
        
        protected RenderTexture _windowRenderTexture;
        
        protected virtual void Start()
        {
            _windowRenderTexture = new RenderTexture(_windowTexture.width, _windowTexture.height,
                0, RenderTextureFormat.ARGBFloat)
            {
                enableRandomWrite = true,
            };
            _windowMaterial.mainTexture = _windowRenderTexture;

        }

        public abstract void Wipe(WipeData wipeData);
    }
}