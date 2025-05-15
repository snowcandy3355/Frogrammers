using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveClearFlower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        transform.DOMove(transform.position + new Vector3(0, 2f, 0), 2f).OnComplete(() =>
        {
            transform.DOMove(transform.position + new Vector3(0, -2f, 0), 2f).OnComplete(() =>
            {
                Move();
            });
        });
    }
}
