using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;


public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private GameObject Menu_Object;
    [SerializeField]
    private RectTransform Menu_RT;

    [SerializeField]
    private Button About_Button;
    [SerializeField]
    private GameObject About_Object;
    [SerializeField]
    private RectTransform About_RT;

    [SerializeField]
    private Button Settings_Button;
    [SerializeField]
    private GameObject Settings_Object;
    [SerializeField]
    private RectTransform Settings_RT;

    [SerializeField]
    private Button Exit_Button;
    [SerializeField]
    private GameObject Exit_Object;
    [SerializeField]
    private RectTransform Exit_RT;

 

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("About Popup")]
    [SerializeField]
    private GameObject AboutPopup_Object;
    [SerializeField]
    private Button AboutExit_Button;



    [Header("Settings Popup")]
    [SerializeField] private AudioController audioController;
    //private AudioSource BG_Sounds;
    //[SerializeField]
    //private AudioSource Button_Sounds;
    //[SerializeField]
    //private AudioSource Spin_Sounds;

   
 
    int CurrentIndex = 0;
    [SerializeField] private GameObject[] paytableList;
    [SerializeField] private Button RightBtn;
    [SerializeField] private Button LeftBtn;


    private bool isMusic = true;
    private bool isSound = true;


    private void Start()
    {
        

        if (Exit_Button) Exit_Button.onClick.RemoveAllListeners();
        if (Exit_Button) Exit_Button.onClick.AddListener(CloseMenu);

        if (About_Button) About_Button.onClick.RemoveAllListeners();
        if (About_Button) About_Button.onClick.AddListener(delegate { OpenPopup(AboutPopup_Object); });

        if (AboutExit_Button) AboutExit_Button.onClick.RemoveAllListeners();
        if (AboutExit_Button) AboutExit_Button.onClick.AddListener(delegate { ClosePopup(AboutPopup_Object); });


        if (Settings_Button) Settings_Button.onClick.RemoveAllListeners();


        //if (BG_Sounds) BG_Sounds.mute = false;
        //if (Spin_Sounds) Spin_Sounds.mute = false;
        //if (Button_Sounds) Button_Sounds.mute = false;
        if (audioController) audioController.ToggleMute(false, "all");

        isMusic = true;
        isSound = true;



      
            paytableList[CurrentIndex = 0].SetActive(true);

        if (LeftBtn) LeftBtn.onClick.RemoveAllListeners();
        if (LeftBtn) LeftBtn.onClick.AddListener(delegate { Slide(-1); });

        if (RightBtn) RightBtn.onClick.RemoveAllListeners();
        if (RightBtn) RightBtn.onClick.AddListener(delegate { Slide(+1); });
      
    }

    

    private void OpenMenu()
    {
   
        if (Exit_Object) Exit_Object.SetActive(true);
        if (About_Object) About_Object.SetActive(true);
    
        if (Settings_Object) Settings_Object.SetActive(true);

        DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y + 150), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
        });

      

        DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y + 450), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
        });
    }

    private void CloseMenu()
    {

        DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y - 150), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
        });

       
        DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y - 450), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
        });

        DOVirtual.DelayedCall(0.1f, () =>
         {
           
             if (Exit_Object) Exit_Object.SetActive(false);
             if (About_Object) About_Object.SetActive(false);
          
             if (Settings_Object) Settings_Object.SetActive(false);
         });
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
        paytableList[CurrentIndex = 0].SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(false);
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
        paytableList[CurrentIndex].SetActive(false);
    }

    private void ToggleMusic()
    {
        isMusic = !isMusic;
        if(isMusic)
        {
           
            //if (BG_Sounds) BG_Sounds.mute = false;
            if (audioController) audioController.ToggleMute(false,"bg");
        }
        else
        {
            if (audioController) audioController.ToggleMute(true,"bg");

            //if (BG_Sounds) BG_Sounds.mute = true;
        }
    }

    private void ToggleSound()
    {
        isSound = !isSound;
        if(isSound)
        {
           
            //if (Spin_Sounds) Spin_Sounds.mute = false;
            //if (Button_Sounds) Button_Sounds.mute = false;
            if (audioController) audioController.ToggleMute(false, "button");
            if (audioController) audioController.ToggleMute(false, "wl");

        }
        else
        {
            
            //if (Spin_Sounds) Spin_Sounds.mute = true;
            //if (Button_Sounds) Button_Sounds.mute = true;
            if (audioController) audioController.ToggleMute(false, "button");
            if (audioController) audioController.ToggleMute(false, "wl");
        }
    }
    private void Slide(int direction)
    {

        if (CurrentIndex < paytableList.Length - 1 && direction > 0)
        {
            if (audioController) audioController.PlayButtonAudio();

            // Move to the next item
            paytableList[CurrentIndex].SetActive(false);
            paytableList[CurrentIndex + 1].SetActive(true);
           
            CurrentIndex++;
            

        }
        else if (CurrentIndex >= 1 && direction < 0)
        {
        if (audioController) audioController.PlayButtonAudio();

            // Move to the previous item
            paytableList[CurrentIndex].SetActive(false);
            paytableList[CurrentIndex - 1].SetActive(true);

            CurrentIndex--;
           

        }

        
    }

}
