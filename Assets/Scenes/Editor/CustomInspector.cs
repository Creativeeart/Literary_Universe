using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(VerifyChecked))]

public enum Status
{
	Good = 0,
	Bad = 1,
}
public class CustomInspector : Editor
{
	public Status status;
	public override void OnInspectorGUI(){
		
		base.OnInspectorGUI();
		status = (Status)EditorGUILayout.EnumPopup("Select status:", status);
		SelectStatus ();
	}
	public void SelectStatus(){
		if (status == Status.Good)
		{
			Debug.Log ("Good");
		}
		if (status == Status.Bad)
		{
			Debug.Log ("Bad");
		}
	}


}