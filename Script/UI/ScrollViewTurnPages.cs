using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollViewTurnPages : MonoBehaviour
{
    public static UIManager instance;
    public Button btnNext;
    public Button btnLast;
    public ScrollRect scrollRect;
    public RectTransform content;
    private float contentWidth;
    private float delta_x;
    private float targetPoint;
    private int allBtnCount;
    private int btnCount = 10;
    public GameObject showP;
    public int pages = 1;
    int allPages;
    private bool isRoll = false;

    void Start()
    {
        scrollRect = transform.GetComponent<ScrollRect>();
        allBtnCount = content.childCount;
        contentWidth = (content.GetComponent<GridLayoutGroup>().cellSize.x + content.GetComponent<GridLayoutGroup>().spacing.x) * 2;
        if (allBtnCount * 1.0 / btnCount > Mathf.CeilToInt(allBtnCount / btnCount))
        {
            allPages = Mathf.CeilToInt(allBtnCount / btnCount) + 1;
        }
        else
        {
            allPages = Mathf.CeilToInt(allBtnCount / btnCount);
        }
        showP.GetComponent<TextMeshProUGUI>().text = pages + "/" + allPages;
        delta_x = contentWidth * allPages + content.GetComponent<GridLayoutGroup>().spacing.x;
        content.sizeDelta = new Vector2(delta_x, content.sizeDelta.y);
        Debug.Log("setPage: " + allPages + " " + allBtnCount * 1.0 / btnCount);

        btnLast.onClick.AddListener(() =>
               {
                   isRoll = true;

                   content.position = new Vector2(content.transform.position.x + contentWidth, 1080);
                   if (pages > 1)
                   {
                       pages--;
                       showP.GetComponent<TextMeshProUGUI>().text = pages + "/" + allPages;
                   }
               });

        btnNext.onClick.AddListener(() =>
        {
            isRoll = true;

            content.position = new Vector2(content.transform.position.x - contentWidth, 1080);
            if (pages < allPages)
            {
                pages++;
                showP.GetComponent<TextMeshProUGUI>().text = pages + "/" + allPages;
            }
        });
    }

    void Update()
    {
        if (UIManager.Instance.isSwitch)
        {
            delta_x = contentWidth * allPages + content.GetComponent<GridLayoutGroup>().spacing.x;
            if (UIManager.Instance.pageCount / 10.0 == 1.0)
                allPages = Mathf.CeilToInt(UIManager.Instance.pageCount / 10);
            else
                allPages = Mathf.CeilToInt(UIManager.Instance.pageCount / 10) + 1;

            content.sizeDelta = new Vector2(delta_x, content.sizeDelta.y);
            showP.GetComponent<TextMeshProUGUI>().text = pages + "/" + allPages;
        }
        SetPageStatic(content.position.x);
    }

    public void ResetPagePos(Vector2 pos)
    {
        content.position = pos;
    }

    public void SetPageStatic(float PosX)
    {
        float left = pages * 1420;
        float right = (pages + 1) * 1420;
        if (PosX > left && PosX < right)
        {
            content.position = new Vector2(left, 1080);
        }
    }
}
