using UnityEngine;

public class PlayerTriggerRelay : MonoBehaviour
{
    public GameObject player;
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("감지함");
        if (other.transform.CompareTag("E_Weapon"))
        {
            //Debug.Log("맞음");
            StartCoroutine(player.GetComponent<PlayerData>().GetHit(10));
        }
    }
}
