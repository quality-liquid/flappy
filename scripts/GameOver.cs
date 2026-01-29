using Godot;
using System;

public partial class GameOver : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if the player presses the flap button, start the game
		if (Input.IsActionJustPressed("flap"))
		{
			GetTree().ChangeSceneToFile("res://scenes/main.tscn");
		}
	}
}
