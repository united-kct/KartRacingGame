#nullable enable

using R3;
using UnityEngine;

namespace InGame.Kart
{
    public class KartAppearanceModel
    {
        private readonly ReactiveProperty<Vector3> _direction;

        public float MaxRotateVelocity { get; private set; }
        public ReadOnlyReactiveProperty<Vector3> Direction => _direction;

        public KartAppearanceModel(Vector3 direction)
        {
            _direction = new(direction);

            MaxRotateVelocity = 60;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction.Value = direction;
        }
    }
}