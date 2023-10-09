using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cylinder : MonoBehaviour
{
    [SerializeField] float _angle;
    [Range(10.0f, 50.0f)]
    [SerializeField] float _rotateTime;
    [SerializeField] float _engageTime;
    [SerializeField] Transform _handleTransform;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _gunshot;
    [SerializeField] AudioClip _dryshot;
    [SerializeField] AudioClip _drumSpin;

    bool _prime;
    bool _disengage;
    bool _engage;

    float _angleToRotate;
    float _currentRotation;
    float _handleRotation;

    public static Cylinder Instance;
    public Chamber[] Chambers = new Chamber[6];

    public bool IsEngaged = true;
    public bool CanFire;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        foreach (Chamber chamber in Chambers)
            chamber.transform.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        if (_prime)
        {
            float rotation = Mathf.Lerp(0.0f, _angle, 1.0f / _rotateTime);
            _currentRotation += rotation;
            transform.Rotate(rotation * Vector3.forward);
            if(_angleToRotate > 0.0f)
            {
                if (_currentRotation >= _angle)
                {
                    _prime = false;
                    _currentRotation = 0.0f;
                }
            }
            else
            {
                if (_currentRotation <= _angle)
                {
                    _prime = false;
                    _currentRotation = 0.0f;
                }
            }
        }

        if (_disengage)
        {
            float rotation = Mathf.Lerp(0.0f, 90.0f, 1.0f / _engageTime);
            _handleRotation += rotation;
            transform.RotateAround(_handleTransform.position, Vector3.forward, rotation);
            if(_handleRotation >= 90.0f)
                _disengage = false;
                
        }

        if (_engage)
        {
            float rotation = Mathf.Lerp(0.0f, -90.0f, 1.0f / _engageTime);
            _handleRotation += rotation;
            transform.RotateAround(_handleTransform.position, Vector3.forward, rotation);
            if (_handleRotation <= 0.0f)
                _engage = false;
        }

    }

    [ContextMenu("Rotate")]
    public void RotateCylinder()
    {
        _angleToRotate = _angle;
        _prime = true;
        Chamber lastChamber = Chambers[Chambers.Length - 1];

        for (int i = Chambers.Length - 1; i >= 1; i--)
            Chambers[i] = Chambers[i - 1];
        _audioSource.PlayOneShot(_drumSpin);
        Chambers[0] = lastChamber;
    }

    [ContextMenu("Fire")]
    public void Fire()
    {
        if (CanFire == false)
            return;
        Chamber firedChamber = Chambers[0];
        if (firedChamber.IsLoaded == false || firedChamber.IsFired == true)
            _audioSource.PlayOneShot(_dryshot);
        else
            _audioSource.PlayOneShot(_gunshot);
        firedChamber.OnFired?.Invoke();
        Hammer.Instance.OnReleased?.Invoke();
        CanFire = false;
    }

    [ContextMenu("Disengage")]
    public void Disengage()
    {
        if (IsEngaged == false || CanFire == true)
            return;
        foreach (Chamber chamber in Chambers)
            chamber.transform.GetComponent<Image>().enabled = true;
        _disengage = true;
        IsEngaged = false;
    }

    [ContextMenu("Engage")]
    public void Engage()
    {
        if (IsEngaged == true)
            return;
        foreach (Chamber chamber in Chambers)
            chamber.transform.GetComponent<Image>().enabled = false;
        _engage = true;
        IsEngaged = true;
    }


}
