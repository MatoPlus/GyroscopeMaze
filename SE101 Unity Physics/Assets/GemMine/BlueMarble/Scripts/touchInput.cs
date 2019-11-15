using UnityEngine;
using System.Collections;

public class touchInput : MonoBehaviour {

	bool mousePressed;

	Vector2 mouseDownPosition;
	Vector2 mousePosition;
	Vector2 mouseUpPosition;

	float yAxis;
	float xAxis;

	float perspectiveZoomSpeed = 0.5f;
	float orthoZoomSpeed = 0.5f;
	float sensitivity = 0.5f;

	float startFieldOfView;

	public GameObject earthGO;
	public GameObject lightGO;

	//public MarkerLocation mLoc;

	// Use this for initialization
	void Start () {
		startFieldOfView = Camera.main.fieldOfView;
		mousePressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		//getMouseInput ();
		getTouchInputDrag();
		getTouchInputZoom ();
	}

	void getTouchInputZoom() {
		// are there two touches on the device
		if (Input.touchCount == 2) {
			// store both touches
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			Vector2 touchZeroPrevPosition = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPosition = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPosition - touchOnePrevPosition).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
			Camera.main.fieldOfView = Mathf.Clamp (Camera.main.fieldOfView, 1f, 120f);
			//mLoc.changeMarkerSize ();
		}
	}

	void getTouchInputDrag() {
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;

			//transform.RotateAround (Vector3.zero, Vector3.up, touchDeltaPosition.x * Camera.main.fieldOfView / startFieldOfView * sensitivity);
			//transform.RotateAround (Vector3.zero, Vector3.right, touchDeltaPosition.y * Camera.main.fieldOfView / startFieldOfView * sensitivity);
		
			earthGO.transform.RotateAround (Vector3.zero, Vector3.up, touchDeltaPosition.x * Camera.main.fieldOfView / startFieldOfView * sensitivity);
			earthGO.transform.RotateAround (Vector3.zero, Vector3.right, touchDeltaPosition.y * Camera.main.fieldOfView / startFieldOfView * sensitivity);
		}
	}



	void getMouseInput() {
		// mouse down
		if (Input.GetMouseButtonDown (0)) {
			mousePressed = true;
			mouseDownPosition = Input.mousePosition;
		}

		// mouse pressed
		if (Input.GetMouseButton (0)) {
			mousePosition = Input.mousePosition;
		}

		// mouse up
		if (Input.GetMouseButtonUp (0)) {
			mousePressed = false;
		}

		if (mousePressed) {
			yAxis = (mouseDownPosition - mousePosition).x * Camera.main.fieldOfView / startFieldOfView;
			xAxis = (mouseDownPosition - mousePosition).y * Camera.main.fieldOfView / startFieldOfView;
			transform.RotateAround (Vector3.zero, Vector3.up, yAxis);
			transform.RotateAround (Vector3.zero, -Vector3.right, xAxis);
			mouseDownPosition = mousePosition;
		}
	}
}
