using ProjectName.Services.Timing;

namespace ProjectName.Gameplay.Mission
{
    public class MissionCoordinator
    {
        private readonly ITimeKeeper _timeKeeper;

        public MissionCoordinator(ITimeKeeper timeKeeper)
        {
            _timeKeeper = timeKeeper;
        }


    }
}