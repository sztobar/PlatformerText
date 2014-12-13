using Microsoft.Xna.Framework.Input;

namespace PlatformerTest.Base
{
    public static class UserInput
    {
        public static UserInputState GetState()
        {
            var userInput = new UserInputState();
            return ReadKeyboardState(userInput);
        }

        private static UserInputState ReadKeyboardState(UserInputState userInput)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left) && state.IsKeyUp(Keys.Right))
                userInput.turnLeft = true;
            else if (state.IsKeyDown(Keys.Right) && state.IsKeyUp(Keys.Left))
                userInput.turnRight = true;

            if (state.IsKeyDown(Keys.Up) && state.IsKeyUp(Keys.Down))
                userInput.turnUp = true;
            if (state.IsKeyDown(Keys.Down) && state.IsKeyUp(Keys.Up))
                userInput.turnDown = true;

            return userInput;
        }
    }
}
