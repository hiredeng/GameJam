using UnityEngine;

namespace ProjectName.World
{

    public class WorldWrapper : MonoBehaviour
    {
        Vector3 _anchorPosition = Vector3.zero;
        float _totalWidth;
        int _roomCount;

        private WorldSegment[] _rooms;
        private Transform _target;
        private bool _ready = false;

        public void Initialize(WorldData world, Transform wrapTarget)
        {
            _ready = true;
            _rooms = world.WorldSegments;
            _target = wrapTarget;
            _roomCount = _rooms.Length;
            for (int i = 0; i < _roomCount; i++)
            {
                _totalWidth += _rooms[i].Width;
            }
        }

        public void Update()
        {
            if (!_ready) return;
            
            Vector3 targetPosition = _target.position;
            float deltaX = targetPosition.x - _anchorPosition.x;

            float factor = deltaX / _totalWidth;
            int fullWrapCount = Mathf.FloorToInt(factor);

            float fullWrapCellXPosition = _anchorPosition.x + _totalWidth * fullWrapCount;

            float internalOffset = targetPosition.x - fullWrapCellXPosition;

            float tempOffset = 0f;
            int leftRoomCount = 0;
            for (int i = 0; i < _roomCount; i++)
            {
                tempOffset += _rooms[i].Width;
                if (internalOffset < tempOffset)
                {
                    leftRoomCount = i;
                    break;
                }
            }

            int halfRoomCount = _roomCount / 2 - 1;
            leftRoomCount -= halfRoomCount;

            if (leftRoomCount < 0)
            {
                leftRoomCount += _roomCount;
                fullWrapCellXPosition -= _totalWidth;
            }


            float leftOffset = 0f;
            for (int i = 0; i < leftRoomCount; i++)
            {
                leftOffset += _rooms[i].Width;
            }



            Vector3 roomPosition = new Vector3(fullWrapCellXPosition + leftOffset, _anchorPosition.y, 0f);

            for (int i = leftRoomCount; i < _roomCount + leftRoomCount; i++)
            {
                var index = i % _roomCount;
                _rooms[index].transform.position = roomPosition;
                roomPosition.x += _rooms[index].Width;
            }
        }        
    }
}