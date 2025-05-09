using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClarTerrainMaterial : MonoBehaviour
{
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrain.materialTemplate = null;
            Debug.Log("Terrain 머티리얼 제거 완료!");
        }
    }
}
