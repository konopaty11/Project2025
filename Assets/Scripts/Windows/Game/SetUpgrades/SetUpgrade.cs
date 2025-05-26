using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetUpgrade : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform rawImage;
    [SerializeField] Upgrades upgrade;
    public Upgrades Upgrade => upgrade;

    bool isReturning = false;
    public bool IsReturning => isReturning;
    Vector3 startPos;
    Game game;

    private void Start()
    {
        startPos = rawImage.position;
        game = FindObjectOfType<Game>();
    }
    void Update()
    {
        if (isReturning)
        {
            float deltaTime = Time.deltaTime * 3;
            rawImage.position = Vector3.Lerp(rawImage.position, startPos, deltaTime);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isReturning = false;
        rawImage.position = eventData.position;

        game.SetActiveUpgradeOutline(upgrade);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isReturning = true;
        game.ActivateUpgradeOutline();
    }
}
