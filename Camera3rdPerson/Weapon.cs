using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponProperties weaponProperties;
    public Transform shotPoint;
    public Transform targetLook;

    public GameObject cameraMain;
    public GameObject decal;
    public GameObject bullet;

    public ParticleSystem muzzleFlash;
    AudioSource audioSource;
    public AudioClip shootClip;

    public GameObject shell;
    public Transform shellPosition;
    public int damage;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        shotPoint.LookAt(targetLook);
        //как будет целиться персонаж
        Vector3 origin = shotPoint.position;
        Vector3 dir = targetLook.position;

        RaycastHit hit;

        //decal.SetActive(false);
        Debug.DrawLine(origin, dir, Color.black);
        Debug.DrawLine(cameraMain.transform.position, dir, Color.black);

        /*if (Physics.Linecast(origin, dir, out hit))
        {
            //decal.SetActive(true);
            //поварачиваем декаль (дырку от пули) и указываем ей новую позицию
            decal.transform.position = hit.point + hit.normal * 0.01f; //при помощи normal отодвигаем декаль на 1 сотую от об-та (или будет мигать)
            //теперь поворачиваем декаль
            decal.transform.rotation = Quaternion.LookRotation(-hit.normal);
        }*/
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        newBullet.GetComponent<Bullet>().damage = damage;
        //во время стрельбы воспроизводятся звук и вспышка
        audioSource.PlayOneShot(shootClip);
        muzzleFlash.Play();

        AddShell();
    }

    void AddShell()
    {
        //сдесь будут позиция, поворот и сила гильзы
        //Создаем гильзу
        GameObject newShell = Instantiate(shell);
        newShell.transform.position = shellPosition.position;
        //Присваиваем поворот
        Quaternion rot = shellPosition.rotation;
        newShell.transform.rotation = rot;
        //Убераем родителя
        newShell.transform.parent = null;
       //Применяем к ней силу
        newShell.GetComponent<Rigidbody>().AddForce(-newShell.transform.forward * Random.Range(80, 120));
        Destroy(newShell, 20);
    }
}