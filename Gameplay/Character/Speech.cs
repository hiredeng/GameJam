using ProjectName.UI;
using UnityEngine;

public class Speech : MonoBehaviour
{
    [SerializeField]
    AbstractBarkUI _barkUIPrefab;

    [SerializeField]
    Vector3 _barkUIOffset;

    public AbstractBarkUI CurrentBarkUI { get; protected set; }

    private void Awake()
    {
        SetupBarkUI();
    }

    private void SetupBarkUI()
    {
        if (_barkUIPrefab != null)
        {
            CurrentBarkUI = Instantiate<AbstractBarkUI>(_barkUIPrefab);
            CurrentBarkUI.transform.SetParent(transform);
            CurrentBarkUI.transform.localPosition = _barkUIOffset;
            CurrentBarkUI.transform.localRotation = Quaternion.identity;
        }
    }

    public void ShowBark(string text, float duration)
    {
        CurrentBarkUI.Bark(new Subtitle()
        {
            Text = text,
            Duration = duration,
        });
    }
}
