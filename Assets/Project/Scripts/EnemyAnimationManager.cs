using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAnimationManager : MonoBehaviour
{
    Animator enemyAnim;
    Transform player;
    NavMeshAgent navAgent;

    bool isAttack = false;
    bool isDetect = false;
    bool isMove = true;
    bool isHit = false;
    bool isTracking = false;

    [Header("Status")]
    public float maxHP = 9;
    float nowHP = 0;
    public int damage = 10;

    [Header("UI")]
    public Slider HP;

    [SerializeField]
    public float chaseDistence = 2.0f;
    float attackAngleThreshold = 45f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        enemyAnim = GetComponent<Animator>();

        nowHP = maxHP;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        HP.value = nowHP / maxHP;

        if (isDetect && isTracking)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer > attackAngleThreshold && isMove && distanceToPlayer <= chaseDistence)
            {
                transform.LookAt(player);
            }
            else if (distanceToPlayer <= chaseDistence && isMove && angleToPlayer <= attackAngleThreshold)
            {
                Debug.Log("공격 범위 안에 감지됨: " + player.name);
                navAgent.isStopped = true;
                enemyAnim.SetBool("Walk", false);
                enemyAnim.SetTrigger("Attack");

                // Rotation Lock
                Vector3 dir = player.position - transform.position;
                dir.y = 0f;
                transform.rotation = Quaternion.LookRotation(dir);

                isMove = false; // 이동 불가 상태로 전환
            }
            else if (distanceToPlayer > chaseDistence && isMove)
            {
                navAgent.isStopped = false;
                enemyAnim.SetBool("Walk", true);
                navAgent.SetDestination(player.position);
                transform.LookAt(player);
            }
        }
        else
        {
            enemyAnim.SetBool("Walk", false);
            navAgent.isStopped = true;
        }
    }

    public void OnChildTriggerEnter(EnemyTriggerRelay.TriggerType type, Collider other)
    {
        switch (type)
        {
            case EnemyTriggerRelay.TriggerType.DetectArea:
                if (other.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("탐지 범위 안에 감지됨: " + other.name);
                    isDetect = true;
                    isTracking = true;
                }
                break;
            /*
        case EnemyTriggerRelay.TriggerType.AttackArea:
            if (other.gameObject.CompareTag("Player"))
            {
                //Debug.Log("공격 범위 안에 감지됨: " + other.name);
                isDetect = true;
                navAgent.isStopped = true;
                Vector3 vector = player.transform.position - transform.position;
                if (isMove)
                {
                    transform.rotation = Quaternion.LookRotation(vector).normalized;
                }
                if (transform.rotation == Quaternion.LookRotation(vector))
                {
                    enemyAnim.SetTrigger("Attack");
                }
            }
            break;*/
            case EnemyTriggerRelay.TriggerType.Weapon:
                if (other.gameObject.CompareTag("Shield") && isAttack)
                {
                    enemyAnim.SetTrigger("Defanded");
                    endAttack();
                    endAttackAnime();
                    //Debug.Log("공격 범위 안에 감지됨: " + other.name);
                }
                break;
        }
    }
    public void OnChildTriggerStay(EnemyTriggerRelay.TriggerType type, Collider other)
    {
        switch (type)
        {
            case EnemyTriggerRelay.TriggerType.AttackArea:
                if (other.gameObject.CompareTag("Player"))
                {
                    //if(!enemyAnim.GetBool("Attack"))
                    //Debug.Log("공격 범위 안에 감지됨: " + other.name);
                    navAgent.isStopped = true;
                    Vector3 vector = player.transform.position - transform.position;
                    if (isMove)
                    {
                        transform.rotation = Quaternion.LookRotation(vector).normalized;
                    }
                    if (transform.rotation == Quaternion.LookRotation(vector))
                    {
                        enemyAnim.SetTrigger("Attack");
                    }
                }
                break;
        }
    }
    public void OnChildTriggerExit(EnemyTriggerRelay.TriggerType type, Collider other)
    {
        switch (type)
        {
            case EnemyTriggerRelay.TriggerType.DetectArea:
                if (other.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("탐지 범위를 벗어남: " + other.name);
                    isDetect = false;
                    isTracking = false;
                }
                break;
            case EnemyTriggerRelay.TriggerType.AttackArea:
                if (other.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("공격 범위를 벗어남: " + other.name);
                }
                break;
        }
    }
    public void OnChildCollisionEnter(EnemyCollisionRelay.CollisionType type, Collision collision)
    {
        switch (type)
        {
            case EnemyCollisionRelay.CollisionType.Weapon:
                //Debug.Log("Hitttttt");
                StartCoroutine(Hit(3f));
                break;
            case EnemyCollisionRelay.CollisionType.Magic:
                //Debug.Log(collision.transform.name);
                if (collision.transform.name == "Fireball(Clone)")
                {
                    StartCoroutine(Hit(3f));
                }
                if (collision.transform.name == "IceBall(Clone)")
                {
                    StartCoroutine(Hit(1f));
                }
                break;
        }

    }
    private IEnumerator Hit(float dagmage)
    {
        if (isHit) { yield break; }

        enemyAnim.SetTrigger("Hit");
        endAttack();
        endAttackAnime();
        nowHP -= dagmage;
        isHit = true;
        isDetect = true;
        isTracking = true;
        //Debug.Log("Hitttttt");
        if (nowHP <= 0)
        {
            enemyAnim.SetBool("Die", true);
        }

        yield return new WaitForSeconds(0.6f);

        isHit = false;
    }
    public void freeze()
    {
        //Debug.Log("얼음");
        isMove = false;
        navAgent.isStopped = true;
        enemyAnim.speed = 0f;
    }
    public void unFreeze()
    {
        isMove = true;
        navAgent.isStopped = false;
        enemyAnim.speed = 1f;
    }
    public void startAttack()
    {
        isAttack = true;
        //Debug.Log("공격실행");
    }
    public void endAttack()
    {
        isAttack = false;
        //Debug.Log("공격종료");
    }
    public void startAttackAnime()
    {
        //Debug.Log("공격 모션 실행");
        isMove = false;
    }
    public void endAttackAnime()
    {
        //Debug.Log("공격 모션 종료");
        isMove = true;
    }
    public void DiedEnemy()
    {
        //Debug.Log("죽음");
        Destroy(gameObject);
    }
}
