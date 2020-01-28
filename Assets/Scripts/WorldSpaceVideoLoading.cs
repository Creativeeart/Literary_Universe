using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class WorldSpaceVideoLoading: MonoBehaviour {
    
    [Header("Загружаемая сцена")]
	public string sceneName;
	[Header("Остальные объекты")]
	public GameObject loadingScreen;
	public Text progressText;
	public Text textLight;
	public VideoPlayer videoPlayer;
    private ConfigurationProject _configurationProject;
    void OnEnable(){
		videoPlayer.loopPointReached += LoopPointReached;
	}
	void OnDisable(){
		videoPlayer.loopPointReached -= LoopPointReached;
	}
	void LoopPointReached(VideoPlayer v){
		StartCoroutine (StartCor ());
	}

    private void Start()
    {
        _configurationProject = GameObject.Find("ConfigurationProject").GetComponent<ConfigurationProject>();
        if (_configurationProject.startIntro == false)
        {
            videoPlayer.Stop();
            StartCoroutine(StartCor());
            textLight.text = string.Empty;
        }
    }

    void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
            videoPlayer.Stop();
            StartCoroutine(StartCor());
            textLight.text = string.Empty;
        }
    }

	IEnumerator StartCor()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
		loadingScreen.SetActive (true);
        while (!async.isDone)
		{
			float progress = async.progress / .9f;
			progressText.text = string.Format("Пожалуйста, подождите... {0:0}%", progress * 100);
			yield return null;
		}
        
    }
    


}