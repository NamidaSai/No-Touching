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
    [SerializeField] GameObject shootVFX = default;
    [SerializeField] float shootFXDuration = 0.5f;

    public IEnumerator Shoot()
    {
        while (true)
        {
            CreateBullet();
            CreateVFX();
            PlaySFX();
            GetComponent<Animator>().SetTrigger("Shoot");
            yield return new WaitForSeconds(shootCooldown);
        }
    }

    private void CreateBullet()
    {
        GameObject newBullet = CreateObject(bulletPrefab);
        AdjustBulletBehaviour(newBullet);
        Destroy(newBullet, bulletDecay);
    }

    private void CreateVFX()
    {
        GameObject newShootFX = CreateObject(shootVFX);
        newShootFX.transform.parent = transform;
        Destroy(newShootFX, shootFXDuration);
    }

    private void PlaySFX()
    {
        FindObjectOfType<AudioManager>().Play("playerShoot");
    }

    private GameObject CreateObject(GameObject prefab)
    {
        GameObject bullet = Instantiate(prefab, hand.position, transform.rotation) as GameObject;
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
