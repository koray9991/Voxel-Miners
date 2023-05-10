using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Power : MonoBehaviour
{
    public GameManager gm;
    public float initialPower;
    public MainObject mainObject;
    
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        mainObject = FindObjectOfType<MainObject>();
    }

    public void PowerUpgrade()
    {
        if (!gm.bigManBool)
        {
            //initialPower = gm.hitTime;
            //gm.hitTime = 0f;
            gm.bigManBool = true;
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Players").transform.childCount; i++)
            {
                //FindObjectsOfType<Player>()[i].mesh.material = FindObjectsOfType<Player>()[i].mats[1];
                //FindObjectsOfType<Player>()[i].powerParticle.SetActive(true);
                GameObject.FindGameObjectWithTag("Players").transform.GetChild(i).gameObject.SetActive(false);
            }
            var myBigMan = Instantiate(gm.bigMan, mainObject.transform.position, Quaternion.identity);


            DOVirtual.DelayedCall(gm.bigManTime, () => {
                //gm.hitTime = initialPower;
                
                Destroy(gameObject);
            });
            gameObject.SetActive(false);
        }
       

    }
}
