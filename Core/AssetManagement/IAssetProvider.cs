using ProjectName.Core.ServiceModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.Core.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public GameObject Instantiate(string path, Vector3 where = default);
        public T Instantiate<T>(string path, Vector3 where = default) where T : Object;
        public T InstantiateWithParent<T>(string path, Transform parent) where T : Object;

        public T Instantiate<T>(T template, Vector3 where = default) where T : Object;
        public T InstantiateWithParent<T>(T template, Transform parent) where T : Object;
    }
}