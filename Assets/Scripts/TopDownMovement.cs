using UnityEngine;

public class TopDownMovement : MonoBehaviour {

	public float walkSpeed = 5f;
	public float maxSpeed;
	public Boundary boundary;
	
	float curSpeed;
	Rigidbody rb;

	AnimationsMummy animMummy;
 
	void Start() {
		maxSpeed = Mathf.Sqrt(2 * walkSpeed * walkSpeed);
		animMummy = GetComponent<AnimationsMummy>();
		rb = GetComponent<Rigidbody>();
		// sprintSpeed = walkSpeed + (walkSpeed / 2);
	}
 
	void FixedUpdate() {
		if (GlobalState.movementEnabled) {
			curSpeed = walkSpeed;

			// Move senteces
			rb.velocity = new Vector3(
				Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f),
				rb.velocity.y,
				Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f)
			);
			var x = rb.velocity.x;
			var z = rb.velocity.z;
			// adjust speed with pythag. ensuring two keys != greater velocity
			// i think this does nothing special, you can ignore it
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2)));

			rb.position = new Vector3(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				rb.position.y,
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);

			// animate character
			if (rb.velocity.magnitude >= 0.2f) {
				// look in direction of movement (direction of velocity)
				Vector3 moveDirection = new Vector3(
					rb.velocity.x,
					0,
					rb.velocity.z
				);
				if (Input.GetKey(KeyCode.UpArrow) 
					|| Input.GetKey(KeyCode.RightArrow)
					|| Input.GetKey(KeyCode.DownArrow)
					|| Input.GetKey(KeyCode.LeftArrow)) {
					transform.rotation = Quaternion.LookRotation(moveDirection.normalized);
				}
				
				if (rb.velocity.magnitude > walkSpeed * 0.5) {
					animMummy.Run();
				} else {
					animMummy.Walk();
				}
			} else {
				animMummy.OtherIdle();
			}
		}
	}
}


[System.Serializable]
public struct Boundary {
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;
}