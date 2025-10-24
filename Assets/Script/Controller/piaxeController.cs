using UnityEngine;

public class piaxeController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateQuantity;
    [SerializeField] private ParticleSystem ps;
    private bool isPlayedps = false;
    private float startRotate;
    private float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startRotate = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * rotateSpeed;
    }
    void FixedUpdate()
    {
        float angle = Mathf.Abs(Mathf.Sin(time)) * rotateQuantity;
        transform.rotation = Quaternion.Euler(0, 0, startRotate + angle);
        if (Mathf.Abs(Mathf.Sin(time)) != Mathf.Sin(time) && !isPlayedps)
        {
            ps.Play();
            isPlayedps = true;
        }
        if (Mathf.Abs(Mathf.Sin(time)) == Mathf.Sin(time) && isPlayedps)
        {
            ps.Play();
            isPlayedps = false;
        }
    }
}
