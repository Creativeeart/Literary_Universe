/**************************************************************************/
/** 	© 2016 NULLcode Studio. License: CC 0.
/** 	Разработано специально для http://null-code.ru/
/** 	WebMoney: R209469863836. Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class LocalizationComponent : MonoBehaviour {

	// обязательная сериализация даных полей
	[SerializeField] private TextMeshProUGUI _target;
	[SerializeField] private int _id;

	private int last_id;

	public int id
	{
		get{ return _id; }
	}

	public TextMeshProUGUI target
	{
		get{ return _target; }
	}
		
	void Awake()
	{
		SetComponent ();
	}
	public void SetComponent()
	{
		TextMeshProUGUI t = GetComponent<TextMeshProUGUI>();

		if(t == null)
		{
			_target = null;
			_id = 0;
		}
		else
		{
			_target = t;
			_id = t.GetInstanceID();

		}
	}
}
