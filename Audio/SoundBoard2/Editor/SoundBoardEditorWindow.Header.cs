using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Zeropoint.BoardSystem;
using Zeropoint.Common;

namespace Pripizden.AudioSystem.SoundBoard
{
    //Header File
    partial class SoundBoardEditorWindow
    {
        private string[] m_soundBoardTitles = null;
        [SerializeField]
        private int m_soundBoardIndex = -1;

        void SetSoundBoardDropdownIndex(int index)
        {
            m_soundBoardIndex = index;
        }

        void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();
            DrawSoundBoardPopup();
            if (GUILayout.Button(new GUIContent("+", "Create a new sound board"), EditorStyles.miniButton))
            {
                ShowCreationPopup();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        void ShowCreationPopup()
        {
            var result = EditorInputDialog.Show("New sound board", "Enter title for the sound board", "New SoundBoard");

            if(result!=null)
            {
                var assetPath = NewAssetFolderPath + result + ".asset";
                var asset = AssetDatabase.LoadAssetAtPath<Board>(assetPath);
                if (asset != null)
                {
                    EditorUtility.DisplayDialog("Soundboard Exists", "A soundboard with this name already exists", "Ok");
                    return;
                }

                var newSoundBoard = ScriptableObject.CreateInstance<Board>();
                newSoundBoard.Title = result;
                newSoundBoard.Translation = new Vector2(1000, 1000);
                AssetDatabase.CreateAsset(newSoundBoard, assetPath);

                Database.SoundBoards.Add(newSoundBoard);
                UpdateSoundBoardTitles();
                m_soundBoardIndex = Database.SoundBoards.Count - 1;
                OpenSoundBoard(newSoundBoard);
                EditorUtility.SetDirty(Database);
            }
        }

        void DrawSoundBoardPopup()
        {
            UpdateSoundBoardTitles();
            int newIndex = EditorGUILayout.Popup(m_soundBoardIndex, m_soundBoardTitles, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            if(m_soundBoardIndex!=newIndex)
            {
                Database.LastBoardIndex = newIndex;
                SetSoundBoardDropdownIndex(newIndex);
                OpenSoundBoard(GetSoundBoardByTitleIndex(m_soundBoardIndex));
            }
        }

        string[] GetSoundBoardTitles()
        {
            List<string> titles = new List<string>();
            if(Database!=null)
            {
                foreach(var sboard in Database.SoundBoards)
                {
                    if (sboard != null)
                    {
                        titles.Add(sboard.Title);
                    }
                }
            }
            return titles.ToArray();
        }

        public Board GetSoundBoardByTitleIndex(int index)
        {
            if (m_soundBoardTitles == null) UpdateSoundBoardTitles();
            if(index>=0&&index<m_soundBoardTitles.Length)
            {
                return Database.SoundBoards[index];
            }
            return null;
        }

        public void UpdateSoundBoardTitles()
        {
            m_soundBoardTitles = GetSoundBoardTitles();
        }

    }
}