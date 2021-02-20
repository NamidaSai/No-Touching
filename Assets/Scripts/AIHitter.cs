using UnityEngine;

public class AIHitter : MonoBehaviour
{
    [SerializeField] float attackPushForce = 1000f;

    [SerializeField] int damage;

    AIStats stats;

    private void Awake()
    {
        stats = GetComponent<AIStats>();
    }

    private void Start()
    {
        damage = stats.GetDamage();
    }

    public int GetJointDamage()
    {
        int thisDamage = damage;

        if (GetComponent<FixedJoint2D>() != null)
        {
            FixedJoint2D[] joints = GetComponents<FixedJoint2D>();

            foreach (FixedJoint2D joint in joints)
            {
                if (joint.connectedBody == null) { continue; }

                thisDamage += joint.connectedBody.gameObject.GetComponent<AIHitter>().GetDamage();
            }
        }

        return thisDamage;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void HitStunned()
    {
        Vector2 playerPosition = FindObjectOfType<PlayerController>().transform.position;

        Vector2 direction = ((Vector2)transform.position - playerPosition).normalized;
        Vector2 pushForce = direction * attackPushForce;
        GetComponent<Rigidbody2D>().AddForce(pushForce);
        GetComponent<AIBreaker>().BreakAllJoints();
    }
}