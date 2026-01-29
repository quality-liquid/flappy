using Godot;
using System;

public partial class PipeSpawner : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// start the pipe spawning timer
		GetNode<Timer>("PipeSpawnTimer").Start();
		// spawn pipes every time the timer times out
		GetNode<Timer>("PipeSpawnTimer").Timeout += () =>
		{
			// spawn a new pipe instance
			PackedScene pipeScene = GD.Load<PackedScene>("res://scenes/pipe.tscn");
			Pipe pipeInstance = pipeScene.Instantiate<Pipe>();
			
			// set the pipe position to a random y position between 75 and 275
			Random random = new Random();
			float yPosition = (float)(random.NextDouble() * 200 - 100);
			pipeInstance.Position = new Vector2(650, yPosition);
			
			// add the pipe instance to the scene tree
			GetParent().AddChild(pipeInstance);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
