#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public class KartColliderView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb = null!;

        public void OnVelocityChanged(Vector3 velocity)
        {
            _rb.velocity = velocity;
        }
    }
}