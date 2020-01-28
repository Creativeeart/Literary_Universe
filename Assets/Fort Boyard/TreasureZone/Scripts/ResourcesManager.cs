using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ResourcesManager : MonoBehaviour {
    public cakeslice.TreasureCalculateZone TreasureCalculateZone;
    public Chest chest;
    public GameObject prefabCoin;
    public GameObject coinAnimatedPrefab;
    public Transform targetCanvas;
    public RectTransform coinIcon;
    public TextMeshProUGUI coinCounterTMPro;
    public int countSpawnCoins = 50;
    public float spawnTime = 1;
    public float destroyTime = 1;
    public int coins = 0;
    public float animationDuration = 1f;
    public Ease animationEase = Ease.Linear;

    Camera targetCamera;
    RandomNumberCoins randomNumberCoins;
    float rand;
    int j = 0;
    Vector3 center, size;
    void Start()
    {
        targetCamera = Camera.main;
        center = transform.position;
        size = transform.localScale;
        coinCounterTMPro.text = coins.ToString();
    }
    
    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3);
        j = 0;
        for (int i = 0; i < countSpawnCoins; i++)
        {
            Vector3 posCube = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            GameObject ins = Instantiate(prefabCoin, posCube, new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 1));
            randomNumberCoins = ins.transform.GetComponent<RandomNumberCoins>();
            rand = Random.Range(0.1f, 3.0f);
            Destroy(ins, destroyTime);
            //yield return new WaitForSeconds(rand);
            CollectCoins(ins, coinAnimatedPrefab, null, coinIcon);
            j++;
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void CollectCoins(GameObject gameObjectTarget, GameObject resourceAnimatedPrefab, GameObject resourceEffectPrefab, RectTransform resourceIcon)
    {
        RectTransform clone = Instantiate(resourceAnimatedPrefab, targetCanvas, false).GetComponent<RectTransform>();
        clone.anchorMin = targetCamera.WorldToViewportPoint(gameObjectTarget.transform.position);
        clone.anchorMax = clone.anchorMin;
        clone.anchoredPosition = clone.localPosition;
        clone.anchorMin = new Vector2(0.5f, 0.5f);
        clone.anchorMax = clone.anchorMin;
        clone.SetParent(resourceIcon);
        clone.DOAnchorPos(Vector3.zero, animationDuration)
            //.SetDelay(rand)
            .SetEase(animationEase)
            .OnComplete(() =>
            {
                Destroy(clone.gameObject);
                
                resourceIcon.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.05f)
                .OnComplete(() =>
                {
                    resourceIcon.localScale = Vector3.one;
                })
                .Play();
                
                coins += randomNumberCoins.gameObject.GetComponent<RandomNumberCoins>().randomNumber;
                coinCounterTMPro.text = coins.ToString();
                if (j == countSpawnCoins)
                {
                    coins = chest.coinsBoyard;
                    coinCounterTMPro.text = coins.ToString();
                    cakeslice.FortBoyardGameController.Instance.FB_CamMovingController.CameraMovingToPoint(cakeslice.FortBoyardGameController.Instance.FB_CamMovingController.pointToTreasure_Calculate_Zone_A);
                    targetCanvas.gameObject.SetActive(false);
                    StartCoroutine(Wait());
                }
            })
            .Play();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        cakeslice.FortBoyardGameController.Instance.FB_CamMovingController.CameraMovingToPoint(cakeslice.FortBoyardGameController.Instance.FB_CamMovingController.pointToTreasure_Calculate_Zone_B);
        yield return new WaitForSeconds(2);
        TreasureCalculateZone.CalculateGold();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
