using System;
using System.Collections.Generic;
using Morph.Components;
using Morph.Components.Navigation;
using Morph.Input.Controllers;

namespace Morph.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Interface for any Morph application
    /// </summary>
    public interface IMorphApplication : IMorphComponentVisitor
    {
        /// <summary>
        /// Player controller
        /// </summary>
        IMorphPlayerController PlayerController { get; }
        
        /// <summary>
        /// Main camera of the application
        /// </summary>
        IMorphDisplay MainDisplay { get; }

        /// <summary>
        /// Controllers available for the application
        /// </summary>
        List<IMorphController> Controllers { get; }

        /// <summary>
        /// List of all Morph components (interactive, ...). Controllers are not components.
        /// </summary>
        List<IMorphComponent> Components { get; }

        /// <summary>
        /// Initialize the application
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>
        /// Add a controller
        /// </summary>
        /// <param name="controller">controller to add</param>
        /// <returns>true if add successful</returns>
        bool AddController(IMorphController controller);

        /// <summary>
        /// Destroy controller
        /// </summary>
        /// <param name="controller">controller to destroy</param>
        /// <returns>true if destroy succesfully</returns>
        bool DestroyController(IMorphController controller);

        /// <summary>
        /// Register a new component into the application
        /// </summary>
        /// <param name="component">component to register</param>
        /// <returns>true if registered</returns>
        bool RegisterComponent(IMorphComponent component);

        /// <summary>
        /// Unregister a component from the application
        /// </summary>
        /// <param name="component">component to unregister</param>
        /// <returns>true if unregistered</returns>
        bool UnregisterComponent(IMorphComponent component);

        /// <summary>
        /// Called when a controller is destroyed
        /// </summary>
        event EventHandler<IMorphController> WhenControllerDestroyed;

        /// <summary>
        /// Called when a component is registered
        /// </summary>
        event EventHandler<IMorphComponent> WhenComponentRegistered;

        /// <summary>
        /// Called when a component is unregistered
        /// </summary>
        event EventHandler<IMorphComponent> WhenComponentUnregistered;
    }
}
