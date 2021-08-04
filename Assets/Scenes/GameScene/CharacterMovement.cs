using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public DebugVariablesDisplayer debugVariablesDisplayer;

    private Animator animator;
    private GUIManager guiManager;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponentInChildren<Animator>();

        debugVariablesDisplayer = GameObject.Find("DebugVariablesDisplayer").GetComponent<DebugVariablesDisplayer>();
        debugVariablesDisplayer.AddVariable("movement");

        this.guiManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();
    }

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if(!this.guiManager.actionable)
        {
            return;
        }

        if(this.animator.GetFloat("Attack") == 0.0f)
        {
            Move(new Vector2(horizontalAxis, verticalAxis), Time.deltaTime);
        }        
    }
    void Move(Vector2 direction, float deltaTime)
    {
        Vector2 movement = new Vector2(moveSpeed * direction.x, moveSpeed * direction.y) * deltaTime;

        transform.Translate(movement);

        debugVariablesDisplayer.UpdateVariable("movement", movement.ToString());
    }
}
