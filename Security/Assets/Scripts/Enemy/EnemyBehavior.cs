using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class EnemyBehavior : MonoBehaviour
{
    public static EnemyBehavior instance;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private Transform transformPlayer;

    [SerializeField] private Transform sightPosition;
    [SerializeField] private LayerMask playerOnSightCheck;
    [SerializeField] private Transform sightArea;

    [SerializeField] private float sightHorizontalSize = 3f;
    [SerializeField] private float sightVerticalSize = 1.3f;
    [SerializeField] private float velocity = 10f;
    [SerializeField] private Transform[] spawnPoints;

    private float playerSpotted = 0f;
    private float currentVelocity;
    private bool isWalking;

    private int isEnemyWalkingAnimatorHash;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        #endregion

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transformPlayer = GetComponent<Transform>();

        GetAnimatorParamentersHash();
    }

    private void Start()
    {
        RandomSpeed();
    }

    private void Update()
    {
        EnemyMove();
        SightCheck();   
        EnemyAnimation();
    }

    private void EnemyMove()
    {
        rigidBody.velocity = new Vector2(1 * currentVelocity, rigidBody.velocity.y);
    }

    public bool PlayerOnSight()
    {
        bool playerIsOnSight = Physics2D.OverlapBox(sightPosition.position, new Vector2(sightHorizontalSize, sightVerticalSize), 0  ,playerOnSightCheck);
        return playerIsOnSight;
    }

    private void SightCheck()
    {
        if (PlayerOnSight())
        {
            currentVelocity = playerSpotted;
            isWalking = false;
        }
        else if (!PlayerOnSight())
        {
            currentVelocity = velocity;
            isWalking = true;
        }
    }

    private void EnemyAnimation()
    {
        if (isWalking && animator.GetBool(isEnemyWalkingAnimatorHash) == false)
        {
            animator.SetBool(isEnemyWalkingAnimatorHash, true);
        }
        else if (!isWalking && animator.GetBool(isEnemyWalkingAnimatorHash) == true)
        {
            animator.SetBool(isEnemyWalkingAnimatorHash, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform spawnPoint;

        if (collision.collider.tag == "EndRoute" || collision.collider.tag == "Enemy")
        {
            int randoSpawn = Random.Range(0, spawnPoints.Count());
            spawnPoint = spawnPoints[randoSpawn];
            transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
            RandomSpeed();
        }
    }

    private void RandomSpeed()
    {
        int randoSpeed = Random.Range(5, 10);
        velocity = randoSpeed;
    }

    private void GetAnimatorParamentersHash()
    {
        isEnemyWalkingAnimatorHash = Animator.StringToHash("isEnemyWalking");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sightPosition.position, new Vector3(sightHorizontalSize, sightVerticalSize));
    }
}
