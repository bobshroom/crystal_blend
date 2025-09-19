using UnityEngine;

public class SuikaManager : MonoBehaviour
{
    public static SuikaManager instance;
    [SerializeField] private GameObject suika;
    public int nextSuika;
    private GameObject currentSuika;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform ParentTransform;
    [SerializeField] private int randomSuikaRangeMin;
    [SerializeField] private int randomSuikaRangeMax;
    [SerializeField] private float interval;
    private bool isDroppable = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        nextSuika = Random.Range(randomSuikaRangeMin, randomSuikaRangeMax-1);
        summonSuika();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropSuika()
    {
        if (currentSuika != null && isDroppable)
        {
            isDroppable = false;
            currentSuika.transform.parent = ParentTransform;
            currentSuika.GetComponent<CircleCollider2D>().enabled = true;
            currentSuika.GetComponent<Rigidbody2D>().gravityScale = 1;
            currentSuika = null;
            Invoke("summonSuika", interval);
        } else {
            Debug.LogError("currentSuikaが指定されていません。");
        }
    }
    void summonSuika()
    {
        currentSuika = Instantiate(suika, new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, 0), Quaternion.identity);
        currentSuika.transform.parent = PlayerTransform;
        currentSuika.GetComponent<CircleCollider2D>().enabled = false;
        currentSuika.GetComponent<Rigidbody2D>().gravityScale = 0;
        suikaController suikaCtrl = currentSuika.GetComponent<suikaController>();
        suikaCtrl.level = nextSuika;
        nextSuika = Random.Range(randomSuikaRangeMin, randomSuikaRangeMax - 1);
        isDroppable = true;
    }
}
