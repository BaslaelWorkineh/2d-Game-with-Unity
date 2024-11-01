using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public float moveSpeed;
    public float runSpeed;
    public float knockbackPower;
    public float checkWallDistance;
    public float checkPlayerInMinDistance;
    public LayerMask whatIsPlayer, whatIsGround;

    public GameObject bombPrefab;
    public GameObject coinPrefab;
}
