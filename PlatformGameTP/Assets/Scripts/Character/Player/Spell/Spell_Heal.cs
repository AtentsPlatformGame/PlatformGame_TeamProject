using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spell_Heal : Spell_Info
{
    PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        spellType = SPELLTYPE.Buff;
    }

    void OnEnable()
    {
        player = FindObjectOfType<PlayerController>();
        player.HealBuff();
    }

    
}
