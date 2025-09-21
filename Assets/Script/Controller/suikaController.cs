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
    [SerializeField] private Sprite defsp;
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
                GameManager.instance.mergedSuikaCount += 1;
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
        float changeSize = CurrentSuikaData.size * defoltSize * CurrentSuikaData.sizeOffset;
        GetComponent<CircleCollider2D>().radius = 0.5f / CurrentSuikaData.sizeOffset;
        transform.localScale = new Vector3(changeSize, changeSize, 1);
        if (CurrentSuikaData.sprite != null) sr.sprite = CurrentSuikaData.sprite;
        else sr.sprite = defsp;
        sr.color = CurrentSuikaData.color;
    }
}
