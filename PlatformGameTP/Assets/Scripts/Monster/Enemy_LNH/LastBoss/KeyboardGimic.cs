using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardGimic :MonoBehaviour
{
    public enum ARROW
    {
        UP, DOWN, LEFT, RIGHT
    }

    public ARROW myArrow;
    public int GetMyArrow()
    {
        return (int)myArrow;
    }

    public void SetMyArrow(int idx)
    {
        myArrow = (ARROW)idx;
    }
}
