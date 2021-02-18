using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float damage = 50f;

    private void Start()
    {
        FindObjectOfType<LevelController>().AddTrapToList(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null)
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
