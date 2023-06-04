using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;

public class HoverMouseToInformation : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject _tooltipBox;

    [SerializeField]
    private GameObject _buySpell;

    private float _height;
    private float _width;

    private bool _isHoverMouse;
    private GraphicRaycaster m_Raycaster;
    private EventSystem m_EventSystem;

    private GameObject _chosenGameObject;

    private void Awake()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    private void Start()
    {
        _height = _tooltipBox.GetComponent<RectTransform>().rect.height + 10f;
        _width = _tooltipBox.GetComponent<RectTransform>().rect.width + 10f;

        _tooltipBox.SetActive(false);
        _buySpell.SetActive(false);
    }

    private void Update()
    {
        DetectIsNeedUi();

        if (_isHoverMouse)
        {
            UpdateDataOnTooltip();
            ChangePositionToMouse();
            CheckElementPosition();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _tooltipBox.SetActive(false);
        }
    }

    private void ChangePositionToMouse()
    {
        _tooltipBox.transform.position = Input.mousePosition;
        _tooltipBox.transform.position += new Vector3(0, _height / 2f, 0f);
    }

    private void DetectIsNeedUi()
    {
        _isHoverMouse = false;
        _tooltipBox.SetActive(false);

        PointerEventData pointerEventData = new PointerEventData(m_EventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == "SpellSlot")
            {
                _chosenGameObject = result.gameObject;
                _isHoverMouse = true;
                _tooltipBox.SetActive(true);
                break;
            }
        }
    }

    private void CheckElementPosition()
    {
        if (IsElementBehindScreen(_tooltipBox))
        {
            
        }
    }

    private bool IsElementBehindScreen(GameObject element)
    {
        Vector3[] corners = new Vector3[4];
        RectTransform rectTransform = element.GetComponent<RectTransform>();
        rectTransform.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[1];
        Vector3 topRight = corners[1];

        // Check if any corner of the element is outside the screen bounds
        bool isBehindScreen =
            bottomLeft.x > Screen.width
            || topRight.x < 0
            || bottomLeft.y > Screen.height
            || topRight.y < 0;

        return isBehindScreen;
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (_chosenGameObject.transform.parent.gameObject.name.Contains("Shop"))
        {
            _buySpell.SetActive(true);
        _buySpell.transform.GetChild(0).GetComponent<BuySpell>().SpellToBuy = _chosenGameObject.GetComponent<Spells>();
        }
        

    }

    private void UpdateDataOnTooltip()
    {
        try
        {
            UpdateField("Name", _chosenGameObject.GetComponent<Spells>().SpellName);
            UpdateField("Price", _chosenGameObject.GetComponent<Spells>().Price.ToString());
            UpdateField("Damage", _chosenGameObject.GetComponent<Spells>().Damage.ToString());
            UpdateField("Range", _chosenGameObject.GetComponent<Spells>().Damage.ToString());
            UpdateField("Cooldown", _chosenGameObject.GetComponent<Spells>().Cooldown.ToString());
            UpdateField("Descr", _chosenGameObject.GetComponent<Spells>().Description);
        }
        catch (NullReferenceException exception)
        {
            Debug.LogError("Null reference exception occurred: " + exception.Message);
        }
        catch (Exception exception)
        {
            Debug.LogError("Exception occurred: " + exception.Message);
        }
    }

    private void UpdateField(string fieldName, string value)
    {
        var textObject = _tooltipBox.transform.Find(fieldName);
        var textMeshPro = textObject.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = value;
    }
}
