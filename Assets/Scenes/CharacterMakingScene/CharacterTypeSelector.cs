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

    private (string group, int lastIndex)[] characterTypes = new (string, int)[]
    {
        ( "Human", 17 ),
        ( "Elf", 4 ),
        ( "Skeleton" , 6 ),
        ( "Devil", 4 )
    };

    private int characterTypeIndex = 0;
    private int characterSpriteIndex = 1;

    void Start()
    {
        this.leftArrowObject = transform.Find("LeftArrow").gameObject;
        this.leftArrowButton = this.leftArrowObject.GetComponent<Button>();

        this.rightArrowObject = transform.Find("RightArrow").gameObject;
        this.rightArrowButton = this.rightArrowObject.GetComponent<Button>();

        this.characterTypeObject = transform.Find("CharacterTypeObject").gameObject;

        this.leftArrowButton.onClick.AddListener(this.onLeftArrowButtonClick);
        this.rightArrowButton.onClick.AddListener(this.onRightArrowButtonClick);
    }

    void Update()
    {
        
    }

    void onLeftArrowButtonClick()
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
    }

    void onRightArrowButtonClick()
    {
        ++this.characterSpriteIndex;
        if(this.characterSpriteIndex > this.characterTypes[this.characterTypeIndex].lastIndex)
        {
            this.characterTypeIndex = (this.characterTypeIndex + 1) % this.characterTypes.Length;
            this.characterSpriteIndex = 0;
        }
    }
}
