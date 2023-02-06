using UnityEngine;
using WindowWiper.Scripts.Input;

namespace WindowWiper.Scripts.Window
{
    public class WindowWiper : MonoBehaviour
    {
        [SerializeField] private WipeableWindow _window;
        [SerializeField] private InputModule _inputModule;

        [Header("Adjustments")]
        [Tooltip("Relative radius to height")]
        [SerializeField] private float _fullWipeRadius;
        [Tooltip("Relative radius to height")]
        [SerializeField] private float _fadeWipeRadius;
        
        private void OnEnable()
        {
            _inputModule.WindowWipeEvent += WipeWindow;
        }

        private void OnDisable()
        {
            _inputModule.WindowWipeEvent -= WipeWindow;
        }

        private void WipeWindow(Vector2 textureCoordinates)
        {
            _window.Wipe(new WipeData
            {
                FullWipeRadius = _fullWipeRadius,
                FadeWipeRadius = _fadeWipeRadius,
                TextureCoordinates = textureCoordinates,
                InteractionTime = Time.deltaTime,
            });
        }
    }
}