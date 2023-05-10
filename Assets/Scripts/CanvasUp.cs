using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CanvasUp : MonoBehaviour
{
    public Transform target;
    private void Start()
    {
        target.DOMoveY(10, 3).OnComplete(() => target.gameObject.SetActive(false));
    }
}
