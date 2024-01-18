using Godot;
using System;

public partial class GameManager : Node
{
   [Export]public PlayerController target;
   [Export]public Label _gameOverLabel;

    public override void _Ready()
    {

    }


    public override void _Process(double delta)
    {
        if(target.isAlive == false)
        {
            _gameOverLabel.Visible = true;
        }
    }
}
