using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.GUI
{
    public class OptionTextBoxList : ITextBox
    {
        private int _index = 0;
        public List<OptionTextBox> OptionTextBoxes;

        public OptionTextBoxList(List<OptionTextBox> options)
        {
            OptionTextBoxes = options;
        }
        public void Interact()
        {
            if (_index < OptionTextBoxes.Count)
            {
                OptionTextBoxes[_index].Interact();
            }
        }

        public bool Interact(Point point)
        {
            return OptionTextBoxes.Any(optionBox => optionBox.Interact(point));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < OptionTextBoxes.Count; i++)
            {
                OptionTextBoxes[i].Draw(spriteBatch, i == _index);
            }
        }

        public void MoveOption(int valueBy)
        {
            var newValue = _index + valueBy;
            if (newValue >= 0 && newValue < OptionTextBoxes.Count)
            {
                _index = newValue;
            }
        }
    }
}
