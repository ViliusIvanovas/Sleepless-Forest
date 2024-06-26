using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform Player;

    public float BulletForce = 20f;
    public float bulletDamage;
    public EnemyStats enemyStats;
    bool shootingCooldown;
    public float shootingCooldownDuration = 0.5f;

    private void Start()
    {
        shootingCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position;

        if (Input.GetButtonDown("Fire1"))
        {
            if (shootingCooldown == false)
            {
                Shooting();
            }
        }
    }

    void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Boullet script = bullet.GetComponent<Boullet>();
        script.Weapon = this.gameObject;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * BulletForce, ForceMode2D.Impulse);
        StartCoroutine(ShootingCooldown());
    }

    private IEnumerator ShootingCooldown()
    {
        shootingCooldown = true;
        yield return new WaitForSeconds(shootingCooldownDuration);
        shootingCooldown = false;

        
    }
}
