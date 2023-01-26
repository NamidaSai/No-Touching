using Audio;
using UnityEngine;

namespace AI
{
    public class AIMerge : MonoBehaviour
    {
        [SerializeField] LayerMask targetLayer = default;

        public bool isMerged = false;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
            {
                if (other.gameObject.GetComponent<AIMerge>() == null) { return; }
                isMerged = true;
                CreateJoint(other.gameObject);
            }
            else if (other.gameObject.tag == "Wall")
            {
                PlaySFX("enemyWall");
            }
        }

        private void CreateJoint(GameObject other)
        {
            bool hasJoint = GetComponent<FixedJoint2D>() != null;

            if (hasJoint)
            {
                bool jointToOther = GetComponent<FixedJoint2D>().connectedBody == other.GetComponent<Rigidbody2D>();

                if (jointToOther) { return; }
            }


            bool otherHasJoint = other.GetComponent<FixedJoint2D>() != null;

            if (otherHasJoint)
            {
                bool otherIsAlreadyJoint = other.GetComponent<FixedJoint2D>().connectedBody == GetComponent<Rigidbody2D>();

                if (otherIsAlreadyJoint) { return; }
            }

            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
            GetComponent<Animator>().SetBool("isMerged", true);
            other.GetComponent<Animator>().SetBool("isMerged", true);

            PlaySFX("enemyMerge");
        }

        private void PlaySFX(string soundName)
        {
            FindObjectOfType<AudioManager>().Play(soundName);
        }
    }
}