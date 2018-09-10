using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Transform target;
    private int pathPointIndex = 0;

    void Start()
    {
        target = PathPoints.Instance.GetPointPosition(pathPointIndex);
    }

    void Update()
    {
        Vector3 direction = GetTargetDirection();
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        float distance = GetTargetDistance();
        if (distance < 0.1f)
        {
            GetNextPoint();
        }
    }

    public void SetEnemySpeed(int _speed)
    {
        speed = _speed;
    }

    private void GetNextPoint()
    {
        if (PathPoints.Instance.GetPointPosition(++pathPointIndex) != null)
        {
            target = PathPoints.Instance.GetPointPosition(pathPointIndex);
        }
        else
        {
            print("Enemy Reached the End point");
            Destroy(gameObject);
        }

    }

    private float GetTargetDistance()
    {
        return Vector3.Distance(transform.localPosition, target.localPosition);
    }

    private Vector3 GetTargetDirection()
    {
        return target.localPosition - transform.localPosition;
    }
}