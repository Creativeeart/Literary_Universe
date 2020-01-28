using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class SelectKeyHolder : MonoBehaviour
    {
        public GateZoneController gateZoneController;
        public Outline _outLine;
        public bool selectObject = false;
        public Material emissionMaterialForKeyHolders;
        public AlertUI alertUI;
        void Start()
        {
            _outLine = gameObject.GetComponent<Outline>();
            _outLine.enabled = false;
            selectObject = false;
        }

        void OnMouseDown()
        {
            if (!selectObject)
            {
                if (gateZoneController.fortBoyardGameController.currentKeys > 0)
                {
                    if (gateZoneController.countAddKeys != 0)
                    {
                        selectObject = true;
                        _outLine.enabled = false;
                        GetComponent<MeshRenderer>().material = emissionMaterialForKeyHolders;
                        transform.GetChild(0).gameObject.SetActive(true);
                        gateZoneController.OpenKey();
                        gateZoneController.insertKeysInHolder++;
                        gateZoneController.arrow3DKeysHolder.transform.localPosition = new Vector3(gateZoneController.arrow3DKeysHolder.transform.localPosition.x, gateZoneController.arrow3DKeysHolder.transform.localPosition.y - 0.17f, gateZoneController.arrow3DKeysHolder.transform.localPosition.z);
                        if (gateZoneController.insertKeysInHolder == 3)
                        {
                            gateZoneController.OpenGateAndEnableOpenTipsMechanism();
                            gateZoneController.arrow3DKeysHolder.SetActive(false);
                            gateZoneController.arrow3DTipsMechanism.SetActive(true);
                        }
                    }
                    else alertUI.ShowWarningModalWindow("Доступ запрещен. У вас нет ключей. \nДополнительные ключи вы можете взять на панели, в нижней части экрана");
                }
                else alertUI.ShowWarningModalWindow("Доступ запрещен. У вас нет ключей. \nДополнительные ключи вы можете взять на панели, в нижней части экрана");
                
            }
        }

        void OnMouseEnter()
        {
            if (selectObject == false) _outLine.enabled = true;
        }

        void OnMouseExit()
        {
            if (selectObject == false) _outLine.enabled = false;
        }
    }
}
