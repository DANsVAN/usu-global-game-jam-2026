using Godot;
using System;

public partial class ShopStall : Control

{
	// Called when the node enters the scene tree for the first time.

public TextureButton myButton;
public GameManager gameManager;
public CanvasLayer shop;
bool MouseEntered = false;
		private void OnMouseEntered()
		{
			// myButton.SelfModulate  = Colors.Red;
			GD.Print("Mouse is over the button!");
			MouseEntered = true;
			// You could also change the color here:
			// SelfModulate = Colors.Yellow;
		}

		private void OnMouseExited()
		{
			// myButton.SelfModulate = Colors.Black;
			GD.Print("Mouse has left the building.");
			MouseEntered = false;
			// SelfModulate = Colors.White;
		}

	public override void _Ready()
	{
			myButton = GetNode<TextureButton>("TextureButton");
			shop = GetParent() as CanvasLayer;
			gameManager = shop.GetParent() as GameManager;
			
			// Connect the signals to local functions
			myButton.MouseEntered += OnMouseEntered;
			myButton.MouseExited += OnMouseExited;
			myButton.SelfModulate = Colors.Black;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print("is_blue_teame_turn");
		if(MouseEntered == true)
		{
			myButton.SelfModulate = Colors.Black;
		}
		else
		{
			if (gameManager.is_blue_teame_turn)
			{
				myButton.SelfModulate = Colors.Blue;
			}
			else if (gameManager.is_red_teame_turn)
			{
				myButton.SelfModulate = Colors.Red;
			}
			else
			{
				myButton.SelfModulate = Colors.Black;
			}
			
		}

	}
}
