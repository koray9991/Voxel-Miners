using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coinCount;
    public Safe safe;
    public GameObject player;
    public MainObject mainObject;
    public int playerCountInStart;
    public float hitTime;
    public float health;
    public int safeLevel;



    public int maxPlayerCount;
    public float minHitTime;
    public float maxHealth;
    public int maxSafeLevel;

    public GameObject safeMaxImage;
    public GameObject playerMaxImage;
    public GameObject healthMaxImage;
    public GameObject hitMaxImage;



    public GameObject upgradePanel;

    int safeCost;
    public TextMeshProUGUI safeCostText;
    int playerCountCost;
    public TextMeshProUGUI playerCountCostText;
    int hitTimeCost;
    public TextMeshProUGUI hitTimeCostText;
    int healthCost;
    public TextMeshProUGUI healthCostText;

    public float cubeCount;
    public int currentPlayerCount;
    public bool failBool;
    public bool winBool;
    public GameObject failPanel;
    public GameObject winPanel;
    public float cubeCountTimer;
    public float maxCubeCount;

    public TextMeshProUGUI playerText;
    public CinemachineVirtualCamera startCam;
    public Image progressBar;
    public bool spawnWithButton;
    public GameObject bigMan;
    public GameObject bigManCanvas;
    public Image bigManImage;
    public TextMeshProUGUI bigManText;
    public bool bigManBool;
    public float bigManTimer;
    public float bigManTime;
    public TextMeshProUGUI levelText;
    public int level;
    int index;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineTransposer transposer;
    public float transposerValue;
    float transposerTimer;
    public AudioClip[] voices;
    private void Awake()
    {
        index = PlayerPrefs.GetInt("index");
        if (index != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(index);
        }

        level = PlayerPrefs.GetInt("level"); 
        if (level == 0) 
        { 
            level = 1;
            PlayerPrefs.SetInt("level", level);
            coinCount = 1000;
            PlayerPrefs.SetInt("coinCount", coinCount);
        }
        levelText.text = "LEVEL " + level;



        coinCount = PlayerPrefs.GetInt("coinCount", coinCount);
        coinText.text = coinCount.ToString();


        playerCountInStart = PlayerPrefs.GetInt("playerCountInStart");
        if (playerCountInStart == 0)
        {
            playerCountInStart = 5;
            PlayerPrefs.SetInt("playerCountInStart", playerCountInStart);
        }
        currentPlayerCount = playerCountInStart;
        playerText.text = currentPlayerCount + "/" + playerCountInStart;
        playerCountCost = PlayerPrefs.GetInt("playerCountCost");
        if (playerCountCost == 0)
        {
            playerCountCost = 10;
            PlayerPrefs.SetInt("playerCountCost", playerCountCost);
        }
        playerCountCostText.text = playerCountCost.ToString();
        if (playerCountInStart >= maxPlayerCount)
        {
            playerMaxImage.SetActive(true);
        }
        else
        {
            playerMaxImage.SetActive(false);
        }






        hitTime = PlayerPrefs.GetFloat("hitTime");
        if (hitTime == 0)
        {
            hitTime = 1;
            PlayerPrefs.SetFloat("hitTime", hitTime);
        }

        hitTimeCost = PlayerPrefs.GetInt("hitTimeCost");
        if (hitTimeCost == 0)
        {
            hitTimeCost = 50;
            PlayerPrefs.SetInt("hitTimeCost", hitTimeCost);
        }
        hitTimeCostText.text = hitTimeCost.ToString();
        if (hitTime <= minHitTime)
        {
            hitMaxImage.SetActive(true);
        }
        else
        {
            hitMaxImage.SetActive(false);
        }





        health = PlayerPrefs.GetFloat("health");
        if (health == 0)
        {
            health = 5;
            PlayerPrefs.SetFloat("health", health);
        }

        healthCost = PlayerPrefs.GetInt("healthCost");
        if (healthCost == 0)
        {
            healthCost = 100;
            PlayerPrefs.SetInt("healthCost", healthCost);
        }
        healthCostText.text = healthCost.ToString();
        if (health >= maxHealth)
        {
            healthMaxImage.SetActive(true);
        }
        else
        {
            healthMaxImage.SetActive(false);
        }




        safe = FindObjectOfType<Safe>();
        safeLevel = PlayerPrefs.GetInt("safeLevel");
        if (safeLevel == 0)
        {
            safeLevel = 1;
            PlayerPrefs.SetInt("safeLevel", safeLevel);
        }
        for (int i = 0; i < safe.safes.Length; i++)
        {
            safe.safes[i].SetActive(false);
        }
        for (int i = 0; i < safeLevel; i++)
        {
            safe.safes[i].SetActive(true);
        }

        safeCost = PlayerPrefs.GetInt("safeCost");
        if (safeCost == 0)
        {
            safeCost = 500;
            PlayerPrefs.SetInt("safeCost", safeCost);
        }
        safeCostText.text = safeCost.ToString();
        if (safeLevel >= maxSafeLevel)
        {
            safeMaxImage.SetActive(true);
        }
        else
        {
            safeMaxImage.SetActive(false);
        }

    }
    private void Start()
    {
     
        mainObject = FindObjectOfType<MainObject>();

        virtualCamera = GameObject.FindGameObjectWithTag("GameCam").GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

          


        upgradePanel.SetActive(true);
        for (int i = 0; i < playerCountInStart; i++)
        {
            SpawnPlayer();
        }
        cubeCount = FindObjectsOfType<Cube>().Length;
        maxCubeCount = cubeCount;
        
    }

    public void EarnCoin()
    {
        safe.CoinSpawn();
    }
    public void EarnCoinValue(int value)
    {
        coinCount += value;
        coinText.text = coinCount.ToString();
        PlayerPrefs.SetInt("coinCount", coinCount);
    }
    private void Update()
    {
        


        if (Input.GetKeyDown(KeyCode.G))
        {
            Coin();
        }
        cubeCountTimer += Time.deltaTime;
        if (cubeCountTimer > 2)
        {
            cubeCount = FindObjectsOfType<Cube>().Length;
            progressBar.fillAmount = (maxCubeCount - cubeCount) / maxCubeCount;
        }
        if (bigManBool)
        {
            bigManCanvas.SetActive(true);
            bigManTimer += Time.deltaTime;
            bigManImage.fillAmount = (bigManTime - bigManTimer) / bigManTime;
            bigManText.text= ((int)bigManTime - (int)bigManTimer).ToString();
            if (bigManTimer > bigManTime)
            {
                for (int i = 0; i < GameObject.FindGameObjectWithTag("Players").transform.childCount; i++)
                {
                    //FindObjectsOfType<Player>()[i].mesh.material = FindObjectsOfType<Player>()[i].mats[1];
                    //FindObjectsOfType<Player>()[i].powerParticle.SetActive(true);
                    GameObject.FindGameObjectWithTag("Players").transform.GetChild(i).gameObject.SetActive(true);
                    bigManBool = false;
                    bigManTimer = 0;
                    bigManCanvas.SetActive(false);
                    Destroy(GameObject.FindGameObjectWithTag("BigMan").gameObject);
                }
            }

        }

        TransposerChange();

        if (!failBool && currentPlayerCount<=0)
        {
            failBool = true;
            failPanel.SetActive(true);
        }
        if(!winBool && cubeCount <= 0)
        {
            winBool = true;
            winPanel.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(voices[3]);
        }
    }
    public void SpawnPlayer()
    {
        var nodeParent = FindObjectOfType<NodeParent>();
        for (int i = 0; i < nodeParent.transform.childCount; i++)
        {
            var node = nodeParent.transform.GetChild(i).GetComponent<Node>();
            if (node.isEmpty)
            {
             var myPlayer=Instantiate(player, node.transform.position, Quaternion.identity);
                myPlayer.transform.parent = GameObject.FindGameObjectWithTag("Players").transform;
                if (spawnWithButton)
                {
                    spawnWithButton = false;
                    myPlayer.GetComponent<Player>().myCanvas.SetActive(true);
                }
                currentPlayerCount = playerCountInStart;
                playerText.text = currentPlayerCount + "/" + playerCountInStart;
                return;
            }
        }
    }
    public void ExtraPlayer()
    {
        var nodeParent = FindObjectOfType<NodeParent>();
        for (int i = 0; i < nodeParent.transform.childCount; i++)
        {
            var node = nodeParent.transform.GetChild(i).GetComponent<Node>();
            if (node.isEmpty)
            {
                var myPlayer = Instantiate(player, mainObject.transform.position, Quaternion.identity);
                myPlayer.transform.parent = GameObject.FindGameObjectWithTag("Players").transform;
                playerCountInStart += 1;
                currentPlayerCount += 1;
                playerText.text = currentPlayerCount + "/" + playerCountInStart;
                return;
            }
        }
    }
    public void PlayButton()
    {
        upgradePanel.SetActive(false);
        startCam.Priority = 0;
        GetComponent<AudioSource>().PlayOneShot(voices[4]);
    }
    public void PlayerUpgradeButton()
    {
        if (coinCount >= playerCountCost && playerCountInStart<maxPlayerCount)
        {
            coinCount -= playerCountCost;
            PlayerPrefs.SetInt("coinCount", coinCount);
            coinText.text = coinCount.ToString();

            playerCountCost += 5;
            PlayerPrefs.SetInt("playerCountCost", playerCountCost);
            playerCountCostText.text = playerCountCost.ToString();

            playerCountInStart += 1;
            PlayerPrefs.SetInt("playerCountInStart", playerCountInStart);
            spawnWithButton = true;
            SpawnPlayer();
            GetComponent<AudioSource>().PlayOneShot(voices[4]);
            if (playerCountInStart >= maxPlayerCount)
            {
                playerMaxImage.SetActive(true);
            }
            else
            {
                playerMaxImage.SetActive(false);
            }
        }
      
    }
    public void HitUpgradeButton()
    {
        if (coinCount >= hitTimeCost && hitTime>minHitTime)
        {
            coinCount -= hitTimeCost;
            PlayerPrefs.SetInt("coinCount", coinCount);
            coinText.text = coinCount.ToString();

            hitTimeCost += 25;
            PlayerPrefs.SetInt("hitTimeCost", hitTimeCost);
            hitTimeCostText.text = hitTimeCost.ToString();

            hitTime -= 0.05f;
            PlayerPrefs.SetFloat("hitTime", hitTime);

            for (int i = 0; i < FindObjectsOfType<Player>().Length; i++)
            {
                FindObjectsOfType<Player>()[i].powerBuffParticle.Play();
            }
            GetComponent<AudioSource>().PlayOneShot(voices[4]);
            if (hitTime <= minHitTime)
            {
                hitMaxImage.SetActive(true);
            }
            else
            {
                hitMaxImage.SetActive(false);
            }
        }

    }

    public void HealthUpgradeButton()
    {
        if (coinCount >= healthCost && health<maxHealth)
        {
            coinCount -= healthCost;
            PlayerPrefs.SetInt("coinCount", coinCount);
            coinText.text = coinCount.ToString();

            healthCost += 50;
            PlayerPrefs.SetInt("healthCost", healthCost);
            healthCostText.text = healthCost.ToString();

            health += 1f;
            PlayerPrefs.SetFloat("health", health);

            for (int i = 0; i < FindObjectsOfType<Player>().Length; i++)
            {
                FindObjectsOfType<Player>()[i].healthBuffParticle.Play();
            }
            GetComponent<AudioSource>().PlayOneShot(voices[4]);

            if (health >= maxHealth)
            {
                healthMaxImage.SetActive(true);
            }
            else
            {
                healthMaxImage.SetActive(false);
            }
        }

    }

    public void SafeUpgradeButton()
    {
        if (coinCount >= safeCost && safeLevel<maxSafeLevel)
        {
            coinCount -= safeCost;
            PlayerPrefs.SetInt("coinCount", coinCount);
            coinText.text = coinCount.ToString();

            safeCost += 250;
            PlayerPrefs.SetInt("safeCost", safeCost);
            safeCostText.text = safeCost.ToString();

            safeLevel += 1;
            PlayerPrefs.SetInt("safeLevel", safeLevel);
            GetComponent<AudioSource>().PlayOneShot(voices[4]);
            for (int i = 0; i < safe.safes.Length; i++)
            {
                safe.safes[i].SetActive(false);
            }
            for (int i = 0; i < safeLevel; i++)
            {
                safe.safes[i].SetActive(true);
            }

            if (safeLevel >= maxSafeLevel)
            {
                safeMaxImage.SetActive(true);
            }
            else
            {
                safeMaxImage.SetActive(false);
            }
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GetComponent<AudioSource>().PlayOneShot(voices[4]);
    }

    public void NextLevel()
    {
        GetComponent<AudioSource>().PlayOneShot(voices[4]);
        PlayerPrefs.DeleteAll();
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
            index = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            index += 1;
        }
        level += 1;
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("index", index);
        coinCount = 1000;
        PlayerPrefs.SetInt("coinCount", coinCount);
    }
    public void Coin()
    {
        coinCount += 100;
        PlayerPrefs.SetInt("coinCount", coinCount);
        coinText.text = coinCount.ToString();
    }
    public void TransposerChange()
    {
        
        transposerTimer += Time.deltaTime;
        if (transposerTimer > 0.5f)
        {

            transposerValue = ((float)currentPlayerCount / 5);
            transposer.m_FollowOffset = new Vector3(5, 30 + transposerValue, -35 - transposerValue);



            transposerTimer = 0;
        }
    }
}
