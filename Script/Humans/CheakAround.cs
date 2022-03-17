using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheakAround : MonoBehaviour
{
    public string tag;
    public bool Flag;
    private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == tag)
        {
            Flag = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == tag)
        {
            Flag = false;
        }
    }
}
