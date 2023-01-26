using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletScaleRate = 1f;
    private void Start()
    {
        Vector3 startScale = Vector3.zero;
        transform.localScale = startScale;
    }

    void Update()
    {
        float scale = transform.localScale.x + (Time.deltaTime * bulletScaleRate);
        Vector3 newScale = new Vector3(scale, scale, 1f);
        transform.localScale = newScale;
    }
}
