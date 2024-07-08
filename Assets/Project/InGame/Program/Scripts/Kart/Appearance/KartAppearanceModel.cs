#nullable enable

namespace InGame.Kart
{
    public class KartAppearanceModel
    {
        private float _rotateVelocity;

        public float MaxRotateVelocity { get; private set; }
        public float RotateVelocity => _rotateVelocity;

        public KartAppearanceModel()
        {
            _rotateVelocity = 0;

            MaxRotateVelocity = 60;
        }

        public void SetRotateVelocity(float velocity)
        {
            _rotateVelocity = velocity;
        }
    }
}