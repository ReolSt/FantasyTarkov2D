using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreator : MonoBehaviour
{
    private InputField playerName;
    private Button confirmButton;
    private Button backButton;

    private StatusPointAllocator statusPointAllocator;
    private CharacterTypeSelector characterTypeSelector;

    private DatabaseAccess databaseAccess;
    private void Start()
    {
        playerName = GameObject.Find("PlayerName").transform.Find("InputField").GetComponent<InputField>();

        confirmButton = GameObject.Find("Confirm").GetComponent<Button>();
        backButton = GameObject.Find("Back").GetComponent<Button>();

        statusPointAllocator = GameObject.Find("StatusPointAllocator").GetComponent<StatusPointAllocator>();
        characterTypeSelector = GameObject.Find("CharacterTypeSelector").GetComponent<CharacterTypeSelector>();

        databaseAccess = GameObject.Find("DatabaseAccess").GetComponent<DatabaseAccess>();

        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        backButton.onClick.AddListener(LoadSelectScene);
    }

    private void Update()
    {
        
    }

    private void OnConfirmButtonClick()
    {
        int remainingSatusPoint = statusPointAllocator.GetRemainingPoint();
        if(remainingSatusPoint != 0)
        {
            // Show an Message Box
            return;
        }

        string query = "insert into player (Name, PrefabGroup, PrefabIndex, MaxHP, MaxMP, Attack, Defense, Speed, Luck) values (";
        string name = this.playerName.text;

        string queryParameters = "";

        string prefabGroup = characterTypeSelector.GetPrefabGroup();
        int prefabIndex = characterTypeSelector.GetPrefabIndex();

        queryParameters += string.Format("'{0}', '{1}', {2}, ", name, prefabGroup, prefabIndex);

        Dictionary<string, int> defaultStatus = new Dictionary<string, int>
        {
            { "MaxHP", 50 },
            { "MaxMP", 10 },
            { "Attack", 10 },
            { "Defense", 5 },
            { "Speed", 20 },
            { "Luck", 0 }
        };

        foreach (string statusName in new string[]{ "MaxHP", "MaxMP", "Attack", "Defense", "Speed", "Luck"})
        {
            int allocatedPoint = statusPointAllocator.GetCurrentPoint(statusName);
            queryParameters += (defaultStatus[statusName] + allocatedPoint) + ",";
        }
        queryParameters = queryParameters.TrimEnd(',');
        query += queryParameters + ")";

        this.databaseAccess.NonQuery(query, LoadSelectScene);
    }

    private void LoadSelectScene()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }
}
