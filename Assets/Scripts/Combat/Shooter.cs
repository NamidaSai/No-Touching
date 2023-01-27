using System;
using Audio;
using UnityEngine;

namespace Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] public float shootCooldown = 2f;
        [SerializeField] GameObject bulletPrefab = default;
        [SerializeField] Transform hand = default;
        [SerializeField] float bulletSpeed = 10f;
        [SerializeField] float bulletDecay = 0.2f;
        [SerializeField] GameObject shootVFX = default;
        [SerializeField] float shootFXDuration = 0.5f;
        private Animator animator;
        private AudioManager audioManager;
        
        public bool isShooting = false;
        private float timeSinceLastShot;

        private void Start()
        {
            animator = GetComponent<Animator>();
            audioManager = FindObjectOfType<AudioManager>();
            timeSinceLastShot = shootCooldown;
        }

        private void Update()
        {
            TryShooting();
        }

        private void TryShooting()
        {
            if (timeSinceLastShot < shootCooldown)
            {
                timeSinceLastShot += Time.deltaTime;
                return;
            }

            if (!isShooting) { return; }
            
            Shoot();
            timeSinceLastShot = 0f;
        }

        public void Shoot()
        {
            CreateBullet();
            CreateVFX();
            PlaySFX();
            animator.SetTrigger("Shoot");
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
            audioManager.Play("playerShoot");
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
}