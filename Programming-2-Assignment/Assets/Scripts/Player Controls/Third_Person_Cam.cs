using System;
using UnityEngine;

public class Third_Person_Cam : MonoBehaviour
{
        [Header("Refs")]
        public Transform orientation;
        public Transform player;
        public Rigidbody rb;


        private void Update()
        {
                Vector3 view_Direction = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
                orientation.forward = view_Direction.normalized;
                
                player.forward = Vector3.Slerp(player.forward, view_Direction.normalized, Time.deltaTime * 50);
        }
}
