using UnityEngine;
using System.Collections;

public class MouthBehaviour : MonoBehaviour {
	public int direction, maxDown, maxUp;
	
	float speed;
	

	
	
	
	// Use this for initialization
	void Start () {
		speed = 4f;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += this.transform.up * direction * speed * Time.deltaTime;
		//this.transform .Rotate(0,0, Time.deltaTime * 180);
		if (this.transform.position.y > maxUp || this.transform.position.y < maxDown)
			direction = -direction;
	}
}