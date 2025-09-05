using UnityEngine;

public class rotateAxes : MonoBehaviour
{
    private float rotationSpeed = 100f; //Speed
    private float targetAngle = 60f; //rotation

    void Update()
    {
        float angle = Mathf.PingPong(Time.time * rotationSpeed, targetAngle);
        transform.rotation = Quaternion.Euler(0, 0, angle); // Rotates on the Y-axis
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player takes damage
            Debug.Log("player took damage");
        }
    }
}
