using Logic.Player;

namespace Logic.Classes
{
    public sealed class InputProvider
    {
        private IPlayerInput _currentInput;

        public IPlayerInput CurrentInput => _currentInput;

        public void SetInput(IPlayerInput input)
        {
            _currentInput = input;
        }
    }
}