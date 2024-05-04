using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Attack : Spell_Info
{
    [SerializeField] LayerMask spellMask;
    [SerializeField] float spellDmg;
    public AudioSource attackSpellAudioSource;
    // Start is called before the first frame update
    void Awake()
    {
        this.spellType = SPELLTYPE.Attack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if((1 << other.gameObject.layer & spellMask) != 0)
        {
            if(attackSpellAudioSource != null)
            {
                attackSpellAudioSource.volume = SoundManager.Instance.soundValue;
                attackSpellAudioSource.Play();
            }
            BattleSystem bs = other.gameObject.GetComponent<BattleSystem>();
            Debug.Log($"{spellDmg} 만큼의 데미지를 입힘");
            if(bs != null)
            {
                bs.TakeDamage(spellDmg);
            }
        }
    }

    private void OnParticleTrigger()
    {
        
    }

}
