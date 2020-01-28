using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matrix3D: MonoBehaviour {
    public int gridWidth = 3;
    public int gridHeight = 3;
    public int gridGlubina = 3;
    public GameObject crate;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                for (int k = 0; k < gridGlubina; k++)
                {
                    var ins = Instantiate(crate, new Vector3(transform.position.x + i, transform.position.y + k, transform.position.z + j), Quaternion.identity);
                    ins.transform.position = new Vector3(ins.transform.position.x + 2.5f + i, ins.transform.position.y + 2.5f + k, ins.transform.position.z + 2.5f + j);
                }     
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
