using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    // Скрипт по обработке нажатий клавиш
    public CharacterStatus characterStatus;
    public Weapon weapon;
    public Transform targetLook;

    public bool debugAiming; 
    public bool isAming;     

    public bool opportunityToArm;   //проверка на возможность прицелиться
    public float distance;          //дистанция от персонажа до припятствия

    void Start()
    {
        targetLook = weapon.targetLook;
    }
    public void InputUpdate ()
    {
        RayCastAiming();

        if (Input.GetMouseButton(1) && opportunityToArm)
        {
            characterStatus.isAiming = true;
            characterStatus.isAimingMove = true;
        }
        if (Input.GetMouseButton(1) && !opportunityToArm)
        {
            characterStatus.isAiming = false;
            characterStatus.isAimingMove = true;
        }
        if (!Input.GetMouseButton(1))
        {
            characterStatus.isAiming = false;
            characterStatus.isAimingMove = false;
        }

        /*if (!debugAiming)
        {
            characterStatus.isAiming = Input.GetMouseButton(1); // Если зажата правая кнопка мыши, то статус true
        }
        else characterStatus.isAiming = isAming;*/

        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) && opportunityToArm)
        {
            weapon.Shoot();
        }
    }
    //в этом методе будем проверять дистанцию от нас до припятствия
    public void RayCastAiming()
    {
        Debug.DrawLine(transform.position + transform.up * 1.4f, targetLook.position, Color.green);

        distance = Vector3.Distance(transform.position + transform.up * 1.4f, targetLook.position);
        if (distance > 1.5f)
        {
            opportunityToArm = true;
        }
        else
        {
            opportunityToArm = false;
        }
    }
}