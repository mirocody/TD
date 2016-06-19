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
    public bool isElevate = false;

	// Use this for initialization
	void Start () {
		damage = Mathf.Pow (2, level - 1) * damage;
	}

	// Update is called once per frame
	void Update () {
		if(target == null) {
			// Our enemy went away!
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - this.transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if(dir.magnitude <= distThisFrame) {
			// We reached the node
			DoBulletHit();
		}
		else {
			// Move towards node
			transform.Translate( dir.normalized * distThisFrame, Space.World );
			Quaternion targetRotation = Quaternion.LookRotation( dir );
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*5);
		}

	}

	void DoBulletHit() {
		if(type!='f') {
			//target.GetComponent<Enemy>().TakeDamage(damage);
			//target.GetComponent<Enemy>().TakeDamage(type, level, damage);
			//damage = Mathf.Pow (2, level - 1) * damage;
			target.GetComponent<Enemy>().TakeDamage(damage, type);
			//Debug.Log ("Normal Attack takes " + damage.ToString() + " damage!");
		}
		else {
			Collider[] cols = Physics.OverlapSphere(transform.position, radius);
			Debug.Log ("Fire tower attack!");
			foreach(Collider c in cols) {
				Enemy e = c.GetComponent<Enemy>();
				if(e != null) {
					// TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
					//e.GetComponent<Enemy>().TakeDamage(damage);
					//e.GetComponent<Enemy>().TakeDamage(type, level, damage);
					//damage = Mathf.Pow (2, level - 1) * damage;
					target.GetComponent<Enemy>().TakeDamage(damage, type);
					Debug.Log ("Fire attack AOE takes " + damage.ToString() + " damage!");
				}
			}
		}

		// TODO: Maybe spawn a cool "explosion" object here?
		Destroy(gameObject);
	}
}
