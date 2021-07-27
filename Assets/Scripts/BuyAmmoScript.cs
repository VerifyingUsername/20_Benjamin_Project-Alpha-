using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAmmoScript : MonoBehaviour
{
    public bool addedAmmo;
    public bool enoughGold;

    public AmmoClipScript ammoScript;
    // Start is called before the first frame update
    void Start()
    {
        //ammoScript.GetComponent<AmmoClipScript>().SetAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyAmmo()
    {
        if (PlayerController.GoldAmount >= 5)
        {
            enoughGold = true;
        }

        if (PlayerController.GoldAmount >= 5 && PlayerController.currentBullet <= 0)
        {
            PlayerController.currentBullet = 10;
            Debug.Log("Added bullets: " + PlayerController.currentBullet);
            //PlayerController.AmmoClipText.GetComponent<Text>().text = PlayerController.currentAmmo + "/2";
            addedAmmo = true;
            //enoughGold = true;
        }
        else if (PlayerController.GoldAmount <5)
        {
            enoughGold = false;
        }
        

        if (addedAmmo == true && enoughGold == true)
        {
            PlayerController.GoldAmount -= 5;
        }
    }
}
