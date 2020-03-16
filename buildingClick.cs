using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cakeslice
{
    public class buildingClick : MonoBehaviour
    {
        private GameObject BuildingUI;
        private GameObject BuildingUIText;

        void Start()
        {
            BuildingUI = GameObject.Find("UI").transform.GetChild(0).gameObject;
            BuildingUIText = GameObject.Find("UI/BuildingUI/Panel/Text");
        }

        private void selectBuilding()
        {
            gameManager.buildingSelected = true;
            gameManager.currSelectedBuilding = this.gameObject;
        }

        private void deselectBuilding()
        {
            gameManager.buildingSelected = false;
            gameManager.currSelectedBuilding = null;
        }

        private void updateBuildingUI(string text)
        {
            BuildingUIText.GetComponent<Text>().text = text;
        }

        private void toggleOutline(bool currGameObj)
        {
            // are we using the curr game object or a reference to the gamemanger saved gameobject??
            if(currGameObj)
            {
                GetComponent<Outline>().enabled = !GetComponent<Outline>().enabled;
            }
            else
            {
                gameManager.currSelectedBuilding.GetComponent<Outline>().enabled = false;
            }
        }

        void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // check if there is already a building highlighted & resolve
                if (gameManager.currSelectedBuilding != null)
                {
                    toggleOutline(false);
                }

                // check curr game object outline state to determine course of action
                if (GetComponent<Outline>().enabled == true)
                {
                    deselectBuilding();
                    updateBuildingUI("");
                }
                else
                {
                    selectBuilding();
                    updateBuildingUI(this.gameObject.transform.name);
                }

                toggleOutline(true);
            }
        }
    }
}
