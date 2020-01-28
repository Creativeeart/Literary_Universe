using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class bl_SpectrumAudio : MonoBehaviour {

    public GameObject SpectrumPrefab;
    public Transform SpectrumPanel;
    public RectTransform RootSpectrumUI;
    [HideInInspector] public List<RectTransform> m_Recs = new List<RectTransform>();
    [Space(5)]
    public int m_Samples = 64;
    public int m_Channel = 1;


	// Use this for initialization
    void Start()
    {
        InstanceSpectrum();
    }
    /// <summary>
    /// 
    /// </summary>
    void InstanceSpectrum()
    {
        //Calculate the size of each UI bar
        float sf = (RootSpectrumUI.sizeDelta.x / m_Samples);
        //start position
        float sp = -sf;

        for (int i = 0; i < m_Samples; i++)
        {
            //next position
            sp += sf;
            GameObject s = Instantiate(SpectrumPrefab) as GameObject;
            s.transform.SetParent(SpectrumPanel, false);

            RectTransform r = s.GetComponent<RectTransform>();
            m_Recs.Add(r);

            Vector2 v = r.anchoredPosition;
            v.x = sp;
            r.anchoredPosition = v;

            Vector2 size = r.sizeDelta;
            size.x = sf;
            r.sizeDelta = size;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        //Get spectrum data samples from audio listener
        float[] data = new float[m_Samples];
        AudioListener.GetOutputData(data, m_Channel);
        //apply to the bars ui
        for (int i = 0; i < m_Recs.Count; i++)
        {
            Vector2 v = m_Recs[i].sizeDelta;
            v.y = (data[i] * (RootSpectrumUI.sizeDelta.y * 0.5f)) ;
            Vector2 vs = m_Recs[i].anchoredPosition;
            vs.y = 0 + ((data[i] * RootSpectrumUI.sizeDelta.y * 0.5f));
            m_Recs[i].anchoredPosition = vs;
            m_Recs[i].sizeDelta = v;  
        }
    }
}