﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUIScript : MonoBehaviour
{
    public static PlayerGUIScript staticThis;

    public static event Action MoveEvent;
    public static event Action AttackEvent;

    public Animator animator;

    public Image guiCharacterImageRenderer;
    public Slider healthSlider;
    public Slider willSlider;

    public Button moveButton;
    public Button attackButton;

    private void Start()
    {
        staticThis = this;
        Implement.IsTurnEvent += SetAsCurrentUnit;
    }

    private void Update()
    {
        animator.SetBool("isPlayerTurn", TurnManager.isPlayerTurn);
    }

    public void Move()
    {
        if (TurnManager.isPlayerTurn)
        {
            MoveEvent?.Invoke();
        }
    }

    public void Attack()
    {
        if (TurnManager.isPlayerTurn)
        {
            AttackEvent?.Invoke();
        }
    }

    public void Skip()
    {
        if (TurnManager.isPlayerTurn)
        {
            TurnManager.EndTurn();
        }
    }

    public void SetAsCurrentUnit(Implement unit)
    {
        if(unit != null)
        {
            guiCharacterImageRenderer.sprite = unit.unitSprite.sprite;
            SetSliders(unit.battle);
            if (TurnManager.isPlayerTurn)
            {
                moveButton.interactable = unit.move.GetCanMove() && !unit.hasMoved;
                attackButton.interactable = unit.battle.GetCanAttack() && !unit.hasAttacked;
            }
        } else
        {
            Debug.LogError("The GUI was feed a null.");
        }
    }

    void SetSliders(IBattle unit)
    {
        healthSlider.value = unit.GetHealthPercent();
        willSlider.value = unit.GetWillPercent();
    }

}
