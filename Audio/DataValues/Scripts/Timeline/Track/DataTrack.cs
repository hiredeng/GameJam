using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.Playables;


namespace Pripizden.DataValues
{

    [TrackColor(1f, 0.2f, 0f)]
    [TrackClipType(typeof(DataClip))]
    [TrackClipType(typeof(VoidEventClip))]
    //[TrackBindingType(typeof(Puppet))]
    public class DataTrack : TrackAsset
    {
        DataMixerBehaviour m_dataMixerBehavior = null;

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixer = DataMixerBehaviour.Create(graph, inputCount);
            m_dataMixerBehavior = mixer.GetBehaviour();

            return mixer;
        }
    }
}