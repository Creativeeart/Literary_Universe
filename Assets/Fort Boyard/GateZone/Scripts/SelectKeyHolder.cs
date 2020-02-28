using UnityEngine;
using cakeslice;
public class SelectKeyHolder : MonoBehaviour
{
    Outline _outLine;
    GateZoneController GateZoneController;
    bool selectObject = false;

    void Start()
    {
        GateZoneController = GateZoneController.Instance;
        _outLine = gameObject.GetComponent<Outline>();
        _outLine.enabled = false;
        selectObject = false;
    }

    void OnMouseDown()
    {
        if (!selectObject)
        {
            if (FortBoyardGameController.Instance.CurrentKeys > 0)
            {
                if (GateZoneController.countAddKeys != 0)
                {
                    selectObject = true;
                    _outLine.enabled = false;
                    GetComponent<MeshRenderer>().material = GateZoneController.emissionMaterialForKeyHolders;
                    transform.GetChild(0).gameObject.SetActive(true);
                    GateZoneController.OpenKey();
                    if (GateZoneController.insertKeysInHolder == 3)
                    {
                        GateZoneController.OpenGateAndEnableOpenTipsMechanism();
                    }
                }
                else
                {
                    ShowAlertMessage();
                }
            }
            else
            {
                ShowAlertMessage();
            }
        }
    }
    void ShowAlertMessage()
    {
        AlertUI.Instance.ShowAlert_DEFAULT("Доступ запрещен. У вас нет ключей. \nДополнительные ключи вы можете взять на панели, в нижней части экрана");
    }

    void OnMouseEnter()
    {
        if (!selectObject) _outLine.enabled = true;
    }

    void OnMouseExit()
    {
        if (!selectObject) _outLine.enabled = false;
    }
}
