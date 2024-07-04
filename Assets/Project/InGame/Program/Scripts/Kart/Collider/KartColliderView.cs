#nullable enable

using UnityEngine;

namespace InGame.Kart
{
    public class KartColliderView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb = null!;

        private RaycastHit _groundHit;

        public GameObject? GroundObject => _groundHit.collider ? _groundHit.collider.gameObject : null;

        public void OnVelocityChanged(Vector3 velocity)
        {
            _rb.velocity = velocity;
        }

        public void UpdateGroundRay(float distance)
        {
            Ray ray = new(transform.position, -Vector3.up);
            Physics.Raycast(ray, out _groundHit, distance);
        }
    }
}