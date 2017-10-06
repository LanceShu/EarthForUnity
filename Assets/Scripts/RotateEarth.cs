using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEarth : MonoBehaviour {

	//球体属性
	public GameObject textObject;

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

	private float oldRotate;
	private float newRotate;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		float rigidSpeed = Rigid ();

		//根据计算出的阻尼和X，Y轴的偏移来旋转地球;
		gameObject.transform.Rotate (new Vector3 (axisY, axisX, 0) * rigidSpeed, Space.World);

		if (rigidSpeed >= 0) {
			//textObject.SendMessage ("getMessage", gameObject.transform.rotation);
		}

		//如果鼠标离开屏幕则标记为已经不再拖拽;
		if(!Input.GetMouseButton(0)){
			onDrag = false;
		}

	}

	//鼠标移动的距离;
	void OnMouseDown(){
		axisX = 0f;
		axisY = 0f;
		oldRotate = 0;
		Debug.Log ("click the ball");
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
