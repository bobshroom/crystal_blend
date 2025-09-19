using System.Collections.Generic;
using UnityEngine;

public class suikaController : MonoBehaviour
{
    [SerializeField] private List<SuikaData> suikaDataList;
    private int maxLevel;
    public int level;
    public SuikaData CurrentSuikaData => suikaDataList[level];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxLevel = suikaDataList.Count - 1;
        transform.localScale = new Vector3(CurrentSuikaData.size, CurrentSuikaData.size, 1);
        //GetComponent<SpriteRenderer>().sprite = CurrentSuikaData.sprite;
        GetComponent<SpriteRenderer>().color = CurrentSuikaData.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suika"))
        {
            suikaController otherSuika = collision.gameObject.GetComponent<suikaController>();
            if (otherSuika.level == level && level < maxLevel && GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                level++;
                transform.localScale = new Vector3(CurrentSuikaData.size, CurrentSuikaData.size, 1);
                transform.position = (transform.position + collision.transform.position) / 2;
                //GetComponent<SpriteRenderer>().sprite = CurrentSuikaData.sprite;
                GetComponent<SpriteRenderer>().color = CurrentSuikaData.color;
                GameManager.instance.AddScore(CurrentSuikaData.score);
                Destroy(collision.gameObject);
            }
        }
    }
}
