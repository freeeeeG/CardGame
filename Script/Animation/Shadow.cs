using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Vector3[] shadowPos;
    public Sprite[] shadowSprites;
    public GameObject shadowObj;
    public SpriteRenderer sr;
    public float shadowLiveTime = 1000f;

    private void Start()
    {
        sr = shadowObj.GetComponent<SpriteRenderer>();
        ShadowColor();
    }
    public void ShadowColor()
    {
        sr.color = new Color(1, 1, 1, 0.5f);
    }
    public void ShadowUP()
    {
        StartCoroutine(WaitShadowForSeconds(0.1f));
    }

    public void ShadowDown()
    {

    }

    IEnumerator WaitShadowForSeconds(float time)
    {
        for (int i = 0; i < shadowPos.Length; i++)
        {

            GameObject shadow = Instantiate(shadowObj, shadowPos[i], Quaternion.identity);
            shadow.GetComponent<SpriteRenderer>().sprite = shadowSprites[i];
            Destroy(shadow, shadowLiveTime);

            yield return new WaitForSeconds(time);
        }


    }
}
