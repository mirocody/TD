using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[HideInInspector]
	public Transform target;

	public float speed = 15f;
	public float damage = 1f;
	public float radius = 10f;
	public int level = 0;
	public char type;
	public bool stunFlag=false;
	public int stunPercent=0;
    public bool isElevate = false;
	InitialData initData;
	public Vector3 destination;

	public AudioClip biu;


	// Use this for initialization
	void Start () {
		//damage = Mathf.Pow (2, level - 1) * damage;
		initData = GameObject.Find("SystemData").GetComponent<InitialData>();
		getBulletData(level,type);


		if (type == 'q') {
			Vector3 direct;
			direct.x = (target.position.x - this.transform.localPosition.x) * 1000F;
			direct.y =	0F;
			direct.z = (target.position.z - this.transform.localPosition.z) * 1000F;
			destination = this.transform.localPosition + direct;
		}
		if(type=='r'){

			int dice = UnityEngine.Random.Range (0,100);
			if(dice<stunPercent){
				Debug.Log("Stun true");
				stunFlag=true;
			}
		}
	}
	public void getBulletData(int level,char type){
		int i=level-1;
		int j=0;
		switch(type){
			case 'e':
			case 'x':
				j=0;
				break;
			case 'm':
			case 'y':
				j=1;
				break;
			case 'g':
			case 'r':
				j=2;
				break;
			case 'f':
			case 'q':
				j=3;
				break;
			case 'w':
			case 'd':
				j=4;
				break;

			default:
				break;
			}
			damage = initData.damage [j, i];
			speed = initData.speed [j, i];
			radius = initData.radius [j, i];
			stunPercent=initData.stun[j,i];
	//		Debug.Log("Stunp0 in getdata is "+initData.stun[3,2]);
		//	Debug.Log("Stunp1 in getdata is "+initData.stun[j,i]);
			//Debug.Log("Stunp1 in getdata is "+stunPercent);
		}
	// Update is called once per frame
	void Update () {
		if (type != 'q') {
			if (target == null) {
				// Our enemy went away!
				Destroy (gameObject);
				return;
			}

			Vector3 dir = target.position - this.transform.localPosition;

			float distThisFrame = speed * Time.deltaTime;

			if (dir.magnitude <= distThisFrame) {
				// We reached the node
				DoBulletHit ();
			} else {
				// Move towards node
				transform.Translate (dir.normalized * distThisFrame, Space.World);
				Quaternion targetRotation = Quaternion.LookRotation (dir);
				this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime * 5);
			}
		}
		else {
			float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(this.transform.position, destination, step);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (this.type == 'q') {
			if (other.tag == "Enemy" || other.tag == "StaticEnemy") {
				Enemy enemy = other.GetComponent<Enemy> ();
				if (enemy != null) {
					enemy.TakeDamage (damage, type,level,false);
	//				Debug.Log ("Line attack!!");
				}
			} else if (other.tag == "Boundary") {
				Destroy (this.gameObject);
			}
		}
	}

	void DoBulletHit() {
//not fire tower, no AOE Attack
		if((type!='f') && (type!='r')&&(type!='d')) {
			target.GetComponent<Enemy>().TakeDamage(damage, type,level,false);
		}
//fire tower, AOE attack
		else{
			int i = 0;
			Collider[] cols = Physics.OverlapSphere(transform.position, radius);
//			Debug.Log ("AOE tower attack!");
			foreach(Collider c in cols) {

				Enemy e = c.GetComponent<Enemy>();
				if(e != null) {
					i++;
					Debug.Log("Take stun damage");
					Debug.Log(stunFlag);
					e.GetComponent<Enemy>().TakeDamage(damage, type,level,stunFlag);
				}
			}
		}
		AudioSource.PlayClipAtPoint (biu, this.transform.localPosition);
		Destroy(this.gameObject);
	}
}
