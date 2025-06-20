using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectManager : MonoBehaviour
{
    public enum magicType { FireBall, IceBall };
    public magicType type;

    public float duration = 2.9f;

    void Start()
    {
        switch (type)
        {
            case magicType.FireBall:
                Invoke("FireBallEffect", 1f);
                break;
            case magicType.IceBall:
                Invoke("IceBallEffect", 3f);
                StartCoroutine(FreezeCycle());
                break;
        }
    }
    void FireBallEffect()
    {
        Debug.Log("이펙트 종료(fire)");
        Destroy(gameObject);
    }

    void IceBallEffect()
    {
        Debug.Log("이펙트 종료(ice)");
        Destroy(gameObject);
    }

    private class FrozenEnemy
    {
        public EnemyAnimationManager enemy;
    }

    private List<FrozenEnemy> frozenEnemies = new List<FrozenEnemy>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("감지됨");
        if (other.transform.CompareTag("Enemy"))
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
