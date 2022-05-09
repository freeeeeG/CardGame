using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Vector3[] shadowPos;
    public Sprite[] shadowSprites;
    public GameObject shadowObj;
    public Color[] color;
    public SpriteRenderer sr;
    public float shadowLiveTime = 1000f;


    private void Start()
    {
        sr = shadowObj.GetComponent<SpriteRenderer>();
        ShadowColor();
    }
    public void ShadowColor()
    {

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

            yield return new WaitForSeconds(time);
            GameObject shadow = Instantiate(shadowObj, shadowPos[i], Quaternion.identity);
            shadow.GetComponent<SpriteRenderer>().sprite = shadowSprites[i];
            sr.color = color[i];
            Debug.Log("shadow");
            Destroy(shadow, shadowLiveTime);

        }


    }
}
