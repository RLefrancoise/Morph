using System.Collections.Generic;
using System.IO;
using System.Linq;
using Morph.Core;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

namespace Morph.Editor
{
    /// <inheritdoc />
    /// <summary>
    /// Morph Application window. You can configure the current application in this window.
    /// </summary>
    public class MorphApplicationWindow : EditorWindow
    {
        #region Fields

        private List<ApplicationData> _applications;
        private int _selectedApplication;
        private List<string> _missingDependencies;

        #endregion

        #region Properties

        /// <summary>
        /// Path of application file
        /// </summary>
        public string ApplicationPath => Path.Combine(Application.dataPath, "Morph", "application.json");

        #endregion

        #region Private Methods

        private static DirectoryInfo CreateApplicationsFolderIfNeeded()
        {
            string applicationsPath = Path.Combine(Application.dataPath, "..", "Applications");
            return FileUtils.CreateFolderIfNeeded(applicationsPath);
        }

        private static DirectoryInfo CreateDependenciesFolderIfNeeded()
        {
            string dependenciesPath = Path.Combine(Application.dataPath, "..", "Dependencies");
            return FileUtils.CreateFolderIfNeeded(dependenciesPath);
        }

        /// <summary>
        /// Is there specified dependency inside the project ?
        /// </summary>
        /// <param name="dependency">Dependency to test</param>
        /// <returns>true if dependency found</returns>
        private static bool HasDependency(string dependency)
        {
            var directories = Directory.EnumerateDirectories(Path.Combine(Application.dataPath), "*", SearchOption.AllDirectories);
            return directories.Any(directory => directory.EndsWith(dependency));
        }

        /// <summary>
        /// Check if some applications have the same name
        /// </summary>
        /// <returns>true if some applications have the same name</returns>
        private bool CheckForDuplicateApplicationName()
        {
            var appNames = _applications.Select(app => app.Name).ToList();
            return appNames.Count != appNames.Distinct().Count();
        }

        private void ChangeCurrentApplication(int applicationIndex)
        {
            File.WriteAllText(ApplicationPath, JsonUtility.ToJson(_applications[applicationIndex]));
            _selectedApplication = applicationIndex;

            Refresh();
        }

        /// <summary>
        /// Is current application scene in the build settings ?
        /// </summary>
        /// <returns></returns>
        private bool HasApplicationSceneInBuildSettings()
        {
            return EditorBuildSettings.scenes.ToList().Any(scene =>
                scene.path.Replace("Assets/", "").Replace(".unity", "") == _applications[_selectedApplication].Scene);
        }

        private void Refresh()
        {
            _applications = new List<ApplicationData>();
            _missingDependencies = new List<string>();

            //Applications
            DirectoryInfo applicationsDirectoryInfo = CreateApplicationsFolderIfNeeded();
            if (applicationsDirectoryInfo == null) return;

            string[] applicationsFiles = Directory.GetFiles(applicationsDirectoryInfo.FullName, "*.json");
            if (applicationsFiles.Length == 0)
            {
                Debug.LogWarning("Morph couldn't find any applications in the folder");
                return;
            }

            Debug.LogFormat("Morph has found {0} applications", applicationsFiles.Length);

            //Dependencies
            DirectoryInfo dependenciesDirectoryInfo = CreateDependenciesFolderIfNeeded();
            if (dependenciesDirectoryInfo == null) return;

            //Parse applications files
            foreach (var applicationFile in applicationsFiles)
            {
                string fileContent = File.ReadAllText(applicationFile);
                ApplicationData data = JsonUtility.FromJson<ApplicationData>(fileContent);
                _applications.Add(data);

                Debug.LogFormat("Morph found application {0}", data.Name);
            }

            //Find current application file
            if (!File.Exists(ApplicationPath))
            {
                Debug.LogWarning("Morph couldn't find any applications in the folder");

                //If no application file found, create one with first available application
                if (_applications.Count > 0)
                {
                    File.WriteAllText(ApplicationPath, JsonUtility.ToJson(_applications[0]));
                }
                else
                {
                    Debug.LogWarning(
                        "Morph couldn't find current application, and no application to use has been found");
                    return;
                }
            }
            else
            {
                ApplicationData appData = JsonUtility.FromJson<ApplicationData>(File.ReadAllText(ApplicationPath));
                for (var applicationIndex = 0; applicationIndex < _applications.Count; applicationIndex++)
                {
                    if (appData.Equals(_applications[applicationIndex]))
                    {
                        _selectedApplication = applicationIndex;
                        Debug.Log($"Morph current application is {appData.Name}");
                        break;
                    }
                }
            }

            //Look if all dependencies are found
            foreach (var dependency in _applications[_selectedApplication].Dependencies)
            {
                if (!HasDependency(dependency))
                {
                    _missingDependencies.Add(dependency);
                }
            }
        }

        #endregion

        #region Unity Specific

        [MenuItem("Morph/Application")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<MorphApplicationWindow>("Morph Application");
        }

        private void Awake()
        {
            Refresh();
        }

        private void OnGUI()
        {
            //Application
            GUILayout.Label("Application Settings", EditorStyles.boldLabel);

            IEnumerable<string> content = _applications.Select(application => application.Name);
            int selectedApplication = EditorGUILayout.Popup("Application", _selectedApplication, content.ToArray());

            //User has changed application
            if (selectedApplication != _selectedApplication)
            {
                ChangeCurrentApplication(selectedApplication);
                Debug.Log("Switch application");
            }

            //Missing dependencies
            foreach (var dependency in _missingDependencies)
            {
                EditorGUILayout.HelpBox($"Dependency {dependency} is missing", MessageType.Warning);
            }

            //Current application scene in build settings ?
            if (!HasApplicationSceneInBuildSettings())
            {
                EditorGUILayout.HelpBox($"Application scene {_applications[_selectedApplication].Scene} is not in build settings", MessageType.Error);
                if (GUILayout.Button("Open Build Settings"))
                {
                    EditorWindow.GetWindow<BuildPlayerWindow>();
                }
            }

            //Duplicate names ?
            if (CheckForDuplicateApplicationName())
            {
                EditorGUILayout.HelpBox("Several applications have the same name. It is not allowed.", MessageType.Error);
            }

            //Refresh button
            if (GUILayout.Button("Refresh"))
            {
                Debug.Log("Morph application window is refreshing...");
                Refresh();
            }
        }

        #endregion
    }
}
