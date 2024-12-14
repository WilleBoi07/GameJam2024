using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;

public class Menu : MonoBehaviour
{
    public Button Game;
    public Button Controls;
    public Button Credits;
    

    [SerializeField] TextMeshProUGUI ControlTitle;
    [SerializeField] Image ControlTitleImage;
    [SerializeField] Image ControlctrlImage;
    [SerializeField] TextMeshProUGUI MovementTitle;
    [SerializeField] Image MovementImage;
    [SerializeField] Image InteractImage;
    [SerializeField] TextMeshProUGUI InteractText;
    [SerializeField] TextMeshProUGUI PressTaxt;
    [SerializeField] TextMeshProUGUI PressTextctrl;
    [SerializeField] Image PressImage;
    [SerializeField] TextMeshProUGUI InteractTextctrl;
    [SerializeField] TextMeshProUGUI MovementTextctrl;

    [SerializeField] TextMeshProUGUI Creditsss;
    [SerializeField] TextMeshProUGUI names;

    private void Start()
    {
        ControlTitle.gameObject.SetActive(false);
        ControlTitleImage.gameObject.SetActive(false);
        ControlctrlImage.gameObject.SetActive(false);
        MovementTitle.gameObject.SetActive(false);
        MovementImage.gameObject.SetActive(false);
        MovementTextctrl.gameObject.SetActive(false);
        PressTaxt.gameObject.SetActive(false);
        PressTextctrl.gameObject.SetActive(false);
        PressImage.gameObject.SetActive(false);
        InteractImage.gameObject.SetActive(false);
        InteractTextctrl.gameObject.SetActive(false);
        InteractText.gameObject.SetActive(false);
        names.gameObject.SetActive(false);
        Creditsss.gameObject.SetActive(false);
    }

    public void Playergame()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayerControls()
    {
        ControlTitle.gameObject.SetActive(true);
        ControlTitleImage.gameObject.SetActive(true);
        ControlctrlImage.gameObject.SetActive(true);
        MovementTitle.gameObject.SetActive(true);
        MovementImage.gameObject.SetActive(true);
        MovementTextctrl.gameObject.SetActive(true);
        PressTaxt.gameObject.SetActive(true);
        PressTextctrl.gameObject.SetActive(true);
        PressImage.gameObject.SetActive(true);
        InteractImage.gameObject.SetActive(true);
        InteractTextctrl.gameObject.SetActive(true);
        InteractText.gameObject.SetActive(true);

        Game.gameObject.SetActive(false);
        Credits.gameObject.SetActive(false);
        Controls.gameObject.SetActive(false);
    }
    public void PlayerCredits()
    {
        names.gameObject.SetActive(true);
        Creditsss.gameObject.SetActive(true);

        Game.gameObject.SetActive(false);
        Credits.gameObject.SetActive(false);
        Controls.gameObject.SetActive(false);
    }
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControlTitle.gameObject.SetActive(false);
            ControlTitleImage.gameObject.SetActive(false);
            ControlctrlImage.gameObject.SetActive(false);
            MovementTitle.gameObject.SetActive(false);
            MovementImage.gameObject.SetActive(false);
            MovementTextctrl.gameObject.SetActive(false);
            PressTaxt.gameObject.SetActive(false);
            PressTextctrl.gameObject.SetActive(false);
            PressImage.gameObject.SetActive(false);
            InteractImage.gameObject.SetActive(false);
            InteractTextctrl.gameObject.SetActive(false);
            InteractText.gameObject.SetActive(false);

            names.gameObject.SetActive(false);
            Creditsss.gameObject.SetActive(false);

            Game.gameObject.SetActive(true);
            Credits.gameObject.SetActive(true);
            Controls.gameObject.SetActive(true);
        }
    }
}
