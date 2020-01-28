using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class TooltipV2 : MonoBehaviour {

	public static string text;
	public static bool isUI;
	public Color BGColor = Color.white;
	public Color textColor = Color.black;
	public enum ProjectMode {Tooltip3D = 0, Tooltip2D = 1};
	public ProjectMode tooltipMode = ProjectMode.Tooltip3D;
	public int fontSize = 14;   // размер шрифта
	public int maxWidth = 250;  // максимальная ширина Tooltip
	public int border = 10;     // ширина обводки
	public RectTransform box;
    public GameObject boxGameObject;
	public TextMeshProUGUI boxText;

	public float speed = 10;    // скорость плавного затухания и проявления
    public float offsetY = 10f;
    public float offsetX  = 10f;

	private Image img;
	private Color BGColorFade;
	private Color textColorFade;

	void Awake()
	{
		img = box.GetComponent<Image>();
        //box.sizeDelta = new Vector2(maxWidth, box.sizeDelta.y);
        boxGameObject.SetActive(false);
        BGColorFade = BGColor;
		BGColorFade.a = 0;
		textColorFade = textColor;
		textColorFade.a = 0;
		isUI = false;
		img.color = BGColorFade;
		boxText.color = textColorFade;
	}

	void Update()
	{
		bool show = false;
		boxText.fontSize = fontSize;

		if(tooltipMode == ProjectMode.Tooltip3D)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.transform.GetComponent<TooltipText>())
				{
					text = hit.transform.GetComponent<TooltipText>().text;
					show = true;
				}
			}
		}
		else
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.transform != null)
			{
				if(hit.transform.GetComponent<TooltipText>())
				{
					text = hit.transform.GetComponent<TooltipText>().text;
					show = true;
				}
			}
		}

		boxText.text = text;
		float width = maxWidth;
        if (boxText.preferredWidth <= maxWidth - border)
        {
            width = boxText.preferredWidth + border;
        }
		box.sizeDelta = new Vector2(width, boxText.preferredHeight + border);
        boxGameObject.SetActive(true);

        if (show || isUI)
		{
            float curY = Input.mousePosition.y + box.sizeDelta.y / 2 + offsetY;
            if(curY + box.sizeDelta.y / 2 > Screen.height) // если Tooltip выходит за рамки экрана, по высоте
            {
                curY = Input.mousePosition.y - box.sizeDelta.y;
            }

            float curX = Input.mousePosition.x + box.sizeDelta.x / 2 + offsetX;
			if(curX + box.sizeDelta.x / 2 > Screen.width) // если Tooltip выходит за рамки экрана, по ширине
            {
		        curX = Input.mousePosition.x - box.sizeDelta.x / 2 - offsetX;
			}
            
            box.anchoredPosition = new Vector2(curX, curY);
			img.color = Color.Lerp(img.color, BGColor, speed * Time.unscaledDeltaTime);
			boxText.color = Color.Lerp(boxText.color, textColor, speed * Time.unscaledDeltaTime);
		}
		else
		{
            Fade();
        }
	}

    public void Fade()
    {
        img = box.GetComponent<Image>();
        //box.sizeDelta = new Vector2(maxWidth, box.sizeDelta.y);
        boxGameObject.SetActive(false);
        BGColorFade = BGColor;
        BGColorFade.a = 0;
        textColorFade = textColor;
        textColorFade.a = 0;
        isUI = false;
        img.color = BGColorFade;
        boxText.color = textColorFade;
    }
}
