using System;
using System.Drawing;
using System.Windows.Forms;

namespace sonic_final
{
	/// <summary>
	/// Classe do Personagem Principal e suas funcionalidades.
	/// </summary>
	public class Hero : Character
	{
		public bool isExploding = false;
		public static bool missileOnScreen = false;
		public int timesStruck = 1;
		
		private MainForm mainForm;
		
		public Hero(MainForm mainForm)
		{
			this.mainForm = mainForm;
			
			Height = 150;
			Width = 150;
			Top = 120;
			Left = 50;
			Load("heroi.gif");
		}
		
		public void KeepHero() 
		{			
			int minY = -15; // Limite mínimo para cima
		    int maxY = 220; // Limite máximo para baixo
		
		    // Verifica se o herói está saindo da tela para cima
		    if (Top < minY)
		    {
		        Top = minY;
		    }
		
		    // Verifica se o herói está saindo da tela para baixo
		    if (Top > maxY)
		    {
		        Top = maxY;
		    }
		}
		
		public void Commands(Keys keyCode)
		{
	    // Aqui TEM o código dos comandos com base na tecla pressionada (keyCode).
	    // Por exemplo, vai verificar a tecla e executar a ação correspondente.
	    
	    	if (isExploding) return;
	
		    switch (keyCode)
		    {
		        case Keys.A:
		        case Keys.Left:
		            // Mover para a esquerda
		           Left -= speed;
		            Load("heroi_invertido.gif");
		            direcao = -1;
		            break;
		        
		        case Keys.D:
		        case Keys.Right:
		            // Mover para a direita
		            Left += speed;
		            Load("heroi.gif");
		            direcao = 1;
		            break;
		        
		        case Keys.W:
		        case Keys.Up:
		            // Mover para cima
		            Top -= speed;
		            break;
		        
		        case Keys.S:
		        case Keys.Down:
		            // Mover para baixo
		            Top += speed;
		            break;	               
		        
		       	case Keys.F:
		            // Atirar
		            Fire();
	            	break;
	            	
	            case Keys.Y:
	            	System.Diagnostics.Debug.WriteLine("heroi BOUNDS: " + Bounds);
	            	break;
		            
		       	default:
		           break;
		    }
		}
		
		public void Fire()
		{
		    // Verifica se já há um míssil na tela
		    if (missileOnScreen) return;
		
		    // Cria um novo míssil do herói e o adiciona à tela
		    HeroMissile missile = new HeroMissile(mainForm, this);
		    missile.Parent = MainForm.fundo;  // Adiciona o míssil ao fundo
		    missile.Location = new Point(this.Left + this.Width / 2, this.Top + 50);  // Inicia o míssil na posição do herói
		
		    missileOnScreen = true;
		}
	}
}
