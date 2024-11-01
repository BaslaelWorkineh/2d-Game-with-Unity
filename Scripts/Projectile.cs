using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject bloodVFX;
    [SerializeField] float force;
    [SerializeField] float life = 3f;
    Transform crossHair;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        crossHair = GameObject.Find("CrossHair").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.right * force, ForceMode2D.Impulse);
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Breakables"))
        //{
         //   collision.GetComponent<Breakables>().Break();
          //  Instantiate(hitVFX, transform.position, transform.rotation);
           // Destroy(gameObject);
           // return;
        //}

        if (collision.CompareTag("Enemy"))

        {
            var player = FindObjectOfType<PlayerMovement>();
            collision.GetComponent<EnemyHealth>().TakeDamage(player.gameObject.transform.position.x);
            Instantiate(bloodVFX, transform.position, transform.rotation);
            Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }
        Instantiate(hitVFX, transform.position, transform.rotation);
    }

   

}
