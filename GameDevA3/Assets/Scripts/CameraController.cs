using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		aspect = Screen.width / Screen.height;
		view_height = cam.orthographicSize * 2;
		view_width = view_height * aspect;
		boundaries = new Rect(0, 0, 60, 52);
	}

	private Camera cam;
	private float pan_speed = 1.0f;

	public float aspect;
	public float view_width;
	public float view_height;
	public Rect viewport;

	public Rect boundaries;

	// Update is called once per frame
	void FixedUpdate () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

		aspect = Screen.width / Screen.height;
		view_height = cam.orthographicSize * 2;
		view_width = view_height * aspect;
		viewport = new Rect(transform.localPosition.x - view_width / 2.0f, transform.localPosition.y - view_height / 2.0f, view_width, view_height);

		float clamped_move_x = 0;
		float clamped_move_y = 0;

		if (x > 0)
		{
			clamped_move_x = Mathf.Min(x * pan_speed, boundaries.right - viewport.right);
		}
		else if (x < 0)
		{
			clamped_move_x = -Mathf.Min(Mathf.Abs(x * pan_speed), Mathf.Abs(boundaries.left - viewport.left));
		}
		
		if (y > 0)
		{
			clamped_move_y = Mathf.Min(y * pan_speed, boundaries.bottom - viewport.bottom);
		} 
		else if (y < 0)
		{
			clamped_move_y = -Mathf.Min(Mathf.Abs(y * pan_speed), Mathf.Abs(boundaries.top - viewport.top));
		}

		transform.Translate(new Vector3(clamped_move_x, clamped_move_y, 0));

		if (Input.GetKey(KeyCode.Space))
		{
			cam.orthographicSize += 0.05f;
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			cam.orthographicSize -= 0.05f;
		}

		viewport = new Rect(transform.localPosition.x - view_width / 2.0f, transform.localPosition.y - view_height / 2.0f, view_width, view_height);
	}
}
