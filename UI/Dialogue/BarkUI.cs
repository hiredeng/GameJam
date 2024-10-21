using Pripizden.UI.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectName.UI
{
    public class BarkUI : AbstractBarkUI
    {
        [Header("References")]

        [SerializeField]
        private Canvas m_canvas;

        [SerializeField]
        private CanvasGroup m_canvasGroup;

        [SerializeField]
        private TMP_Text m_barkText;

        [SerializeField]
        private Image m_bubbleImage;

        [SerializeField]
        private TMProTypewriterEffect m_typewriter;

        [Header("Settings")]

        [SerializeField]
        private float m_fadeDuration = 1f;

        [SerializeField]
        [Tooltip("Keep bark canvas anchor point always in camera view.")]
        private bool m_keepInView;
        [SerializeField]
        [Tooltip("Ignore vertical bounds when aligning the bark ui to view")]
        private bool m_ignoreVertical;

        [SerializeField]
        private bool m_waitForPhraseEndIfPossible = true;

        [SerializeField]
        [Tooltip("The duration in seconds to show the bark text before fading it out. If zero, use the Dialogue Manager's Bark Settings.")]
        private float m_duration = 4f;

        [SerializeField]
        private float m_referenceCameraOrtographicSize = 5f;

        private string m_currentSubtitleEntryTag = string.Empty;

        private float m_doneTime = 0f;

        private Vector3 m_originalCanvasLocalPosition;

        private Vector3 m_originalScale;

        private Camera m_mainCamera;

        private int m_numSequencesActive = 0;

        private bool m_waitingForPhraseEnd = false;

        private CanvasTweener m_alphaTweener;


        private class CanvasTweener
        {
            private readonly CanvasGroup _canvasGroup;
            private readonly float _fadeDuration;

            public bool IsPlaying { get; set; }
            private bool _goingForward;
            private float _alpha = 0f;
            private float _oneOverDuration;

            public CanvasTweener(CanvasGroup canvasGroup, float fadeDuration)
            {
                _alpha = canvasGroup.alpha;
                _canvasGroup = canvasGroup;
                _fadeDuration = fadeDuration;
                _oneOverDuration = 1f / fadeDuration;
            }

            public void Update()
            {
                if (!IsPlaying) return;
                if(_goingForward)
                {
                    _alpha += Time.deltaTime * _oneOverDuration;
                    if(_alpha >=1f)
                    {
                        _alpha = 1f;
                        IsPlaying = false;
                    }
                    _canvasGroup.alpha = _alpha;
                }
                else
                {
                    _alpha -= Time.deltaTime * _oneOverDuration;
                    if (_alpha <= 0f)
                    {
                        _alpha = 0f;
                        IsPlaying = false;
                    }
                    _canvasGroup.alpha = _alpha;
                }
            }

            public void PlayForward()
            {
                IsPlaying = true;
                _goingForward = true;
            }

            public void PlayBackward()
            {
                IsPlaying = true;
                _goingForward = false;
            }
        }

        public override bool isPlaying
        {
            get
            {
                if (m_waitingForPhraseEnd)
                    return true;

                return (m_canvas != null) && m_canvas.enabled && (Time.time < m_doneTime);
            }
        }

        private bool m_bubbleFlipX = false;
        bool BubbleFlipX
        {
            set
            {
                if (m_bubbleFlipX == value)
                    return;

                m_bubbleFlipX = value;

                m_bubbleImage.rectTransform.localScale = new Vector3(m_bubbleFlipX ? -1f : 1f, m_bubbleFlipY ? -1f : 1f, 1f);
            }
        }

        private bool m_bubbleFlipY = false;
        bool BubbleFlipY
        {
            set
            {
                if (m_bubbleFlipY == value)
                    return;
                m_bubbleFlipY = value;

                m_bubbleImage.rectTransform.localScale = new Vector3(m_bubbleFlipX ? -1f : 1f, m_bubbleFlipY ? -1f : 1f, 1f);
            }
        }

        private void Awake()
        {
            m_originalScale = transform.localScale;

            m_canvasGroup.alpha = 0f;
            m_alphaTweener = new CanvasTweener(m_canvasGroup, m_fadeDuration);

            m_mainCamera = Camera.main;
        }

        private void Start()
        {
            if (m_canvas != null)
            {
                if (m_canvas.worldCamera == null)
                    m_canvas.worldCamera = Camera.main;

                m_originalCanvasLocalPosition = m_canvas.GetComponent<RectTransform>().localPosition;
            }
        }

        private Vector3 barkBubbleSize;

        private void Update()
        {
            m_alphaTweener.Update();
            // Canvas viewport clamping
            if ((isPlaying || m_alphaTweener.IsPlaying))
            {
                /* Scale correction */
                Vector3 parentCorrectedScale = m_originalScale;
                float camZoomFactor = m_referenceCameraOrtographicSize / m_mainCamera.orthographicSize;
                parentCorrectedScale.x *= 1 / transform.parent.localScale.x / camZoomFactor;
                parentCorrectedScale.y *= 1 / transform.parent.localScale.y / camZoomFactor;
                transform.localScale = parentCorrectedScale;

                // Not so elegant solution...

                // Return position to original
                m_canvas.transform.localPosition = m_originalCanvasLocalPosition;

                var canvasRectTransform = m_canvas.transform as RectTransform;

                // Canvas position on viewport
                var pos = Camera.main.WorldToViewportPoint(m_canvas.transform.position);
                barkBubbleSize.Set(
                    m_barkText.rectTransform.rect.width * canvasRectTransform.lossyScale.x + transform.position.x,
                    m_barkText.rectTransform.rect.height * canvasRectTransform.lossyScale.y + transform.position.y,
                    0f
                    );

                // Canvas extends position on viewport
                var posWithCanvasExtends = Camera.main.WorldToViewportPoint(barkBubbleSize);

                var bubbleViewportWidth = (posWithCanvasExtends.x - pos.x);

                if (m_keepInView)
                {
                    pos.x = Mathf.Clamp(pos.x, 0f, 1f - (posWithCanvasExtends.x - pos.x));
                    pos.y = Mathf.Clamp(pos.y, 0f, 1f - (posWithCanvasExtends.y - pos.y));
                }

                m_canvas.transform.position = Camera.main.ViewportToWorldPoint(pos);

                BubbleFlipX = (m_canvas.transform.localPosition.x * -1f) > m_bubbleImage.rectTransform.rect.width * canvasRectTransform.localScale.x / 2f;
                BubbleFlipY = (m_canvas.transform.localPosition.y * -1f) > m_bubbleImage.rectTransform.rect.height * canvasRectTransform.localScale.y;
            }
        }





        public override void Bark(Subtitle subtitle)
        {
            m_typewriter.StartTyping(subtitle.Text);
            var barkDuration = subtitle.Duration;
            CancelInvoke("Hide");
            m_doneTime = (Time.time + barkDuration);
            Invoke("Hide", barkDuration);

            m_alphaTweener.PlayForward();
        }




        public override void Hide()
        {
            m_numSequencesActive = 0;
            m_doneTime = 0f;
            m_alphaTweener.PlayBackward();
        }
    }
}