using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrateController : MonoBehaviour
{
    public AudioSource AudioSource;
    public Barrel_Trigger _barrelTrigger;
    public AudioClip crateMovingSound;
    public bool win = false;
    public int steps = 50;
    public float distanceRay = 1.0625f;
    public float offsetYposArrow = 1.5f;
    //public int gridWidth = 10;
    //public int gridHeight = 10;
    //public GameObject crate;
    public bool moving = false;
    public float speedCrate = 5f;
    public TextMeshProUGUI stormCountUI, stepsUI;
    public Material crackCrateMaterial, defaultMaterial;
    public GameObject arrow;
    public GameObject[] crates;
    public int countCrackCrates = 4;
    int[] rands;
    //float timeToStorm = 10f;
    public GameObject tipsScreenRotation;
    public static CrateController Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        AudioSource = gameObject.GetComponent<AudioSource>();
        AudioSource.pitch = Random.Range(1, 1.5f);
        //for (int i = 0; i < gridWidth; i++)
        //{
        //    for(int j = 0; j < gridHeight; j++)
        //    {
        //        var ins = Instantiate(crate, new Vector3(transform.position.x + i, transform.position.y, transform.position.z + j), Quaternion.identity);
        //        ins.transform.position = new Vector3(ins.transform.position.x + 2.5f + i, ins.transform.position.y, ins.transform.position.z + 2.5f + j);
        //    }
        //}
        //Randomizer();
    }

    private void Update()
    {
        //timeToStorm -= Time.deltaTime;
        //int temptime = (int)timeToStorm;
        //stormCountUI.text = "Шторм через: " + temptime.ToString();
        //if (timeToStorm <= 0)
        //{
        //    //Randomizer();
        //    timeToStorm = 10f;
        //}
    }

    void Cleaner()
    {
        for (int i = 0; i < crates.Length; i++)
        {
            crates[i].tag = "Crate";
            crates[i].GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }
    void Randomizer()
    {
        Cleaner();
        rands = new int[countCrackCrates];
        for (int i = 0; i < countCrackCrates; i++)
        {
            rands[i] = Random.Range(0, crates.Length);
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    if (rands[i] == rands[j])
                    {
                        i--;
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < rands.Length; i++)
        {
            crates[rands[i]].tag = "baseBarrel";
            crates[rands[i]].GetComponent<MeshRenderer>().material = crackCrateMaterial;
        }
    }
}