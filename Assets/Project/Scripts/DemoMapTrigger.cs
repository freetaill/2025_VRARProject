using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoMapTrigger : MonoBehaviour
{
    public enum TriggerType { ChangeStage, SpownEnemy, EndPoint }
    public TriggerType type;
    DemoMapManager demo;

    void Start()
    {
        demo = FindAnyObjectByType<DemoMapManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (type == TriggerType.ChangeStage)
            {
                demo.ChangeStage();
            }
            else if (type == TriggerType.EndPoint)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (type == TriggerType.SpownEnemy && other.transform.CompareTag("Player"))
        {
            Debug.Log("스폰");
            demo.OnMapTrigger(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}
