using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace sonic_final
{
	/// <summary>
	/// Lógica e classe da principal arma do inimigo.
	/// </summary>
	public class Fireball : Character
	{
		public Timer fireTimer = new Timer();
		public Hero heroi;
		
		private MainForm mainForm;
		
		public Fireball(MainForm mainForm, Hero heroi)
		{
			this.mainForm = mainForm;
			this.heroi = heroi;
			
			Parent = MainForm.fundo;
			Height = 70;
			Width = 70;
			Top = 120;
			Left = 0;
			Load("fireball.gif");
			fireTimer.Tick += fireTimer_Tick;
			fireTimer.Interval = 30;  // 30 milissegundos.
			speed = 15;
			direcao = 1;
			SendToBack();
		}
		
		private void Atingido()
		{
		    // Carrega o GIF de explosão
		    heroi.Load("explosion.gif");
		    heroi.isExploding = true;  // Define isExploding como true quando a animação da explosão começa
		
		    // Cria um Timer
		    Timer timer = new Timer();
		    timer.Interval = 1500; // Define o intervalo para 1.5 segundos
		
		    // Define o evento Tick para alterar a imagem de volta ao normal após 1.5 segundos
		    timer.Tick += (s, e) =>
		    {
		        // Carrega a imagem normal
		        heroi.Load("heroi.gif");
		        heroi.isExploding = false;  // Define isExploding como false quando a animação da explosão termina
		
		        // Para o Timer
		        timer.Stop();
		        timer.Dispose();
		    };
		
		    // Inicia o Timer
		    timer.Start();
		}

	
		void fireTimer_Tick(object sender, EventArgs e)
		{			
			Left -= direcao * speed;
			
			if(Left >= MainForm.fundo.Width)
			{
				fireTimer.Enabled = false;
				Dispose();
			}
			
			if(Left <= 0)
			{
				fireTimer.Enabled = false;
				Dispose();
			}
			
			if(heroi != null && heroi.Bounds.IntersectsWith(Bounds))
			{			
				int noLife = 3;
				
				if (heroi.timesStruck == noLife)
				{
					Atingido();
					fireTimer.Enabled = false; // Para o timer da bola de fogo
					ShowGameOverMessage(); // Mostra a caixa de mensagem assíncrona
				}
				
				heroi.timesStruck++; // Incrementa o número de vezes que o herói foi atingido
				Dispose();
			   	System.Diagnostics.Debug.WriteLine("Vezes atingido: " + heroi.timesStruck);
			   	
			   	// Usa um for reverso para percorrer a lista de bolas de fogo e descartá-las
			   	for (int i = MainForm.lista.Items.Count - 1; i >= 0; i--)
			   	{
			   		Fireball fire = (Fireball)MainForm.lista.Items[i];
			   		fire.Dispose();
			   		fire.fireTimer.Enabled = false;
			   		MainForm.lista.Items.RemoveAt(i);
			   	}
			}
		}
		
		// Método assíncrono para mostrar a caixa de mensagem com botões "Sim" e "Não"
		private async void ShowGameOverMessage()
		{
		    // Desabilita o fundo do jogo
		    MainForm.fundo.Visible = false;
		    
		    // Remove o herói, inimigo e para a música e carrega a imagem de corações vazios.
		    MainForm.fundo.Controls.Remove(heroi);
		    MainForm.fundo.Controls.Remove(mainForm.inimigo);
		    MainForm.fundo.Controls.Remove(heroi);
		    mainForm.game.sound.Stop();
		    mainForm.game.life.Load("hearts4.png");
		    
		    // Aguarda 1,5 segundos
		    await Task.Delay(1500);
		
		    // Mostra a caixa de mensagem de forma assíncrona
		    DialogResult result = await Task.Run(() => MessageBox.Show("Deseja reiniciar o jogo?", "Game Over!", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
		    
		    if (result == DialogResult.Yes)
		    {
		        // Se apertar no "sim" irá reiniciar o jogo.
		    	Application.Restart();
		    }
		    else if (result == DialogResult.No)
		    {
		        // Se apertar "não" vai fechar o jogo. 
		    	Application.Exit();
		    }
		}
	}
}
