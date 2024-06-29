using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

namespace InGame.Kart
{
    public class KartColliderModel : MonoBehaviour
    {
        [SerializeField] private float _moveAcceleration = 2.78f; // 10 * 1000 / 3600
        [SerializeField] private float _maxVelocity = 13.9f; // 50 * 1000 / 3600

        private readonly ReactiveProperty<Vector3> _velocity = new(new Vector3(0, 0, 0));

        public float MoveAcceleration => _moveAcceleration;
        public float MaxVelocity => _maxVelocity;
        public ReadOnlyReactiveProperty<Vector3> Velocity => _velocity;

        public void SetVelocity(Vector3 velocity)
        {
            _velocity.Value = velocity;
        }
    }
}