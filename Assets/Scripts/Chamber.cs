using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chamber : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] Image _bulletImage;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _dropShellClip;
    [SerializeField] AudioClip _loadBulletClip;
    [SerializeField] ParticleSystem _particleSystem;

    Image _chamberImage;

    public bool IsLoaded = false;
    public bool IsFired = false;
    public Action OnFired;
    public Action OnLoaded;
    public Bullet LoadedBullet;

    public static Chamber Focused;

    void Start()
    {
        _chamberImage = transform.GetComponent<Image>();
        _bulletImage.enabled = false;
        OnFired += Fire;
    }

    void Fire()
    {
        if (IsLoaded == true && IsFired == false)
        {
            _bulletImage.sprite = LoadedBullet.FiredSprite;
            LoadedBullet.OnFired?.Invoke();
            IsFired = true;
        }
    }

    public void Load(Bullet bullet)
    {
        LoadedBullet = bullet;
        _bulletImage.enabled = true;
        _bulletImage.sprite = LoadedBullet.LoadedSprite;
        IsLoaded = true;
        _audioSource.PlayOneShot(_loadBulletClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Focused == null)
            Focused = this;
        if (Ammo.HeldAmmo != null)
            Ammo.HeldAmmo.UpdateSprite();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsFired && _chamberImage.enabled)
        {
            IsFired = false;
            IsLoaded = false;
            _bulletImage.enabled = false;
            _audioSource.PlayOneShot(_dropShellClip);
            _particleSystem.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Focused = null;
        if (Ammo.HeldAmmo != null)
            Ammo.HeldAmmo.UpdateSprite(false);
    }

}
