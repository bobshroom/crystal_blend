using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;
    public int currentCombo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        currentCombo = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddCombo()
    {
        currentCombo += 1;
        if (currentCombo - 1 > GameManager.instance.maxCombo)
        {
            GameManager.instance.maxCombo = currentCombo - 1;
            MasterGameManager.instance.upLoadMaxComboToUnityroom();
        }
    }
    public void ResetCombo()
    {
        currentCombo = 0;
    }
}
