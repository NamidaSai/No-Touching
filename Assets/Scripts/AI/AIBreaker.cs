using System.Collections;
using Audio;
using Combat;
using UnityEngine;

namespace AI
{
    public class AIBreaker : MonoBehaviour
    {
        [SerializeField] float stunDuration = 2f;
        [SerializeField] GameObject stunnedFXPrefab = default;
        [SerializeField] float stunFXDuration = 2f;
        [SerializeField] float stunMoveForce = 1f;
        [SerializeField] float maxBreakSpeed = 10f;
        [SerializeField] float maxBreakForce = 10f;
        [SerializeField] LayerMask targetLayers = default;

        public bool isStunned = false;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
            {
                GetComponent<Health>().TriggerHitFX();
                BreakAllJoints();
                PlaySFX("enemyHit");
            }
        }

        private void PlaySFX(string soundName)
        {
            FindObjectOfType<AudioManager>().Play(soundName);
        }

        private void AddBreakingForce(Vector2 jointPosSum, Vector2 collisionPos)
        {
            Vector2 directionFromOther = (jointPosSum - (Vector2)transform.position).normalized;
            Vector2 directionFromCollision = (collisionPos - (Vector2)transform.position).normalized;
            Vector2 sumDirection = directionFromOther + directionFromCollision;

            Vector2 steer = sumDirection - GetComponent<Rigidbody2D>().velocity;
            steer *= maxBreakSpeed;
            steer = Vector2.ClampMagnitude(steer, maxBreakForce);
            StartCoroutine(BeStunned());
            GetComponent<Rigidbody2D>().AddForce(steer);
        }

        public void BreakAllJoints()
        {
            Vector2 sumDirection = new Vector2();
            int directionCount = 0;

            FixedJoint2D[] joints = GetComponents<FixedJoint2D>();

            if (joints.Length == 0) { return; }

            foreach (FixedJoint2D joint in joints)
            {
                if (joint.connectedBody == null)
                {
                    Destroy(joint);
                    continue;
                }

                float distance = Vector2.Distance(transform.position, joint.connectedBody.gameObject.transform.position);

                if (distance > 0)
                {
                    Vector2 direction = (transform.position - joint.connectedBody.gameObject.transform.position).normalized;
                    Vector2 weightedDirection = direction / distance;
                    sumDirection += weightedDirection;
                    directionCount++;
                }

                joint.GetComponent<AIBreaker>().StartCoroutine(BeStunned());
                Destroy(joint);
            }

            if (directionCount > 0)
            {
                sumDirection /= directionCount;
                sumDirection *= maxBreakSpeed;
                Vector2 steer = sumDirection - GetComponent<Rigidbody2D>().velocity;
                steer = Vector2.ClampMagnitude(steer, maxBreakForce);
                GetComponent<Rigidbody2D>().AddForce(steer);
            }
        }

        public IEnumerator BeStunned()
        {
            isStunned = true;
            GetComponent<AIMover>().SetMaxForce(stunMoveForce);
            TriggerStunnedFX();
            GetComponent<Animator>().SetBool("isMerged", false);

            yield return new WaitForSeconds(stunDuration);

            isStunned = false;
            GetComponent<AIMover>().ResetMoveSpeed();
            GetComponent<AIMerge>().isMerged = false;
        }

        public void TriggerStunnedFX()
        {
            if (stunnedFXPrefab == null) { return; }

            GameObject stunnedFX = Instantiate(stunnedFXPrefab, transform.position, transform.rotation) as GameObject;
            stunnedFX.transform.parent = transform;
            stunnedFX.transform.localScale = new Vector3(1, 1, 1);
            Destroy(stunnedFX, stunFXDuration);
        }
    }
}