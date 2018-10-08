using System;
using System.Collections.Generic;
using Morph.Components;
using Morph.Input.Controllers;
using UnityEngine;

namespace Morph.Core.Applications
{
    /// <inheritdoc cref="IMorphApplication"/>
    /// <summary>
    /// Default Morph application
    /// </summary>
    public class MorphDefaultApplication : MonoBehaviour, IMorphApplication
    {
        [SerializeField]
        private GameObject _mainCamera;

        [SerializeField]
        private List<GameObject> _controllers;

        #region Properties
        public IMorphCamera MainCamera { get; private set; }
        public List<IMorphController> Controllers { get; private set; }
        public List<IMorphComponent> Components { get; private set; }

        #endregion

        public bool Initialize()
        {
            //Main camera
            if (!_mainCamera) return false;

            MainCamera = _mainCamera.GetComponent<IMorphCamera>();

            //Components
            Components = new List<IMorphComponent>();

            //Controllers
            Controllers = new List<IMorphController>();

            foreach (var controller in _controllers)
            {
                if (!controller) continue;

                IMorphController morphController = controller.GetComponent<IMorphController>();
                if (morphController == null)
                {
                    Debug.LogWarningFormat("Controller game object {0} has no Morph controller", controller.name);
                    continue;
                }

                if (!AddController(morphController))
                {
                    Debug.LogErrorFormat("Failed to add controller {0}", controller.name);
                    continue;
                }
            }

            return true;
        }

        public bool AddController(IMorphController controller)
        {
            //Initialize controller
            if (!controller.Initialize()) return false;

            //Add controller to list
            Controllers.Add(controller);
            return true;
        }

        public bool DestroyController(IMorphController controller)
        {
            if (!Controllers.Contains(controller)) return false;

            controller.DeInitialize();
            WhenControllerDestroyed?.Invoke(this, controller);
            Controllers.Remove(controller);
            return true;
        }

        public bool RegisterComponent(IMorphComponent component)
        {
            if(!Components.Contains(component))
                Components.Add(component);

            WhenComponentRegistered?.Invoke(this, component);

            return true;
        }

        public bool UnregisterComponent(IMorphComponent component)
        {
            if (Components.Contains(component))
                Components.Remove(component);

            WhenComponentUnregistered?.Invoke(this, component);

            return true;
        }

        public event EventHandler<IMorphController> WhenControllerDestroyed;
        public event EventHandler<IMorphComponent> WhenComponentRegistered;
        public event EventHandler<IMorphComponent> WhenComponentUnregistered;
    }
}
