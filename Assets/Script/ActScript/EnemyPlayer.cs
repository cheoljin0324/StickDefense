using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour
{
    Transform EnemyTransform;
    public enum PlayerState { Idle, Move, Battle, Attack, Die, Skill }
    PlayerState playerState = PlayerState.Idle;
    private float speed = 0.1f;
    public GameObject AttackTarget;
    MobData myhp;
    public int Sethp;
    public float AttackSpd = 1f;
    public int AttackAge = 3;

    private void Awake()
    {
        myhp = GetComponent<MobData>();
        Sethp = myhp.hp;
    }

    void Start()
    {
        EnemyTransform = GameObject.Find("Player").GetComponent<Transform>();
        EnemyTransform.position = new Vector3(EnemyTransform.position.x, -1.23f, 0f);
    }

    private void OnEnable()
    {
        myhp.hp = Sethp;
        playerState = PlayerState.Move;
        if(EnemyTransform != null) EnemyTransform.position = new Vector3(EnemyTransform.position.x, -1.23f, 0f);
        
    }

    void Update()
    {
        CheckState();
    }


    protected void Move()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, EnemyTransform.position, speed / 100);
    }
    void Battle()
    {
        StartCoroutine("Attack");
    }

    public virtual IEnumerator Attack()
    {
        playerState = PlayerState.Attack;
        yield return new WaitForSeconds(5f - AttackSpd);
        if (AttackTarget.GetComponent<MobData>().hp < 1)
        {
            playerState = PlayerState.Move;
            StopCoroutine("Attack");

        }
        AttackTarget.GetComponent<MobData>().hp -= 3;

        for (int i = 0; i < 3; i++)
        {
            AttackTarget.GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(0.3f);
            AttackTarget.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }


        playerState = PlayerState.Battle;
    }

    protected void Die()
    {
        PoolManager.DestroyAPS(gameObject);
        Debug.Log("Die");
        playerState = PlayerState.Idle;
    }

    protected void CheckState()
    {
        if (myhp.hp < 1)
        {
            playerState = PlayerState.Die;
        }

        switch (playerState)
        {
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Battle:
                Battle();
                break;
            case PlayerState.Die:
                Die();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCard"))
        {
            playerState = PlayerState.Battle;
            AttackTarget = collision.gameObject;
        }
    }
}
