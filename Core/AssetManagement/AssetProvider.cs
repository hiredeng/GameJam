using ProjectName.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectName.Core.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IObjectResolver _resolver;

        public AssetProvider(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public GameObject Instantiate(string path, Vector3 where = default)
        {
            return Instantiate<GameObject>(path, where);
        }

        public T Instantiate<T>(string path, Vector3 where = default) where T : Object
        {
            T asset = Resources.Load<T>(path);
            if (asset.NotExists()) throw new KeyNotFoundException($"No asset found at path: {path}");
            T instance = _resolver.Instantiate<T>(asset, where, Quaternion.identity);
            return instance;
        }

        public T InstantiateWithParent<T>(string path, Transform parent) where T : Object
        {
            T asset = Resources.Load<T>(path);
            if (asset.NotExists()) throw new KeyNotFoundException($"No asset found at path: {path}");
            T instance = _resolver.Instantiate<T>(asset, parent);
            return instance;
        }

        public T Instantiate<T>(T template, Vector3 where = default) where T : Object
        {
            T instance = _resolver.Instantiate<T>(template, where, Quaternion.identity);
            return instance;
        }

        public T InstantiateWithParent<T>(T template, Transform parent) where T : Object
        {
            T instance = _resolver.Instantiate<T>(template, parent);
            return instance;
        }
    }
}