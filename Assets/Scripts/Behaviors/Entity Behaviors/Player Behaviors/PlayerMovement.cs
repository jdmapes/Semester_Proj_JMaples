using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    private const float SPEED = 8f;
    private const float YAW_SPEED = 420f;
    private const float PITCH_SPEED = 420f;
    private const float GRAVITY_FORCE = -9.8f;

    public Transform pitchObject;


    private float moveDis = 1f;
    private CharacterController characterController;

    private float yaw = 0f;
    private float pitch = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EditorApplication.ExitPlaymode();
        }

        var delta = Time.deltaTime;

        // Rotation Turn left/right
        // Look up and down on stacked camera

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        var yawDelta = mouseX * YAW_SPEED * delta;
        var pitchDelta = mouseY * PITCH_SPEED * delta;

        yaw += yawDelta;
        yaw %= 360f;
        var yawRotation = Quaternion.Euler(0f, yaw, 0f);
        transform.rotation = yawRotation;

        pitch += pitchDelta;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        pitchObject.localRotation = Quaternion.Euler(-pitch, 0f, 0f);

        // Movement WASD

        var inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            inputVector.z += moveDis;
        }

        if (Input.GetKey(KeyCode.S)) {
            inputVector.z -= moveDis;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += moveDis;
        }

        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= moveDis;
        }

        // Grab vertical velocity every frame from character controller
        var verticalVelocity = characterController.velocity.y;
        verticalVelocity += GRAVITY_FORCE * delta;

        
        // Select veloctiy every frame-stop immediately
        var velocity = yawRotation * inputVector * SPEED * delta;
        velocity += Vector3.up * verticalVelocity * delta;

        characterController.Move(velocity);

        // Die if you fall off the edge
        if (transform.position.y < - 100)
        {
            GetComponent<Health>().Damage(100);
        }
    }
}
