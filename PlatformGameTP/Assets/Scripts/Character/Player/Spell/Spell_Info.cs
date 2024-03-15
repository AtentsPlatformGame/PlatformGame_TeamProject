using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Info : MonoBehaviour
{
    public enum SPELLTYPE
    {
        None,
        Attack,
        Buff
    }
    protected Image _spellIcon;
    protected SPELLTYPE _spellType = SPELLTYPE.None;
    protected string _spellName;
    protected string _spellDescription;

    public Image spellIcon 
    {
        get => this._spellIcon;
        protected set
        {
            this._spellIcon = value;
        }
    }
    public SPELLTYPE spellType
    {
        get => this._spellType;
        
        protected set
        {
            this._spellType = value;
        }
    }
    
    public string spellName
    {
        get => this._spellName;
        protected set
        {
            this._spellName = value;
        }
    }
    public string spellDescription
    {
        get => this._spellDescription;
        protected set
        {
            this._spellDescription = value;
        }
    }

}
