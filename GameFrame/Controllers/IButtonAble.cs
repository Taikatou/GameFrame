using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers
{
    public interface IButtonAble
    {
        bool Active { get; }
        bool PreviouslyActive { get; }
        Buttons Button { get; }
        void Update();
    }
}
