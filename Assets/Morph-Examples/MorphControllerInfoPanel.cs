using System.Collections.Generic;
using System.Linq;
using Morph.Core;
using Morph.Input.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Morph_Examples
{
    public class MorphControllerInfoPanel : MonoBehaviour
    {
        private Canvas _canvas;
        private Text _text;
        private List<IMorphController> _controllers;

        void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _text = GetComponentInChildren<Text>();

            _controllers = MorphMain.Instance.Application.Controllers;
        }

        void Update()
        {
            _canvas.transform.position = MorphMain.Instance.Application.MainDisplay.Camera.transform.position;
            _canvas.transform.rotation = Quaternion.LookRotation(MorphMain.Instance.Application.MainDisplay.Camera.transform.forward);
            _canvas.transform.Translate(Vector3.forward * 2f);

            _text.text = "";

            foreach (var controller in _controllers)
            {
                if (controller.HasFeatures(MorphControllerFeatures.Buttons))
                {
                    //Buttons
                    _text.text += "Buttons pressed:\n";
                    foreach (var button in controller.Buttons.Buttons)
                    {
                        _text.text += $"{button.ButtonName}: {button.Pressed}\n";
                    }
                }
                if (controller.HasFeatures(MorphControllerFeatures.TouchPad))
                {
                    //Touchpad
                    _text.text += "Touchpad:\n";
                    foreach (var touchpad in controller.TouchPad.TouchPads)
                    {
                        _text.text += $"H: {touchpad.HorizontalAxisValue}\n" +
                                      $"V: {touchpad.VerticalAxisValue}\n" +
                                      $"Clicked: {touchpad.Clicked}\n";
                    }
                }
            }
        }
    }
}
