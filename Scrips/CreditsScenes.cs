using Godot;
using System;

public partial class CreditsScenes : Node
{
    public override void _Ready()
    {
        // Inicializamos la funcion 
        ChangeToScene();
    }

    public async void ChangeToScene()
    {
        // Esperara 5 segundos y luego cambiara a la escena "menu"
        await ToSignal(GetTree().CreateTimer(5), "timeout");
        GetTree().ChangeSceneToFile("res://Scenes/Scenarios/menu.tscn");
    }
}
