using UnityEngine;
using System.Collections;

public class BodyAngleControler : MonoBehaviour {

	BodyAngleControler PlayerBody;

	// Use this for initialization
	void Start () {
		PlayerBody = this;
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		PlayerBody.transform.eulerAngles += new Vector3 (0.0f, moveHorizontal, 0.0f);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
