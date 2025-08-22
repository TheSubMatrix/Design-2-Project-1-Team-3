using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    //player speed
    private float playerSpeed = 5f;
    private Rigidbody rb;
    public Vector3 jump;
    //player Jump height
    private float jumpForce = 5f;
    private float horizontalInput, verticalInput;

    //Ground Check
    private bool groundCheck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 5f, 0.0f);
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
            groundCheck = false;
        }
        //shift to sprint (maybe player has a limit like a statmina bar)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 10;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            groundCheck = true;
        }
    }
}
