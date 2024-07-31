using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISound : MonoBehaviour
{
   
   public void ClickSound()
   {
    AudioManager.Instance.PlaySFX("ui_button1");
   }
   
   public void HoverSound()
   {
      AudioManager.Instance.PlaySFX("ui_hover");
   }
}
