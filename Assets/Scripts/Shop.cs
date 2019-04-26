using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

	public TurretBlueprint cannonTurret;
	public TurretBlueprint rocketTurret;
	public TurretBlueprint wallTurret;
	public TurretBlueprint laserTurret;

	BuildManager buildManager;

	private void Start()
	{
		buildManager = BuildManager.instance;

		cannonTurret.costText.text = "$" + cannonTurret.cost.ToString();
		rocketTurret.costText.text = "$" + rocketTurret.cost.ToString();
		wallTurret.costText.text = "$" + wallTurret.cost.ToString();
		laserTurret.costText.text = "$" + laserTurret.cost.ToString();
	}

	public void SelectWall()
	{
		Debug.Log("Wall Purchased");
		buildManager.SelectTurretToBuild(wallTurret);
	}

	public void SelectCannonTurret()
	{
		Debug.Log("Cannon Turret Purchased");
		buildManager.SelectTurretToBuild(cannonTurret);
	}

	public void SelectRocketTurret()
	{
		Debug.Log("Cannon Turret Purchased");
		buildManager.SelectTurretToBuild(rocketTurret);
	}

	public void SelectLaserTurret()
	{
		Debug.Log("Laser Turret Purchased");
		buildManager.SelectTurretToBuild(laserTurret);
	}
}
