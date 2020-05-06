using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RandomSpawn : MonoBehaviour {
	[Header("Положительные")]
	private GameObject [] spawnPoints_good; //Точки спавна ПОЛОЖИТЕЛЬНЫХ предметов
	public GameObject [] objectsForSpawn_good; //ПОЛОЖИТЕЛЬНЫЕ объекты для спавна
	public Transform [] folder_good; //Папка в иерархии положительных объектов
	private Vector3 [] spawnPointsPosition_good; //Позиция точек спавна положительных предметов
	private Quaternion [] spawnPointsRotation_good; //Ротация точек спавна положительных предметов

	[Header("Отрицательные")]
	private GameObject [] spawnPoints_bad; //Точки спавна ОТРИЦАТЕЛЬНЫХ предметов
	public GameObject [] objectsForSpawn_bad; //ОТРИЦАТЕЛЬНЫЕ объекты для спавна
	public Transform []  folder_bad; //Папка в иерархии отрицательных объектов
	private Vector3 [] spawnPointsPosition_bad; //POSITION точек спавна отрицательных предметов
	private Quaternion [] spawnPointsRotation_bad; //ROTATION точек спавна отрицательных предметов

	private Vector3[] spawnPointsPosition_summ;
	public GameObject [] cloneObjects;
	public bool spawnObjects = false;
	void Start () {
	}

	void Update(){
		if (spawnObjects == true){
			for (int i = 0; i < cloneObjects.Length; i++) {
				Vector3 down = transform.TransformDirection(Vector3.down) * 0.1f;
				Debug.DrawRay (cloneObjects[i].transform.position, down, Color.yellow);
				RaycastHit hit;
				if(Physics.Raycast(cloneObjects[i].transform.position, down, out hit, 0.1f)){
					cloneObjects [i].transform.position = new Vector3 (cloneObjects[i].transform.position.x, cloneObjects[i].transform.position.y - hit.distance, cloneObjects[i].transform.position.z);
				}
			}
		spawnObjects = false;
		}
	}

	public void AddingInfoPointsGood(){
		spawnPoints_good = GameObject.FindGameObjectsWithTag ("spawnPoint_good");  //Ищем объекты с тегом "spawnPoint_good" и заносим в массив spawnPoints_good
		spawnPoints_good = spawnPoints_good.OrderBy(go => go.name).ToArray(); //сортируем полученный лист по имени в алфавитном порядке
		spawnPointsPosition_good = new Vector3[spawnPoints_good.Length];  //создаем строки для POSITION исходя из кол-ва точек спавна
		spawnPointsRotation_good = new Quaternion[spawnPoints_good.Length]; //создаем строки для ROTATION исходя из кол-ва точек спавна
		for (int i = 0; i < spawnPoints_good.Length; i++) //Заполняем массив данными о POSITION и ROTATION
		{ 
			spawnPointsPosition_good [i] = spawnPoints_good [i].transform.localPosition;
			spawnPointsRotation_good [i] = spawnPoints_good[i].transform.localRotation;
		}
	}

	public void AddingInfoPointsBad(){
		spawnPoints_bad = GameObject.FindGameObjectsWithTag ("spawnPoint_bad"); 
		spawnPoints_bad = spawnPoints_bad.OrderBy(go => go.name).ToArray(); 
		spawnPointsPosition_bad = new Vector3[spawnPoints_bad.Length]; 
		spawnPointsRotation_bad = new Quaternion[spawnPoints_bad.Length];
		for (int i = 0; i < spawnPoints_bad.Length; i++){ 
			spawnPointsPosition_bad [i] = spawnPoints_bad [i].transform.localPosition;
			spawnPointsRotation_bad [i] = spawnPoints_bad[i].transform.localRotation;
		}
	}
	public void MergeInfoPointsPositions(){
		spawnPointsPosition_summ = new Vector3[spawnPointsPosition_good.Length + spawnPointsPosition_bad.Length]; //Объединение в один массив
		spawnPointsPosition_good.CopyTo(spawnPointsPosition_summ, 0);
		spawnPointsPosition_bad.CopyTo(spawnPointsPosition_summ, spawnPointsPosition_good.Length);
	}

	public void FindCloneGameItems(){
		cloneObjects = GameObject.FindGameObjectsWithTag ("GameItems"); 
	}

	public void RandomSpawner () {
		Mix (spawnPointsPosition_summ);    // Перемешивание массива
		SpawnGoodObj();
		SpawnBadObj ();
		FindCloneGameItems ();
		spawnObjects = true;
	}

	void SpawnGoodObj(){
		spawnPointsPosition_good = spawnPointsPosition_summ.Take(spawnPoints_good.Length).ToArray(); //Берем первые пять элементов
		System.Random rand_good = new System.Random();
		objectsForSpawn_good = objectsForSpawn_good.OrderBy(x => rand_good.Next()).ToArray();
		for (int i = 0; i < spawnPoints_good.Length; i++){
			spawnPoints_good [i].transform.localPosition = spawnPointsPosition_good[i];
			var obj_good  = Instantiate (objectsForSpawn_good [i], spawnPoints_good[i].transform.position, objectsForSpawn_good[i].transform.localRotation);
			obj_good.transform.parent = folder_good [i];
			obj_good.transform.localRotation = new Quaternion (0,obj_good.transform.rotation.y,obj_good.transform.rotation.z,obj_good.transform.rotation.w);
		}
	}

	void SpawnBadObj(){
		spawnPointsPosition_bad = spawnPointsPosition_summ.Skip(spawnPoints_bad.Length).ToArray();
		System.Random rand_bad = new System.Random();
		objectsForSpawn_bad = objectsForSpawn_bad.OrderBy(x => rand_bad.Next()).ToArray();
		for (int i = 0; i < spawnPoints_bad.Length; i++){
			spawnPoints_bad [i].transform.localPosition = spawnPointsPosition_bad[i];
			var obj_bad = Instantiate (objectsForSpawn_bad [i], spawnPoints_bad[i].transform.position, objectsForSpawn_bad[i].transform.localRotation);
			obj_bad.transform.parent = folder_bad [i];
			obj_bad.transform.localRotation = new Quaternion (0,obj_bad.transform.rotation.y,obj_bad.transform.rotation.z,obj_bad.transform.rotation.w);
		}
	}
	void OnTriggerEnter(Collider other){
		Debug.Log (other.name);
	}

	public void DeleteSpawnObjects(){
		for (int i = 0; i < cloneObjects.Length; i++) {
			Destroy(cloneObjects [i]);
			cloneObjects [i] = null;
		}
	}

	Vector3 [] Mix (Vector3 [] num){
		for (int i = 0; i < num.Length; i++) {
			Vector3 currentValue = num [i];
			int randomIndex = Random.Range (i, num.Length);
			num [i] = num [randomIndex];
			num [randomIndex] = currentValue;
		}
		return num;
	}

}
