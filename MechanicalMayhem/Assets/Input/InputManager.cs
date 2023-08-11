public static class InputManager
{

    public static MainInput INPUT_ACTIONS;

    static InputManager()
    {
        Initialise();
    }

    public static void Initialise()
    {
        INPUT_ACTIONS = new MainInput();
        INPUT_ACTIONS.Enable();
    }

}
