using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobData : MonoBehaviour
{
    protected int _hp;
    public int HP
    {
        get { return _hp; }
        protected set { _hp = value;}
    }
    protected float AttackSpd = 1f;
    protected int AttackAge = 3;
    protected enum PlayerState { Idle, Move, Battle, Attack, Die, Skill }
    protected PlayerState playerState = PlayerState.Idle;
    protected float speed = 0.2f;

    public void OnDamaging(int damage)
    {
        HP -= damage;
    }
}
