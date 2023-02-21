using Chowen;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickupTextField;
    private bool textActive = false;
    private void Start()
    {
        //pickupTextField.canvasRenderer.SetAlpha(0f);
    }
    private void DisplayPickupText(string text)
    {
        if (!textActive)
        {
            textActive = true;
            StartCoroutine(EnablePickupText(text));
        }
        else pickupTextField.text = text;
    }

    private IEnumerator EnablePickupText(string text)
    {
        pickupTextField.text = text;
        pickupTextField.CrossFadeAlpha(1f, 0.1f, true);
        yield return new WaitForSeconds(1f);
        pickupTextField.CrossFadeAlpha(0f, 0.1f, true);
        yield return new WaitForSeconds(0.1f);
        textActive = false;
    }

    private void OnEnable()
    {
        EventManager.OnPelletEaten += DisplayPickupText;
    }
    private void OnDisable()
    {
        EventManager.OnPelletEaten -= DisplayPickupText;
    }

}
