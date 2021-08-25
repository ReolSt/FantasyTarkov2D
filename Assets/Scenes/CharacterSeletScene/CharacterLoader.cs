using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterLoader : MonoBehaviour
{
    public RectTransform slotContainerTransform;

    private GameObject slotSample;
    private GameObject slotNew;

    private int slotIndex = 0;

    private CharacterPrefabLoader characterPrefabLoader;
    private PlayerSender playerSender;
    private DatabaseAccess databaseAccess;
    // Start is called before the first frame update
    private void Start()
    {

        this.slotSample = this.slotContainerTransform.Find("SlotSample").gameObject;
        this.slotNew = this.slotContainerTransform.Find("SlotNew").gameObject;
        this.slotNew.GetComponent<Button>().onClick.AddListener(OnSlotNewClick);

        characterPrefabLoader = gameObject.GetComponent<CharacterPrefabLoader>();

        GameObject databaseAccessObject = GameObject.Find("DatabaseAccess");
        this.databaseAccess = databaseAccessObject.GetComponent<DatabaseAccess>();

        string query = "Select Id, PlayTime, Name, PrefabGroup, PrefabIndex from Player";

        databaseAccess.Read(query, OnRead);

        Destroy(slotSample);

        StretchSlotContainer(100.0f);

        this.slotNew.transform.SetAsLastSibling();

        this.playerSender = GameObject.Find("PlayerSender").GetComponent<PlayerSender>();
    }

    private void StretchSlotContainer(float size)
    {
        RectTransform slotContainerRectTransform = this.slotContainerTransform;
        Vector2 newSizeDelta = slotContainerRectTransform.sizeDelta;
        newSizeDelta.y += size;

        slotContainerRectTransform.sizeDelta = newSizeDelta;
    }

    private void OnRead(IDataReader reader)
    {
        int id = reader.GetInt32(0);
        string playTime = reader.GetString(1);
        string name = reader.GetString(2);
        string prefabGroup = reader.GetString(3);
        int prefabIndex = reader.GetInt32(4);

        GameObject newSlot = Instantiate(this.slotSample);
        newSlot.name = "Slot" + this.slotIndex;

        newSlot.transform.SetParent(this.slotContainerTransform);
        newSlot.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        newSlot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        newSlot.GetComponent<Button>().onClick.AddListener(() =>
        {
            this.playerSender.playerId = id;
            SceneManager.LoadScene("LobbyScene");
        });


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

        StretchSlotContainer(100.0f);

        ++this.slotIndex;
    }

    void OnSlotNewClick()
    {
        SceneManager.LoadScene("CharacterMakingScene");
    }

    private void Update()
    {
        
    }
}
