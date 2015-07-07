using UnityEngine;
using System.Collections;

public class BodyAngleControler : MonoBehaviour {
	public GameObject projectilePrefab;
	public GameObject cannon;
	private float magnitude;
	
	void FixedUpdate()
	{
		if (!Input.GetMouseButton (1) && !Input.GetMouseButton (2))
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			this.transform.eulerAngles += new Vector3 (0.0f, moveHorizontal, 0.0f);
			cannon.transform.eulerAngles -= new Vector3 (moveVertical, 0.0f, 0.0f);

			if (Input.GetKeyDown("space"))
				magnitude = Time.time;
			
			if (Input.GetKeyUp("space")) {
				magnitude = Time.time - magnitude;
				Fire(magnitude*5);
			}
		}
	}

	void Fire(float mod)
	{
		GameObject projectile = (GameObject)Instantiate(projectilePrefab,
		                                                transform.position + cannon.transform.TransformVector (new Vector3(0.0f,1.0f,0.0f)),
		                                                Quaternion.LookRotation(cannon.transform.forward));
		
		Rigidbody projectileRb = projectile.GetComponent<Rigidbody> ();
		projectileRb.velocity = cannon.transform.TransformVector(new Vector3(0.0f, mod, 0.0f));
	}
}
