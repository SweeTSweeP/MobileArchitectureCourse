using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEditor.Build.Pipeline.Utilities;

namespace UnityEditor.Build.Pipeline.Tasks
{
    /// <summary>
    /// Validates scene dependency data and stores it in the cache.
    /// </summary>
    public class PreviewSceneDependencyData : IBuildTask
    {
        /// <inheritdoc />
        public int Version { get { return 1; } }

#pragma warning disable 649
        [InjectContext(ContextUsage.In)]
        IBuildParameters m_Parameters;

        [InjectContext(ContextUsage.In)]
        IBuildContent m_Content;

        [InjectContext]
        IDependencyData m_DependencyData;

        [InjectContext(ContextUsage.In, true)]
        IProgressTracker m_Tracker;

        [InjectContext(ContextUsage.In, true)]
        IBuildCache m_Cache;
#pragma warning restore 649

        CachedInfo GetCachedInfo(GUID scene, IEnumerable<ObjectIdentifier> references, SceneDependencyInfo sceneInfo, BuildUsageTagSet usageTags)
        {
            var info = new CachedInfo();
            info.Asset = m_Cache.GetCacheEntry(scene, Version);

            var dependencies = new HashSet<CacheEntry>();
            foreach (ObjectIdentifier reference in references)
                dependencies.Add(m_Cache.GetCacheEntry(reference));
            info.Dependencies = dependencies.ToArray();

            info.Data = new object[] { sceneInfo, usageTags };

            return info;
        }

        /// <inheritdoc />
        public ReturnCode Run()
        {
            IList<CachedInfo> cachedInfo = null;
            IList<CachedInfo> uncachedInfo = null;
            if (m_Parameters.UseCache && m_Cache != null)
            {
                IList<CacheEntry> entries = m_Content.Scenes.Select(x => m_Cache.GetCacheEntry(x, Version)).ToList();
                m_Cache.LoadCachedData(entries, out cachedInfo);

                uncachedInfo = new List<CachedInfo>();
            }

            for (int i = 0; i < m_Content.Scenes.Count; i++)
            {
                GUID scene = m_Content.Scenes[i];
                string scenePath = AssetDatabase.GUIDToAssetPath(scene.ToString());

                SceneDependencyInfo sceneInfo;
                BuildUsageTagSet usageTags;

                if (cachedInfo != null && cachedInfo[i] != null)
                {
                    if (!m_Tracker.UpdateInfoUnchecked(string.Format("{0} (Cached)", scenePath)))
                        return ReturnCode.Canceled;

                    sceneInfo = (SceneDependencyInfo)cachedInfo[i].Data[0];
                    usageTags = cachedInfo[i].Data[1] as BuildUsageTagSet;
                }
                else
                {
                    if (!m_Tracker.UpdateInfoUnchecked(scenePath))
                        return ReturnCode.Canceled;

                    var references = new HashSet<ObjectIdentifier>();
                    string[] dependencies = AssetDatabase.GetDependencies(scenePath);
                    foreach (var assetPath in dependencies)
                    {
                        var assetGuid = new GUID(AssetDatabase.AssetPathToGUID(assetPath));
                        if (ValidationMethods.ValidAsset(assetGuid) != ValidationMethods.Status.Asset)
                            continue;

                        // TODO: Use Cache to speed this up?
                        var assetIncludes = ContentBuildInterface.GetPlayerObjectIdentifiersInAsset(assetGuid, m_Parameters.Target);
                        var assetReferences = ContentBuildInterface.GetPlayerDependenciesForObjects(assetIncludes, m_Parameters.Target, m_Parameters.ScriptInfo);
                        references.UnionWith(assetIncludes);
                        references.UnionWith(assetReferences);
                    }

                    sceneInfo = new SceneDependencyInfo();
                    usageTags = new BuildUsageTagSet();

                    sceneInfo.SetScene(scenePath);
                    sceneInfo.SetProcessedScene(scenePath);
                    sceneInfo.SetReferencedObjects(references.ToArray());

                    if (uncachedInfo != null)
                        uncachedInfo.Add(GetCachedInfo(scene, sceneInfo.referencedObjects, sceneInfo, usageTags));
                }

                SetOutputInformation(scene, sceneInfo, usageTags);
            }

            if (m_Parameters.UseCache && m_Cache != null)
                m_Cache.SaveCachedData(uncachedInfo);

            return ReturnCode.Success;
        }

        void SetOutputInformation(GUID asset, SceneDependencyInfo sceneInfo, BuildUsageTagSet usageTags)
        {
            // Add generated scene information to BuildDependencyData
            m_DependencyData.SceneInfo.Add(asset, sceneInfo);
            m_DependencyData.SceneUsage.Add(asset, usageTags);
        }
    }
}
