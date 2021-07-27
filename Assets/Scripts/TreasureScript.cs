using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureScript : MonoBehaviour
{
    public Slider slider;

    public void SetMaxTreasure(int TreasureCount)
    {
        slider.maxValue = TreasureCount;
        slider.value = TreasureCount;
    }

    public void SetTreasure(int TreasureCount)
    {
        slider.value = TreasureCount;
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
