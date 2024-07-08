#nullable enable

using Common.MasterData;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using System;
using UnityEngine;

namespace InGame.Kart
{
    public class KartColliderPresenter : MonoBehaviour
    {
        private KartColliderModel _colliderModel = null!;
        [SerializeField] private KartColliderView _colliderView = null!;
        [SerializeField] private KartAppearanceView _appearanceView = null!;

        private double _maxSqrVelocity;

        private void Start()
        {
            _colliderModel = new();

            _maxSqrVelocity = Math.Pow(_colliderModel.MaxVelocity, 2);

            _colliderModel.Velocity.Subscribe(_colliderView.OnVelocityChanged).AddTo(this);
            this.FixedUpdateAsObservable().Subscribe(_ => Move()).AddTo(this);
        }

        private void Move()
        {
            MoveForward();
            MoveBackward();
            Abrade();
        }

        private void MoveForward()
        {
            if (Input.GetKey(KeyCode.W) && _colliderModel.IsAboveGround && _colliderModel.Velocity.CurrentValue.sqrMagnitude < _maxSqrVelocity)
            {
                _colliderModel.Accelerate(_appearanceView.transform.forward, _colliderModel.MoveAcceleration);
            }
        }

        private void MoveBackward()
        {
            if (Input.GetKey(KeyCode.S) && _colliderModel.IsAboveGround && _colliderModel.Velocity.CurrentValue.sqrMagnitude < _maxSqrVelocity)
            {
                _colliderModel.Accelerate(-_appearanceView.transform.forward, _colliderModel.MoveAcceleration);
            }
        }

        private void Abrade()
        {
            _colliderView.UpdateGroundRay(_colliderModel.GroundDistance);
            if (_colliderView.GroundObject && MasterDataDB.DB.FrictionTable.TryFindByTagName(_colliderView.GroundObject!.tag, out Friction? friction))
            {
                _colliderModel.IsAboveGround = true;

                Vector3 velocity = _colliderModel.Velocity.CurrentValue;
                if (velocity.sqrMagnitude < Math.Pow(friction.FrictionalAcceleration / 50, 2))
                {
                    _colliderModel.SetVelocity(new Vector3(0, 0, 0));
                }
                else
                {
                    _colliderModel.Accelerate(-velocity.normalized, friction.FrictionalAcceleration);
                }
            }
            else
            {
                _colliderModel.IsAboveGround = false;
            }
            //Debug.Log(_colliderModel.Velocity.CurrentValue);
            //Debug.Log(_colliderModel.IsAboveGround);
        }
    }
}