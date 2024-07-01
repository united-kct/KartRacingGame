#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using R3;
using R3.Triggers;

namespace InGame.Kart
{
    public class KartColliderPresenter : MonoBehaviour
    {
        [SerializeField] private KartColliderModel _colliderModel = null!;
        [SerializeField] private KartColliderView _colliderView = null!;
        [SerializeField] private KartAppearanceView _appearanceView = null!;

        private double _maxSqrVelocity;

        private void Start()
        {
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
            Vector3 velocity = _colliderModel.Velocity.CurrentValue;
            if (Input.GetKey(KeyCode.W) && _colliderModel.IsAboveGround && velocity.sqrMagnitude < _maxSqrVelocity)
            {
                _colliderModel.SetVelocity(velocity + _appearanceView.transform.forward * _colliderModel.MoveAcceleration / 50);
            }
        }

        private void MoveBackward()
        {
            Vector3 velocity = _colliderModel.Velocity.CurrentValue;
            if (Input.GetKey(KeyCode.S) && _colliderModel.IsAboveGround && velocity.sqrMagnitude < _maxSqrVelocity)
            {
                _colliderModel.SetVelocity(velocity - _appearanceView.transform.forward * _colliderModel.MoveAcceleration / 50);
            }
        }

        private void Abrade()
        {
            _colliderView.UpdateGroundRay(_colliderModel.GroundDistance);
            if (_colliderView.GroundObject && Frictions.FrictionalAccelerationList.ContainsKey(_colliderView.GroundObject!.tag))
            {
                _colliderModel.IsAboveGround = true;

                float abradeAcceleration = Frictions.FrictionalAccelerationList[_colliderView.GroundObject.tag];
                Vector3 velocity = _colliderModel.Velocity.CurrentValue;
                if (velocity.sqrMagnitude < Math.Pow(abradeAcceleration / 50, 2))
                {
                    _colliderModel.SetVelocity(new Vector3(0, 0, 0));
                }
                else
                {
                    _colliderModel.SetVelocity(velocity - velocity.normalized * abradeAcceleration / 50);
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