using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using DG.Tweening;

namespace Common.View
{
    public class KartCollider : MonoBehaviour
    {
        [SerializeField] private Transform _kartModel;
        [SerializeField] private float _moveAcceleration = 2.78f; // 10 * 1000 / 3600
        [SerializeField] private float _maxVelocity = 13.9f; // 50 * 1000 / 3600
        [SerializeField] private float _maxRotateVelocity = 60;
        [SerializeField] private float _abradeAcceleration = .556f; // 2 * 1000 / 3600

        private Rigidbody _rb;
        private bool _isAboveGround;
        private bool _isDrifting;
        private int _driftDirection;
        private float _driftPower;
        private bool _isFirst, _isSecond, _isThird;
        private int _driftMode = 0;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            CancellationToken ct = this.GetCancellationTokenOnDestroy();
            MoveForward(ct).Forget();
            MoveBackward(ct).Forget();
            Rotate(ct).Forget();
        }

        private void Update()
        {
            // ドリフトの開始処理
            if (Input.GetKeyDown(KeyCode.Space) && !_isDrifting && Input.GetAxis("Horizontal") != 0)
            {
                _isDrifting = true;
                _driftDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;

                _kartModel.DOComplete();
                _kartModel.DOPunchPosition(_kartModel.transform.up * .2f, .3f, 5, 1);
            }

            // ドリフトの終了処理
            if (Input.GetKeyUp(KeyCode.Space) && _isDrifting)
            {
                Boost();
            }
        }

        private void FixedUpdate()
        {
            _kartModel.transform.position = transform.position;
            //Debug.Log(_isAboveGround);

            if (_isDrifting)
            {
                float control = _driftDirection == 1 ? UtilMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 0, 2) : UtilMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 2, 0);
                float powerControl = _driftDirection == 1 ? UtilMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, .2f, 1) : UtilMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 1, .2f);
                _kartModel.transform.eulerAngles = new Vector3(0, _kartModel.transform.eulerAngles.y + _maxRotateVelocity / 50 * _driftDirection * control, 0);
                _driftPower += powerControl;

                ColorDrift();
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            _isAboveGround = collision.gameObject.tag.StartsWith("Ground");

            if (collision.gameObject.tag == "GroundRoad")
            {
                if (_rb.velocity.sqrMagnitude < Math.Pow(_abradeAcceleration / 50, 2))
                {
                    _rb.velocity = new Vector3(0, 0, 0);
                }
                else
                {
                    _rb.velocity -= _rb.velocity.normalized * _abradeAcceleration / 50;
                }
                //Debug.Log("road");
            }
            if (collision.gameObject.tag == "GroundOffroad")
            {
                if (_rb.velocity.sqrMagnitude < Math.Pow(_abradeAcceleration / 13, 2))
                {
                    _rb.velocity = new Vector3(0, 0, 0);
                }
                else
                {
                    _rb.velocity -= _rb.velocity.normalized * _abradeAcceleration / 13;
                }
                //Debug.Log("offroad");
            }
            //Debug.Log(_rb.velocity);
        }

        private void OnCollisionExit(Collision collision)
        {
            _isAboveGround = false;
        }

        private void ColorDrift()
        {
            if (_driftPower > 50 && _driftPower < 99 && !_isFirst)
            {
                _isFirst = true;
                _driftMode = 1;
            }

            if (_driftPower > 100 && _driftPower < 149 && !_isSecond)
            {
                _isSecond = true;
                _driftMode = 2;
            }

            if (_driftPower > 150 && !_isThird)
            {
                _isThird = true;
                _driftMode = 3;
            }
        }

        private void Boost()
        {
            _isDrifting = false;

            if (_driftMode > 0)
            {
                DOVirtual.Float(_maxVelocity * 2, _maxVelocity, .3f * _driftMode, value => _rb.velocity = _kartModel.transform.forward * _maxVelocity);
            }

            _driftPower = 0;
            _driftMode = 0;
            _isFirst = false; _isSecond = false; _isThird = false;
        }

        private async UniTaskVoid MoveForward(CancellationToken ct)
        {
            double maxSqrVelocity = Math.Pow(_maxVelocity, 2);
            while (true)
            {
                await UniTask.WaitUntil(() => Input.GetKey(KeyCode.W) && _isAboveGround && _rb.velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
                _rb.velocity += _kartModel.transform.forward * _moveAcceleration / 50;
            }
        }

        private async UniTaskVoid MoveBackward(CancellationToken ct)
        {
            double maxSqrVelocity = Math.Pow(_maxVelocity / 5, 2);
            while (true)
            {
                await UniTask.WaitUntil(() => Input.GetKey(KeyCode.S) && _isAboveGround && _rb.velocity.sqrMagnitude < maxSqrVelocity, cancellationToken: ct);
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
                _rb.velocity -= _kartModel.transform.forward * _moveAcceleration / 50;
            }
        }

        private async UniTaskVoid Rotate(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => Input.GetAxis("Horizontal") != 0 && !_isDrifting, cancellationToken: ct);
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
                _kartModel.transform.eulerAngles = new Vector3(0, _kartModel.transform.eulerAngles.y + _maxRotateVelocity / 50 * Input.GetAxis("Horizontal"), 0);
            }
        }
    }
}