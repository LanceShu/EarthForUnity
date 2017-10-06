using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBall : MonoBehaviour {

	public GameObject text;
	public GameObject ball;
	public GameObject camera;
	private float axisY;
	private float axisX;
	private float rigidSpeed;
	private Quaternion quaternion;
	private float angle;
	private bool touch = false;

	// Use this for initialization
	void Start () {
		
	}

	void getMessage(Quaternion str){
		quaternion = str;
	}

	private void OnMouseDown(){
		Debug.Log ("text");
		//AndroidJavaObject javaObject = new AndroidJavaObject ();

	}

	void findCountry(){
		touch = true;
	}
	
	// Update is called once per frame
	void Update () {
		text.transform.rotation = quaternion;
		angle = Vector3.Angle (text.transform.forward, camera.transform.forward);
		if (touch) {
			Vector3 targetDir = -text.transform.forward;
			Vector3 cameraDir = -Vector3.forward;
			Quaternion rotation = Quaternion.FromToRotation (targetDir, cameraDir) * transform.rotation;
			ball.transform.rotation = Quaternion.Slerp (ball.transform.rotation, rotation, Time.deltaTime * 3f);

			if (Vector3.Angle (targetDir, cameraDir) < 1f) {
				touch = false;
			}
		}
	}
}
