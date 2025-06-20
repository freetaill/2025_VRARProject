using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject Enemy;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    public void SpownEnemy()
    {
        GameObject enemy = Instantiate(Enemy, transform.position, Quaternion.identity);
        enemy.transform.parent = this.transform;
        enemy.transform.rotation = enemy.transform.parent.rotation;
    }
}
