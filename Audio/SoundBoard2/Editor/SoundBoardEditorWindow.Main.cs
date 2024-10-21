using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Zeropoint.BoardSystem;
using Zeropoint.Common;


namespace Pripizden.AudioSystem.SoundBoard
{
    //Main file.
    public partial class SoundBoardEditorWindow : EditorWindow
    {
        //[SerializeField]
        SoundProject m_database = null;
        //[SerializeField]
        Board m_currentSoundBoard = null;

        NodeView m_nodeView = null;

        string DatabasePath { get; set; }
        string NewAssetFolderPath { get { return DatabasePath.Substring(0, DatabasePath.LastIndexOf("/")+1); } }
        SoundProject Database { 
            get {
                return m_database; 
            } 
            set { 
                m_database = value; 
                DatabasePath=AssetDatabase.GetAssetPath(m_database); 
            } 
        }

        void Initialize()
        {
            m_nodeView = new NodeView(ReflectionUtility.GetSubclassesInAllAssemblies(typeof(Node)), () => { Repaint(); });
            m_nodeView.OpenBoard((Board)m_currentSoundBoard);
        }





        public void OnEnable()
        {
            DatabasePath = "Assets\\__Pripizden\\Sound\\Board\\PripizdenSounds.asset";
            Database = AssetDatabase.LoadAssetAtPath<SoundProject>(DatabasePath);
            if (Database != null)
            {
                UpdateSoundBoardTitles();
                if (m_soundBoardTitles.Length > Database.LastBoardIndex)
                {
                    OpenSoundBoard(Database.SoundBoards[Database.LastBoardIndex]);
                    m_soundBoardIndex = Database.LastBoardIndex;
                }
            }
        }

        public void OnDisable()
        {           
            if (m_currentSoundBoard != null)
            {
                m_currentSoundBoard.SaveAssetReferences();
                m_currentSoundBoard.UnloadBoard();
                UnityEditor.EditorUtility.SetDirty(m_currentSoundBoard);
            }
            if (Database != null)
            {
                UnityEditor.EditorUtility.SetDirty(Database);
            }

            m_nodeView?.OnDisable();
            DatabasePath = "";
            m_database = null;
            m_currentSoundBoard = null;
        }

        public void OnGUI()
        {
            if (m_nodeView == null) Initialize();
            wantsMouseMove = true;
            DrawHeader();

            var nodeViewRect = new Rect(5, EditorGUIUtility.singleLineHeight + 5, position.width - 10, position.height - 10);

            m_nodeView.Draw(nodeViewRect);

            wantsMouseMove = true;
        }

        void OpenSoundBoard(Board soundBoard)
        {
            m_currentSoundBoard = soundBoard;
            m_nodeView?.OpenBoard(m_currentSoundBoard);
        }
    }
}