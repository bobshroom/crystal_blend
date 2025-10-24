using UnityEngine;

public class backgroundEffectController : MonoBehaviour
{
    public float rotateSpeed = 1f;
    public float fallSpeed = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
        transform.position += new Vector3(0, -fallSpeed, 0);
        if (transform.position.y < -10) Destroy(this.gameObject);
    }
}