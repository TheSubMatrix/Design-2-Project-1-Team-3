using System.Collections;
using UnityEngine;

public class Level3platforms : MonoBehaviour
{
    [SerializeField]

    public GameObject _platform1;
    public GameObject _platform2;
    public GameObject _platform3;
    public GameObject _platform4;
    public GameObject _platform5;
    public GameObject _platform6;
    public GameObject _platform7;
    //
    Vector3 ImpulseVectorLR = new Vector3(0f, 0f, 60.0f);
    Vector3 ImpulseVectorRL = new Vector3(0f, 0f, -60.0f);
    IEnumerator Platform1()
    {
        //right to left
        Vector3 spawnPosition = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));

            //spawning
            spawnPosition.x = Random.Range(-109.5f, -109.51f);
            spawnPosition.y = Random.Range(2.44f, 2.45f);
            spawnPosition.z = Random.Range(34.8f, 34.81f);
            GameObject platform1 = Instantiate(_platform1, spawnPosition, Quaternion.identity);
            platform1.GetComponent<Rigidbody>().AddForce(ImpulseVectorRL, ForceMode.Impulse);
            Destroy(platform1, 4);

        }
    }
    IEnumerator Platform2()
    {
        //left to right
        Vector3 spawnPosition1 = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            //spawning
            spawnPosition1.x = Random.Range(-125.3f, -125.31f);
            spawnPosition1.y = Random.Range(2.44f, 2.45f);
            spawnPosition1.z = Random.Range(-36.5f, -36.51f);
            GameObject platform2 = Instantiate(_platform2, spawnPosition1, Quaternion.identity);
            platform2.GetComponent<Rigidbody>().AddForce(ImpulseVectorLR, ForceMode.Impulse);
            Destroy(platform2, 4);

        }
    }
    IEnumerator Platform3()
    {
        //right to left
        Vector3 spawnPosition2 = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));

            //spawning
            spawnPosition2.x = Random.Range(-143.9f, -143.91f);
            spawnPosition2.y = Random.Range(2.44f, 2.45f);
            spawnPosition2.z = Random.Range(35.1f, 35.11f);
            GameObject platform3 = Instantiate(_platform3, spawnPosition2, Quaternion.identity);
            platform3.GetComponent<Rigidbody>().AddForce(ImpulseVectorRL, ForceMode.Impulse);
            Destroy(platform3, 4f);

        }
    }

    IEnumerator Platform4()
    {
        //left to right

        Vector3 spawnPosition3 = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            //spawning
            spawnPosition3.x = Random.Range(-163.4f, -163.41f);
            spawnPosition3.y = Random.Range(2.44f, 2.45f);
            spawnPosition3.z = Random.Range(-35.97f, -35.98f);
            GameObject platform4 = Instantiate(_platform4, spawnPosition3, Quaternion.identity);
            platform4.GetComponent<Rigidbody>().AddForce(ImpulseVectorLR, ForceMode.Impulse);
            Destroy(platform4, 4f);

        }
    }
    IEnumerator Platform5()
    {
        //right to left
        Vector3 spawnPosition4 = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));

            //spawning
            spawnPosition4.x = Random.Range(-185f, -185.1f);
            spawnPosition4.y = Random.Range(2.44f, 2.45f);
            spawnPosition4.z = Random.Range(34.73f, 34.74f);
            GameObject platform5 = Instantiate(_platform5, spawnPosition4, Quaternion.identity);
            platform5.GetComponent<Rigidbody>().AddForce(ImpulseVectorRL, ForceMode.Impulse);
            Destroy(platform5, 4f);

        }
    }

    IEnumerator Platform6()
    {
        //left to right

        Vector3 spawnPosition5 = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            //spawning
            spawnPosition5.x = Random.Range(-204.7f, -204.71f);
            spawnPosition5.y = Random.Range(2.44f, 2.45f);
            spawnPosition5.z = Random.Range(-36.18f, -36.181f);
            GameObject platform6 = Instantiate(_platform6, spawnPosition5, Quaternion.identity);
            platform6.GetComponent<Rigidbody>().AddForce(ImpulseVectorLR, ForceMode.Impulse);
            Destroy(platform6, 4f);

        }
    }
    IEnumerator Platform7()
    {
        //right to left
        Vector3 spawnPosition6 = Vector3.zero;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));

            //spawning
            spawnPosition6.x = Random.Range(-223.1f, -223.11f);
            spawnPosition6.y = Random.Range(2.44f, 2.45f);
            spawnPosition6.z = Random.Range(35.1f, 35.11f);
            GameObject platform7 = Instantiate(_platform7, spawnPosition6, Quaternion.identity);
            platform7.GetComponent<Rigidbody>().AddForce(ImpulseVectorRL, ForceMode.Impulse);
            Destroy(platform7, 4f);

        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Platform1());
        StartCoroutine(Platform2());
        StartCoroutine(Platform3());
        StartCoroutine(Platform4());
        StartCoroutine(Platform5());
        StartCoroutine(Platform6());
        StartCoroutine(Platform7());
        GetComponent<Rigidbody>();

    }
}
