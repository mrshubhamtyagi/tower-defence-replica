using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public Transform bulletPrefab;
    public Transform bulletPosition;
    public float rotationSpeed = 5f;
    public float range = 3f;
    public Color gizmoColor = Color.yellow;
    public float fireRate = 0.5f;

    private Transform target;
    private float fireCountdown = 0f;

    private void Start()
    {
        InvokeRepeating("GetNearestTarget", 0f, 0.1f);
    }

    void GetNearestTarget()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
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

        FollowTarget(target);

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Transform bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Chase(target);
    }

    private void FollowTarget(Transform _target)
    {
        // get direaction of the enemy
        Vector3 _direction = _target.transform.localPosition - transform.localPosition;
        // get the rotation from the enemy direction
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        // convert the rotation(Quaternion) to Vector3 to only use the Y-Axis
        Vector3 _rotation = Quaternion.Lerp(transform.rotation, _lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        // set the transform rotation (Y-axis only)
        transform.rotation = Quaternion.Euler(0, _rotation.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.localPosition, range);

    }
}
