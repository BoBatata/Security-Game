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

    private InputManager inputManager;

    private Animator animator;
    private Rigidbody2D rigidBody;

    private Vector2 moveDirection;
    private bool playerIsOnSight;
    private float guardAwareness;

    [SerializeField] private float velocity = 10f;
    [SerializeField] private float levelAwarenessUP = 0.2f;
    [SerializeField] private float levelAwarenessDown = 0.1f;

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
    }

    private void Update()
    {
        playerIsOnSight = EnemyBehavior.instance.PlayerOnSight();
        isOnSight(playerIsOnSight);
        Movement();
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
    }

    public void isOnSight(bool OnSight) 
    {
        if (OnSight == true)
        {
            Debug.Log("Estou te vendo");
            guardAwareness += Time.deltaTime * levelAwarenessUP;
        }
        else
        {
            Debug.Log("Sumiu");
            guardAwareness -= Time.deltaTime * levelAwarenessDown;
        }
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
