using System;
using System.Collections;
using Events;
using UnityEngine;

namespace Lights
{
    public class EnemyLightController : MonoBehaviour
    {
        [SerializeField] private float speedMovement;
        [SerializeField] private float distanceToBeDestroyed;
        [SerializeField] private float distanceToHuntPlayer;
        [SerializeField] private int life;
        private Rigidbody _rigidbody;
        private LightPlayerController player;
        private bool move;
        

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            player = FindObjectOfType<LightPlayerController>();
            move = true;
            DestroyMe();
            GameEvents.instance.OnUserCollisionWithMessage += DestoyController;
        }

        private void DestoyController(string obj)
        {
            Destroy(this);
        }

        private void DestroyMe()
        {
            if (GetDistanceToPlayer() < distanceToBeDestroyed)
            {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            if (GetDistanceToPlayer() < distanceToHuntPlayer && move)
            {
                Attack();
            }
        }

        private float GetDistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }


        private void Attack()
        {
            var playerDirection = GetPlayerDirection();
            transform.forward = playerDirection;
            _rigidbody.velocity = playerDirection * speedMovement;
        }

        private Vector3 GetPlayerDirection()
        {
            var playerDirection = (player.transform.position - transform.position).normalized;
            return playerDirection;
        }

        public void MakeDamage(float damage)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(-GetPlayerDirection() * damage, ForceMode.Impulse);
            StartCoroutine(StopMoving());
            RemoveLife();
        }

        private IEnumerator StopMoving()
        {
            move = false;
            yield return new WaitForSeconds(2);
            move = true;
        }

        private void RemoveLife()
        {
            life -= 1;
            if(life <= 0) Destroy(transform.gameObject);
        }
    }
}