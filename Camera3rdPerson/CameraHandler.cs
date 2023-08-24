//Скрипт вешается на GameObject CameraHandler

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform camTrans;
    public Transform pivot;
    public Transform character;
    public Transform mTransform;

    public CharacterStatus characterStatus;
    public CameraConfig cameraConfig;
    public bool leftPivot;
    public float delta;

    public Transform targetLook;

    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXVelocity;
    public float smoothYVelocity;
    public float lookAngle;
    public float titleAngle;

    void Update ()
    {
        Tick();
    }

    void Tick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, character.position, 1);
        mTransform.position = targetPosition;

        TargetLook();
    }

    void TargetLook()
    {
        //отправим лучь на объект и в зависимости от места где будет соприкосновение - туда и отправим таргет лук
        Ray ray = new Ray(camTrans.position, camTrans.forward * 2000);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetLook.position = Vector3.Lerp(targetLook.position, hit.point, Time.deltaTime * 40);
        }
        else
        {
            //если персонаж смотрит в небо
            targetLook.position = Vector3.Lerp(targetLook.position, targetLook.transform.forward * 200, Time.deltaTime * 5);
        }
    }

    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;

        if (characterStatus.isAiming)
        {
            targetX = cameraConfig.aimX;
            targetZ = cameraConfig.aimZ;
        }

        if (leftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = camTrans.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCameraPosition, t);
    }

    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraConfig.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraConfig.turnSmooth);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraConfig.Y_rot_speed;
        Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = targetRot;

        titleAngle -= smoothY * cameraConfig.X_rot_speed;
        titleAngle = Mathf.Clamp(titleAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(titleAngle, 0, 0);
    }
}