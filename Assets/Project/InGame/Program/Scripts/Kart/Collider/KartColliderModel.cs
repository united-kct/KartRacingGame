#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

namespace InGame.Kart
{
    public class KartColliderModel : MonoBehaviour
    {
        [SerializeField] private float _moveAcceleration = 5.56f; // 20 * 1000 / 3600
        [SerializeField] private float _maxVelocity = 13.9f; // 50 * 1000 / 3600
        [SerializeField] private float _groundDistance = 0.6f;

        private readonly ReactiveProperty<Vector3> _velocity = new(new Vector3(0, 0, 0));

        public float MoveAcceleration => _moveAcceleration;
        public float MaxVelocity => _maxVelocity;
        public float GroundDistance => _groundDistance;
        public ReadOnlyReactiveProperty<Vector3> Velocity => _velocity;

        public bool IsAboveGround { get; set; }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity.Value = velocity;
        }
    }
}