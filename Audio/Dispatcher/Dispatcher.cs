using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Pripizden.Legacy
{
    public enum MessageID
    {
        SFX,
        Gameplay,
        PlayerInput,
        FmodEventEnd, 
        PhraseStart,
        PhraseEnd,
        Inventory,
        Actor,
        DialogueUI,
        SceneSound,
        SoundControl,
        Cutscene, 
        InputController,
        ActorAnimation,
        SubtitleStart,
        SubtitleEnd,
        SceneTransition,
        Accomplishment,
    }

    public class Dispatcher
    {
        private Dispatcher()
        {
            _messageIDActionCollection = new Dictionary<MessageID, List<Action<object>>>();
            _instance = this;
        }

        public static Dispatcher Instance { get { if (_instance == null) 
                {
                    _instance = new Dispatcher();
                }
                return _instance;} }

        private static Dispatcher _instance;

#if DEBUG
        private void SendDispatcherMessage(double id, string message)
        {
            Send((MessageID)id, message);
        }
#endif

        public void Send(MessageID id, object o)
        {
            if (!_messageIDActionCollection.ContainsKey(id))
            {
                //Debug.LogError("[Messenger] Trying to send message with id that has no registered subscribers.");
                return;
            }
            List<Action<object>> list = _messageIDActionCollection[id];
            if (list == null)
            {
                Debug.Log("[Messenger] No one is listening to id: " + id);
            }
            foreach (Action<object> action in list)
            {
                action(o);
            }
        }

        public void SendSfx(string message, Vector3 position)
        {
            Send(MessageID.SFX, new SfxReport(message, new SoundArgs(position)));
        }

        public void SendSfx(string message, Transform body)
        {
            Send(MessageID.SFX, new SfxReport(message, new SoundArgs(body.position, body)));
        }

        public void Register(MessageID id, Action<object> action)
        {
            List<Action<object>> list = null;
            if (_messageIDActionCollection.ContainsKey(id))
            {
                list = _messageIDActionCollection[id];
            }
            if (list == null)
            {
                list = new List<Action<object>>();
            }
            list.Add(action);
            _messageIDActionCollection[id] = list;
        }

        public void Unregister(MessageID id, Action<object> action)
        {
            if (this._messageIDActionCollection.ContainsKey(id))
            {
                List<Action<object>> list = _messageIDActionCollection[id];
                list.Remove(action);
                _messageIDActionCollection[id] = list;
            }
            else
            {
                Debug.LogError("[Dispatcher] Trying to unregister from id " + id.ToString() + " but it had no subscribers.");
            }
        }

        private Dictionary<MessageID, List<Action<object>>> _messageIDActionCollection;
    }

}