using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBarScript : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void SetMaxBullet(int bullet)
    {
        slider.maxValue = bullet;
        slider.value = bullet;
    }

    public void SetBullet(int bullet)
    {
        slider.value = bullet;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
