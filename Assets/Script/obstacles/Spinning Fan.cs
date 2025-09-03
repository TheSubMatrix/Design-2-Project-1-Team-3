using UnityEngine;

public class SpinningFan : MonoBehaviour//,ISlowable
{
    private float fanSpeed = 400f;
    private bool Froozen = false;
    // Update is called once per frame
    void Update()
    {

        transform.Rotate(fanSpeed * Time.deltaTime, 0, 0);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ice")&& Froozen == false)
        {
            //plays an auido of correct sound
            Froozen = true;
        fanSpeed = 40f;
            Invoke("slowsDown", 5f);
        }
        else if (other.gameObject.CompareTag("Fire") || other.gameObject.CompareTag("Thunder"))
        {
        //plays an auido of incorrect sound
        }
        if (other.gameObject.CompareTag("Player"))
          {
         //takes damage if gets moves backwards (so the player doesnt get comboed)
          }
    }
    public void slowsDown()
    {
        Froozen = false;
        fanSpeed = 400f;
    }
}
