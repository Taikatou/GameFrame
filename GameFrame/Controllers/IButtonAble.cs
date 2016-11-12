using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers
{
    public interface IButtonAble
    {
        bool Active { get; }
        bool PreviouslyActive { get; }
        void Update();
    }
}
