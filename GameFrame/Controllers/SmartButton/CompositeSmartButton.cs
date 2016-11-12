﻿using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Controllers.SmartButton
{
    public class CompositeSmartButton : AbstractSmartButton
    {
        private readonly List<IButtonAble> _buttons;

        public override bool ButtonJustPressed => _buttons.Any(ButtonMethods.ButtonJustPressed);

        public override bool ButtonReleased => _buttons.Any(ButtonMethods.ButtonReleased);

        public override bool ButtonHeldDown => _buttons.Any(ButtonMethods.ButtonHeldDown);

        public CompositeSmartButton()
        {
            _buttons = new List<IButtonAble>();
        }

        public CompositeSmartButton(IEnumerable<IButtonAble> buttons)
        {
            _buttons = new List<IButtonAble>(buttons);
        }

        public void AddButton(IButtonAble button)
        {
            _buttons.Add(button);
        }

        public override void Update()
        {
            foreach (var button in _buttons)
            {
                button.Update();
            }
            base.Update();
        }
    }
}
