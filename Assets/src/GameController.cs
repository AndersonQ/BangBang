using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Camera playerCamera;
	public Camera overHeadCamera;
	public int cameraThrust;
	public int playing;
	public int turn;





	void Awake()
	{
		playing = 0;
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButton (1)) {
			float hAxis = Input.GetAxis ("Horizontal");
			float vAxis = Input.GetAxis ("Vertical");

			float rotationX = playerCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X");
			float rotationY = playerCamera.transform.localEulerAngles.x + Input.GetAxis("Mouse Y");

			//Swap camera
			overHeadCamera.enabled = false;
			playerCamera.enabled = true;

			playerCamera.transform.position += new Vector3 (hAxis, 0, vAxis);// * cameraThrust;
			playerCamera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);


		} else {
			//Swap camera back
			overHeadCamera.enabled = true;
			playerCamera.enabled = false;

		}
	}
}
