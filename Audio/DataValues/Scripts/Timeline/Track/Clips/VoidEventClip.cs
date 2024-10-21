using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine;

namespace Pripizden.DataValues
{
    public class VoidEventClip : PlayableAsset, ITimelineClipAsset
    {
        public VoidEventBehavior Template = new VoidEventBehavior();
        public VoidEvent DataEvent = null;

        public ClipCaps clipCaps // ITimelineClipAsset
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<VoidEventBehavior>.Create(graph, Template);
            VoidEventBehavior behavior = playable.GetBehaviour();
            behavior.DataEvent = DataEvent;
            //here you set shit from inspector;
            //behavior.shit = ;

            return playable;
        }

    }
}
