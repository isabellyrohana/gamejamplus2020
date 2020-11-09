using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UiPressArrow : MonoBehaviour
{
    
    [SerializeField] private Image arrowUp;
    [SerializeField] private Image arrowRight;
    [SerializeField] private Image arrowDown;
    [SerializeField] private Image arrowLeft;
    
    private Image imageArrow;
    private Text textKey;
    
    void Awake()
    {
        imageArrow = GetComponent<Image>();
        textKey = GetComponentInChildren<Text>();

        imageArrow.gameObject.SetActive(false);

        Events.ObserverManager.Register<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, ShowArrow);
        Events.ObserverManager.Register(NotifyEvent.Interactions.Arrows.Hide, HideArrow);
    }

    private void ShowArrow(string key, Vector2 position)
    {
        HideArrows();
        if (!string.IsNullOrEmpty(key))
        {
            switch (key)
            {
                case "UP": arrowUp.gameObject.SetActive(true); break;
                case "RIGHT": arrowRight.gameObject.SetActive(true); break;
                case "DOWN": arrowDown.gameObject.SetActive(true); break;
                case "LEFT": arrowLeft.gameObject.SetActive(true); break;
                case "W": case "A": case "S": case "D": 
                case "I": case "J": case "K": case "L": 
                    textKey.gameObject.SetActive(true); 
                    textKey.text = key;
                    break;
            }

            imageArrow.rectTransform.position = position;
            imageArrow.gameObject.SetActive(true);
        }
    }

    private void HideArrow()
    {
        HideArrows();
        imageArrow.gameObject.SetActive(false);
    }

    private void HideArrows()
    {
        textKey.text = "";
        textKey.gameObject.SetActive(false);
        arrowUp.gameObject.SetActive(false);
        arrowRight.gameObject.SetActive(false);
        arrowDown.gameObject.SetActive(false);
        arrowLeft.gameObject.SetActive(false);
    }

}
