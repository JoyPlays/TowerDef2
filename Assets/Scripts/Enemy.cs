using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public float startHealth = 100;
	private float health;
	public int moneyValue = 10;

	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;
	public bool slowEffectOn = false;

	[Header("Effects")]
	public GameObject deathEffect;

	

	[Header("Unity Stuff")]
	public Transform healthCanvas;
	public Image healthBar;

	// Start is called before the first frame update
	void Start()
	{
		speed = startSpeed;
		health = startHealth;
	}

	public void TakeDamage(float amount)
	{
		health -= amount;


		healthBar.fillAmount = health / startHealth;

		if (health <= 0)
		{
			Die();
		}
	}

	public void ActiveSlow(float percentage)
	{
		speed = startSpeed * (1f - percentage);
	}

	public void PassiveSlow(float percentage)
	{
		if (!slowEffectOn)
		{
			StartCoroutine("PassiveSlowEffect", percentage);
		}
	}

	IEnumerator PassiveSlowEffect(float percentage)
	{
		slowEffectOn = true;
		speed = startSpeed * (1f - percentage);
		yield return new WaitForSeconds(3f);
		slowEffectOn = false;
	}

	void Die()
	{
		PlayerStats.Money += moneyValue;
		SpawnMonsters.EnemyCount--;

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
		Destroy(effect, 5f);
	}

	private void Update()
	{
		healthCanvas.rotation = Quaternion.Euler(90f, 0f, 0f);
	}
}
