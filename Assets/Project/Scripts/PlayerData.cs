using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [Header("Status")]
    public int hp = 50;
    public int mp = 60;
    public int damage = 0;
    public float nowHP = 0;
    public float nowMP = 0f;

    [Header("UI")]
    public Slider HP;
    public Slider MP;

    bool isHit = false;

    void Start()
    {
        nowHP = hp;
        nowMP = mp;
    }
    void Update()
    {
        HP.value = nowHP / hp;
        MP.value = nowMP / mp;

        if (nowHP <= 0)
        {
            nowHP = hp;
            nowMP = mp;
            DemoMapManager demo = FindAnyObjectByType<DemoMapManager>();
            demo.PlayerRespown();
        }
    }
    public IEnumerator GetHit(int damage)
    {
        if(isHit){ yield break; }
        Debug.Log("hittttttt");
        isHit = true;
        nowHP -= damage;

        yield return new WaitForSeconds(3f);

        isHit = false;
    }
    
}
