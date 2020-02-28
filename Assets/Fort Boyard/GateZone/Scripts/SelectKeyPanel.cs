using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class SelectKeyPanel : MonoBehaviour
    {
        public GateZoneController gateZoneController;
        public Outline _outLine;
        public bool selectObject = false;
        // Use this for initialization
        void Start()
        {
            _outLine = gameObject.GetComponent<Outline>();
            _outLine.enabled = false;
            selectObject = false;
        }

        void OnMouseDown()
        {
            //if (!selectObject)
            //{
                //selectObject = true;
                //_outLine.enabled = false;
                //gateZoneController.CountingKeys();
            //}
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