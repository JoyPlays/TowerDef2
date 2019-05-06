using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
	private NavMeshPath path;
	private float elapsed = 0.0f;

	private Enemy enemy;

	public NavMeshAgent agent;
	public LevelSettings levelSettings;

	void Start()
	{
		enemy = GetComponent<Enemy>();

		levelSettings = (LevelSettings)FindObjectOfType(typeof(LevelSettings));
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.speed = enemy.startSpeed;
		agent.SetDestination(levelSettings.destination.transform.position);
		path = new NavMeshPath();
		elapsed = 0.0f;
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

		agent.speed = enemy.speed;

		if (enemy.slowEffectOn)
			return;

		agent.speed = enemy.startSpeed;
	}
}
