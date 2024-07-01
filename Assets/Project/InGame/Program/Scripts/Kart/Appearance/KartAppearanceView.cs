#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public class KartAppearanceView : MonoBehaviour
    {
        public void OnDirectionChanged(Vector3 direction)
        {
            transform.eulerAngles = direction;
        }
    }
}