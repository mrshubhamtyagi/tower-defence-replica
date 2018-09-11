using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    public Transform target;

    private void Shoot(Transform _target)
    {
        Vector3.MoveTowards(transform.localPosition, _target.localPosition, speed * Time.deltaTime);
    }

    private void Update()
    {
        if (target == null)
            return;

        Shoot(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
