using Godot;
using System;


public partial class GameManager : Node
{
   [Export]public PlayerController target;
   [Export]public Label gameOverLabel;
   [Export]public Button buttonMenu;
   [Export]public Label score;
   [Export]public Label life;
   public Global global;


    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
    }
    public override void _Process(double delta)
    {
        if(target.isAlive == false)
        {
            gameOverLabel.Visible = true;
            buttonMenu.Visible = true;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        int numScore = global.pointsPlayer;
        score.Text = $"Score: {numScore}";

        int lifePlayer = global.lifePlayer;
        life.Text = $"{lifePlayer}";
    }

    public void OnPressedButton()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Scenarios/menu.tscn");
    }
}
