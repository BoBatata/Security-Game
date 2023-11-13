using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]


public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior instance;

    public InputManager inputManager;

    private Animator animator;
    private Rigidbody2D rigidBody;

    #region Movement Variables
    private Vector2 moveDirection;
    private bool isMoving;
    #endregion

    #region Sight Variables
    private bool playerIsOnSight;
    private float guardAwareness;
    #endregion

    private int isWalkingAnimatorHash;

    #region Serialize variables
    [SerializeField] private float velocity = 5f;
    [SerializeField] private float levelAwarenessUP = 0.2f;
    [SerializeField] private float levelAwarenessDown = 0.1f;
    #endregion

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        inputManager = new InputManager();

        GetAnimatorParamentersHash();

    }

    private void Update()
    {
        playerIsOnSight = EnemyBehavior.instance.PlayerOnSight();
        isOnSight(playerIsOnSight);
        Movement();
        PlayerAnimation();
    }

    private void Movement()
    {
        if (moveDirection.x < 0)
        {
            transform.rotation = new Quaternion(0, -180, 0, 0);
        }
        else if (moveDirection.x > 0)
        {
            transform.rotation = quaternion.identity;
        }

        moveDirection.x = inputManager.Movement.HorizontalWalk.ReadValue<float>();
        moveDirection.y = inputManager.Movement.VerticalWalk.ReadValue<float>();
        rigidBody.velocity = new Vector2(moveDirection.x * velocity, moveDirection.y * velocity);
        isMoving = moveDirection.x != 0;
    }

    public void isOnSight(bool OnSight)
    {
        if (OnSight == true)
        {
            Debug.Log("Estou te vendo");
            guardAwareness += Time.deltaTime * levelAwarenessUP / 2;
        }
        else if (OnSight == false && guardAwareness != 0)
        {
            Debug.Log("Sumiu");
            guardAwareness -= Time.deltaTime * levelAwarenessDown / 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            GameManager.instance.IncreaseScore(1);
            CoinSpawn.instance.CoinTake();
            Destroy(collision.collider.gameObject);
        }
    }

    private void PlayerAnimation()
    {
        if (isMoving && animator.GetBool(isWalkingAnimatorHash) == false)
        {
            animator.SetBool(isWalkingAnimatorHash, true);
        }
        else if (!isMoving && animator.GetBool(isWalkingAnimatorHash) == true)
        {
            animator.SetBool(isWalkingAnimatorHash, false);
        }
    }

    private void GetAnimatorParamentersHash()
    {
        isWalkingAnimatorHash = Animator.StringToHash("isWalking");
    }

    public float GetGuardAwareness()
    {
        return guardAwareness;
    }

    private void OnEnable()
    {
        inputManager.Enable();
    }

    private void OnDisable()
    {
        inputManager.Disable();
    }


}
