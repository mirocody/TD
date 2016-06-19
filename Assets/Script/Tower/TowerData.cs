using UnityEngine;
using System.Collections;

public class TowerData : MonoBehaviour
{

    public int cost = 5;

    public float range = 10f;

    [HideInInspector]
    public float elevateRange;

    public float turnSpeed = 5.0f;

    [HideInInspector]
    public float elevateTurnSpeed;

    public float errorAmount = 1.0f;

    [HideInInspector]
    public float elevateErrorAmount;

    public float fireCoolDown = 0.5f;

    [HideInInspector]
    public float elevateFireCoolDown;

    public float rechargeRate = 10.0f;

    [HideInInspector]
    public float elevateRechangeRate;

    public int level = 0;

    public int upgradeCost;

    public char towerType;

    public float elevateRate = 0f;

    public float elevateRadius = 0f;

    public bool isElevated = false;

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
            Debug.Log("Tower Type not found!");

        return type;
    }

}
