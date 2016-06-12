using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	[HideInInspector]
	public Transform target;

	public float damage = 5.0f;
	public float radius = 10.0f;
	private char element;


	// Use this for initialization
	void Start () {
		element='f';	
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] cols = Physics.OverlapSphere(transform.position, radius);
		Debug.Log ("Bomb Explosion!");
		foreach(Collider c in cols) {
			Enemy e = c.GetComponent<Enemy>();
			if(e != null) {
				// TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
				e.GetComponent<Enemy>().TakeDamage(damage,element);
				Debug.Log ("Boom Explosion takes " + damage.ToString() + " damage!");
			}
		}
	}
}
