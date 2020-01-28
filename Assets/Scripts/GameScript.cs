using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameScript : MonoBehaviour {
	public GameObject[] stars;
	public TextMeshProUGUI qText;
	public TextMeshProUGUI countTextInfo;
	public QuestionList[] questions;
	public Button[] answerButtons = new Button[3];
	public TextMeshProUGUI[] answersText;
	public GameObject oprosForm;
	public GameObject congratulationForm;
	public GameObject failForm;
	public GameObject failFormTime;
	public GameObject descriptionBook;
	public GameObject cover;
	float scoreCount;
	public Text score;
	public TextMeshProUGUI scoreFinalCount;
	List<object> qList;
	QuestionList currentQ;
	int randQ;
	private TimerGameQuiz _timerGameQuiz;
	int pravilno;

	void Start(){
		_timerGameQuiz = gameObject.GetComponent<TimerGameQuiz>();
	}
	public void EndTimeFunction(){
		oprosForm.SetActive (false);
		failFormTime.SetActive (true);
		cover.SetActive (false);
	}
	public void StartVote(){
		oprosForm.SetActive (true);
		descriptionBook.SetActive (false);
		failForm.SetActive (false);
		failFormTime.SetActive (false);
		congratulationForm.SetActive (false);
		cover.SetActive (true);
		_timerGameQuiz.seconds = _timerGameQuiz.oldTime;
		_timerGameQuiz.RunTime = true;
		pravilno = 0;
		stars [0].SetActive (false);
		stars [1].SetActive (false);
		stars [2].SetActive (false);
		scoreCount = 0;
		score.text = scoreCount.ToString ();
	}
	public void CloseVote(){
		oprosForm.SetActive (false);
		congratulationForm.SetActive (false);
		descriptionBook.SetActive (true);
		failForm.SetActive (false);
		failFormTime.SetActive (false);
		cover.SetActive (true);
		_timerGameQuiz.RunTime = false;
		_timerGameQuiz.seconds = _timerGameQuiz.oldTime;
		pravilno = 0;
		stars [0].SetActive (false);
		stars [1].SetActive (false);
		stars [2].SetActive (false);
		scoreCount = 0;
		score.text = scoreCount.ToString ();
	}
	public void OnClickPlay(){
		qList = new List<object> (questions);
		QuestionGenerate ();
//		if (!qText.GetComponent<Animator> ().enabled) {
//			qText.GetComponent<Animator> ().enabled = true;
//		} else {
//			qText.GetComponent<Animator> ().SetTrigger ("In");
//		}
	}
	void QuestionGenerate(){
		if (qList.Count > 0) {
			randQ = Random.Range (0, qList.Count);
			currentQ = qList [randQ] as QuestionList;
			qText.text = currentQ.question;
			List<string> answers = new List<string> (currentQ.answers);
			for (int i = 0; i < currentQ.answers.Length; i++) {
				int rand = Random.Range (0, answers.Count);
				answersText [i].text = answers [rand];
				answers.RemoveAt (rand);
			}
			StartCoroutine (AnimButtons ());
		} else {
			if (pravilno == 0) {
				oprosForm.SetActive (false);
				failForm.SetActive (true);
				cover.SetActive (false);
			}
			if (pravilno >= 1) {
//				StartCoroutine (Star1 ());
				stars [0].SetActive (true);
				oprosForm.SetActive (false);
				congratulationForm.SetActive (true);
				scoreFinalCount.text = scoreCount.ToString ();
				cover.SetActive (false);
				countTextInfo.text = "<b><color=#ffa500ff>1</color></b> из <b><color=#ffa500ff>3</color></b>";
				_timerGameQuiz.RunTime = false;
				_timerGameQuiz.seconds = _timerGameQuiz.oldTime;
			}
			if (pravilno >= 2) {
//				StartCoroutine (Star1 ());
//				StartCoroutine (Star2 ());
//				stars [0].SetActive (true);
//				stars [1].SetActive (true);
				for (int i = 0; i < 2; i++){
					stars [i].SetActive (true);
//					System.Threading.Thread.Sleep (1000);
				}
				oprosForm.SetActive (false);
				congratulationForm.SetActive (true);
				scoreFinalCount.text = scoreCount.ToString ();
				cover.SetActive (false);
				countTextInfo.text = "<b><color=#ffa500ff>2</color></b> из <b><color=#ffa500ff>3</color></b>";
				_timerGameQuiz.RunTime = false;
				_timerGameQuiz.seconds = _timerGameQuiz.oldTime;
			}
			if (pravilno == 3) {
//				StartCoroutine (Star1 ());
//   			StartCoroutine (Star2 ());
//				StartCoroutine (Star3 ());
//				stars [0].SetActive (true);
//				stars [1].SetActive (true);
//				stars [2].SetActive (true);
				for (int i = 0; i < 3; i++){
					stars [i].SetActive (true);
//					System.Threading.Thread.Sleep (1000);
				}
				oprosForm.SetActive (false);
				congratulationForm.SetActive (true);
				scoreFinalCount.text = scoreCount.ToString ();
				cover.SetActive (false);
				countTextInfo.text = "<b><color=#ffa500ff>3</color></b> из <b><color=#ffa500ff>3</color></b>";
				_timerGameQuiz.RunTime = false;
				_timerGameQuiz.seconds = _timerGameQuiz.oldTime;
			}

		}
	}
	IEnumerator Star1(){
		yield return new WaitForSeconds (1);
		for (int i = 0; i < 1; i++){
			stars [i].SetActive (true);
			System.Threading.Thread.Sleep (1000);
		}
	}
	IEnumerator Star2(){
		yield return new WaitForSeconds (2);
		for (int i = 0; i < 2; i++){
			stars [i].SetActive (true);
		}
	}
	IEnumerator Star3(){
		yield return new WaitForSeconds (3);
		for (int i = 0; i < 3; i++){
			stars [i].SetActive (true);
		}
	}
	IEnumerator AnimButtons(){
		yield return new WaitForSeconds (1);
		for (int i = 0; i < answerButtons.Length; i++){
			answerButtons [i].interactable = false;
		}
		int a = 0;
		while (a < answerButtons.Length) {
			if (!answerButtons [a].gameObject.activeSelf) {
				answerButtons [a].gameObject.SetActive (true);
			} else {
//				answerButtons [a].gameObject.GetComponent<Animator> ().SetTrigger ("In");
			}
			a++;
		}
		for (int i = 0; i < answerButtons.Length; i++){
			answerButtons [i].interactable = true;
		}
		yield break;
	}
	public void AnswerButtons(int index){
		if (answersText [index].text.ToString () == currentQ.answers [0]) {
			print ("Правильно");
			pravilno++;
			scoreCount += (int)(100.0f * _timerGameQuiz.seconds / 2.0f);
			score.text = scoreCount.ToString ();
			qList.RemoveAt (randQ);
			QuestionGenerate ();
			_timerGameQuiz.seconds = _timerGameQuiz.oldTime;
		} else {
			print ("Неправильно");
			qList.RemoveAt (randQ);
			QuestionGenerate ();
			_timerGameQuiz.seconds = _timerGameQuiz.oldTime;

		}

	}

}
[System.Serializable]
public class QuestionList{
	public string question;
	public string[] answers = new string[3];
}
