using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class WallCollision : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Rigidbody player;
        [SerializeField] private float bounceStrength = 2f;

        private void OnCollisionEnter(Collision collision)
        {
            audioManager.Play("WallCollision");

            //Vector3 forceDirection = -player.velocity.normalized * bounceStrength;
            //Debug.Log(forceDirection);
            //player.AddForce(new Vector3(100, 0, 100));

            //switch (gameObject.name)
            //{
            //    case "TL":
            //        player.AddForce()
            //        break;
            //    case "TR":

            //        break;
            //    case "BL":

            //        break;
            //    case "BR":

            //        break;
            //    default:
            //        break;



            //}
        }
    }
}