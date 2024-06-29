using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;

namespace InGame.Kart
{
    public class KartAppearancePresenter : MonoBehaviour
    {
        [SerializeField] private KartAppearanceModel _appearanceModel;
        [SerializeField] private KartAppearanceView _appearanceView;
        [SerializeField] private KartColliderView _colliderView;

        private void Start()
        {
            CancellationToken ct = this.GetCancellationTokenOnDestroy();
            Rotate(ct).Forget();

            this.FixedUpdateAsObservable().Subscribe(_ => _appearanceView.transform.position = _colliderView.transform.position).AddTo(this);
        }

        private async UniTaskVoid Rotate(CancellationToken ct)
        {
            while (true)
            {
                //await UniTask.WaitUntil(() => Input.GetAxis("Horizontal") != 0 && !_isDrifting, cancellationToken: ct);
                await UniTask.WaitUntil(() => Input.GetAxis("Horizontal") != 0, PlayerLoopTiming.FixedUpdate, ct);
                _appearanceView.transform.eulerAngles = new Vector3(0, _appearanceView.transform.eulerAngles.y + _appearanceModel.MaxRotateVelocity / 50 * Input.GetAxis("Horizontal"), 0);
            }
        }
    }
}