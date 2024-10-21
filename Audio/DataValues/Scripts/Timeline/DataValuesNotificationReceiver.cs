using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Pripizden.DataValues.Timeline
{
    public class DataValuesNotificationReceiver : MonoBehaviour, INotificationReceiver
    {
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            switch (notification)
            {
                case DataEventMarker dataEventMarker:
                    dataEventMarker.Invoke();
                    break;
                case DataValueMarker dataValueMarker:
                    dataValueMarker.Invoke();
                    break;
            }
        }
    }
}
