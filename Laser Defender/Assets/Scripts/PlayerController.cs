using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float minX, maxX;
    public float speed;
    public float padding = 1f;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health;

    // Use this for initialization

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;
    }

    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
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

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00000001f, firingRate);

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        //restrict player to gamespace
        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

}
