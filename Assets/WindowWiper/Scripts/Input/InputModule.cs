using System;
using UnityEngine;

namespace WindowWiper.Scripts.Input
{
    public abstract class InputModule : MonoBehaviour
    {
        public abstract event Action<Vector2> WindowWipeEvent;
    }
}