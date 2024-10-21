using Pripizden;
using UnityEngine;
using VContainer;
using TempCharacter = Pripizden.Gameplay.Character.Character;

namespace ProjectName.UI
{
    public class CharacterHud : MonoBehaviour
    {
        [SerializeField]
        private ParameterView _progressView;
        [SerializeField]
        private ParameterView _productivityView;


        [Inject]
        public void Construct()
        {

        }

        public void Init(TempCharacter character)
        {
            _progressView.Init(character.Stats);
            _productivityView.Init(character.Stats);
        }
    }
}