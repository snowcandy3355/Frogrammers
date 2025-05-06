using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
    }

    public void SetTriggerMode(bool value)
    {
        col.isTrigger = value;
    }
}
