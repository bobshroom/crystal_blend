using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (transform.position.y < -10f)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (MasterGameManager.instance.gameState == "title")
        {
            suikaController otherSuika = collision.gameObject.GetComponent<suikaController>();
            if (collision.gameObject.CompareTag("suika"))
            {
                if (otherSuika.level == level && level < maxLevel && GetInstanceID() < collision.gameObject.GetInstanceID())
                {
                    level++;
                    reSize();
                    transform.position = (transform.position + collision.transform.position) / 2;
                    Destroy(collision.gameObject);
                    return;
                }
            }
        }
        if (collision.gameObject.CompareTag("suika"))
        {
            suikaController otherSuika = collision.gameObject.GetComponent<suikaController>();
            if (otherSuika.level == level && level < maxLevel && GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                GameManager.instance.mergedSuikaCount += 1;
                level++;
                if (GameManager.instance.maxMargeLevel < level) GameManager.instance.maxMargeLevel = level;
                SoundManager.instance.PlaySE("suikaMerge");
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
        if (MasterGameManager.instance.gameState == "title")
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            sr.enabled = false;
            GetComponent<UnityEngine.UI.Image>().enabled = true;
            GetComponent<UnityEngine.UI.Image>().sprite = CurrentSuikaData.sprite;
            GetComponent<UnityEngine.UI.Image>().color = CurrentSuikaData.color;
            transform.localScale = new Vector3(3f * CurrentSuikaData.sizeOffset * CurrentSuikaData.size * defoltSize, 3f * CurrentSuikaData.sizeOffset * CurrentSuikaData.size * defoltSize, 1);
            GetComponent<CircleCollider2D>().radius = 0.15f / CurrentSuikaData.sizeOffset;
        }
    }
}
