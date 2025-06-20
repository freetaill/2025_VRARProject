using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    public float duration = 2.9f;
    private void Start()
    {
        StartCoroutine(FreezeCycle());
    }
    private class FrozenEnemy
    {
        public EnemyAnimationManager enemy;
    }

    private List<FrozenEnemy> frozenEnemies = new List<FrozenEnemy>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("감지됨");
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            Debug.Log("적 감지됨: " + other.name);

            // 중복 정지 방지
            bool alreadyFrozen = frozenEnemies.Exists(e => e.enemy == enemy);
            if (alreadyFrozen) return;

            enemy.GetComponent<EnemyAnimationManager>().freeze();

            frozenEnemies.Add(new FrozenEnemy { enemy = enemy.GetComponent<EnemyAnimationManager>() });
        }
    }

    private IEnumerator FreezeCycle()
    {
        yield return new WaitForSeconds(duration);

        foreach (var frozen in frozenEnemies)
        {
            frozen.enemy.unFreeze();
        }
    }
}
