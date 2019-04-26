using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

	public static BuildManager instance;

	private void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
		}
		instance = this;
	}

	[Header("Effects")]
	public GameObject buildEffect;

	private TurretBlueprint turretToBuild;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
	
	public void SelectTurretToBuild(TurretBlueprint turretBlueprint)
	{
		turretToBuild = turretBlueprint;
	}

	public void BuildTurretOn(Node node)
	{
		// Build a turret
		if (PlayerStats.Money < turretToBuild.cost)
		{
			Debug.Log("Not enough money!");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;

		GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Debug.Log("Turret built! Money left: " + PlayerStats.Money);
	}
}
