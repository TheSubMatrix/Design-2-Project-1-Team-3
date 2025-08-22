using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    //player speed
    private float m_playerSpeed = 5f;
    private Rigidbody m_rb;
    [FormerlySerializedAs("jump")] public Vector3 m_jump;
    //player Jump height
    private float m_jumpForce = 2f;
    private float m_horizontalInput, m_verticalInput;

    //camera follows player
    [FormerlySerializedAs("cameraSpeed")] public float m_cameraSpeed = 3f;

    //Ground Check
    [FormerlySerializedAs("groundCheck")] public bool m_groundCheck = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_jump = new Vector3(0.0f, 4f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_rb.AddForce(new Vector3(m_horizontalInput, 0, m_verticalInput).normalized * m_playerSpeed);
        //press the space bar to jump
        if (Input.GetKeyDown(KeyCode.Space) && m_groundCheck)
        {
            m_rb.AddForce(m_jump * m_jumpForce, ForceMode.Impulse);
        }
        //shift to sprint (maybe the player has a limit like a stamina bar)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_playerSpeed = 10;
        }

        // Camera moves with player
        float mouseX = Input.GetAxis("Mouse X") * m_cameraSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * m_cameraSpeed;

        transform.Rotate(Vector3.up * mouseX);
        if (Camera.main is not null) Camera.main.transform.localRotation *= Quaternion.Euler(-mouseY, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        m_groundCheck = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        m_groundCheck = false;
    }
}
