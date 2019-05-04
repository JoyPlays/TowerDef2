using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
	public NavMeshAgent agent;
	public LevelSettings levelSettings;

	public float startHealth = 100;
	private float health;
	public int moneyValue = 10;

	[Header("Effects")]
	public GameObject deathEffect;

	private NavMeshPath path;
	private float elapsed = 0.0f;

	[Header("Unity Stuff")]
	public Transform healthCanvas;
	public Image healthBar;

	// Start is called before the first frame update
	void Start()
	{
		health = startHealth;

		levelSettings = (LevelSettings)FindObjectOfType(typeof(LevelSettings));
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(levelSettings.destination.transform.position);
		path = new NavMeshPath();
		elapsed = 0.0f;
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
		if (Vector3.Distance(agent.destination, agent.transform.position) <= 2)
		{
				PlayerStats.Lives -= 1;
				Destroy(gameObject);
		}
		// Update the way to the goal every second.
		elapsed += Time.deltaTime;
		if (elapsed > 1.0f)
		{
			elapsed -= 1.0f;
			NavMesh.CalculatePath(transform.position, levelSettings.destination.transform.position, NavMesh.AllAreas, path);
			agent.CalculatePath(levelSettings.destination.transform.position, path);
		}
		for (int i = 0; i < path.corners.Length - 1; i++)
			Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

		healthCanvas.rotation = Quaternion.Euler(90f, 0f, 0f);
	}
}
