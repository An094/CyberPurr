using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject m_HelicopterController;
    public GameObject m_Gun;
    public Button m_btnStart;
    public Sprite m_sprClicked;

    public void StartGame()
    {
        m_btnStart.image.sprite = m_sprClicked;
        m_HelicopterController.SetActive(true);
        m_Gun.SetActive(true);
        SoundManager.PlaySound("background");
        this.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
