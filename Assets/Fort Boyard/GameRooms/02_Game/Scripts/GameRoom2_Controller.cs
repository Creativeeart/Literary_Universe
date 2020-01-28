using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
namespace cakeslice
{
    public class GameRoom2_Controller : MonoBehaviour {
        public AudioSource audioSource;
        public AudioClip rotatePipe;
        public AudioClip rotateValve;
        public AudioClip valveSmoke;
        public AudioClip openGrid;
        public Text checksumText;
        public FortBoyardGameController _fortBoyardGameController;
        public GameObject podskazkaForm;
        public bool random = true;
        public Animator cell, arrow, ventil, key;
        public Material green;
        public MeshRenderer[] pipeLights;
        public RotatePipe[] _rotatePipes;
        public int checkSumm = 0;

        public Button podskazkaButton;
        public Image podskazkaImage;
        public bool isReady = true;
        public TextMeshProUGUI timeTMPRO, timeShadorTMPRO;
        public int timer = 0;

        private void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            Podskazka();
        }

        public void CheckSumm()
        {
            checkSumm = 0;
            for (int i = 0; i < _rotatePipes.Length; i++)
            {
                if (_rotatePipes[i].isCorrectAngle)
                {
                    checkSumm++;
                }
            }
            if (checkSumm == _rotatePipes.Length)
            {
                StartCoroutine(Poryadok());
            }
            checksumText.text = checkSumm.ToString();
        }

        IEnumerator Poryadok()
        {
            for (int i = 0; i < pipeLights.Length; i++)
                pipeLights[i].material = green;
            _fortBoyardGameController.StopTimer();
            yield return new WaitForSeconds(1f);
            ventil.enabled = true;
            audioSource.pitch = 1;
            audioSource.PlayOneShot(rotateValve);
            yield return new WaitForSeconds(1f);
            audioSource.PlayOneShot(valveSmoke);
            yield return new WaitForSeconds(1f);
            arrow.SetBool("Max", true);
            yield return new WaitForSeconds(1f);
            cell.enabled = true;
            audioSource.pitch = 2;
            audioSource.PlayOneShot(openGrid);
            yield return new WaitForSeconds(1f);
            key.SetBool("KeyFly", true);
            yield return new WaitForSeconds(2f);
            key.SetBool("KeyAfterFly", true);
            yield return new WaitForSeconds(2f);
            _fortBoyardGameController.WinnerRoom("Keys");
            
        }
        public void PodskazkaShow()
        {
            podskazkaForm.SetActive(true);
            isReady = false;
            StartCoroutine(HideAfterTime(7)); 
        }

        public void PodskazkaHide()
        {
            podskazkaForm.SetActive(false);
            isReady = true;
        }
        IEnumerator HideAfterTime(float time)
        {
            timer = (int)time;
            while(time >= 0)
            {
                time -=Time.deltaTime;
                timer = (int)time;
                timeTMPRO.text = "ЗАКРЫТЬ " + "(" + timer + ")";
                timeShadorTMPRO.text = timeTMPRO.text;
                yield return null;
            }
            PodskazkaHide();            
        }
        public void Podskazka()
        {
            if (!isReady)
            {
                podskazkaImage.fillAmount -= Time.deltaTime / 2;
                podskazkaButton.interactable = false;
            }
            else
            {
                podskazkaImage.fillAmount += Time.deltaTime / 15;
            }
            if (podskazkaImage.fillAmount == 1)
            {
                podskazkaButton.interactable = true;
            }
        }
    }
}
