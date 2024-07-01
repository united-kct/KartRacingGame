#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;

namespace InGame.Kart
{
    public class KartAppearancePresenter : MonoBehaviour
    {
        private KartAppearanceModel _appearanceModel = null!;
        [SerializeField] private KartAppearanceView _appearanceView = null!;
        [SerializeField] private KartColliderView _colliderView = null!;

        private void Start()
        {
            _appearanceModel = new(_appearanceView.transform.eulerAngles);

            _appearanceModel.Direction.Subscribe(_appearanceView.OnDirectionChanged).AddTo(this);
            this.FixedUpdateAsObservable().Subscribe(_ => _appearanceView.transform.position = _colliderView.transform.position).AddTo(this);
            this.FixedUpdateAsObservable().Subscribe(_ => Rotate()).AddTo(this);
        }

        private void Rotate()
        {
            //if(Input.GetAxis("Horizontal") != 0 && !_isDrifting)
            if (Input.GetAxis("Horizontal") != 0)
            {
                _appearanceModel.SetDirection(new Vector3(0, _appearanceView.transform.eulerAngles.y + _appearanceModel.MaxRotateVelocity / 50 * Input.GetAxis("Horizontal"), 0));
            }
        }
    }
}