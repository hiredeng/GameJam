using System.Collections.Generic;

namespace ProjectName.Gameplay.Interactive.Types
{

    public interface IRequireChoice
    {
        
    }

    public interface IRequireChoice<TInteractor> where TInteractor : class
    {
        IEnumerable<string> GetOptions();
        void SelectOption(TInteractor invoker, int index);
    }

}
