using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyData data;
    private float currentHealth;

    private int knockBackDir;

    private Rigidbody2D rb;
    private void Start()
    {
        currentHealth = data.maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(float direction)
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Instantiate(data.coinPrefab, transform.position, transform.rotation);
            Instantiate(data.coinPrefab, transform.position, transform.rotation);
            Instantiate(data.coinPrefab, transform.position, transform.rotation);
            Instantiate(data.coinPrefab, transform.position, transform.rotation);
            Instantiate(data.coinPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        TakeDirection(direction);
        GetComponent<Animator>().SetBool("hurt", true);
        
        
       
        

    }

    public void TakeDirection(float direction)
    {
        if (direction > transform.position.x)
        {
            knockBackDir = -1;
        }
        else
        {
            knockBackDir = 1;
        }
    }

    public void KnockBack()
    {
        rb.AddForce(Vector3.right.normalized * knockBackDir * data.knockbackPower);
        rb.AddForce(Vector3.up.normalized * data.knockbackPower);
    }

    public void HurtAnimFinish()
    {
        GetComponent<Animator>().SetBool("hurt", false);
    }
}
