using System;
using System.Collections.Generic;
using Morph.Components;
using Morph.Components.Interaction;
using Morph.Components.Navigation;
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
        private GameObject player;
        
        [SerializeField]
        private GameObject _mainCamera;

        [SerializeField]
        private List<GameObject> _controllers;

        #region Properties

        /// <inheritdoc />
        public IMorphPlayerController PlayerController { get; private set; }
        public IMorphDisplay MainDisplay { get; private set; }
        public List<IMorphController> Controllers { get; private set; }
        public List<IMorphComponent> Components { get; private set; }

        #endregion

        public bool Initialize()
        {
            //Player controller
            if (!player) return false;

            PlayerController = player.GetComponent<IMorphPlayerController>();
            
            //Main camera
            if (!_mainCamera) return false;

            MainDisplay = _mainCamera.GetComponent<IMorphDisplay>();

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

            //Accept the application as a visitor
            component.Accept(this);

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

        #region IMorphComponentVisitor

        public void Visit(IMorphComponent component)
        {
            foreach(var controller in Controllers)
                component.Accept(controller);
        }

        public void Visit(IMorphInteractiveComponent interactiveComponent)
        {
            foreach(var controller in Controllers)
                interactiveComponent.Accept(controller);
        }

        public void Visit(IMorphComponentInteraction interaction)
        {
            foreach (var controller in Controllers)
            {
                interaction.Accept(controller);
            }
        }

        public void Visit(IMorphWarpZone warpZone)
        {
            foreach(var controller in Controllers)
                warpZone.Accept(controller);
        }

        /// <inheritdoc />
        public void Visit(IMorphPlayerController playerController)
        {
            foreach(var controller in Controllers)
                playerController.Accept(controller);
        }

        #endregion
    }
}
