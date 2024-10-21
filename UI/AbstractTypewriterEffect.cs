using UnityEngine;

namespace Pripizden.UI.DialogueSystem
{
    public abstract class AbstractTypewriterEffect : MonoBehaviour
    {

        /// <summary>
        /// Set `true` to type right to left.
        /// </summary>
        [Tooltip("Tick for right-to-left text such as Arabic.")]
        public bool rightToLeft = false;

        /// <summary>
        /// How fast to "type."
        /// </summary>
        [Tooltip("How fast to type. This is separate from Dialogue Manager > Subtitle Settings > Chars Per Second.")]
        public float charactersPerSecond = 50;

        /// <summary>
        /// Play a full pause on these characters.
        /// </summary>
        [Tooltip("Play a full pause on these characters.")]
        public string fullPauseCharacters = string.Empty;

        /// <summary>
        /// Play a quarter pause on these characters.
        /// </summary>
        [Tooltip("Play a quarter pause on these characters.")]
        public string quarterPauseCharacters = string.Empty;

        /// <summary>
        /// Duration to pause on when text contains '\\.'
        /// </summary>
        [Tooltip("Duration to pause on when text contains '\\.'")]
        public float fullPauseDuration = 1f;

        /// <summary>
        /// Duration to pause when text contains '\\,'
        /// </summary>
        [Tooltip("Duration to pause when text contains '\\,'")]
        public float quarterPauseDuration = 0.25f;

        /// <summary>
        /// Ensures this GameObject has only one typewriter effect.
        /// </summary>
        [Tooltip("Ensure this GameObject has only one typewriter effect.")]
        public bool removeDuplicateTypewriterEffects = true;

        /// <summary>
        /// Play using the current text content whenever component is enabled.
        /// </summary>
        [Tooltip("Play using the current text content whenever component is enabled.")]
        public bool playOnEnable = true;

        /// <summary>
        /// Wait one frame to allow layout elements to setup first.
        /// </summary>
        [Tooltip("Wait one frame to allow layout elements to setup first.")]
        public bool waitOneFrameBeforeStarting = false;

        /// <summary>
        /// Stop typing when the conversation ends.
        /// </summary>
        [Tooltip("Stop typing when the conversation ends.")]
        public bool stopOnConversationEnd = false;

        public abstract bool isPlaying { get; }

        protected bool paused = false;

        /// <summary>
        /// Returns the typewriter's charactersPerSecond.
        /// </summary>
        public virtual float GetSpeed()
        {
            return charactersPerSecond;
        }

        /// <summary>
        /// Sets the typewriter's charactersPerSecond. Takes effect the next time the typewriter is used.
        /// </summary>
        public virtual void SetSpeed(float charactersPerSecond)
        {
            this.charactersPerSecond = charactersPerSecond;
        }

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {
            
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }

        public virtual void StopOnConversationEnd(Transform actor)
        {
            if (isPlaying) Stop();
        }

        public abstract void Stop();

        public abstract void StartTyping(string text, int fromIndex = 0);

        public abstract void StopTyping();

        protected bool IsFullPauseCharacter(char c)
        {
            return IsCharacterInString(c, fullPauseCharacters);
        }

        protected bool IsQuarterPauseCharacter(char c)
        {
            return IsCharacterInString(c, quarterPauseCharacters);
        }

        protected bool IsCharacterInString(char c, string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c) return true;
            }
            return false;
        }
    }

}
