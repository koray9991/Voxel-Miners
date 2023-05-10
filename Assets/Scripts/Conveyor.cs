using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    MeshRenderer mr;
    float yOffset;
    float xOffset;
    public float ySpeed;
    public float xSpeed;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

 
    void FixedUpdate()
    {
        yOffset += Time.fixedDeltaTime * ySpeed;
        xOffset += Time.fixedDeltaTime * xSpeed;
        mr.material.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}
