using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    public void Left()
    {
        enemy.Move(-1.5f);
    }
    public void Right()
    {
        enemy.Move(1.5f);
    }
    public void MoveStop()
    {
        enemy.Move(0);
    }

    public void Attack()
    {
        enemy.Attack();
    }
    public void Hit()
    {
        enemy.GetComponent<IHitable>().IDamage(0);
    }
    public void Dead()
    {
        enemy.GetComponent<IHitable>().IDamage(999);
    }
    public void Respawn()
    {
        enemy.isPlaying = true;
        enemy.gameObject.SetActive(true);
        Attack();
    }


}
