using UnityEngine;

public class EnemyCollisionRelay : MonoBehaviour
{
    public enum CollisionType {Weapon, Magic}
    private EnemyAnimationManager enemyController;
    void Start()
    {
        enemyController = GetComponentInParent<EnemyAnimationManager>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            //Debug.Log("데미지Hit!");
            enemyController.OnChildCollisionEnter(CollisionType.Weapon, collision);
        }
        else if (collision.gameObject.CompareTag("Magic"))
        {
            //Debug.Log("데미지Hit!");
            enemyController.OnChildCollisionEnter(CollisionType.Magic, collision);
        }
    }
}
