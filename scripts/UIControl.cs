using Godot;
using System;

public partial class UIControl : Node
{
	private Label _scoreLabel;
	private Label _highScoreLabel;
	private GameManager _gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get the label nodes (adjust paths to match your scene structure)
		_scoreLabel = GetNode<Label>("Main/CanvasLayer/VBoxContainer/HBoxContainer/CurrScore");
		_highScoreLabel = GetNode<Label>("Main/CanvasLayer/VBoxContainer/HBoxContainer2/HighScore");
		
		// Get the GameManager autoload
		_gameManager = GetNode<GameManager>("/root/GameManager");
		
		// Connect to the signals
		_gameManager.ScoreChanged += OnScoreChanged;
		_gameManager.HighScoreChanged += OnHighScoreChanged;
		
		// Initialize with current values
		OnScoreChanged(_gameManager.CurrentScore);
		OnHighScoreChanged(_gameManager.HighScore);
	}

	public override void _ExitTree()
	{
		// Disconnect signals when the node is removed
		if (_gameManager != null)
		{
			_gameManager.ScoreChanged -= OnScoreChanged;
			_gameManager.HighScoreChanged -= OnHighScoreChanged;
		}
	}

	private void OnScoreChanged(int newScore)
	{
		_scoreLabel.Text = $"Score: {newScore}";
	}

	private void OnHighScoreChanged(int newHighScore)
	{
		_highScoreLabel.Text = $"High Score: {newHighScore}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
