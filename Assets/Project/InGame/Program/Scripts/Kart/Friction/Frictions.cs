using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public static class Frictions
    {
        public static readonly Dictionary<string, float> FrictionalAccelerationList = new()
        {
            { "GroundRoad", .556f },
            { "GroundOffroad", .139f }
        };
    }
}