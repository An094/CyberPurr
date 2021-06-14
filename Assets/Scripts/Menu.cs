using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject prefabHelicopterController;
    public GameObject Gun;
    public Button buttonStart;
    public Sprite buttonClicked;
    //private void Awake()
    //{
    //    prefabHelicopterController.SetActive(false);
    //}
    // Start is called before the first frame update
    public void StartGame()
    {
        buttonStart.image.sprite = buttonClicked;
        prefabHelicopterController.SetActive(true);
        Gun.SetActive(true);
        SoundManager.PlaySound("background");
        this.gameObject.SetActive(false);
    }
}
