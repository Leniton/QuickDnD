using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverKeyword : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] KeywordTextHandler textHandler;
    Camera _cameraToUse = null;
    int lastId = -1;
    Coroutine checking;

    public void OnPointerEnter(PointerEventData eventData)
    {
        checking = StartCoroutine(CheckHover());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (checking == null) return;
        StopCoroutine(checking);
        checking = null;
        PopUpComponent.instance.Hide();
        lastId = -1;
    }

    IEnumerator CheckHover()
    {
        while (true)
        {
            yield return null;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            int intersectingLink = TMP_TextUtilities.FindIntersectingLink(textHandler.Text, mousePosition, _cameraToUse);

            if (lastId == intersectingLink) continue;
            lastId = intersectingLink;
            if (intersectingLink < 0)
            {
                PopUpComponent.instance.Hide();
                continue;
            }

            TMP_LinkInfo linkInfo = textHandler.Text.textInfo.linkInfo[intersectingLink];
            Keyword keyword = KeywordDictionary.Get(linkInfo.GetLinkID());
            if (keyword == null) continue;
            PopUpComponent.instance.Show(keyword.description);
        }
    }
}
