#nullable enable

using UnityEngine;

namespace InGame.Kart
{
    public class KartAppearanceView : MonoBehaviour
    {
        public void UpdateDirection(float velocity)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + velocity / 50, 0);
        }

        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}