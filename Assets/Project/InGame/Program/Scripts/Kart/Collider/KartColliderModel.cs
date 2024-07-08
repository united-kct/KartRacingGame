#nullable enable

using R3;
using System;
using UnityEngine;

namespace InGame.Kart
{
    public class KartColliderModel
    {
        private readonly ReactiveProperty<Vector3> _velocity;

        public float MoveAcceleration { get; private set; }
        public float MaxVelocity { get; private set; }
        public float GroundDistance { get; private set; }
        public bool IsAboveGround { get; set; }
        public ReadOnlyReactiveProperty<Vector3> Velocity => _velocity;
        public double MaxSqrVelocity { get; private set; }

        public KartColliderModel()
        {
            _velocity = new(new Vector3(0, 0, 0));

            MoveAcceleration = 5.56f; // 20 * 1000 / 3600
            MaxVelocity = 13.9f; // 50 * 1000 / 3600
            GroundDistance = 0.6f;

            MaxSqrVelocity = Math.Pow(MaxVelocity, 2);
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity.Value = velocity;
        }

        public void Accelerate(Vector3 direction, float acceleration)
        {
            _velocity.Value += direction * acceleration / 50;
        }
    }
}