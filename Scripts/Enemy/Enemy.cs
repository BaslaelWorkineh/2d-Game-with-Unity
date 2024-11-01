using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public EnemyData data;


    [Header("CheckSurroundings")]

    private int facingDirection = 1;
    [SerializeField]Transform checkWallPos, checkPlayerInMinPos, checkPlayerInMaxPos, bombSpawnPos;
    bool isPlayerNear, isWallDetected, isGroundDetected, isPlayerInBack;

    private Vector2 velocityWorkSpace;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // SetVelocity(data.runSpeed);
        CheckSurroundings();
    }

    void CheckSurroundings()
    {
        isWallDetected = Physics2D.Raycast(checkWallPos.position, transform.right, data.checkWallDistance, data.whatIsGround);
        isPlayerNear = Physics2D.Raycast(checkPlayerInMinPos.position, transform.right, data.checkPlayerInMinDistance, data.whatIsPlayer);
        isPlayerInBack = Physics2D.Raycast(checkPlayerInMinPos.position, -transform.right, data.checkPlayerInMinDistance / 2, data.whatIsPlayer);

        if (isWallDetected == true)
        {
            animator.SetBool("idle", true);
            
        }
        else if(isWallDetected == false)
        {
            animator.SetBool("idle", false);
        }


        if (isPlayerNear)
        {
            animator.SetBool("playerDetected", true);
        }
        else if (!isPlayerNear)
        {
            animator.SetBool("playerDetected", false);
        }

        if (isPlayerInBack)
        {
            animator.SetBool("playerInBack", true);
        }
        else if (!isPlayerInBack)
        {
            animator.SetBool("playerInBack", false);
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    public void SpawnBomb()
    {
        Instantiate(data.bombPrefab, bombSpawnPos.position, bombSpawnPos.rotation);
    }

    protected virtual void OnDrawGizmos()
    {
        //Gizmos.DrawRay(checkGroundPos.position, Vector2.down);
        Gizmos.DrawRay(checkWallPos.position, transform.right * data.checkWallDistance);

        //check player in max
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(checkPlayerInMinPos.position, -transform.right * data.checkPlayerInMinDistance/2);
        //Gizmos.DrawLine(transform.position, checkPlayerDistMax.position);

        //check player in min
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkPlayerInMinPos.position, transform.right * data.checkPlayerInMinDistance);
        //Gizmos.DrawLine(transform.position, checkPlayerDistMin.position);

        //attack range
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPosition.position, enemyData.attackRange);

        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, checkPlayerInBackPos.position);

    }

}
