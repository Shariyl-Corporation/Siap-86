// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class StateManager : MonoBehaviour
// {

//     // public OptionsManager optionsManager;

//     private bool idle, talking;

//     private bool givenVerdict = false;

//     private AudioSource audioSource;

//     void Start()
//     {
//         idle = false;
//         talking = false;

//         audioSource = GetComponent<AudioSource>();
//         audioSource.Stop();
//     }

//     void Update()
//     {
//         if (givenVerdict && idle && !talking)
//         {
//             idle = false;
//             optionsManager.Win();
//         }
//         else
//         {
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//             RaycastHit hit;
//             bool collided = Physics.Raycast(ray, out hit);

//             if (Input.GetMouseButtonDown(0))
//             {
//                 if (idle)
//                 {
//                     if (collided && !talking)
//                     {
//                         if (hit.transform.name == "chatButton")
//                         {
//                             audioSource.Play();
//                             setIdle(false);
//                             talking = true;
//                             optionsManager.Talk(actionNumber);
//                         }
//                         else if (hit.transform.name == "interrogateButton")
//                         {
//                             audioSource.Play();
//                             setIdle(false);
//                             optionsManager.Leave();
//                         }
//                     }
//                     else if (collided && talking)
//                     {
//                         if (hit.transform.name.Contains("Option"))
//                         {
//                             audioSource.Play();

//                             int chosenOption = int.Parse(hit.transform.name.Split(' ')[1]);
//                             optionsManager.Choose(chosenOption - 1);
//                             setIdle(false);
//                             talking = false;
//                         }
//                     }
//                 }
//             }
//         }
//     }

//     public void setIdle(bool state)
//     {
//         //Debug.Log("Setting idle to: " + state);
//         idle = state;
//     }

//     public void endGame(bool victory, string reason)
//     {
//         SceneManager.LoadScene(victory ? "Victory" : "GameOver");
//     }
// }
