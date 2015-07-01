using UnityEngine;
using System.Collections;

public class CylinderAngleController : MonoBehaviour {
	public GameObject projectile;
	private float magnetud;

	void Start () {

			}
	void FixedUpdate ()
	{

		float moveVertical = Input.GetAxis ("Vertical");
		this.transform.eulerAngles -= new Vector3 (moveVertical, 0.0f, 0.0f);
		if (Input.GetKeyDown("space"))
				magnetud = Time.time;

		if (Input.GetKeyUp("space")) {
			magnetud = Time.time - magnetud;
			Atirar(magnetud*30);
		}



	}
	void Atirar(float mod)
	{ //transform.localPosition
		GameObject Proj = (GameObject)Instantiate (projectile,transform.position + this.transform.TransformVector (new Vector3(0.0f,1.0f,0.0f)) ,Quaternion.LookRotation(this.transform.forward));

		Proj.GetComponents<Rigidbody> () [0].velocity = (this.transform.TransformVector(new Vector3(0.0f,1.0f,0.0f))*mod);
		//Proj.GetComponents<Rigidbody> () [0].velocity = (this.transform.TransformVector (this.transform.localPosition)*mod);
	}
	// Update is called once per frame
	void Update () {

	}
}
