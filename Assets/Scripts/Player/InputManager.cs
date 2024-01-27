using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Movement movement;

    [SerializeField] PointAndClick pointAndClick;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    PlayerControls.MouseActions mouseActions;

    Vector2 horizontalInput;
    Vector2 mousePosition;

    private void OnEnable()
    {
        controls.Enable();
    }

    public void Awake()
    {
        movement = GetComponent<Movement>();

        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;
        mouseActions = controls.Mouse;

        //groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        mouseActions.Click.performed += OnMouseClick;
    }

    private void Update()
    {
        horizontalInput = groundMovement.HorizontalMovement.ReadValue<Vector2>();
        mousePosition = mouseActions.Position.ReadValue<Vector2>();

        pointAndClick.ReceivePosition(mousePosition);
        movement.ReceiveInput(horizontalInput);
        //Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        //characterController.Move(horizontalVelocity * Time.deltaTime);
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        pointAndClick.OnMouseClick();
    }

    public void OnDisable()
    {
        controls.Disable();
    }
}
 