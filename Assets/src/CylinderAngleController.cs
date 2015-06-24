using UnityEngine;
using System.Collections;

public class CylinderAngleController : MonoBehaviour {
	public GameObject Projetil;
	CylinderAngleController Cylinder;

	// Use this for initialization
	void Start () {
		Cylinder = this;

			}
	void FixedUpdate ()
	{
		float moveVertical = Input.GetAxis ("Vertical");
		Cylinder.transform.eulerAngles -= new Vector3 (moveVertical, 0.0f, 0.0f);
		if (Input.GetButton("Fire1"))
		{
			Object Proj =  Instantiate(Projetil,Cylinder.transform.position,Cylinder.transform.rotation);

		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
