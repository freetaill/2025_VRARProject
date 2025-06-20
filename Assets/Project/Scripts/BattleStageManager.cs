using System.Collections.Generic;
using UnityEngine;

public class BattleStageManager : MonoBehaviour
{
    public GameObject door;

    private List<GameObject> enemiesInZone = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            door.GetComponent<MeshCollider>().convex = true;
        }
        if (other.transform.CompareTag("Enemy") && !enemiesInZone.Contains(other.gameObject))
        {
            enemiesInZone.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInZone.Remove(other.gameObject);
        }
    }
    private void Update()
    {
        enemiesInZone.RemoveAll(e => e == null); // 죽은 적 제거

        if (enemiesInZone.Count == 0)
        {
            Debug.Log("영역 내 적 없음");
            door.GetComponent<MeshCollider>().convex = false;
            
            this.gameObject.SetActive(false);
        }
    }
}
