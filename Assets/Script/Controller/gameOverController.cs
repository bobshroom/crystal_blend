using System.Collections;
using UnityEngine;

public class gameOverController : MonoBehaviour
{
    private Vector3 finalPos;
    private Vector3 finalScale;
    public GameObject stoneTipsPrefab;
    public float gameOverStoneTipsx = 2.5f;
    public float gameOverStoneTipsy = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalPos = transform.position;
        finalScale = transform.localScale;
        StartCoroutine(moveLogo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator moveLogo()
    {
        float time = 0;
        Vector3 startPos = new Vector3(0, 5, 0);
        Vector3 startScale = new Vector3(200f, 200f, 1);
        while (time < 0.1f)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, finalPos, time * 10);
            transform.localScale = Vector3.Lerp(startScale, finalScale, time * 10);
            yield return null;
        }
        transform.position = finalPos;
        transform.localScale = finalScale;
        SoundManager.instance.PlaySound("gameOverInto");

        for (int i = 0; i < 20; i++)    //石チップスを20個生成
        {
            makeStoneTips(new Vector3(Random.Range(-gameOverStoneTipsx, gameOverStoneTipsx), Random.Range(-gameOverStoneTipsy, gameOverStoneTipsy) + 3, 0));
        }

        for (int i = 0; i < 10; i++)    //振動させる
        {
            transform.position = finalPos + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = finalPos;
    }
    void makeStoneTips(Vector3 pos)
    {
        Instantiate(stoneTipsPrefab, pos, new Quaternion(0, 0, UnityEngine.Random.Range(0, 360), 0));
    }
}
