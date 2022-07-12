using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("JSObjects")]
    [SerializeField] private Image _jsContainer;
    [SerializeField] private Image _joystick;
    [Space]
    [SerializeField] private bool _isStationary;

    public Vector3 InputDirection;

    public event Action<bool> OnJSDrag;

    void Start()
    {
        _jsContainer.gameObject.SetActive(_isStationary);
        InputDirection = Vector3.zero;
    }
    public void OnDrag(PointerEventData ped)
    {
        Vector2 position = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_jsContainer.rectTransform,
                ped.position,
                ped.pressEventCamera,
                out position);
        position.x = (position.x / _jsContainer.rectTransform.sizeDelta.x);
        position.y = (position.y / _jsContainer.rectTransform.sizeDelta.y);

        float x = position.x * 2;
        float y = position.y * 2;

        InputDirection = new Vector3(x, y, 0);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

        _joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (_jsContainer.rectTransform.sizeDelta.x / 3)
                                                               , InputDirection.y * (_jsContainer.rectTransform.sizeDelta.y) / 3);

    }
    public void OnPointerDown(PointerEventData ped)
    {
        ChangeJSState(true);
        OnDrag(ped);
        OnJSDrag?.Invoke(true);
    }
    public void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        _joystick.rectTransform.anchoredPosition = Vector3.zero;
        OnJSDrag?.Invoke(false);
        ChangeJSState(false);
    }
    private void ChangeJSState(bool isActive)
    {
        if (!_isStationary)
        {
            _jsContainer.gameObject.SetActive(isActive);
            _jsContainer.transform.position = Input.mousePosition;
        }
    }
}