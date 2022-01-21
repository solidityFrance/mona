#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Mona
{
    // Quailty Checks that run both in editor and in private-build
    public static partial class QualityAssurance
    {

        // Is a list of error codes generated by the space.
        public static List<string[]> SpaceErrors;
        private static List<int> s_iconRegisters;

        private static Scene s_spaceScene, s_artifactsScene, s_portalsScene;

        // Run QA check
        public static void CheckQuality()
        {
            // Add a test error
            SpaceErrors = new List<string[]>();

            if (s_iconRegisters == null)
            {
                s_iconRegisters = new List<int>();
            }
            else
            {
                // Remove all registered icons
                foreach (int i in s_iconRegisters)
                {
                    TreeIcon.UnregisterHierarchyItem(i);
                }
            }

#if UNITY_EDITOR
            s_spaceScene = EditorSceneManager.GetSceneByName("Space");
            s_artifactsScene = EditorSceneManager.GetSceneByName("Artifacts");
            s_portalsScene = EditorSceneManager.GetSceneByName("Portals");

            // Force all scenes to be loaded
            if (!s_spaceScene.isLoaded)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Space.unity", OpenSceneMode.Additive);
            }

            if (!s_artifactsScene.isLoaded)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Artifacts.unity", OpenSceneMode.Additive);
            }

            if (!s_portalsScene.isLoaded)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Portals.unity", OpenSceneMode.Additive);
            }
#else
            // Runtime loaded scenes
            s_spaceScene = SceneManager.GetSceneByName("Space");
            s_artifactsScene = SceneManager.GetSceneByName("Artifacts");
            s_portalsScene = SceneManager.GetSceneByName("Portals");
#endif

            // Scene tests
            TestSceneExistance();
            TestLayerTags();

            // Portal tests
            TestPortalPlacments();
            TestPortalTag();
            TestPortalCollider();
            TestPortalNames();

            // Artifacts tests
            TestArtifactPlacments();
            TestArtifactTag();
            TestArtifactCollider();
            TestArtifactNames();

            // Canvas tests
            TestCanvasPlacments();
            TestCanvasNames();

        }
    }

    public static class MonaConstants
    { 

    }
}
#endif