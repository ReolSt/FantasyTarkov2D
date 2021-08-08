using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject player;

    private GameObject mainCamera;
    private Camera mainCameraComponent;

    private GameObject guiCanvas;
    private GameObject pauseMenu;
    private GameObject skillPanel;
    private GameObject inventory;
    private GameObject statusBar;

    public bool actionable = true;

    private void Start()
    {
        this.mainCamera = GameObject.Find("MainCamera");
        this.mainCameraComponent = this.mainCamera.GetComponent<Camera>();

        this.guiCanvas = GameObject.Find("GUI");
        this.guiCanvas.SetActive(true);

        this.pauseMenu = GameObject.Find("PauseMenu");
        this.pauseMenu.SetActive(false);

        this.skillPanel = GameObject.Find("SkillPanel");
        this.skillPanel.SetActive(true);

        this.statusBar = GameObject.Find("StatusBar");
        this.statusBar.SetActive(true);

        this.inventory = GameObject.Find("Inventory");
        this.inventory.SetActive(false);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = player.transform.position;

        float fire1Axis = Input.GetAxis("Fire1");
        float fire2Axis = Input.GetAxis("Fire2");
        float fire3Axis = Input.GetAxis("Fire3");

        float cancelAxis = Input.GetAxis("Cancel");
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(this.pauseMenu.activeSelf)
                {
                    this.pauseMenu.SetActive(false);
                    actionable = true;
                }
                else
                {
                    this.pauseMenu.SetActive(true);
                    actionable = false;
                }                
            }

            if(Input.GetKeyDown(KeyCode.I))
            {
                if(this.inventory.activeSelf)
                {
                    this.inventory.SetActive(false);
                }
                else
                {
                    this.inventory.SetActive(true);
                }
            }
        }
    }

    public bool GUIContainsCursor()
    {
        RectTransform canvasRectTransform = guiCanvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRectTransform.rect.size;

        Vector2 mousePosition = Input.mousePosition;

        GameObject[] GUIObjectList = { this.pauseMenu, this.skillPanel, this.statusBar, this.inventory };

        foreach (GameObject guiObject in GUIObjectList)
        {
            if(!guiObject.activeSelf)
            {
                continue;
            }

            RectTransform guiObjectRectTransform = guiObject.GetComponent<RectTransform>();
            Rect guiObjectRect = guiObjectRectTransform.rect;

            if (RectTransformUtility.RectangleContainsScreenPoint(guiObjectRectTransform, mousePosition))
            {
                return true;
            }
        }

        return false;
    }
}
