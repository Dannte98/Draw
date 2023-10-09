using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hammer : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClicked;
    public UnityEvent OnReleased;

    public static Hammer Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Cylinder.Instance.IsEngaged == false)
            return;
        Cylinder.Instance.CanFire = true;
        OnClicked?.Invoke();
    }
}
