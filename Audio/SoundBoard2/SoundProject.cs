using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeropoint.BoardSystem;


namespace Pripizden.AudioSystem.SoundBoard
{
    [CreateAssetMenu(fileName = "new SoundProject", menuName = "Sound Project", order = 51)]
    [System.Serializable]
    public class SoundProject : ScriptableObject
    {
        [SerializeField]
        private string m_version = "";
        [SerializeField]
        private List<Board> m_soundBoards = new List<Board>();
        [SerializeField]
        private int m_lastUsedBoardIndex = 0;

        public string Version => m_version;
        public List<Board> SoundBoards => m_soundBoards;
        public int LastBoardIndex { get { return m_lastUsedBoardIndex; } set { m_lastUsedBoardIndex = value; } }
    }
}