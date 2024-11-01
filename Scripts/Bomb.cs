using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject bombVFX;
    [SerializeField] float upForce;
    Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.AddForce(transform.up * upForce/2, ForceMode2D.Impulse);
        rb.AddForce(-transform.right * upForce/4, ForceMode2D.Impulse);
        Invoke("Explode", 2.5f);
    }

   void Explode()
    {
        Instantiate(bombVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
