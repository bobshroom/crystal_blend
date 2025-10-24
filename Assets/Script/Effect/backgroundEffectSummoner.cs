using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class backgroundEffectSummoner : MonoBehaviour
{
    public GameObject backgroundEffect;
    [SerializeField] private float intervalMulti = 1f;
    private int score;
    private int maxMargeLevel = 2;
    [SerializeField] private List<backgroundEffectData> effectDataArray;
    [SerializeField] private FloatRange randomeSpeedRange;
    [SerializeField] private FloatRange randomeRotateRange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("updateScoreAndMargeLevel", 1f, 1f);
        StartCoroutine(summonEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void updateScoreAndMargeLevel()
    {
        if (score != GameManager.instance.score || maxMargeLevel != GameManager.instance.maxMargeLevel)
        {
            score = GameManager.instance.score;
            maxMargeLevel = GameManager.instance.maxMargeLevel;
        }
    }

    void instantEffect(int type)
    {
        GameObject effect = Instantiate(backgroundEffect, new Vector3(Random.Range(-9.0f, 9.0f), 6, 0), Quaternion.identity);
        effect.GetComponent<SpriteRenderer>().sprite = effectDataArray[type].sprite;
        effect.transform.localScale = new Vector3(effectDataArray[type].size, effectDataArray[type].size, 1);
        effect.GetComponent<backgroundEffectController>().fallSpeed = randomeSpeedRange.GetRandom();
        effect.GetComponent<backgroundEffectController>().rotateSpeed = randomeRotateRange.GetRandom();
    }

    /* アイデアをいったん整理する 
        エフェクトは最大でも作成された段階までの宝石とする
        スコアが多ければ多いほどエフェクトも増え、また、エフェクトの質も上がる
        目安:1500～2000金　3000～5000ルビー　7000～10000サファイア　
        500超えるとエフェクトが出始める
        実装方法：最大マージまでのリストを作り、すべてに100を入れる。スコアに応じてリストの中身を加算したり減算したりする
        最終的にリストの要素が大きいほど、高確率で抽選されるようにする。
        */
    IEnumerator summonEffect()
    {
        while (true)
        {
            List<int> effectTypeList = new List<int>();
            if (score < 500)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
            for (int i = 0; i <= maxMargeLevel; i++) effectTypeList.Add(100);   //すべてを100で初期化
            if (score < 2000)// 300 200 100...
            {
                for (int i = 0; i < effectTypeList.Count; i++) effectTypeList[i] += 100 * (effectTypeList.Count - i - 1);
                if (score < 1000) yield return new WaitForSeconds(Random.Range(0.8f, 1f) * intervalMulti);
                else yield return new WaitForSeconds(Random.Range(0.5f, 0.7f) * intervalMulti);
            }
            else if (score < 5000)
            {
                for (int i = 0; i < effectTypeList.Count; i++) effectTypeList[i] += 50 * (effectTypeList.Count - i - 1);    // 400 350 300 250 200 150 100
                for (int i = effectTypeList.Count - 1; i > 2; i--) effectTypeList[i] += 30 * i;                             // 400 350 300 340 320 300 280
                effectTypeList[0] -= 100;
                yield return new WaitForSeconds(Random.Range(0.3f, 0.5f) * intervalMulti);
            }
            else if (score < 10000)
            {
                for (int i = 0; i < 3; i++) effectTypeList[i] = 40; // 40 40 40 100 100 ...
                yield return new WaitForSeconds(Random.Range(0.15f, 0.3f) * intervalMulti);
            }
            else if (score < 15000)
            {
                for (int i = 0; i < effectTypeList.Count; i++) effectTypeList[i] += i * 50;
                for (int i = 0; i < 5; i++) effectTypeList[i] = 5;                                 // 5 5 5 5 5 350 400 450 500
                yield return new WaitForSeconds(Random.Range(0.1f, 0.2f) * intervalMulti);
            }
            else
            {
                for (int i = 0; i < effectTypeList.Count; i++) effectTypeList[i] += i * 50;
                for (int i = 0; i < 5; i++) effectTypeList[i] = 0;                                 // 0 0 0 0 0 350 400 450 500
                yield return new WaitForSeconds(Random.Range(0.07f, 0.1f) * intervalMulti);
            }
            if (MasterGameManager.instance.lowQuality) yield return new WaitForSeconds(0.3f);
            instantEffect(choiceEffectType(effectTypeList));
        }
    }
    int choiceEffectType(List<int> effectTypeList)
    {
        int total = effectTypeList.Sum();
        int randomPoint = Random.Range(0, total);
        int currentPoint = 0;
        for (int i = 0; i < effectTypeList.Count; i++)
        {
            currentPoint += effectTypeList[i];
            if (randomPoint < currentPoint)
            {
                return i;
            }
        }
        return effectTypeList.Count - 1;
    }
}
