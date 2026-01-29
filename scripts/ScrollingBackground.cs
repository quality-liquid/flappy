using Godot;
using System;

public partial class ScrollingBackground : Node2D
{

	[Export] public float ScrollSpeed = 100.0f;

	private Sprite2D Background1;
	private Sprite2D Background2;
	private float _backgroundWidth;
	private Vector2 _startPosition1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Background1 = GetNode<Sprite2D>("Background1");
		Background2 = GetNode<Sprite2D>("Background2");
		_backgroundWidth = Background1.Texture.GetWidth();
		Background2.Position = new Vector2(Background1.Position.X + _backgroundWidth, Background1.Position.Y);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Background1.Position -= new Vector2(ScrollSpeed * (float)delta, 0);
		Background2.Position -= new Vector2(ScrollSpeed * (float)delta, 0);

		if (Background1.Position.X <= -_backgroundWidth/2)
		{
			Background1.Position = new Vector2(Background2.Position.X + _backgroundWidth, Background1.Position.Y);
		}

		if (Background2.Position.X <= -_backgroundWidth/2)
		{
			Background2.Position = new Vector2(Background1.Position.X + _backgroundWidth, Background1.Position.Y);
		}
	}
}
