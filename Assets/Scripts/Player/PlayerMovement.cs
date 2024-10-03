using UnityEngine;
using UnityEngine.InputSystem;

//Edited to read movement input from Action from InputMap instead of polling input in Update()


public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;

	private PlayerInput playerInput;
    Vector2 playerMoveInput = Vector2.zero;

	int isWalkingAnimationHash = Animator.StringToHash("IsWalking");

    void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();

		playerInput = new PlayerInput();
		playerInput.Player.Movement.performed += ReadMovementInput;
		playerInput.Player.Movement.canceled += ReadMovementInput;

		playerInput.Enable();

	}

    private void OnDisable()
    {
        playerInput.Player.Movement.performed -= ReadMovementInput;
        playerInput.Player.Movement.canceled -= ReadMovementInput;
    }

    void FixedUpdate()
	{
		Move(playerMoveInput.x, playerMoveInput.y);
		Turning();
		Animating(playerMoveInput.x, playerMoveInput.y);
	}

	private void ReadMovementInput(InputAction.CallbackContext ctx)
	{
		if(ctx.performed)
		{
			playerMoveInput = ctx.ReadValue<Vector2>();
		}
		else if(ctx.canceled)
		{
			playerMoveInput = Vector2.zero;
		}
	}


	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;

		anim.SetBool(isWalkingAnimationHash, walking);
	}
}
