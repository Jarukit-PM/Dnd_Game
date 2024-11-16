using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class OnMouseOverRaceandClass : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] List<GameObject> existListItem = new List<GameObject>();
    [SerializeField] bool isMouseOver = false;
    [SerializeField] GameObject  detailListItemPrefab, detailListPanel;
    [SerializeField] TMP_Text headerText;
    [SerializeField] Transform detailContent;
    [SerializeField] CharacterCreation characterCreation;

    void Start()
    {
        
    }
    void Update()
    {
        if (isMouseOver)
        {
            detailListPanel.SetActive(true);
        }
        else
        {
            detailListPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        generateRaceDetailUI();
        isMouseOver = true;
       //Debug.Log("Mouse is over the dropdown.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        generateRaceDetailUI();
        isMouseOver = false;
        //Debug.Log("Mouse is no longer over the dropdown.");
    }



    private void generateRaceDetailUI()
    {
        foreach (GameObject item in existListItem)
        {
            Destroy(item);
        }
        existListItem = new List<GameObject>();

        Race race = characterCreation.GetSelectedRace();
        if (race != null)
        {
            float totalHeight = 0f;
            headerText.text = race.name;

            foreach (Skill skill in race.raceSkills)
            {
                // Instantiate and set up the prefab
                GameObject item = Instantiate(detailListItemPrefab, detailContent);
                existListItem.Add(item);
                item.GetComponent<SkillListItem>().SetUp(race.name, skill.detail);

                // Force layout rebuild to ensure the final height is accurate
                RectTransform prefabRect = item.GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(prefabRect);

                // Accumulate the prefab height
                totalHeight += prefabRect.rect.height;
            }

            // Adjust the panel size to fit all items
            RectTransform panelRect = detailListPanel.GetComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(panelRect.sizeDelta.x, totalHeight);

            // Force layout rebuild for the parent panel
            LayoutRebuilder.ForceRebuildLayoutImmediate(panelRect);
        }


        detailListPanel.SetActive(true);
    }
}
