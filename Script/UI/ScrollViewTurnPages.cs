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
    int pages = 1;
    int allPages;
    private bool isRoll = false;


    void Start()
    {
        scrollRect = transform.GetComponent<ScrollRect>();
        allBtnCount = content.childCount;
        contentWidth = (content.GetComponent<GridLayoutGroup>().cellSize.x + content.GetComponent<GridLayoutGroup>().spacing.x) * 2;
        allPages = Mathf.CeilToInt(allBtnCount / btnCount);
        delta_x = contentWidth * Mathf.CeilToInt(allBtnCount / btnCount) + content.GetComponent<GridLayoutGroup>().spacing.x;
        showP.GetComponent<TextMeshProUGUI>().text = pages + "/" + allPages;

        content.sizeDelta = new Vector2(delta_x, content.sizeDelta.y);

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
                Debug.Log(pages);
            }
        });
    }

    void Update()
    {
        Debug.Log(UIManager.Instance.isSwitch);
        if (UIManager.Instance.isSwitch)
        {
            allPages = Mathf.CeilToInt(UIManager.Instance.eachCount / 10) + 1;
            Debug.Log(allPages);
            pages = 1;
        }
    }
}
