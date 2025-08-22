using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    //player speed
    private float playerSpeed = 5f;
    private Rigidbody rb;
    public Vector3 jump;
    //player Jump height
    private float jumpForce = 2f;
    private float horizontalInput, verticalInput;

    //camera follows player
    public float cameraSpeed = 3f;

    //Ground Check
    public bool groundCheck = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 4f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(horizontalInput, 0, verticalInput).normalized * playerSpeed);
        //press space bar to jump
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
        //shift to sprint (maybe player has a limit like a statmina bar)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 10;
        }

        // Camera moves with player
        float mouseX = Input.GetAxis("Mouse X") * cameraSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSpeed;

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation *= Quaternion.Euler(-mouseY, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        groundCheck = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        groundCheck = false;
    }
}
