using Godot;
using System;

public class Tom : KinematicBody
{
	[Export]
	public float Speed = 2.5f;

	[Export]
	public float JumpSpeed = 10.0f;

	[Export]
	public float JumpAcceleration = 0.2f;

	[Export]
	public float Gravity = 50f;

	[Export]
	public float RotationSpeed = 1.0f; // Speed of rotation in degrees per second

	private Vector3 velocity = new Vector3();

	private bool isJumping = false;

	public override void _Ready()
	{
		GD.Print("Tom ready...");
	}

	public override void _PhysicsProcess(float delta)
	{
		Vector3 movementInput = new Vector3();

		if (Input.IsActionPressed("move_right"))
			movementInput.x += 1;
		if (Input.IsActionPressed("move_left"))
			movementInput.x -= 1;
		if (Input.IsActionPressed("move_forward"))
			movementInput.z -= 1;
		if (Input.IsActionPressed("move_backward"))
			movementInput.z += 1;

		movementInput = movementInput.Normalized();

		velocity = movementInput * Speed;

		if (isJumping && velocity.y < 0)
		{
			isJumping = false;
		}

		if (IsOnFloor() && Input.IsActionJustPressed("jump"))
		{
			velocity.y = JumpSpeed;
			isJumping = true;
		}

		if (isJumping && Input.IsActionPressed("jump"))
		{
			velocity.y += JumpAcceleration * delta;
		}

		velocity.y -= Gravity * delta;

		MoveAndSlide(velocity, Vector3.Up);

		// Rotate the player when E or Q is pressed
		if (Input.IsActionPressed("rotate_clockwise"))
		{
			RotateY(Mathf.Deg2Rad(90) * delta * RotationSpeed); // Rotate clockwise by 90 degrees
		}
		else if (Input.IsActionPressed("rotate_counterclockwise"))
		{
			RotateY(Mathf.Deg2Rad(-90) * delta * RotationSpeed); // Rotate counterclockwise by 90 degrees
		}
	}
}
