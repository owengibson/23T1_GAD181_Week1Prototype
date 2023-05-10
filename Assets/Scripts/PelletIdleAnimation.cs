using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSTD
{
    public class PelletIdleAnimation : MonoBehaviour
    {
        public float rotSpeed = 40;
        private float minimum = 0.4f;

        private float yPos = 0.5f;
        [SerializeField] private float bounceSpeed = 2f;

        void Update()
        {
            yPos = Mathf.Abs(Mathf.Sin(Time.time * bounceSpeed)) * 0.65f;
            transform.position = new Vector3(transform.position.x, yPos + minimum, transform.position.z);

            //Rotate
            transform.Rotate(Vector3.up, Time.deltaTime * rotSpeed);

        }
    }
}