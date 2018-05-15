using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public float health = 150f;
    public float projectileSpeed = 10f;
    public float shotsPerSecond = 0.5f;
    public GameObject projectile;
    Animator anim;
    int arrivalState = Animator.StringToHash("arrivalState");

    private void Awake()
    {
        int rand = Random.Range(0, 2);
        anim = GetComponent<Animator>();
        anim.SetInteger("arrivalState", rand);

        Debug.Log(anim.GetInteger("arrivalState"));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity);
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }
    }
}
