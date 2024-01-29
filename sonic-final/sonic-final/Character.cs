using System;
using System.Windows.Forms;
using System.Drawing;

namespace sonic_final
{	
	/// <summary>
	/// Classe dos Personagens com seus respectivos atributos.
	/// </summary>
	public class Character : PictureBox
	{
		public Character()
		{
			SizeMode = PictureBoxSizeMode.StretchImage; // Ocupar o espaço todo.
			BackColor = Color.Transparent; // Define o fundo transparente.
		}
		
		// Atributos dos personagens.
		public int hp = 100;
		public int attack = 30;
		public int defense = 10;
		public int speed = 15;
		public int direcao = 1;
	}
}
