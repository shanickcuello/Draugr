using System;
using System.Collections;
using Events;
using TMPro;
using UnityEngine;

namespace Message
{
    public class MessageView : MonoBehaviour
    {
        private TextMeshProUGUI _uiText;

        private void Awake()
        {
            _uiText = GetComponent<TextMeshProUGUI>();
            SetMessage("Encuentra el mensaje de hoy, vuelve por otro mañana");
            StartCoroutine(DeleteMessage());
        }

        private IEnumerator DeleteMessage()
        {
            yield return new WaitForSeconds(10);
            _uiText.text = "";
        }

        private void Start()
        {
            GameEvents.instance.OnUserCollisionWithMessage += SetMessage;
        }

        private void SetMessage(string message)
        {
            var charMessage = message.ToCharArray();
            StartCoroutine(ShowLetterByLetter(charMessage));
        }

        private IEnumerator ShowLetterByLetter(char[] charMessage)
        {
            foreach (var c in charMessage)
            {
                if (c.ToString() != " ")
                    yield return new WaitForSeconds(0.1f);
                _uiText.text += c;
            }
        }
    }
}
