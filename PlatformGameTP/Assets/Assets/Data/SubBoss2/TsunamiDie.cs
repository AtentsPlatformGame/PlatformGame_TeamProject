using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiDie : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("쓰나미 충돌!!!!");
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("쓰나미 충돌!!!!");
    }
}
