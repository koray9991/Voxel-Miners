using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EmissionColor : MonoBehaviour
{
    public MeshRenderer mr;
    public float r;

    private void Start()
    {
        Up();
    }
    private void Update()
    {
        mr.material.SetColor("_EmissionColor", new Color(r, 0, 0));
    }
    public void Up()
    {
        DOTween.To(x => r = x, r, 1, 0.5f).OnComplete(() => Down());
    }
    public void Down()
    {
        DOTween.To(x => r = x, r, 0, 0.5f).OnComplete(() => Up());
    }
}
