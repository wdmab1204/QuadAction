using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class test : MonoBehaviour
{
    delegate void MyDelegate();
    MyDelegate attack;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attack?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            attack += PrimaryAttack;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            attack += SecondaryAttack;
        }
    }
    void PrimaryAttack()
    {
        // Primary attack
    }
    void SecondaryAttack()
    {
        // Secondary attack
    }

}
