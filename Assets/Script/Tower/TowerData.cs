using UnityEngine;
using System.Collections;

public class TowerData : MonoBehaviour {

	public int cost = 5;

	public float range = 10f;

	public float turnSpeed = 5.0f;

	public float errorAmount = 1.0f;

	public float fireCoolDown = 0.5f;

	public float rechargeRate = 10.0f;

	public int level = 0;

	public int upgradeCost;

	public char towerType;

//	public int getUpgradeCost(int currentLevel, string towerTag)
//	{
//		switch (towerTag) {
//		case "EarthTower":
//			return 10;
//			break;
//		case "FireTower":
//			return 10;
//			break;
//		case "MetalTower":
//			return 10;
//			break;
//		case "WaterTower":
//			return 10;
//			break;
//		case "WoodTower":
//			return 10;
//			break;
//		default:
//			return 5;
//			break;
//		}
//	}

	public char getTowerType()
	{
		char type = '?';

		if (gameObject.tag == "GoldTower")
			type = 'g';
		else if (gameObject.tag == "WoodTower")
			type = 'm';
		else if (gameObject.tag == "WaterTower")
			type = 'w';
		else if (gameObject.tag == "FireTower")
			type = 'f';
		else if (gameObject.tag == "EarthTower")
			type = 'e';
		else
			Debug.Log ("Tower Type not found!");

		return type;
	}

}
