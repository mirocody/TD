using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[HideInInspector]
	public Transform target;

	public float speed = 15f;
	public float damage = 1f;
	public float radius = 0;
	private char element;

	// Use this for initialization
	void Start () {
		element = 'f';
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
			// TODO: Consider ways to smooth this motion.

			// Move towards node
			transform.Translate( dir.normalized * distThisFrame, Space.World );
			Quaternion targetRotation = Quaternion.LookRotation( dir );
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*5);
		}

	}

	void DoBulletHit() {
		// TODO:  What if it's an exploding bullet with an area of effect?

		if(radius == 0) {
			target.GetComponent<Enemy>().TakeDamage(damage,element);
			Debug.Log ("Normal Attack takes " + damage.ToString() + " damage!");

		}
		else {
			Collider[] cols = Physics.OverlapSphere(transform.position, radius);
			Debug.Log ("Enhanced Attack!");
			foreach(Collider c in cols) {
				Enemy e = c.GetComponent<Enemy>();
				if(e != null) {
					// TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
					e.GetComponent<Enemy>().TakeDamage(damage,element);
					Debug.Log ("Enhanced Attack takes " + damage.ToString() + " damage!");
				}
			}
		}

		// TODO: Maybe spawn a cool "explosion" object here?

		Destroy(gameObject);
	}
}
