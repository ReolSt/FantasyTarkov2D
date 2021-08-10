using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterLoader : MonoBehaviour
{
    public GameObject slotContainer;

    private GameObject slotSample;

    private int slotIndex = 0;

    CharacterPrefabLoader characterPrefabLoader;
    DatabaseAccess databaseAccess;
    // Start is called before the first frame update
    void Start()
    {
        this.slotSample = this.slotContainer.transform.Find("SlotSample").gameObject;

        characterPrefabLoader = gameObject.GetComponent<CharacterPrefabLoader>();

        GameObject databaseAccessObject = GameObject.Find("DatabaseAccess");
        this.databaseAccess = databaseAccessObject.GetComponent<DatabaseAccess>();

        string query = "Select Id, PlayTime, Name, PrefabGroup, PrefabIndex from Player";

        databaseAccess.Read(query, OnRead);

        Destroy(slotSample);
    }

    void OnRead(IDataReader reader)
    {
        Debug.Log(reader.ToString());
        int id = reader.GetInt32(0);
        string playTime = reader.GetString(1);
        string name = reader.GetString(2);
        string prefabGroup = reader.GetString(3);
        int prefabIndex = reader.GetInt32(4);

        GameObject newSlot = Instantiate(this.slotSample);
        newSlot.name = "Slot" + this.slotIndex;

        newSlot.transform.SetParent(this.slotContainer.transform);
        newSlot.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        newSlot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


        GameObject portraitContainer = newSlot.transform.Find("Portrait").gameObject;
        GameObject InfoContainer = newSlot.transform.Find("Info").gameObject;
        GameObject nameContainer = InfoContainer.transform.Find("Name").gameObject;
        GameObject playTimeContainer = InfoContainer.transform.Find("PlayTime").gameObject;

        GameObject characterPrefab = this.characterPrefabLoader.Load(prefabGroup, prefabIndex);
        characterPrefab.transform.SetParent(portraitContainer.transform);
        characterPrefab.transform.localPosition = new Vector3(10.0f, -20.0f, 0.0f);
        characterPrefab.transform.localScale = new Vector3(60.0f, 60.0f, 1.0f);

        characterPrefab.AddComponent<SpriteMaskInteractionSetter>();

        nameContainer.transform.Find("value").gameObject.GetComponent<Text>().text = name;
        playTimeContainer.transform.Find("value").gameObject.GetComponent<Text>().text = playTime;

        RectTransform slotContainerRectTransform = this.slotContainer.GetComponent<RectTransform>();
        Vector2 newSizeDelta = slotContainerRectTransform.sizeDelta;
        newSizeDelta.y += 100;

        slotContainerRectTransform.sizeDelta = newSizeDelta;

        ++this.slotIndex;
    }

    void Update()
    {
        
    }
}
