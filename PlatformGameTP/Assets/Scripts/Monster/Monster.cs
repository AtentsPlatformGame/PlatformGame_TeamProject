using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : BattleSystem
{
    public int maxHP = 5;
    public int currentHP;
    public int attackDamage = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        Initialize();
    }

    public new void TakeDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            Die();
        }
        Debug.Log("TakeDamage");
    }

    void Die()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
