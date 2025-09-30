using UnityEngine;

public class goMarkSpin : MonoBehaviour
{
    public float speed;
    public float kakudo;
    private float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(time * speed) * kakudo);
    }
}
