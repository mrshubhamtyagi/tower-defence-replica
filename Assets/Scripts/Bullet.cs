using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject hitParticles;

    private Transform target;

    public void Chase(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 _direction = target.position - transform.position;
        transform.Translate(_direction.normalized * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject hitparticle = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(hitparticle, 2f);
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
}
