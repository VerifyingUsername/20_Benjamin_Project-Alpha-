using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoClipScript : MonoBehaviour
{
    public Slider slider;

    public void SetMaxAmmo(int AmmoClip)
    {
        slider.maxValue = AmmoClip;
        slider.value = AmmoClip;
    }

    public void SetAmmo(int AmmoClip)
    {
        slider.value = AmmoClip;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
