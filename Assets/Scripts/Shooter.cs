using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float shootCooldown = 2f;
    [SerializeField] GameObject bulletPrefab = default;
    [SerializeField] Transform hand = default;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletDecay = 0.2f;

    public IEnumerator Shoot()
    {
        while (true)
        {
            GameObject newBullet = CreateBullet();
            AdjustBulletBehaviour(newBullet);
            Destroy(newBullet, bulletDecay);
            yield return new WaitForSeconds(shootCooldown);
        }
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, hand.position, transform.rotation) as GameObject;
        return bullet;
    }

    private void AdjustBulletBehaviour(GameObject bullet)
    {
        Vector2 bulletVelocity = new Vector2();
        Vector2 direction = transform.up;
        bulletVelocity.x = bulletSpeed * direction.x;
        bulletVelocity.y = bulletSpeed * direction.y;

        bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
    }
}
