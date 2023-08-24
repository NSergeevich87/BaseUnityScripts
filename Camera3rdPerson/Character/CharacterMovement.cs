//Скрипт вешается на персонажа

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform CameraTransform;
    public CharacterStatus characterStatus;

    public float vertical;
    public float horizontal;
    public float moveAmount;
    public float rotationSpeed;

    public Vector3 rotationDirection;
    public Vector3 moveDirection;
    public void MoveUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        //Перемещение физикой
        /*transform.Translate(Vector3.right * horizontal * Time.deltaTime);
        transform.Translate(Vector3.forward * vertical * Time.deltaTime);*/

        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterStatus.isSprint = true;
        }
        else
        {
            characterStatus.isSprint = false;
        }
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            characterStatus.isJump = true;
        }
        else
        {
            characterStatus.isJump = false;
        }

        //Перемещаем камеру
        Vector3 moveDir = CameraTransform.forward * vertical;
        moveDir += CameraTransform.right * horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = CameraTransform.forward;

        RotationNormal();
        characterStatus.isGround = Ground();
    }

    //Для поворота персонажа создадим метод
    public void RotationNormal()
    {
        //Сначала проверяем целится ли персонаж
        if (!characterStatus.isAiming)
        {
            rotationDirection = moveDirection;
        }

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        //Сам поворот персонажа
        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, rotationSpeed);
        transform.rotation = targetRot;
    } 

    //Новый метод, для того, чтобы персонаж не проходил сквозь объекты
    public bool Ground()
    {
        //Сделаем проверку есть ли под персонажем что-то
        Vector3 origin = transform.position;
        origin.y += 0.6f;
        Vector3 dir = Vector3.up;
        float dis = 0.7f;
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, dis))
        {
            Vector3 tp = hit.point;
            transform.position = tp;
            return true;
        }
        return false; 
    }
}