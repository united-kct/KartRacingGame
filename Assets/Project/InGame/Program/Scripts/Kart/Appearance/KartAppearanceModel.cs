#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public class KartAppearanceModel
    {
        public float MaxRotateVelocity { get; private set; }

        public KartAppearanceModel()
        {
            MaxRotateVelocity = 60;
        }
    }
}