#nullable enable

using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;

namespace InGame.Kart
{
    public class KartAppearancePresenter : MonoBehaviour
    {
        private KartAppearanceModel _appearanceModel = null!;
        [SerializeField] private KartAppearanceView _appearanceView = null!;
        [SerializeField] private KartColliderView _colliderView = null!;

        private void Start()
        {
            _appearanceModel = new();

            this.FixedUpdateAsObservable().Subscribe(_ => _appearanceView.UpdateDirection(_appearanceModel.RotateVelocity)).AddTo(this);
            this.FixedUpdateAsObservable().Subscribe(_ => _appearanceView.UpdatePosition(_colliderView.transform.position)).AddTo(this);
            this.FixedUpdateAsObservable().Subscribe(_ => Rotate()).AddTo(this);
        }

        private void Rotate()
        {
            //if(Input.GetAxis("Horizontal") != 0 && !_isDrifting)
            _appearanceModel.SetRotateVelocity(_appearanceModel.MaxRotateVelocity * Input.GetAxis("Horizontal"));
        }
    }
}