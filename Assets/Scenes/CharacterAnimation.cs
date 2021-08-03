using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject characterSpriteObject;

    private Animator animator;
    private GUIManager guiManager;

    void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
        this.guiManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        float Fire1Axis = Input.GetAxis("Fire1");

        if(!this.guiManager.actionable)
        {
            RunEnd();
            AttackEnd();
            return;
        }

        if(horizontalAxis != 0.0f && this.animator.GetFloat("Attack") == 0.0f)
        {
            SetDirection(horizontalAxis);
        }        

        if ((horizontalAxis != 0.0f || verticalAxis != 0.0f) && this.animator.GetFloat("Run") == 0.0f)
        {
            RunStart(horizontalAxis > 0.0f);
        }

        if (Fire1Axis > 0.0f && this.animator.GetFloat("Attack") == 0.0f && !this.guiManager.GUIContainsCursor())
        {
            AttackStart();
        }

        if ((horizontalAxis == 0.0f && verticalAxis == 0.0f) && this.animator.GetFloat("Run") == 1.0f)
        {
            RunEnd();
        }

        if ( Fire1Axis == 0.0f && this.animator.GetFloat("Attack") > 0.0f )
        {
            AttackEnd();
        }
    }

    void RunStart(bool mirror)
    {
        this.animator.SetFloat("Run", 1.0f);        
    }

    void SetDirection(float axis)
    {
        characterSpriteObject.transform.rotation = Quaternion.Euler(0.0f, axis > 0.0f ? 180.0f : 0.0f, 0.0f);
    }

    void RunEnd()
    {
        this.animator.SetFloat("Run", 0.0f);
    }

    void AttackStart()
    {
        this.animator.SetFloat("Attack", 1.0f);
    }

    void AttackEnd()
    {
        this.animator.SetFloat("Attack", 0.0f);
    }
}
