using Godot;
using System;

public partial class Piece : Node2D
{
	public int movement = 1;
	public int move_dir = 1;
	public	int damage = 1;
	public 	int HP = 1;
	// Called when the node enters the scene tree for the first time.

	public void SetPos(float positionX, float positionY)
	{
		// 1. Get current position
		Vector2 currentPos = Position;

		// 2. Modify only what you need
		currentPos.X = positionX; 
		currentPos.Y = positionY; 

		// 3. Re-assign it
		Position = currentPos;
	}
	public override void _Ready()
	{

	}
	public void init_data(int m_dir){
		move_dir = m_dir;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
