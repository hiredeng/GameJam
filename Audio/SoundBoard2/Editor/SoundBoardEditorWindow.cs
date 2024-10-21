using UnityEditor;

namespace Pripizden.AudioSystem.SoundBoard
{
    partial class SoundBoardEditorWindow
    {
        [MenuItem("Pripizden/Tools/Sound Board Editor", false, -1)]
        public static SoundBoardEditorWindow OpenSoundBoardEditorWindow()
        {
            var window = GetWindow<SoundBoardEditorWindow>("Soundboard");
            return window;
        }
    }   
}