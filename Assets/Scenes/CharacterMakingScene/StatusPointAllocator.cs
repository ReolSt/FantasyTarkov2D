using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class StatusAllocator
{
    public GameObject allocatorObject;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public GameObject remainingPointDisplay;
    private Text remainingPointDisplayComponent;

    private List<GameObject> LEDs = new List<GameObject>();

    public int maxPoint;
    private int currentPoint = 0;
    public StatusAllocator(GameObject allocatorObject, GameObject remainingPointDisplay, int maxPoint)
    {
        this.allocatorObject = allocatorObject;
        this.maxPoint = maxPoint;

        this.leftArrow = this.allocatorObject.transform.Find("LeftArrow").gameObject;
        this.rightArrow = this.allocatorObject.transform.Find("RightArrow").gameObject;

        this.remainingPointDisplay = remainingPointDisplay;
        this.remainingPointDisplayComponent = this.remainingPointDisplay.GetComponent<Text>();

        for (int i = 1; i <= this.maxPoint; ++i)
        {
            LEDs.Add(this.allocatorObject.transform.Find(i.ToString()).gameObject);
        }

        this.leftArrow.GetComponent<Button>().onClick.AddListener(Decrease);
        this.rightArrow.GetComponent<Button>().onClick.AddListener(Increase);
    }

    public void Increase()
    {
        int currentRemainingPoint = Int32.Parse(this.remainingPointDisplayComponent.text);
        if(currentRemainingPoint == 0)
        {
            return;
        }

        if(this.currentPoint >= this.maxPoint)
        {
            return;
        }

        ++this.currentPoint;

        Image LEDImage = LEDs[this.currentPoint - 1].GetComponent<Image>();
        Color activeColor = LEDImage.color;
        activeColor.a = 255.0f / 255.0f;

        LEDImage.color = activeColor;

        this.remainingPointDisplayComponent.text = (Int32.Parse(this.remainingPointDisplayComponent.text) - 1).ToString();
    }

    public void Decrease()
    {
        if(this.currentPoint <= 0)
        {
            return;
        }

        Image LEDImage = LEDs[this.currentPoint - 1].GetComponent<Image>();
        Color deactiveColor = LEDImage.color;
        deactiveColor.a = 50.0f / 255.0f;

        LEDImage.color = deactiveColor;

        --this.currentPoint;

        this.remainingPointDisplayComponent.text = (Int32.Parse(this.remainingPointDisplayComponent.text) + 1).ToString();
    }
}


public class StatusPointAllocator : MonoBehaviour
{
    public int allocatableStatusPoint = 10;
    public int maxStatusPoint = 6;

    public string[] statusNames;

    private GameObject remainingPoints;

    private Dictionary<string, StatusAllocator> statusAllocators = new Dictionary<string, StatusAllocator>();

    private DebugVariablesDisplayer debugVariablesDisplayer;

    private void Start()
    {
        this.remainingPoints = GameObject.Find("RemainingPoints").gameObject;
        this.remainingPoints.GetComponent<Text>().text = allocatableStatusPoint.ToString();

        foreach (string statusName in statusNames)
        {
            statusAllocators.Add(statusName, new StatusAllocator(GetAllocatorObject(statusName), this.remainingPoints, this.maxStatusPoint));
        }

        this.debugVariablesDisplayer = GameObject.Find("DebugVariablesDisplayer").GetComponent<DebugVariablesDisplayer>();
    }

    private GameObject GetAllocatorObject(string name)
    {
        GameObject allocatorContainer = GameObject.Find(name);
        return allocatorContainer.transform.Find("Allocator").gameObject;
    }

    private void Update()
    {
        
    }
}
