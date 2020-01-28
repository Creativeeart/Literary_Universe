using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnStars : MonoBehaviour {
    public Vector3 [] spawnPoints;
    public bool isCoinClickable = true;
    public float spawnTimeEnd = 10f;
    public GameObject prefabCoin;
    public float spawnTime;
    public float destroyTime;
    public bool isCircle = true;
    public int countStar = 10;

    Vector3 center, size;
    float radiusCircle;
    float time = 0;

    void Start()
    {
        center = transform.position;
        size = transform.localScale;
        radiusCircle = transform.localScale.x * transform.localScale.z;
        for (int i = 0; i < countStar; i++)
        {
            Vector3 posRotation = RandomCircle(center, radiusCircle);
            GameObject ins = Instantiate(prefabCoin, posRotation, Quaternion.identity);
            //GameObject ins = Instantiate(prefabCoin, spawnPoints[i], Quaternion.identity);
        }
    }
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i].name);
            i++;
        }
    }
    void Update()
    {
        //if (isCoinClickable)
        //{
        //    time += Time.deltaTime;
        //    if (time > spawnTime)
        //    {
        //        SpawnCoin();
        //        time = 0;
        //    }
        //}
        //else
        //{
        //    if (spawnTimeEnd > 0)
        //    {
        //        time += Time.deltaTime;
        //        if (time > spawnTime)
        //        {
        //            SpawnCoin();
        //            time = 0;
        //        }
        //        spawnTimeEnd -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        spawnTimeEnd = 0;
        //    }
        //}
    }

    void SpawnCoin()
    {
        if (!isCircle)
        {
            Vector3 posCube = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            GameObject ins = Instantiate(prefabCoin, posCube, new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 1));
            Destroy(ins, destroyTime);
        }
        else
        {
            Vector3 posRotation = RandomCircle(center, radiusCircle);
            GameObject ins = Instantiate(prefabCoin, posRotation, Quaternion.identity);
            Destroy(ins, destroyTime);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad) * Random.value;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad) * Random.value;
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad) * Mathf.Sin(ang * Mathf.Deg2Rad) * Random.value;
        return pos;
    }

    void OnDrawGizmosSelected()
    {
        if (!isCircle)
        {
            Gizmos.color = new Color(1, 0, 0, 0.4f);
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
        else
        {
            radiusCircle = transform.localScale.x * transform.localScale.z;
            Gizmos.color = new Color(1, 0, 0, 0.4f);
            Gizmos.DrawSphere(transform.position, radiusCircle);
        }
    }
}
