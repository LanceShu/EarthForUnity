using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEarth : MonoBehaviour {

	//球体属性
	private float r = 10f;
	private float size = 2.2f;
	private float distance = 10f;
	public bool isCity = false;
	public GameObject textObject;
	public ArrayList coutries;
	public Transform cameraTarget;

	//是否被拖拽；
	private bool onDrag = false;

	//旋转速度；
	private float speed = 2f;

	//阻尼速度;
	private float tempSpeed;

	//鼠标沿水平方向移动的增量；
	private float axisX;

	//鼠标沿垂直方向移动的增量;
	private float axisY;

	//滑动距离（鼠标）;
	private float cXY;

	// Use this for initialization
	void Start () {
		
		coutries = new ArrayList ();

		for (int i = 0; i < 10; i++) {
			coutries.Add ("China");
		}

		createSphere (coutries.Count);

		/*
		foreach (string countryName in Constrant.pros) {
			coutries.Add (countryName);
		}

		
		*/
	}

	public void createSphere(float N){
		float inc = Mathf.PI * (3 - Mathf.Sqrt (5));
		float off = 2 / N;
		for (int i = 0; i < (N); i++) {
			float y = i * off - 1 + (off / 2);
			float r = Mathf.Sqrt (1 - y * y);
			float phi = i * inc;
			Vector3 pos = new Vector3 ((Mathf.Cos (phi) * r * size),y * size,Mathf.Sin(phi)*r*size);

			GameObject text = (GameObject)Instantiate (textObject, pos, Quaternion.identity);

			text.transform.parent = gameObject.transform;

			text.transform.localScale = new Vector3 (0.5f, 0.5f,0.5f);

			foreach (Transform child in text.transform) {
				TextMesh tm = (TextMesh)child.GetComponent<TextMesh> ();
				tm.text = "China";
			}

		}
	}

	// Update is called once per frame
	void Update () {



		//根据计算出的阻尼和X，Y轴的偏移来旋转地球;
		gameObject.transform.Rotate (new Vector3 (axisY, axisX, 0) * Rigid (), Space.World);

		//如果鼠标离开屏幕则标记为已经不再拖拽;
		if(!Input.GetMouseButton(0)){
			onDrag = false;
		}

	}

	//鼠标移动的距离;
	void OnMouseDown(){
		axisX = 0f;
		axisY = 0f;
	}

	//鼠标拖拽时的操作;
	void OnMouseDrag(){
		onDrag = true;
		axisX = -Input.GetAxis ("Mouse X");
		axisY = Input.GetAxis ("Mouse Y");

		cXY = Mathf.Sqrt (axisX * axisX + axisY * axisY);
	}

	//计算阻尼速度;
	float Rigid(){
		if (onDrag) {
			tempSpeed = speed;
		} else {
			if (tempSpeed > 0) {
				if (cXY != 0) {
					tempSpeed = tempSpeed - speed * 2 * Time.deltaTime / cXY;	
				}
			} else {
				tempSpeed = 0;
			}
		}
		return tempSpeed;
	}
}
