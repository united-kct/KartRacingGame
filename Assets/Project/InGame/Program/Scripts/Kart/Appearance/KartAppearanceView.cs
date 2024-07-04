#nullable enable

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