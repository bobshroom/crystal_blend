using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveRange;
    [SerializeField] private GameObject suika;
    [SerializeField] private float interval;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timer <= 0)
        {
            GameObject instance = Instantiate(suika);
            instance.transform.position = transform.position + new Vector3(0, -1.0f, 0);
            timer = interval;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > moveRange) mousePosition.x = moveRange;
        else if (mousePosition.x < -moveRange) mousePosition.x = -moveRange;
        Vector3 newPosition = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
