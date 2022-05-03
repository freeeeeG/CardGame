using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Vector3[] shadowPos;
    public Sprite[] shadowSprites;
    public float shadowLiveTime = 1000f;
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

            GameObject shadow = Instantiate(gameObject, shadowPos[i], Quaternion.identity);
            shadow.GetComponent<SpriteRenderer>().sprite = shadowSprites[i];
            Destroy(shadow, shadowLiveTime);

            yield return new WaitForSeconds(time);
        }


    }
}
