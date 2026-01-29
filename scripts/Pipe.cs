using Godot;
using System;

public partial class Pipe : Area2D
{
	[Export] public float ScrollSpeed = 200.0f;
	private bool _scored = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Connect the body entered signal to detect when the player collides with the pipe
		BodyEntered += (Node2D body) =>
		{
			GD.Print("Body entered: " + body.Name);
			if (body is Player)
			{
				// Change to game over scene
				GetTree().ChangeSceneToFile("res://scenes/GameOver.tscn");	
			}
		};
		
		// Find and connect to the score area
		var scoreArea = GetNodeOrNull<Area2D>("ScoreArea");
		if (scoreArea != null)
		{
			scoreArea.BodyEntered += (Node2D body) =>
			{
				if (body is Player && !_scored)
				{
					_scored = true;
					GetNode<GameManager>("/root/GameManager").AddScore();
				}
			};
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Move pipe left at constant speed
		Position += new Vector2(-ScrollSpeed * (float)delta, 0);
		
		// Remove pipe when it goes off-screen
		if (Position.X < -500)
		{
			QueueFree();
		}
	}
}
