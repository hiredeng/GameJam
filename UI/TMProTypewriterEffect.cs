using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pripizden.UI.DialogueSystem
{

    [DisallowMultipleComponent]
    public class TMProTypewriterEffect : AbstractTypewriterEffect
    {

        [System.Serializable]
        public class AutoScrollSettings
        {
            [Tooltip("Automatically scroll to bottom of scroll rect. Useful for long text. Works best with left justification.")]
            public bool autoScrollEnabled = false;
            public UnityEngine.UI.ScrollRect scrollRect = null;
        }

        /// <summary>
        /// Optional auto-scroll settings.
        /// </summary>
        public AutoScrollSettings autoScrollSettings = new AutoScrollSettings();

        public UnityEvent onBegin = new UnityEvent();
        public UnityEvent onCharacter = new UnityEvent();
        public UnityEvent onEnd = new UnityEvent();

        /// <summary>
        /// Indicates whether the effect is playing.
        /// </summary>
        /// <value><c>true</c> if this instance is playing; otherwise, <c>false</c>.</value>
        public override bool isPlaying { get { return typewriterCoroutine != null; } }

        protected const string RPGMakerCodeQuarterPause = @"\,";
        protected const string RPGMakerCodeFullPause = @"\.";
        protected const string RPGMakerCodeSkipToEnd = @"\^";
        protected const string RPGMakerCodeInstantOpen = @"\>";
        protected const string RPGMakerCodeInstantClose = @"\<";

        protected enum RPGMakerTokenType
        {
            None,
            QuarterPause,
            FullPause,
            SkipToEnd,
            InstantOpen,
            InstantClose
        }

        protected Dictionary<int, List<RPGMakerTokenType>> rpgMakerTokens = new Dictionary<int, List<RPGMakerTokenType>>();

        protected TMPro.TMP_Text m_textComponent = null;
        protected TMPro.TMP_Text textComponent
        {
            get
            {
                if (m_textComponent == null) m_textComponent = GetComponent<TMPro.TMP_Text>();
                return m_textComponent;
            }
        }

        protected LayoutElement m_layoutElement = null;
        protected LayoutElement layoutElement
        {
            get
            {
                if (m_layoutElement == null)
                {
                    m_layoutElement = GetComponent<LayoutElement>();
                    if (m_layoutElement == null) m_layoutElement = gameObject.AddComponent<LayoutElement>();
                }
                return m_layoutElement;
            }
        }

        protected bool started = false;
        protected int charactersTyped = 0;
        protected Coroutine typewriterCoroutine = null;
        protected MonoBehaviour coroutineController = null;

        public override void Start()
        {
            if (!isPlaying && playOnEnable)
            {
                StopTypewriterCoroutine();
                StartTypewriterCoroutine(0);
            }
            started = true;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            if (!isPlaying && playOnEnable && started)
            {
                StopTypewriterCoroutine();
                StartTypewriterCoroutine(0);
            }
        }

        public override void OnDisable()
        {
            base.OnEnable();
            Stop();
        }

        /// <summary>
        /// Pauses the effect.
        /// </summary>
        public void Pause()
        {
            paused = true;
        }

        /// <summary>
        /// Unpauses the effect. The text will resume at the point where it
        /// was paused; it won't try to catch up to make up for the pause.
        /// </summary>
        public void Unpause()
        {
            paused = false;
        }

        public void Rewind()
        {
            charactersTyped = 0;
        }

        /// <summary>
        /// Starts typing, optionally from a starting index. Characters before the 
        /// starting index will appear immediately.
        /// </summary>
        /// <param name="text">Text to type.</param>
        /// <param name="fromIndex">Character index to start typing from.</param>
        public override void StartTyping(string text, int fromIndex = 0)
        {
            StopTypewriterCoroutine();
            textComponent.text = text;
            StartTypewriterCoroutine(fromIndex);
        }

        public override void StopTyping()
        {
            Stop();
        }

        /// <summary>
        /// Play typewriter on text immediately.
        /// </summary>
        /// <param name="text"></param>
        public virtual void PlayText(string text, int fromIndex = 0)
        {
            StopTypewriterCoroutine();
            textComponent.text = text;
            StartTypewriterCoroutine(fromIndex);
        }

        MonoBehaviour GetCoroutineRunner()
        {
            return this;
        }

        protected void StartTypewriterCoroutine(int fromIndex)
        {
            if (coroutineController == null || !coroutineController.gameObject.activeInHierarchy)
            {
                // This MonoBehaviour might not be enabled yet, so use one that's guaranteed to be enabled:
                MonoBehaviour controller = GetCoroutineRunner();
                coroutineController = controller;
                if (coroutineController == null) coroutineController = this;
            }
            typewriterCoroutine = coroutineController.StartCoroutine(Play(fromIndex));
        }

        /// <summary>
        /// Plays the typewriter effect.
        /// </summary>
        public virtual IEnumerator Play(int fromIndex)
        {
            if ((textComponent != null) && (charactersPerSecond > 0))
            {
                if (waitOneFrameBeforeStarting) yield return null;
                onBegin.Invoke();
                paused = false;
                float delay = 1 / charactersPerSecond;
                float lastTime = Time.time;
                float elapsed = fromIndex / charactersPerSecond;
                textComponent.maxVisibleCharacters = fromIndex;
                textComponent.ForceMeshUpdate();
                yield return null;
                textComponent.maxVisibleCharacters = fromIndex;
                textComponent.ForceMeshUpdate();
                TMPro.TMP_TextInfo textInfo = textComponent.textInfo;
                var parsedText = textComponent.GetParsedText();
                int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
                charactersTyped = fromIndex;
                int skippedCharacters = 0;
                while (charactersTyped < totalVisibleCharacters)
                {
                    if (!paused)
                    {
                        var deltaTime = Time.time - lastTime;
                        elapsed += deltaTime;
                        var goal = (elapsed * charactersPerSecond) + skippedCharacters;
                        while (charactersTyped < goal)
                        {
                            if (rpgMakerTokens.ContainsKey(charactersTyped))
                            {
                                var tokens = rpgMakerTokens[charactersTyped];
                                for (int i = 0; i < tokens.Count; i++)
                                {
                                    var token = tokens[i];
                                    switch (token)
                                    {
                                        case RPGMakerTokenType.QuarterPause:
                                            yield return new WaitForSeconds(quarterPauseDuration);
                                            break;
                                        case RPGMakerTokenType.FullPause:
                                            yield return new WaitForSeconds(fullPauseDuration);
                                            break;
                                        case RPGMakerTokenType.SkipToEnd:
                                            charactersTyped = totalVisibleCharacters - 1;
                                            break;
                                        case RPGMakerTokenType.InstantOpen:
                                            var close = false;
                                            while (!close && charactersTyped < totalVisibleCharacters)
                                            {
                                                charactersTyped++;
                                                skippedCharacters++;
                                                if (rpgMakerTokens.ContainsKey(charactersTyped) && rpgMakerTokens[charactersTyped].Contains(RPGMakerTokenType.InstantClose))
                                                {
                                                    close = true;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            var typedCharacter = (0 <= charactersTyped && charactersTyped < parsedText.Length) ? parsedText[charactersTyped] : ' ';
                            onCharacter.Invoke();
                            charactersTyped++;
                            textComponent.maxVisibleCharacters = charactersTyped;
                            if (IsFullPauseCharacter(typedCharacter)) yield return new WaitForSeconds(fullPauseDuration);
                            else if (IsQuarterPauseCharacter(typedCharacter)) yield return new WaitForSeconds(quarterPauseDuration);
                        }
                    }
                    textComponent.maxVisibleCharacters = charactersTyped;
                    HandleAutoScroll();
                    //---Uncomment the line below to debug: 
                    //Debug.Log(textComponent.text.Substring(0, charactersTyped).Replace("<", "[").Replace(">", "]") + " (typed=" + charactersTyped + ")");
                    lastTime = Time.time;
                    var delayTime = Time.time + delay;
                    int delaySafeguard = 0;
                    while (Time.time < delayTime && delaySafeguard < 999)
                    {
                        delaySafeguard++;
                        yield return null;
                    }
                }
            }
            Stop();
        }

        protected bool PeelRPGMakerTokenFromFront(ref string source, out RPGMakerTokenType token)
        {
            token = RPGMakerTokenType.None;
            if (string.IsNullOrEmpty(source) || source.Length < 2 || source[0] != '\\') return false;
            var s = source.Substring(0, 2);
            if (string.Equals(s, RPGMakerCodeQuarterPause))
            {
                token = RPGMakerTokenType.QuarterPause;
            }
            else if (string.Equals(s, RPGMakerCodeFullPause))
            {
                token = RPGMakerTokenType.FullPause;
            }
            else if (string.Equals(s, RPGMakerCodeSkipToEnd))
            {
                token = RPGMakerTokenType.SkipToEnd;
            }
            else if (string.Equals(s, RPGMakerCodeInstantOpen))
            {
                token = RPGMakerTokenType.InstantOpen;
            }
            else if (string.Equals(s, RPGMakerCodeInstantClose))
            {
                token = RPGMakerTokenType.InstantClose;
            }
            else
            {
                return false;
            }
            source = source.Remove(0, 2);
            return true;
        }

        protected void StopTypewriterCoroutine()
        {
            if (typewriterCoroutine == null) return;
            if (coroutineController == null)
            {
                StopCoroutine(typewriterCoroutine);
            }
            else
            {
                coroutineController.StopCoroutine(typewriterCoroutine);
            }
            typewriterCoroutine = null;
            coroutineController = null;
        }

        /// <summary>
        /// Stops the effect.
        /// </summary>
        public override void Stop()
        {
            if (isPlaying)
            {
                onEnd.Invoke();
            }
            StopTypewriterCoroutine();
            if (textComponent != null) textComponent.maxVisibleCharacters = textComponent.textInfo.characterCount;
            HandleAutoScroll();
        }

        protected void HandleAutoScroll()
        {
            if (!autoScrollSettings.autoScrollEnabled) return;

            var layoutElement = textComponent.GetComponent<LayoutElement>();
            if (layoutElement == null) layoutElement = textComponent.gameObject.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = textComponent.textBounds.size.y;
            if (autoScrollSettings.scrollRect != null)
            {
                autoScrollSettings.scrollRect.normalizedPosition = new Vector2(0, 0);
            }
        }

    }
}
