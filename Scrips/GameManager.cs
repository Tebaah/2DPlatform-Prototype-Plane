using Godot;
using System;


public partial class GameManager : Node
{
   [Export]public PlayerController target;
   [Export]public Label gameOverLabel;
   [Export]public Button buttonMenu;
   

    public override void _Process(double delta)
    {
        if(target.isAlive == false)
        {
            gameOverLabel.Visible = true;
            buttonMenu.Visible = true;
        }
    }

    public void OnPressedButton()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Scenarios/menu.tscn");
    }
}
