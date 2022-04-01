using System;
using System.Configuration;
using UnityEngine;

namespace Camera
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Vector3 offset;

        [SerializeField] float lerpRange;
        private void LateUpdate()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.Lerp(transform.position, playerPosition.position, lerpRange) + offset;
            transform.LookAt(playerPosition);
        }
    }
}
