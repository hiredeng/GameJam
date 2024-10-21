using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pripizden.DataValues
{
    public class DataMixerBehaviour : PlayableBehaviour
    {
        private PlayableDirector _director;

        private HashSet<int> _activeClips = new HashSet<int>();

        public static ScriptPlayable<DataMixerBehaviour> Create(PlayableGraph graph, int inputCount)
        {
            return ScriptPlayable<DataMixerBehaviour>.Create(graph, inputCount);
        }

        public override void OnPlayableCreate(Playable playable)
        {
            _director = (playable.GetGraph().GetResolver() as PlayableDirector);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            int clipCount = playable.GetInputCount();

            for (int i = 0; i < clipCount; i++)
            {
                float clipWeight = playable.GetInputWeight(i);

                if (clipWeight > 0.9f)
                {
                    if (!_activeClips.Contains(i))
                    {
                        _activeClips.Add(i);
                        var input = playable.GetInput(i);
                        var beh = ((ScriptPlayable<VoidEventBehavior>)input).GetBehaviour();
                        beh.Fire();
                    }
                }
                else
                {
                    if(_activeClips.Contains(i))
                    {

                    }
                }
            }
        }
    }
}