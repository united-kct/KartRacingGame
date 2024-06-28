using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

namespace InGame.Kart
{
    public class KartColliderPresenter : MonoBehaviour
    {
        [SerializeField] private KartColliderModel _model;
        [SerializeField] private KartColliderView _colliderView;
        [SerializeField] private KartAppearanceView _appearanceView;

        private void Start()
        {
            CancellationToken ct = this.GetCancellationTokenOnDestroy();
            MoveForward(ct).Forget();
            MoveBackward(ct).Forget();
        }

        private async UniTaskVoid MoveForward(CancellationToken ct)
        {
            double maxSqrVelocity = Math.Pow(_model.MaxVelocity, 2);
            while (true)
            {
                //await UniTask.WaitUntil(() => Input.GetKey(KeyCode.W) && _isAboveGround && _rb.velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.WaitUntil(() => Input.GetKey(KeyCode.W) && _colliderView.Velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
                _colliderView.SetVelocity(_colliderView.Velocity + _appearanceView.transform.forward * _model.MoveAcceleration / 50);
            }
        }

        private async UniTaskVoid MoveBackward(CancellationToken ct)
        {
            double maxSqrVelocity = Math.Pow(_model.MaxVelocity / 5, 2);
            while (true)
            {
                //await UniTask.WaitUntil(() => Input.GetKey(KeyCode.S) && _isAboveGround && _rb.velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.WaitUntil(() => Input.GetKey(KeyCode.S) && _colliderView.Velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
                _colliderView.SetVelocity(_colliderView.Velocity - _appearanceView.transform.forward * _model.MoveAcceleration / 50);
            }
        }
    }
}