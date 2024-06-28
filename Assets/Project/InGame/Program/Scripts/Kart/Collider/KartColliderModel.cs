using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public class KartColliderModel : MonoBehaviour
    {
        [SerializeField] private float _moveAcceleration = 2.78f; // 10 * 1000 / 3600
        [SerializeField] private float _maxVelocity = 13.9f; // 50 * 1000 / 3600

        public float MoveAcceleration => _moveAcceleration;
        public float MaxVelocity => _maxVelocity;
    }
}