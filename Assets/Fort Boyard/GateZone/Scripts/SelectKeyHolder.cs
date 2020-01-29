using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class SelectKeyHolder : MonoBehaviour
    {
        public Outline _outLine;
        public bool selectObject = false;
        public Material emissionMaterialForKeyHolders;
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
                if (FortBoyardGameController.Instance.CurrentKeys > 0)
                {
                    if (GateZoneController.Instance.countAddKeys != 0)
                    {
                        selectObject = true;
                        _outLine.enabled = false;
                        GetComponent<MeshRenderer>().material = emissionMaterialForKeyHolders;
                        transform.GetChild(0).gameObject.SetActive(true);
                        GateZoneController.Instance.OpenKey();
                        GateZoneController.Instance.insertKeysInHolder++;
                        GateZoneController.Instance.arrow3DKeysHolder.transform.localPosition = new Vector3(GateZoneController.Instance.arrow3DKeysHolder.transform.localPosition.x, GateZoneController.Instance.arrow3DKeysHolder.transform.localPosition.y - 0.17f, GateZoneController.Instance.arrow3DKeysHolder.transform.localPosition.z);
                        if (GateZoneController.Instance.insertKeysInHolder == 3)
                        {
                            GateZoneController.Instance.OpenGateAndEnableOpenTipsMechanism();
                            GateZoneController.Instance.arrow3DKeysHolder.SetActive(false);
                            GateZoneController.Instance.arrow3DTipsMechanism.SetActive(true);
                        }
                    }
                    else AlertUI.Instance.ShowWarningModalWindow("Доступ запрещен. У вас нет ключей. \nДополнительные ключи вы можете взять на панели, в нижней части экрана");
                }
                else AlertUI.Instance.ShowWarningModalWindow("Доступ запрещен. У вас нет ключей. \nДополнительные ключи вы можете взять на панели, в нижней части экрана");
                
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
