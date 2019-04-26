﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Transform target;
	public float speed = 70f;
	public float explosionRadius = 0f;
	public float damage = 20;

	[Header("Bullet Effects")]
	public GameObject impactEffect;
	public float destroyEffectAfterTime = 2f;

	public void Seek(Transform _target)
	{
		target = _target;
	}


    // Update is called once per frame
    void Update()
    {
        if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
    }

	void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damage(target);
		}

		Destroy(effectIns, destroyEffectAfterTime);
		Destroy(gameObject);

	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				Damage(collider.transform);
			}
		}
	}

	void Damage(Transform _enemy)
	{
		MonsterController enemy = _enemy.GetComponent<MonsterController>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}