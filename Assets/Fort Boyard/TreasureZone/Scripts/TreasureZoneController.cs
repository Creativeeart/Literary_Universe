using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using cakeslice;
public class TreasureZoneController : MonoBehaviour {
    public GameObject UI_TreasureZone;
    public GameObject prefabCoin;
    public GameObject coinAnimatedPrefab;
    public Transform targetCanvas;
    public RectTransform coinIcon;
    public TextMeshProUGUI coinCounterTMPro;
    public int countSpawnCoins = 50;
    public float spawnTime = 1;
    public float destroyTime = 1;
    //public int Coins { get { return Coins; } set { value = 0; } }
    public float animationDuration = 1f;
    public Ease animationEase = Ease.Linear;

    public int Coins { get; set; }
    Camera targetCamera;
    RandomNumberCoins randomNumberCoins;
    //float rand;
    int j = 0;
    Vector3 center, size;

    public static TreasureZoneController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void TreasureZoneEntered()
    {
        
        if (FortBoyardGameController.Instance.IsTreasureZone)
        {
            UI_TreasureZone.SetActive(true);
            
            targetCamera = Camera.main;
            center = transform.position;
            size = transform.localScale;
            coinCounterTMPro.text = Coins.ToString();
        }
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
            //rand = Random.Range(0.1f, 3.0f);//
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

                Coins += randomNumberCoins.gameObject.GetComponent<RandomNumberCoins>().randomNumber;
                coinCounterTMPro.text = Coins.ToString();
                if (j == countSpawnCoins)
                {
                    StartCoroutine(WaitingEndCollectMoney());
                }
                
                
            })
            .Play();
    }
    IEnumerator WaitingEndCollectMoney()
    {
        Coins = Chest.Instance.coinsBoyard;
        coinCounterTMPro.text = Coins.ToString();
        yield return new WaitForSeconds(2);
        if (Coins == Chest.Instance.coinsBoyard)
        {
            FortBoyardGameController.Instance.watchUI.SetActive(false);
            UI_TreasureZone.SetActive(false);
            StartCoroutine(FB_CamMovingController.Instance.GoToTreasureCalculateZone()); // Переход к зоне подсчета золота
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
