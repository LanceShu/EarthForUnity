using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthControls : MonoBehaviour {

	private Transform m_Transform;

	// Use this for initialization
	void Start () {
		m_Transform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		MoveControl ();
	}
		
	void MoveControl(){
		
		if (Input.GetMouseButton(0)) {
			m_Transform.Rotate (Vector3.down, Input.GetAxis ("Mouse X"));
			m_Transform.Rotate (Vector3.right, Input.GetAxis ("Mouse Y"));
		}

	}
		
}
