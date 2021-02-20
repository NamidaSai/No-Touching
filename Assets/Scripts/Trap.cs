using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] int damage = 50;
    [SerializeField] float trapHideAnimTime = 1f;

    private void Start()
    {
        FindObjectOfType<TrapManager>().AddTrapToList(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null)
        {
            other.GetComponent<Health>().TakeDamage(damage);
            GetComponent<Animator>().SetTrigger("Kill");
        }
    }

    public IEnumerator HideTrap()
    {
        GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(trapHideAnimTime);
        gameObject.SetActive(false);
    }
}
