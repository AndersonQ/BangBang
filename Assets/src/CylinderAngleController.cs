using UnityEngine;
using System.Collections;

public class CylinderAngleController : MonoBehaviour {
	public GameObject Projetil;
	public GameObject Flecha;
	CylinderAngleController Cylinder;
	private bool cont = true;
	// Use this for initialization
	void Start () {
		Cylinder = this;

			}
	void FixedUpdate ()
	{

			float moveVertical = Input.GetAxis ("Vertical");
			Cylinder.transform.eulerAngles -= new Vector3 (moveVertical, 0.0f, 0.0f);
			if (Input.GetButton ("Fire1") && cont) {
			Atirar(10.0f); // Trocar o valor 10 por mod da velocidade inicial aplicada.
			//cont = false;
			}

	}
	void Atirar(float mod)
	{
		GameObject Proj = (GameObject)Instantiate (Projetil, Flecha.transform.position, Cylinder.transform.rotation);
		Proj.GetComponents<Rigidbody> () [0].velocity = (Flecha.transform.TransformVector (Flecha.transform.localPosition)*mod);
	}
	// Update is called once per frame
	void Update () {

	}
}
