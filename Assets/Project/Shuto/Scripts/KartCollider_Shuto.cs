#nullable enable

using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Common.View
{
    public class KartCollider_Shuto : MonoBehaviour
    {
        [SerializeField] private Transform _kartModel = null!;
        [SerializeField] private float _moveAcceleration = 2.78f; // 10 * 1000 / 3600
        [SerializeField] private float _maxVelocity = 13.9f; // 50 * 1000 / 3600
        [SerializeField] private float _maxRotateVelocity = 60;
        [SerializeField] private float _abradeAcceleration = .556f; // 2 * 1000 / 3600

        private Rigidbody _rb = null!;
        private int _driftDirection;
        private float _driftPower;
        private int _driftMode = 0;

        private bool _isBoosting;
        private bool _isAboveGround;
        private bool _isDrifting;
        [SerializeField] private bool _isPC;

        private RaycastHit _hit;

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
            // �h���t�g�̊J�n����
            if (Input.GetKeyDown(KeyCode.Space) && !_isDrifting && Input.GetAxis("Horizontal") != 0)
            {
                _isDrifting = true;
                _driftDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;

                _kartModel.DOComplete();
                _kartModel.DOPunchPosition(_kartModel.transform.up * .2f, .3f, 5, 1f)
                    .OnUpdate(() => _kartModel.transform.position = new Vector3(transform.position.x, _kartModel.transform.position.y, transform.position.z));
                //_kartModel.DOJump(_kartModel.transform.position, .2f, 1, .3f);
                Debug.Log(1);
            }

            // �h���t�g�̏I������
            if (Input.GetKeyUp(KeyCode.Space) && _isDrifting)
            {
                Boost();
                Debug.Log(2);
            }
        }

        private void FixedUpdate()
        {
            _kartModel.transform.position = transform.position;
            //Debug.Log(_isAboveGround);

            // �n�ʔ���
            Ray ray = new Ray(transform.position, -Vector3.up);
            Physics.Raycast(ray, out _hit, 0.6f);

            _isAboveGround = _hit.collider?.gameObject.tag.StartsWith("Ground") ?? false;

            Abrade();

            if (_isDrifting)
            {
                float control = _driftDirection == 1 ? Utils.Remap(Input.GetAxis("Horizontal"), -1, 1, 0, 2) : Utils.Remap(Input.GetAxis("Horizontal"), -1, 1, 2, 0);
                float powerControl = _driftDirection == 1 ? Utils.Remap(Input.GetAxis("Horizontal"), -1, 1, .2f, 1) : Utils.Remap(Input.GetAxis("Horizontal"), -1, 1, 1, .2f);
                _kartModel.transform.eulerAngles = new Vector3(0, _kartModel.transform.eulerAngles.y + _maxRotateVelocity / 50 * _driftDirection * control, 0);
                _driftPower += powerControl;

                ColorDrift();
            }
        }

        private void Abrade()
        {
            if (!_hit.collider)
            {
                return;
            }
            string tag = _hit.collider.gameObject.tag;

            if (tag == "GroundRoad" && !_isBoosting)
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
            if (tag == "GroundOffroad")
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

        private void ColorDrift()
        {
            if (_driftPower > 50 && _driftPower < 99 && _driftMode < 1)
            {
                _driftMode = 1;
            }

            if (_driftPower > 100 && _driftPower < 149 && _driftMode < 2)
            {
                _driftMode = 2;
            }

            if (_driftPower > 150 && _driftMode < 3)
            {
                _driftMode = 3;
            }
        }

        private void Boost()
        {
            _isDrifting = false;

            if (_driftMode > 0)
            {
                _isBoosting = true;
                DOVirtual.Float(_maxVelocity * 3, _maxVelocity, 1.5f * _driftMode, value => _rb.velocity = _kartModel.transform.forward * value).OnComplete(() => _isBoosting = false);
            }

            _driftPower = 0;
            _driftMode = 0;
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