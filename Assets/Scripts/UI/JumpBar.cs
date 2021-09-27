using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JumpBar : MonoBehaviour
{   
    [SerializeField]
    private RectTransform _bar;

    [SerializeField]
    private Image _background;

    private Image _barImage;

    private EventManager _eventManager;

    private float _maxWidth;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _eventManager.OnSetBarActive += SetActive;
        _eventManager.OnUpdateJumpBar += Updatebar;

        _barImage = _bar.GetComponent<Image>();

        _maxWidth = _bar.rect.width;
    }

    private void SetActive(bool b)
    {
        _barImage.enabled = b;
        _background.enabled = b;
    }

    private void Updatebar(float progress)
    {
        _bar.sizeDelta = new Vector2(_maxWidth * progress, _bar.sizeDelta.y);
    }
}
