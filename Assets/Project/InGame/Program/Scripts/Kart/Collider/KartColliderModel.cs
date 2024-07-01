#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

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

        public KartColliderModel()
        {
            _velocity = new(new Vector3(0, 0, 0));

            MoveAcceleration = 5.56f; // 20 * 1000 / 3600
            MaxVelocity = 13.9f; // 50 * 1000 / 3600
            GroundDistance = 0.6f;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity.Value = velocity;
        }
    }
}