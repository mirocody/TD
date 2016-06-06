using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour {
	
	public Light light;
	public float Delay = 0.5f;
	public float Down = 1;

	private float time = 0;

	void Update ()
	{
		time += Time.deltaTime;

		if(time > Delay)
		{
			if(light.intensity > 0)
				light.intensity -= Time.deltaTime*Down;

			if(light.intensity <= 0)
				light.intensity = 0;
		}
	}
}
