using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spell_Info;

public class Spell_SpeedBuff : Spell_Info
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
        player.SpeedBuff();
    }
}
