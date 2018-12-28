using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    void Start() {
        FadeInTrigger();
    }

    public void FadeInTrigger() {
        print("run");
        foreach (Transform child in transform) {
            //get all the children
            Material childMat = child.GetComponent<Material>();
            if (childMat != null) {
                childMat.SetFloat("_AlphaClip", 0);
            }
        }
    }
}
