using UnityEngine;

public class FreeFlyCamera : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float fastMovementSpeed = 50.0f;
    public float rotationSpeed = 100.0f;
    public float mouseSensitivity = 0.25f;

    private bool looking = false;

    void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    void HandleMovementInput()
    {
        float speed = movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = fastMovementSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            transform.position -= transform.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            looking = true;
            Cursor.visible = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            looking = false;
            Cursor.visible = true;
        }

        if (looking)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity * rotationSpeed * Time.deltaTime;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensitivity * rotationSpeed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }
    }
}
