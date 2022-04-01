using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Events;
using Lights;
using UnityEngine;

namespace Message
{
    public class Message : MonoBehaviour
    {
        private string _todayMessage;
        private Dictionary<DateTime, string> _days = new Dictionary<DateTime, string>();


        private void Start()
        {
            MakeDictionary();
            _todayMessage = GetTodayMessage(DateTime.Today);
            SetRotation();
        }

        private void Update()
        {
            AlwaysRotate();
        }

        private void AlwaysRotate()
        {
            transform.Rotate(Vector3.up, 40 * Time.deltaTime);
        }

        private void SetRotation()
        {
            transform.Rotate(transform.forward, 45);
            transform.Rotate(transform.up, 45);
        }

        private void MakeDictionary()
        {
            _days.Add(new DateTime(2022, 04, 1), "Hoy vi un auto amarillo y no tuve con quién jugar.");
            _days.Add(new DateTime(2022, 04, 2), "Hoy apollé el brazo en la cama pero no te acostaste.");
            _days.Add(new DateTime(2022, 04, 3), "Hoy mire la luna y me di vuelta, para verla como vos.");
            _days.Add(new DateTime(2022, 04, 4), "Hoy comí chocolate, y te guardé un pedazo.");
            _days.Add(new DateTime(2022, 04, 5), "Hoy prendí una vela, pero no te iluminó");
            _days.Add(new DateTime(2022, 04, 6), "Hoy me peleé con alguien, pero pensé en vos y lo resolví con paz.");
            _days.Add(new DateTime(2022, 04, 7), "Hoy lei un artículo, y me dieron ganas de leerte.");
            _days.Add(new DateTime(2022, 04, 8), "Hoy me dormí mirando nuestro retrato.");
            _days.Add(new DateTime(2022, 04, 9), "Hoy me hice un café, y no pude ofrecerte uno para que lo enfríes.");
            _days.Add(new DateTime(2022, 04, 10), "Hoy vi una bandeja de mimbre, y te la compré");
            _days.Add(new DateTime(2022, 04, 11), "Hoy te extraño mucho, más de lo normal.");
            _days.Add(new DateTime(2022, 04, 12), "Hoy abracé la almohada, pero no me alcanza con imaginarte.");
            _days.Add(new DateTime(2022, 04, 13), "Hoy me quedé sin aire, porque me di cuenta que te veo en 3 días");
            _days.Add(new DateTime(2022, 04, 14), "Hoy vi tu crema Elvive. Aca te espera.");
            _days.Add(new DateTime(2022, 04, 15), "Te amo");
            _days.Add(new DateTime(2022, 04, 16), "Hoy te veo <3");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<LightPlayerController>())
            {
                GameEvents.instance.UserCollisionWithMessage(_todayMessage);
            }
        }

        string GetTodayMessage(DateTime today)
        {
            foreach (var day in _days.Where(day => day.Key == today))
            {
                return day.Value;
            }

            return "Te amo";
        }
    }
}