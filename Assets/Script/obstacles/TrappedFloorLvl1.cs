using UnityEngine;
using System.Collections;

public class TrappedFloorLvl1 : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    private bool isRotating = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(RotateObject(Vector3.up, 90));
            //Destroy(gameObject);
            transform.Rotate(90, 0, 0);
        }
    }
    private IEnumerator RotateObject(Vector3 axis, float angle)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + axis * angle);
        float t = 0;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}
