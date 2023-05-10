using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


namespace TSTD
{
    [RequireComponent(typeof(Rigidbody))]
    public class TiltControl : MonoBehaviour
    {
        [SerializeField] private Vector3 acceleration = Vector3.zero;
        [SerializeField] private float moveSpeed;
        
        private Rigidbody _rigidbody;

        private void Start()
        {
            Input.gyro.enabled = true;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            acceleration.x = Input.acceleration.x;
            acceleration.z = Input.acceleration.y;
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(acceleration * moveSpeed);
        }
    }
}