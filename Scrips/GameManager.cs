using Godot;
using System;


public partial class GameManager : Node
{
    // Variables para controlar elementos graficos 
   [Export]public PlayerController target;
   [Export]public Label gameOverLabel;
   [Export]public Button buttonMenu;
    [Export]public Label score;
    [Export]public Label life;
   public Global global;


    public override void _Ready()
    {
        // Inicializamos la variable global
        global = GetNode<Global>("/root/Global");
    }
    public override void _Process(double delta)
    {
        // Si el objetivo esta vivo
        if(target.isAlive == false)
        {
            // Los elementos graficos no estan visibles
            gameOverLabel.Visible = true;
            buttonMenu.Visible = true;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // Modifica los valores globales y son actualizados los elementos graficos
        int numScore = global.pointsPlayer;
        score.Text = $"Score: {numScore}";

        int lifePlayer = global.lifePlayer;
        life.Text = $"{lifePlayer}";
    }

    public void OnPressedButton()
    {
        // Cambio de escena al presionar el boton
        GetTree().ChangeSceneToFile("res://Scenes/Scenarios/menu.tscn");
    }
}
