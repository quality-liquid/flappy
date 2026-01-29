using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	[Signal]
	public delegate void ScoreChangedEventHandler(int newScore);
	
	[Signal]
	public delegate void HighScoreChangedEventHandler(int newHighScore);

	public int CurrentScore { get; private set; }
	public int HighScore { get; private set; }

	private Dictionary<string, PackedScene> scenes = new Dictionary<string, PackedScene>();
	private const string SavePath = "user://highscore.save";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scenes["game"] = GD.Load<PackedScene>("res://scenes/main.tscn");
		scenes["gameOver"] = GD.Load<PackedScene>("res://scenes/GameOver.tscn");
		scenes["start"] = GD.Load<PackedScene>("res://scenes/Start.tscn");
		LoadHighScore();
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ChangeScene(string sceneName)
	{
		if (scenes.ContainsKey(sceneName))
		{
			GetTree().ChangeSceneToPacked(scenes[sceneName]);
		}
	}

	public void AddScore(int points = 1)
	{
		CurrentScore += points;
		EmitSignal(SignalName.ScoreChanged, CurrentScore);
		GD.Print($"Score: {CurrentScore}, High Score: {HighScore}");
		
		if (CurrentScore > HighScore)
		{
			HighScore = CurrentScore;
			EmitSignal(SignalName.HighScoreChanged, HighScore);
			SaveHighScore();
		}
	}

	public void ResetScore()
	{
		CurrentScore = 0;
		EmitSignal(SignalName.ScoreChanged, CurrentScore);
		GD.Print("Score reset.");
	}
	
	private void SaveHighScore()
	{
		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
		if (file != null)
		{
			file.Store32((uint)HighScore);
			GD.Print($"High score saved: {HighScore}");
		}
	}
	
	private void LoadHighScore()
	{
		if (FileAccess.FileExists(SavePath))
		{
			using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
			if (file != null)
			{
				HighScore = (int)file.Get32();
				GD.Print($"Loaded high score: {HighScore}");
			}
		}
	}
}