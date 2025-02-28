using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private float hp;
    [SerializeField] private float attackPower;


    private void Awake()
    {
        hp = 100;
    }
    void Start()
    {
    }

    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void Damage(float damage)
    {
        hp -= damage;
    }
}
