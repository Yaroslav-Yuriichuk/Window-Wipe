using System;
using UnityEngine;

namespace WindowWiper.Scripts.Input
{
    public class TouchInputModule : InputModule
    {
        public override event Action<Vector2> WindowWipeEvent;

        [SerializeField] private LayerMask _windowLayerMask;
        
        private const float MaxRaycastDistance = 15f;

        private Camera _mainCamera;
        
        private Ray _ray;
        private RaycastHit _hit;
        
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (UnityEngine.Input.touchCount != 1)
            {
                return;
            }

            _ray = _mainCamera.ScreenPointToRay(UnityEngine.Input.GetTouch(0).position);

            if (Physics.Raycast(_ray, out _hit, MaxRaycastDistance, _windowLayerMask))
            {
                WindowWipeEvent?.Invoke(_hit.textureCoord);
            }
        }
    }
}