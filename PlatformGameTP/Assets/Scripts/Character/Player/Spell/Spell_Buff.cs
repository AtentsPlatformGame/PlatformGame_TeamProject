using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spell_Buff : Spell_Info
{
    public UnityEvent BuffActs;
    // Start is called before the first frame update
    void Awake()
    {
        spellType = SPELLTYPE.Buff;
    }

    void OnEnable()
    {
        BuffActs?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
