using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.Legacy
{
   
	public class SfxReport
	{
        public string Message;
        public List<string> KeyWords;
        public SoundArgs Args;
        public SfxReport(string message, SoundArgs args = null, List<string> kwrds = null)
        {
            Message = message; Args = args;
            KeyWords = kwrds;
        }
	}

    public class SoundArgs
    {
        public Vector3 Position;
        public Transform Obj;

        public SoundArgs(Vector3 position)
        {
            Position = position;
        }
        public SoundArgs(Vector3 position, Transform obj)
        {
            Position = position;
            Obj = obj;
        }
    }
    
}