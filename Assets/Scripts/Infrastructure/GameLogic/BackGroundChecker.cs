using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.GameLogic;
using Infrastructure.GameLogic.Actions;
using UnityEngine;

public class BackGroundChecker : MonoBehaviour
{
    [SerializeField] private GameObject _whiteBackground;
    [SerializeField] private GameObject _blackBackground;

    private void OnEnable()
    {
        LightAction.OnLight += ChangeBackground;
    }

    private void ChangeBackground(bool isOn)
    {
        if (isOn)
        {
            _blackBackground.SetActive(true);
            _whiteBackground.SetActive(false);
        }
        else
        {
            _blackBackground.SetActive(false);
            _whiteBackground.SetActive(true);
        }
    }
}
