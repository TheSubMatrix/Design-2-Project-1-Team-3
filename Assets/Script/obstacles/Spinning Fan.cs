using UnityEngine;

public class SpinningFan : MonoBehaviour//,ISlowable
{
    private float fanSpeed = 400f;
    private bool Froozen = false;
    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, 0, fanSpeed * Time.deltaTime);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (/* when prefab ice colliders with fan &&*/Froozen == false )
        {
            //plays an auido of incorrect sound
            Froozen = true;
        fanSpeed = 40f;
            Invoke("slowsDown", 5f);
        }
        /*
        else if (  when player shoots at the wall that is not ice)
        {
        plays an auido of incorrect sound
        }
        */

        /*if (when player enter the fans hit box)
         * {
         *takes damage if gets moves backwards (so the player doesnt get comboed)
         * }
         */
    }
    public void slowsDown()
    {
        Froozen = false;
        fanSpeed = 400f;
    }
}
