using System.Collections.Generic;
using UnityEngine;

public class DemoMapManager : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject Tutorial_UI;

    [Header("SpownPoints")]
    public List<Transform> spownPoints = new List<Transform>();
    List<GameObject> spownPointsTutorial = new List<GameObject>();
    List<GameObject> spownPointsCave = new List<GameObject>();
    List<GameObject> spownPointsJail = new List<GameObject>();
    List<GameObject> spownPointsTemple = new List<GameObject>();

    [Header("Trigger Wall")]
    public List<GameObject> triggerTrutorial = new List<GameObject>();
    public List<GameObject> triggerCave = new List<GameObject>();
    public List<GameObject> triggerJail = new List<GameObject>();
    public List<GameObject> triggerTemple = new List<GameObject>();

    [Header("PlayerSpownPoint")]
    public GameObject player;
    public List<Transform> playerSpownPoint = new List<Transform>();
    public int SpownNum = 0;

    [Header("BattleStage")]
    public GameObject stageCave;
    public GameObject stageTemple;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataManager data = FindAnyObjectByType<DataManager>();
        
        SpownNum = data.spownpointNum;

        if (SpownNum == 0)
        {
            player.transform.position = playerSpownPoint[0].position;
        }
        else if (SpownNum == 1)
        {
            player.transform.position = playerSpownPoint[1].position;
            triggerTrutorial[1].GetComponent<BoxCollider>().isTrigger = false;
        }
        
        for (int i = 0; i < spownPoints[0].childCount; i++)
        {
            spownPointsTutorial.Add(spownPoints[0].GetChild(i).gameObject);
        }
        for (int i = 0; i < spownPoints[1].childCount; i++)
        {
            spownPointsCave.Add(spownPoints[1].GetChild(i).gameObject);
        }
        for (int i = 0; i < spownPoints[2].childCount; i++)
        {
            spownPointsJail.Add(spownPoints[2].GetChild(i).gameObject);
        }
        for (int i = 0; i < spownPoints[3].childCount; i++)
        {
            spownPointsTemple.Add(spownPoints[3].GetChild(i).gameObject);
        }
    }
    public void OnMapTrigger(GameObject wall)
    {
        if (wall.transform.CompareTag("Tutorial") && wall == triggerTrutorial[0])
        {
            Debug.Log("튜토리얼 스폰");
            SpownNum = 0;
            SpownTutorial();
        }
        else if (wall.transform.CompareTag("Cave") && wall == triggerCave[0])
        {
            Debug.Log("동굴 스폰");
            Tutorial_UI.SetActive(false);
            SpownNum = 1;
            SpownCave();
        }
        else if (wall.transform.CompareTag("Jail") && wall == triggerJail[0])
        {
            Debug.Log("감옥 스폰");
            SpownJail();
        }
        else if (wall.transform.CompareTag("Temple") && wall == triggerJail[1])
        {
            Debug.Log("신전 스폰");
            SpownTemple();
        }
    }

    public void SpownTutorial()
    {
        foreach (GameObject spown in spownPointsTutorial)
        {
            spown.GetComponent<EnemySpawnPoint>().SpownEnemy();
        }
    }
    public void SpownCave()
    {
        foreach (GameObject spown in spownPointsCave)
        {
            spown.GetComponent<EnemySpawnPoint>().SpownEnemy();
        }
        stageCave.SetActive(true);
    }
    public void SpownJail()
    {
        foreach (GameObject spown in spownPointsJail)
        {
            spown.GetComponent<EnemySpawnPoint>().SpownEnemy();
        }
    }
    public void SpownTemple()
    {
        foreach (GameObject spown in spownPointsTemple)
        {
            spown.GetComponent<EnemySpawnPoint>().SpownEnemy();
        }
        stageTemple.SetActive(true);
    }
    public void ChangeStage()
    {
        SpownNum = 2;
        player.transform.position = playerSpownPoint[SpownNum].position;
    }
    public void PlayerRespown()
    {
        player.transform.position = playerSpownPoint[SpownNum].position;
    }
}
