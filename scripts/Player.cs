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
	public int monedas  { get; set; }

	public AnimatedSprite2D powerUps;
	public AnimatedSprite2D animation;
	private Label textammo;
	private Label texthealth;
	private Label textmonedas;
	private PackedScene bullet;

	public override void _Ready(){
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		bullet = GD.Load<PackedScene>("res://scenes/entity/bullet.tscn");
		textammo = GetNode<Label>("HUD/ammoSprite/ammo");
		texthealth = GetNode<Label>("HUD/healthSprite/health");
		textmonedas = GetNode<Label>("HUD/monedasSprite/monedas");
		powerUps = GetNode<AnimatedSprite2D>("HUD/Sprite2D");
		updateAmmoDisplay();
		updateHeatlhDisplay();
		updateMonedasDisplay();
	}
	
	private void updateMonedasDisplay()
	{
		textmonedas.Text = monedas.ToString();
	}
	
	
	private void updateAmmoDisplay()
	{
		textammo.Text = ammo.ToString();
	}
	private void updateHeatlhDisplay(){
		texthealth.Text = vida.ToString();
	}
	
	public void recibirDano(int dano)
	{
		vida -= dano;
		if(vida < 0) vida = 0;
		updateHeatlhDisplay();
		
	}
	
	

	public void recibirDinero(int cantidad){
		monedas += cantidad;
	}
	
	public void recibirBalas(int cantidad){
		ammo += cantidad;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		updateAmmoDisplay();
		updateHeatlhDisplay();
		updateMonedasDisplay();
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
			GD.Print(JumpVelocity);
		}

		
		//if (Input.IsActionJustPressed("fire"))
		//{
				//if(ammo > 0){
					//Bullet instBullet = (Bullet) bullet.Instantiate();
					//instBullet.Position = this.GlobalPosition + new Vector2(10, 15);
					//instBullet.RotationDegrees = animation.FlipH ? 180 : 0;
					//instBullet.speedBullet = animation.FlipH ? -speedBullet : speedBullet;
					//GetParent().AddChild(instBullet);
					//
				//GD.Print("Bala disparada en posición: " + instBullet.GlobalPosition);
				//ammo--;
				//}else{
					//GD.Print("no ammo");
				//}
				//updateAmmoDisplay();
		//}
		
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
