using UnityEngine;
using System.Collections;

public class TowerData : MonoBehaviour
{
    InitialData initData;

    public int cost;

    public float range;

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

    public void init(){
      initData=GameObject.Find("SystemData").GetComponent<InitialData>();
      getTowerData(level,towerType);
    }

    public void getTowerData(int level,char type){
      int i=level-1;
      int j=0;
      switch(type){
        case 'e':
          j=0;
          break;
        case 'm':
          j=1;
          break;
        case 'g':
          j=2;
          break;
        case 'f':
          j=3;
          break;
        case 'w':
          j=4;
          break;
        default:
          break;
        }
//Assign date to certain level and element tower
    //Debug.Log("I is "+i+"J is "+j);
          range=initData.range[j,i];
          cost=initData.cost[j,i];
          upgradeCost=initData.upgradeCost[j,i];
          turnSpeed=initData.turnSpeed[j,i];
          errorAmount=initData.errorAmount[j,i];
          fireCoolDown=initData.fireCooldown[j,i];
//          fireCoolDownLeft=initData.fireCoolDownLeft[i,j];
          rechargeRate=initData.rechargeRate[j,i];
//          elevateRange=initData.elevateRange[i,j];
//          elevateRadius=initData.elevateRadius[i,j];
    }
}
