using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float range = 3f;
    public Color gizmoColor = Color.yellow;

    public Transform target;

    private float shortestDistance;
    private GameObject nearestEnemy = null;

    private void Start()
    {
        shortestDistance = range * 2;
        InvokeRepeating("GetNearestTarget", 0f, 0.1f);
    }

    void GetNearestTarget()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys)
        {
            // get the distance to enemy
            float _distance = Vector3.Distance(transform.localPosition, enemy.transform.localPosition);

            // compare and update the shortest distance
            if (_distance < shortestDistance)
            {
                shortestDistance = _distance;
                nearestEnemy = enemy; // nearest enemy updated
                print("Target Updated");
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    private void Update()
    {
        if (target == null)
            return;
        else
            FollowTarget(target);
    }

    private void FollowTarget(Transform _target)
    {
        // get direaction of the enemy
        Vector3 _direction = _target.transform.localPosition - transform.localPosition;
        // get the rotation from the enemy direction
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        // convert the rotation(Quaternion) to Vector3 to only use the Y-Axis
        Vector3 _rotation = _lookRotation.eulerAngles;
        // set the transform rotation (Y-axis only)
        transform.rotation = Quaternion.Euler(0, _rotation.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.localPosition, range);

    }
}
