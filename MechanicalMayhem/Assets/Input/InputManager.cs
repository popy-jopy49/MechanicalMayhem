public static class InputManager
{

    public static MainInput INPUT_ACTIONS;

    static InputManager()
    {
        INPUT_ACTIONS = new MainInput();
        INPUT_ACTIONS.Enable();
    }

}
