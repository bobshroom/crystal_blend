using System.Collections.Generic;
using UnityEngine;

public class suikaController : MonoBehaviour
{
    [SerializeField] private List<SuikaData> suikaDataList;
    private int maxLevel;
    public int level;
    public SuikaData CurrentSuikaData => suikaDataList[level];
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    [SerializeField] private float defoltSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxLevel = suikaDataList.Count - 1;
        
        reSize();
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
                SoundManager.instance.PlaySound("suikaMerge");
                ComboManager.instance.AddCombo();
                UiManagerGame.instance.ComboEffect(transform.position, ComboManager.instance.currentCombo);
                reSize();
                transform.position = (transform.position + collision.transform.position) / 2;
                GameManager.instance.AddScore(CurrentSuikaData.score * ComboManager.instance.currentCombo);
                Destroy(collision.gameObject);
            }
        }
    }
    void reSize()
    {
        transform.localScale = new Vector3(CurrentSuikaData.size * defoltSize, CurrentSuikaData.size * defoltSize, 1);
        //sr.sprite = CurrentSuikaData.sprite;
        sr.color = CurrentSuikaData.color;
    }
}
