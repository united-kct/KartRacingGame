#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Kart
{
    public class KartAppearanceView : MonoBehaviour
    {
        private RaycastHit _groundHit;

        public GameObject? GroundObject => _groundHit.collider ? _groundHit.collider.gameObject : null;

        public void UpdateGroundRay(float distance)
        {
            Ray ray = new(transform.position, -Vector3.up);
            Physics.Raycast(ray, out _groundHit, distance);
        }
    }
}