using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ammo : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera _uiCamera;
    Image _ammoImage;

    public Sprite ToLoadSprite;
    public Sprite UnloadedSprite;
    public Bullet Bullet;

    public static Ammo HeldAmmo;
    public static Image _heldAmmoImage;

    void OnEnable()
    {
        _uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        _ammoImage = GetComponent<Image>();
        _heldAmmoImage = GameObject.FindGameObjectWithTag("HeldAmmo").GetComponent<Image>();
    }

    public void UpdateSprite(bool aboutToLoad = true) => _heldAmmoImage.sprite = aboutToLoad ? ToLoadSprite : UnloadedSprite;

    public void OnPointerEnter(PointerEventData eventData) => HeldAmmo = this;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _ammoImage.enabled = false;
        _heldAmmoImage.sprite = _ammoImage.sprite;
        _heldAmmoImage.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = _uiCamera.ScreenToWorldPoint(eventData.position);
        _heldAmmoImage.transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Chamber.Focused != null && Chamber.Focused.IsLoaded == false)
        {
            Chamber.Focused.Load(HeldAmmo.Bullet);
            Destroy(gameObject);
        }
        else 
            _ammoImage.enabled = true;

        _heldAmmoImage.enabled = false;
    }
}
