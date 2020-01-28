// этот скрипт будет работать только в редакторе
#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LocalizationCompact))]

public class LocalizationEditorCompact : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		LocalizationCompact lcz = (LocalizationCompact)target;
		GUILayout.Label("Внимание заменит весь текст!", EditorStyles.boldLabel);
		if(GUILayout.Button("Заполнить XML"))
		{
			lcz.AddXML();
		}
		GUILayout.Label("Внимание заменит весь текст!", EditorStyles.boldLabel);
		if(GUILayout.Button("Заполнить текст в сцене из XML"))
		{
			lcz.LoadLocale();
		}
	}
}
#endif
