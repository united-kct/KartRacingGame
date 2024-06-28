using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.View
{
    public class KartColliderView : MonoBehaviour
    {
        public void OnVelocityChanged(Rigidbody rb, Vector3 velocity)
        {
            rb.velocity = velocity;
        }
    }
}