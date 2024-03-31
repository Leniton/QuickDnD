using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickKeyword : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] KeywordTextHandler textHandler;
    Camera _cameraToUse = null;

    public Action<Keyword> onKeywordClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        //eventData.position
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(textHandler.Text, mousePosition, _cameraToUse);

        if (intersectingLink < 0)
        {
            return;
        }

        TMP_LinkInfo linkInfo = textHandler.Text.textInfo.linkInfo[intersectingLink];
        Keyword keyword = KeywordDictionary.Get(linkInfo.GetLinkID());
        if (keyword == null) return;
        KeywordClicked(keyword);
    }

    private void KeywordClicked(Keyword keyword)
    {
        Debug.Log($"clicked on {keyword}: {keyword.description}");
        onKeywordClicked?.Invoke(keyword);
    }
}
