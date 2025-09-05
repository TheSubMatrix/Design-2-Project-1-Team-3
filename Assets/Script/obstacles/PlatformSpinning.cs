using UnityEngine;

public class PlatformSpinning : MonoBehaviour
{
    private float fanSpeed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, fanSpeed * Time.deltaTime, 0);
    }
}
