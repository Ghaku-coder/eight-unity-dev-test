// using System;
// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class MenuControl : MonoBehaviour
// {
//     private GameManager gameManager;
//     public GameObject background;
//     public UIManager uIManager;
//     public CutsceneCamera cutsceneCamera;
    
//     public void Play()
//     {
//         background.SetActive(false);
        
//         if(gameManager.score <= 0)
//         {
//             StartCoroutine(Intro());
//         }
//     }

//     IEnumerator Intro()
//     {
//         StartCoroutine(cutsceneCamera.PlayIntro());
//         yield return new WaitForSeconds(1f);
//         StartCoroutine(uIManager.ShowMission());
//     }

//     public void Reset()
//     {
//         background.SetActive(false);
//         GameManager.Instance.ResetScore();
//         StartCoroutine(Intro());
        
//         uIManager.missionShown = false;
//         uIManager.congratulationShown = false;
//     }

//     public void openMenu()
//     {
//         background.SetActive(true);
//     }
// }
