using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
   public GameObject damageTextPrefab;
   public GameObject healthTextPrefab;
   public Canvas gameCanvas;

   private void Awake()
   {
      gameCanvas = GameObject.Find("GameCanvas").GetComponent<Canvas>();
   }

   private void OnEnable()
   {
      CharacterEvents.characterDamaged += CharacterTookDamage;
      CharacterEvents.characterHealed += CharacterHealed;
   }

   private void OnDisable()
   {
      CharacterEvents.characterDamaged -=CharacterTookDamage;
      CharacterEvents.characterHealed -=CharacterHealed;
   }

   public void OnExit(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
         
         #endif
         #if(UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
         #elif (UNITY_STANDALONE)
            Application.Quit();
         #elif (UNITY_WEBGL)
            SceneManager.LoadScene("QuitScene");
         #endif
      }
   }

   public void CharacterTookDamage(GameObject character, int damageReceived)
   {
      Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
      
      TMP_Text damageText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();
      damageText.text = damageReceived.ToString();
   }
   
   public void CharacterHealed(GameObject character, int health)
   {
      Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
      
      TMP_Text healthText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();
      healthText.text = health.ToString();
   }
}
