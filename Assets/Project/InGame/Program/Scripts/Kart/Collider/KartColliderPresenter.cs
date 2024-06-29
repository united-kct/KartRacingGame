using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using R3;

namespace InGame.Kart
{
    public class KartColliderPresenter : MonoBehaviour
    {
        [SerializeField] private KartColliderModel _colliderModel;
        [SerializeField] private KartColliderView _colliderView;
        [SerializeField] private KartAppearanceView _appearanceView;

        private void Start()
        {
            _colliderModel.Velocity.Subscribe(_colliderView.OnVelocityChanged).AddTo(this);

            CancellationToken ct = this.GetCancellationTokenOnDestroy();
            MoveForward(ct).Forget();
            MoveBackward(ct).Forget();
        }

        private async UniTaskVoid MoveForward(CancellationToken ct)
        {
            double maxSqrVelocity = Math.Pow(_colliderModel.MaxVelocity, 2);
            while (true)
            {
                //await UniTask.WaitUntil(() => Input.GetKey(KeyCode.W) && _isAboveGround && _rb.velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.WaitUntil(() => Input.GetKey(KeyCode.W) && _colliderModel.Velocity.CurrentValue.sqrMagnitude < maxSqrVelocity, PlayerLoopTiming.FixedUpdate, ct);
                _colliderModel.SetVelocity(_colliderModel.Velocity.CurrentValue + _appearanceView.transform.forward * _colliderModel.MoveAcceleration / 50);
            }
        }

        private async UniTaskVoid MoveBackward(CancellationToken ct)
        {
            double maxSqrVelocity = Math.Pow(_colliderModel.MaxVelocity / 5, 2);
            while (true)
            {
                //await UniTask.WaitUntil(() => Input.GetKey(KeyCode.S) && _isAboveGround && _rb.velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.WaitUntil(() => Input.GetKey(KeyCode.S) && _colliderModel.Velocity.CurrentValue.sqrMagnitude < maxSqrVelocity, PlayerLoopTiming.FixedUpdate, ct);
                _colliderModel.SetVelocity(_colliderModel.Velocity.CurrentValue - _appearanceView.transform.forward * _colliderModel.MoveAcceleration / 50);
            }
        }
    }
}