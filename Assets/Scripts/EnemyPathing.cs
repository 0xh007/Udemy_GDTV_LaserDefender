using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
        #region Configuration Parameters

        [SerializeField]
        private List<Transform> waypoints;

        [SerializeField]
        private float moveSpeed = 2f;

        #endregion

        #region State
        
        private int waypointIndex = 0;

        #endregion

        #region Cached References

        #endregion

        #region Event Methods 

        private void Start()
        {
            transform.position = waypoints[waypointIndex].transform.position;
        }

        private void Update()
        {
            MoveToWaypoint();
        }

        private void MoveToWaypoint()
        {
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var movementThisFrame = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
}
