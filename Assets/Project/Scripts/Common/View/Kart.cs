using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Kart : MonoBehaviour
{
    [SerializeField] private float maxPower;
    [SerializeField] private float angle;
    [SerializeField] private float breake;
    [SerializeField] private WheelCollider wcFR, wcFL, wcRR, wcRL;

    private void Start()
    {
        CancellationToken ct = this.GetCancellationTokenOnDestroy();
        Move(ct).Forget();
        Rotate(ct).Forget();
        Breake(ct).Forget();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private async UniTaskVoid Move(CancellationToken ct)
    {
        while (true)
        {
            await UniTask.WaitUntil(() => Input.GetAxis("Vertical") != 0, cancellationToken: ct);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
            float power = maxPower * Input.GetAxis("Vertical");
            wcFR.motorTorque = power;
            wcFL.motorTorque = power;
        }
    }

    private async UniTaskVoid Rotate(CancellationToken ct)
    {
        while (true)
        {
            await UniTask.WaitUntil(() => Input.GetAxis("Horizontal") != 0, cancellationToken: ct);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
            float steering = angle * Input.GetAxis("Horizontal");
            wcFR.steerAngle = steering;
            wcFL.steerAngle = steering;
        }
    }

    private async UniTaskVoid Breake(CancellationToken ct)
    {
        while (true)
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, ct);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                wcFL.brakeTorque = breake;
                wcFR.brakeTorque = breake;
                wcRL.brakeTorque = breake;
                wcRR.brakeTorque = breake;
            }
            else
            {
                wcFL.brakeTorque = 0;
                wcFR.brakeTorque = 0;
                wcRL.brakeTorque = 0;
                wcRR.brakeTorque = 0;
            }
        }
    }
}