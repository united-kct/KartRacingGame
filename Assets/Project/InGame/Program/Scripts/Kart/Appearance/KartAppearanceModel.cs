#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public class KartAppearanceModel : MonoBehaviour
    {
        [SerializeField] private float _maxRotateVelocity = 60;

        public float MaxRotateVelocity => _maxRotateVelocity;
    }
}