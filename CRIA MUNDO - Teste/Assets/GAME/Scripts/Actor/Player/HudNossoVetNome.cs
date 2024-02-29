using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System.Net.Mail;

public class HudNossoVetNome : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputField;
    public TextMeshProUGUI _title;
    public string  ew;

    public void ButtonMudarNome( )
    {
        _title.text = inputField.text;
    }
  
    private void DisplayReactionToInput()
    {
       

    }

}
