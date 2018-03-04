using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFOV : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    [HideInInspector]
    public List<Transform> visiblePlayers = new List<Transform>();
    Transform trackPlayerPosition;

    void Awake()
    {
        StartCoroutine("FindTargetAfterDelay", 0.2f);
    }

    IEnumerator FindTargetAfterDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindPlayers();
        }
    }

    // Find players within the field of view that aren't obstructed by obstacles
    void FindPlayers()
    {
        visiblePlayers.Clear();
        // Add players to list if they are within the field of view circle
        Collider2D[] playersInCircle = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        // For every player within the field of view circle, check if they are actually within the field of view
        for (int i = 0; i < playersInCircle.Length; i++)
        {
            Transform target = playersInCircle[i].transform;
            trackPlayerPosition = target.Find("TrackPlayerPosition");
            Vector3 dirToTarget = (trackPlayerPosition.position - (transform.position)).normalized;
            if (Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                // If the raycast line is not obstructed, add players to list of visible players
                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstructionMask))
                {
                    visiblePlayers.Add(target);
                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        // Failsafe in case angles are wrong
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}