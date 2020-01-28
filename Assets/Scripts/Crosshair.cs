using UnityEngine;
using System.Collections;
public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture_std;
    public Texture2D crosshairTexture_move;
    public Texture2D crosshairTexture_hover;

    void Start()
    {
        Cursor.visible = false;
    }
	void Update(){
		if (Input.GetKey (KeyCode.Escape)) {
			Cursor.visible = true;
		}
	}
    void OnGUI()
    {
        Vector3 mousePos = Input.mousePosition;
        Rect pos = new Rect(mousePos.x - crosshairTexture_std.width * 0.5f, Screen.height - mousePos.y - crosshairTexture_std.height * 0.5f,
                             crosshairTexture_std.width, crosshairTexture_std.height);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.GetComponent<Rigidbody>() == true)
            {
                if (Input.GetMouseButton(1) || (Input.GetMouseButton(0)))
                {
                    GUI.DrawTexture(pos, crosshairTexture_move);
                }
                else
                    GUI.DrawTexture(pos, crosshairTexture_hover);
            }
            
        }
        else
        {
            if (Input.GetMouseButton(1) || (Input.GetMouseButton(0)))
            {
                GUI.DrawTexture(pos, crosshairTexture_move);
            }
            else
            {
                GUI.DrawTexture(pos, crosshairTexture_std);

            }

        }
        
    }
}
