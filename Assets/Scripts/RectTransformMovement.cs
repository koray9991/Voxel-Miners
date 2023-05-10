using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RectTransformMovement : MonoBehaviour
{
    public Transform target;
    public float maxY, minY;
    public bool move = true;
    private void Start()
    {
        if (move)
        {
            Up();
        }
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
      
    }
    private void Update()
    {
        transform.LookAt(target);
    }
    public void Up()
    {
        transform.DOMoveY(maxY, 1).OnComplete(() => Down());
    }
    public void Down()
    {
        transform.DOMoveY(minY, 1).OnComplete(() => Up());
    }
}
