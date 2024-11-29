using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed;
	[Export]
	public float JumpVelocity;
	[Export]
	public float speedBullet;
	[Export]
	public int ammo  { get; set; }
	[Export] 
	public int vida;
	[Export]
	public int vidaMax;

	
	public AnimatedSprite2D animation;
	private Label texto;
	private PackedScene bullet;

	public override void _Ready(){
		vida = vidaMax;
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		bullet = GD.Load<PackedScene>("res://scenes/entity/bullet.tscn");
		texto = GetNode<Label>("wammo");
		
		UpdateAmmoDisplay();
	}
	
	private void UpdateAmmoDisplay()
	{
		texto.Text = "Ammo: " + ammo.ToString();
	}
	
	public void recibirDano(int dano)
	{
		GD.Print(vida);
		vida -= dano;
		if(vida < 0) vida = 0;
	}
	
	public Player ClonePlayer()
	{
		// Crea una nueva instancia y copia los valores
		Player clone = (Player)GD.Load<PackedScene>("res://scenes/entity/Player.tscn").Instantiate();
		clone.Speed = this.Speed;
		clone.JumpVelocity = this.JumpVelocity;
		clone.speedBullet = this.speedBullet;
		clone.ammo = this.ammo;
		clone.Position = this.Position;
		return clone;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			animation.Play("jump");
			animation.FlipH = direction.X < 0;
			
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		
		if (Input.IsActionJustPressed("fire"))
		{
				if(ammo > 0){
					Bullet instBullet = (Bullet) bullet.Instantiate();
					instBullet.Position = this.GlobalPosition + new Vector2(0, 15);
					instBullet.RotationDegrees = animation.FlipH ? 180 : 0;
					instBullet.speedBullet = animation.FlipH ? -speedBullet : speedBullet;
					GetParent().AddChild(instBullet);
					
				GD.Print("Bala disparada en posición: " + instBullet.GlobalPosition);
				ammo--;
				}else{
					GD.Print("no ammo");
				}
				UpdateAmmoDisplay();
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		
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
