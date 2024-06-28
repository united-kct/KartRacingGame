using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public class KartColliderView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        public Vector3 Velocity => _rb.velocity;
        public Vector3 Position => transform.position;

        public void SetVelocity(Vector3 velocity)
        {
            _rb.velocity = velocity;
        }
    }
}