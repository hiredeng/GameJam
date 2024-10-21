using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine;

namespace Pripizden.DataValues
{
    public class DataClip : PlayableAsset, ITimelineClipAsset
    {
        public DataBehavior Template = new DataBehavior();

        public DataValues.BaseEvent DataEvent;

        public ClipCaps clipCaps // ITimelineClipAsset
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DataBehavior>.Create(graph, Template);
            DataBehavior behavior = playable.GetBehaviour();

            //here you set shit from inspector;
            //behavior.shit = ;

            return playable;
        }       

    }
}
