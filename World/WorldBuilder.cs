using ProjectName.Core.AssetManagement;
using ProjectName.Services.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.World
{
    public interface IWorldBuilder
    {
        IWorldBuilder WithSegment(WorldSegment prefab);
        WorldData Build();
    }
    public class WorldBuilder : IWorldBuilder
    {
        private List<WorldSegment> _segments;
        private readonly IAssetProvider _assetProvider;

        public WorldBuilder(IAssetProvider assetProvider)
        {
            _segments = new List<WorldSegment>();
            _assetProvider = assetProvider;
        }

        public IWorldBuilder WithSegment(WorldSegment prefab)
        {
            _segments.Add(prefab);
            return this;
        }

        public WorldData Build()
        {
            WorldSegment[] segments = new WorldSegment[_segments.Count];

            Vector3 worldPosition = Vector3.zero;

            for(int i = 0; i<_segments.Count; i++)
            {
                WorldSegment segment = _segments[i];
                var segmentInstance = _assetProvider.Instantiate<WorldSegment>(segment, worldPosition);
                worldPosition.x += segment.Width;
                segments[i] = segmentInstance;
            }

            WorldData data = new WorldData()
            {
                WorldSegments = segments
            };

            return data;
        }
    }
}