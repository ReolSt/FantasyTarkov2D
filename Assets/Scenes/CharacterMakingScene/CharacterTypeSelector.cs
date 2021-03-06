using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTypeSelector : MonoBehaviour
{
    private GameObject leftArrowObject;
    private Button leftArrowButton;

    private GameObject rightArrowObject;
    private Button rightArrowButton;

    private GameObject characterTypeObject;

    private GameObject characterPrefabContainer;

    private (string group, int lastIndex)[] characterTypes = new (string, int)[]
    {
        ( "Human", 17 ),
        ( "Elf", 4 ),
        ( "Skelton" , 6 ),
        ( "Devil", 3 )
    };

    private int characterTypeIndex = 0;
    private int characterSpriteIndex = 1;

    private CharacterPrefabLoader characterPrefabLoader;

    private DebugVariablesDisplayer debugVariablesDisplayer;

    private void Start()
    {
        this.leftArrowObject = transform.Find("LeftArrow").gameObject;
        this.leftArrowButton = this.leftArrowObject.GetComponent<Button>();

        this.rightArrowObject = transform.Find("RightArrow").gameObject;
        this.rightArrowButton = this.rightArrowObject.GetComponent<Button>();

        this.characterTypeObject = transform.Find("Value").gameObject;

        this.characterPrefabContainer = GameObject.Find("CharacterPrefab");

        this.leftArrowButton.onClick.AddListener(this.onLeftArrowButtonClick);
        this.rightArrowButton.onClick.AddListener(this.onRightArrowButtonClick);

        this.characterPrefabLoader = gameObject.GetComponent<CharacterPrefabLoader>();

        this.debugVariablesDisplayer = GameObject.Find("DebugVariablesDisplayer").GetComponent<DebugVariablesDisplayer>();

        UpdateCharacter();
    }

    private void Update()
    {
        
    }

    private void UpdateCharacter()
    {
        foreach (Transform childTransform in this.characterPrefabContainer.transform)
        {
            GameObject.Destroy(childTransform.gameObject);
        }

        GameObject prefab = characterPrefabLoader.Load(this.characterTypes[this.characterTypeIndex].group, this.characterSpriteIndex);

        prefab.transform.SetParent(this.characterPrefabContainer.transform);
        prefab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        prefab.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        this.characterTypeObject.GetComponent<Text>().text = this.characterTypes[this.characterTypeIndex].group + " " + this.characterSpriteIndex;
    }

    private void onLeftArrowButtonClick()
    {
        --this.characterSpriteIndex;
        if(this.characterSpriteIndex < 1)
        {
            --this.characterTypeIndex;
            if(this.characterTypeIndex < 0)
            {
                this.characterTypeIndex = this.characterTypes.Length - 1;
            }

            this.characterSpriteIndex = this.characterTypes[this.characterTypeIndex].lastIndex;
        }

        UpdateCharacter();
    }

    private void onRightArrowButtonClick()
    {
        ++this.characterSpriteIndex;
        if(this.characterSpriteIndex > this.characterTypes[this.characterTypeIndex].lastIndex)
        {
            this.characterTypeIndex = (this.characterTypeIndex + 1) % this.characterTypes.Length;
            this.characterSpriteIndex = 1;
        }

        UpdateCharacter();
    }

    public string GetPrefabGroup()
    {
        return this.characterTypes[this.characterTypeIndex].group;
    }

    public int GetPrefabIndex()
    {
        return this.characterSpriteIndex;
    }
}
