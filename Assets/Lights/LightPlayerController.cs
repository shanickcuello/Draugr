using System;
using System.Collections;
using System.Configuration;
using UnityEngine;

namespace Lights
{
    public class LightPlayerController : MonoBehaviour
    {
        [SerializeField] private float speedMovement;
        [SerializeField] private float rangeAttack;
        [SerializeField] private float damage;
        [SerializeField] private Light light;
        private float normalLightIntensity;
        private Rigidbody _rigidbody;
        public bool canAttack;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            canAttack = true;
            normalLightIntensity = light.intensity;
        }

        private void Update()
        {
            MoveByInputs();
            AttackByInputs();
        }

        private void AttackByInputs()
        {
            if (!Input.GetKeyUp(KeyCode.Space)) return;
            if (canAttack)
            {
                light.intensity += 4;
                canAttack = false;
                var colliders = Physics.OverlapSphere(transform.position, rangeAttack);
                foreach (var collider in colliders)
                {
                    if (collider.GetComponent<EnemyLightController>())
                        collider.transform.GetComponent<EnemyLightController>().MakeDamage(damage);
                }
            }

            StartCoroutine(ResetLightSettings());
            StartCoroutine(Reload());
        }

        private IEnumerator ResetLightSettings()
        {
            yield return new WaitForSeconds(2);
            light.intensity = normalLightIntensity;
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(5);
            canAttack = true;
        }

        private void MoveByInputs()
        {
            var movement = GetVerticalAndHorizontalInputs();
            movement = NormalizeAndSetMovementSpeed(movement);
            MoveByRigidBody(movement);
        }

        private void MoveByRigidBody(Vector3 movement)
        {
            _rigidbody.velocity = movement;
        }

        private Vector3 NormalizeAndSetMovementSpeed(Vector3 movement)
        {
            movement = movement.normalized * (Time.deltaTime * speedMovement);
            return movement;
        }

        private static Vector3 GetVerticalAndHorizontalInputs()
        {
            var movement = new Vector3(Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical"));
            return movement;
        }
    }
}