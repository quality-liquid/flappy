using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float JumpVelocity = -400.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<GameManager>("/root/GameManager").ResetScore();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		velocity += GetGravity() * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("flap"))
		{
			velocity.Y = JumpVelocity;
		}

		Velocity = velocity;

		// handle bird tilting in the direction of movement with lerp
		Rotation = Mathf.Lerp(Rotation, Mathf.Clamp(Velocity.Y / 400.0f, -0.5f, 0.5f), 0.1f);

		MoveAndSlide();

		// if the player hits the floor, change scene to game over
		if (IsOnFloor())
		{
			GetTree().ChangeSceneToFile("res://scenes/GameOver.tscn");
		}
	}
}
