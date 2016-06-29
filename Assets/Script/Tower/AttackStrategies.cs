using UnityEngine;
using System.Collections;

public class AttackStrategies : MonoBehaviour {

	public string attackStrategy;

	[HideInInspector]
	public Transform targetEnemy;

	private float dist;
	private Enemy nearestEnemy;
	private TowerData towerData;

	// Use this for initialization
	void Start () {
		nearestEnemy = null;
		dist = Mathf.Infinity;
		towerData = gameObject.GetComponent<TowerData> ();
	}

	// Update is called once per frame
	void Update () {
		switch (attackStrategy) {
		case "NearestEnemy":
			FindNearestEnemy ();
			break;
		case "NearestGoalEnemy":
			FindNearestGoalEnemy ();
			break;
		case "LatestEnemy":
			FindLatestEnemy ();
			break;
		case "StrongestEnemy":
			FindStrongestEnemy ();
			break;
		case "WeakestEnemy":
			FindWeakestEnemy ();
			break;
		default:
            AttackTargetEnemy();
			break;
		}

	}

	void FindNearestEnemy()
	{
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

		//Enemy nearestEnemy = null;
		//float dist = Mathf.Infinity;
		//Debug.Log("enemies:"+enemies[0]);
		foreach(Enemy e in enemies) {
            if (e.transform.tag != "StaticEnemy" || TargetEnemyController.targetEnemey == e.transform)
            {
                float d = Vector3.Distance(this.transform.position, e.transform.position);

                dist = towerData.range;
                if (nearestEnemy == null || d < dist)
                {
                    nearestEnemy = e;
                    dist = d;
                }
            }
		}

		if(nearestEnemy == null) {
			//Debug.Log("No enemies?");
			return;
		}

		targetEnemy = nearestEnemy.transform;
	}

	void FindLatestEnemy()
	{
		// Find the latest enemy entering the attack range
	}

	void FindNearestGoalEnemy()
	{
		// Find the enemy who is nearest to the goal
	}

	void FindStrongestEnemy()
	{
		// the method name is self explanatory
	}

	void FindWeakestEnemy()
	{
		// the method name is self explanatory
	}

	//	void OnTriggerEnter(Collider other)
	//	{
	//		if (other.gameObject.CompareTag ("Enemy")) {
	//			targetTransform = other.gameObject.transform;
	//		}
	//	}
	//
	//	void OnTriggerExit(Collider other)
	//	{
	//		if (other.gameObject.transform == targetTransform) {
	//			targetTransform = null;
	//		}
	//	}
    void AttackTargetEnemy()
    {
        if (TargetEnemyController.targetEnemey != null)
        {
            float d = Vector3.Distance(this.transform.position, TargetEnemyController.targetEnemey.transform.position);
            dist = towerData.range;
            if (d <= dist) targetEnemy = TargetEnemyController.targetEnemey;
            else FindNearestEnemy();
        }
        else
        {
            FindNearestEnemy();
        }
    }
}