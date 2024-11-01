using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        int force = Random.Range(10, 20);
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);

        int index = Random.Range(1, 3);

        if(index == 1)
        {
            rb.AddForce(-transform.right * force/5, ForceMode2D.Impulse);
        }
        else if(index == 2)
        {
            rb.AddForce(transform.right * force/5, ForceMode2D.Impulse);
        }
     
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
