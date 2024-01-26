using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Movement movement;
    
    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    
    Vector2 horizontalInput;

    private void OnEnable()
    {
        controls.Enable();
    }

    public void Awake()
    {
        movement = GetComponent<Movement>();

        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;

        //groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        horizontalInput = groundMovement.HorizontalMovement.ReadValue<Vector2>();
        movement.ReceiveInput(horizontalInput);
        //Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        //characterController.Move(horizontalVelocity * Time.deltaTime);
    }


    public void OnDisable()
    {
        controls.Disable();
    }
}
 