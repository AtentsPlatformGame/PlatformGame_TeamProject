using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savethis : MonoBehaviour
{
    public static Vector3 savedPosition;
    void Start()
    {
        savedPosition = transform.position;
    }
}
