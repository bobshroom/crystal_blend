using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float timeScale = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void SetTimeScale(float scale)
    {
        timeScale = scale;
        Time.timeScale = timeScale;
    }
    public void Pause(bool isPause)
    {
        if (!isPause)
        {
            Time.timeScale = timeScale;
            return;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
