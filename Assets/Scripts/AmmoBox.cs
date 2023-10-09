using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AmmoBox : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform _ammoBelt;
    [SerializeField] GameObject _ammoPrefab;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_ammoBelt.childCount < 6)
            AddAmmo();
    }

    void AddAmmo()
    {
        object[] bulletSO = Resources.LoadAll("Scriptable Objects");
        int random = UnityEngine.Random.Range(0, bulletSO.Length);
        GameObject ammoObject = Instantiate(_ammoPrefab, _ammoBelt);
        Ammo ammo = ammoObject.GetComponent<Ammo>();
        Bullet bullet = (Bullet)bulletSO[random];

        ammoObject.GetComponent<Image>().sprite = bullet.UnloadedSprite;
        ammo.Bullet = bullet;
        ammo.ToLoadSprite = bullet.LoadedSprite;
        ammo.UnloadedSprite = bullet.UnloadedSprite;

        ammo.enabled = true;
    }
}
