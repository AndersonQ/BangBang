using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject projectilePrefab;
	public float projSpeed;
	public float projYaw;
	public float projPitch;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject proj;
		// TODO discover how we will translate pitch and yaw to quaternions
		// see: http://answers.unity3d.com/questions/416169/finding-pitchrollyaw-from-quaternions.html
		Quaternion rotation = (GameObject) Quaternion.Euler(new Vector3(45, 45, 45));
		if (Input.GetButton("Fire1")) {
			proj = Instantiate(projectilePrefab, 
			                   transform.position, 
			                   rotation);

			//proj.
		}
	}
}
