using UnityEngine;

public class DemoPlayManager : MonoBehaviour
{
    public GameObject suika;
    public Transform suikaParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SummonSuika", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SummonSuika()
    {
        GameObject instans = Instantiate(suika, suikaParent);
        instans.GetComponent<suikaController>().level = Random.Range(0, 5);
        instans.transform.Rotate(0, 0, Random.Range(0f, 360f));
        instans.transform.localPosition = new Vector3(Random.Range(-8f, 8f), 8, 0);
    }
}
