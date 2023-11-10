using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public static EnemyBehavior instance;

    [SerializeField] private Transform sightPosition;
    [SerializeField] private LayerMask playerOnSightCheck;
    [SerializeField] private Transform sightArea;

    [SerializeField] private float sightHorizontalSize = 3f;
    [SerializeField] private float sightVerticalSize = 1.3f;

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
    }

    public bool PlayerOnSight()
    {
        bool playerIsOnSight = Physics2D.OverlapBox(sightPosition.position, new Vector2(sightHorizontalSize, sightVerticalSize), playerOnSightCheck);
        return playerIsOnSight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sightPosition.position, new Vector3(sightHorizontalSize, sightVerticalSize));
    }
}
