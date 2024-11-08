using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
	[Export]
	public float Speed;
	[Export]
	public float JumpVelocity;
	[Export]
	public float speedBullet;
	[Export]
	public int ammo;
	
	private AnimatedSprite2D animation;

	private PackedScene bullet;

	public override void _Ready(){
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		bullet = GD.Load<PackedScene>("res://scenes/bullet.tscn");
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			animation.Play("jump");
			
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		if (Input.IsActionJustPressed("fire"))
		{
				Bullet instBullet = (Bullet) bullet.Instantiate();
				instBullet.Position = new Vector2(0,19);
				instBullet.speedBullet = this.speedBullet;
				AddChild(instBullet);
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			animation.FlipH = velocity.X < 0;
			if (IsOnFloor())
			{
				animation.Play("walk");
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (IsOnFloor())
			{
				animation.Play("idle");
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
