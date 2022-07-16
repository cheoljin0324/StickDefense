using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl_Normal : MonoBehaviour
{
    public Transform EnemyTransform;
    public enum PlayerState {Idle, Move, Battle , Attack, Die, Skill}
    PlayerState playerState = PlayerState.Idle;
    private float speed = 1f;
    public GameObject AttackTarget;
    MobData myhp;
    public float AttackSpd = 1f;
    public int AttackAge = 3;

    private void Awake()
    {
        EnemyTransform = GameObject.Find("EnemySpawn").GetComponent<Transform>();
        myhp = GetComponent<MobData>();
    }

    void Start()
    {
        playerState = PlayerState.Move;
        EnemyTransform.position = new Vector3(EnemyTransform.position.x, -1.23f, 0f);
    }

    void Update()
    {
        CheckState();
    }


    protected void Move()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, EnemyTransform.position, speed/100);
    }
    void Battle()
    {
        if (AttackTarget == null)
        {
            Debug.LogWarning("Missing");
            AttackTarget = null;
            playerState = PlayerState.Move;
        }
        else
        {
            StartCoroutine("Attack");
        }  
    }

    public virtual IEnumerator Attack()
    {
        playerState = PlayerState.Attack;
        yield return new WaitForSeconds(5f-AttackSpd);
        if (AttackTarget == null)
        {
            playerState = PlayerState.Move;
            Debug.LogWarning("Missing");
            AttackTarget = null;
            StopCoroutine("Attack");
        }
        else if (AttackTarget.GetComponent<MobData>().hp < 1)
        {
            playerState = PlayerState.Move;
            AttackTarget = null;
            StopCoroutine("Attack");

        }
        if (AttackTarget != null)
        {
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
        
       
    }

    protected void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Die");
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
        if (collision.CompareTag("Enemy"))
        {
            playerState = PlayerState.Battle;
            AttackTarget = collision.gameObject;
        }
    }

}
