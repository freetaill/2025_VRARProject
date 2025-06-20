using UnityEngine;

public class EnemyTriggerRelay : MonoBehaviour
{
    public enum TriggerType { DetectArea, AttackArea, Weapon, Shield }
    public TriggerType triggerType;

    private EnemyAnimationManager enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyAnimationManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Shield"))
        {
            enemyController.OnChildTriggerEnter(triggerType, other);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyController.OnChildTriggerEnter(triggerType, other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyController.OnChildTriggerExit(triggerType, other);
        }
    }
}
