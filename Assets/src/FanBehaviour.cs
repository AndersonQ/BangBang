using UnityEngine;
using System.Collections;

public class FanBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform .Rotate(0,0, Time.deltaTime * 180);
	}
}
