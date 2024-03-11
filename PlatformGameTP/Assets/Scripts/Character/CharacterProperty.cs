using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    Animator _anim = null;
    public Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    Renderer[] _allRenderer;

    public Renderer[] allRenderer
    {
        get
        {
            if (_allRenderer == null)
            {
                _allRenderer = GetComponentsInChildren<Renderer>();
            }
            return _allRenderer;
        }
    }
}
