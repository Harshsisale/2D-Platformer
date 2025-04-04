﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class intended to work with grid layout groups to create an image based health bar
/// </summary>
public class HealthDisplay : UIelement
{
    [Header("Settings")]
    [Tooltip("The image which represents one unit of health")]
    public GameObject healthDisplayImage = null;
    [Tooltip("The prefab to use to display the number")]
    public GameObject numberDisplay = null;
    [Tooltip("The maximum number of images to display before switching to just a number")]
    public int maximumNumberToDisplay = 3;

    /// <summary>
    /// Description:
    /// Upadates this UI element
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    public override void UpdateUI()
    {
        if (GameManager.instance != null && GameManager.instance.player != null)
        {
            Health playerHealth = GameManager.instance.player.GetComponent<Health>();
            if (playerHealth != null)
            {
                SetChildImageNumber(playerHealth.currentHealth);
            }
        }
    }

    /// <summary>
    /// Description:
    /// Deletes and spawns images until this gameobject has as many children as the player has health
    /// Input: 
    /// int
    /// Return: 
    /// void (no return)
    /// </summary>
    /// <param name="number">The number of images that this object should have as children</param>
    private void SetChildImageNumber(int number)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if (healthDisplayImage != null)
        {
            if (maximumNumberToDisplay >= number)
            {
                for (int i = 0; i < number; i++)
                {
                    Instantiate(healthDisplayImage, transform);
                }
            }
            else
            {
                Instantiate(healthDisplayImage, transform);
                GameObject createdNumberDisp = Instantiate(numberDisplay, transform);
                createdNumberDisp.GetComponent<TMP_Text>().SetText(number.ToString());
            }
        }
    }
}
