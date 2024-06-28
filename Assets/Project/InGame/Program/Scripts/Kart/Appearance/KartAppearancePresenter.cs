using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace InGame.Kart
{
    public class KartAppearancePresenter : MonoBehaviour
    {
        [SerializeField] private KartAppearanceModel _appearanceModel;
        [SerializeField] private KartColliderView _colliderView;

        private void Start()
        {
            CancellationToken ct = this.GetCancellationTokenOnDestroy();
            Rotate(ct).Forget();
        }

        private void FixedUpdate()
        {
            transform.position = _colliderView.transform.position;
        }

        private async UniTaskVoid Rotate(CancellationToken ct)
        {
            while (true)
            {
                //await UniTask.WaitUntil(() => Input.GetAxis("Horizontal") != 0 && !_isDrifting, cancellationToken: ct);
                await UniTask.WaitUntil(() => Input.GetAxis("Horizontal") != 0, PlayerLoopTiming.FixedUpdate, ct);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + _appearanceModel.MaxRotateVelocity / 50 * Input.GetAxis("Horizontal"), 0);
            }
        }
    }
}