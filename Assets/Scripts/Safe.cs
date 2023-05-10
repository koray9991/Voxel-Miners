using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class Safe : MonoBehaviour
{
    public GameObject coin;
    public Transform[] coinSpawnPoint;
    public float x, z;
    public GameManager gm;
    public GameObject[] safes;
    public LeanGameObjectPool coinPool;
    public int randomValue;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    public void CoinSpawn()
    {

        randomValue = Random.Range(1, 3);
        if (randomValue == 1)
        {
            for (int i = 0; i < gm.safeLevel; i++)
            {
                GameObject myCoin = coinPool.Spawn(coinSpawnPoint[Random.Range(0, gm.safeLevel)].position + new Vector3(Random.Range(-0.4f, 0.4f), 0, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Trash").transform);
                myCoin.GetComponent<Coin>().x = x;
                myCoin.GetComponent<Coin>().z = z;
                myCoin.GetComponent<Coin>().collected = false;
                myCoin.GetComponent<Coin>().rb.useGravity = true;
               coinPool.Despawn(myCoin, 15);

                gm.EarnCoinValue(10);
            }
        }
     
       
    }
}
