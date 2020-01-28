/**************************************************************************/
/** 	© 2016 NULLcode Studio. License: CC 0.
/** 	Разработано специально для http://null-code.ru/
/** 	WebMoney: R209469863836. Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************/

#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Localization))]

public class LocalizationEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		Localization e = (Localization)target;
		GUILayout.Label("Default Locale:", EditorStyles.boldLabel);
		if(GUILayout.Button("Create / Update"))
		{
			e.SetComponents();
		}
	}
}
#endif
