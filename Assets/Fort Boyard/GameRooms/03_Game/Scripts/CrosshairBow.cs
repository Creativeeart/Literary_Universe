using UnityEngine;
using System.Collections;

public class CrosshairBow : MonoBehaviour
{
    public ZoomCam _zoomCam;
	public Texture2D crosshairTexture;

	void Start ()
	{
		//Cursor.visible = false;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
    }

    void OnGUI ()
	{
        if (_zoomCam.isZoomed || _zoomCam.isPressedLeftKeyMouse)
        {
            Vector3 mousePos = Input.mousePosition;
            Rect pos = new Rect(mousePos.x - crosshairTexture.width * 0.5f, Screen.height - mousePos.y - crosshairTexture.height * 0.5f,
                                 crosshairTexture.width, crosshairTexture.height);
            GUI.DrawTexture(pos, crosshairTexture);
        }
	}
	
}
