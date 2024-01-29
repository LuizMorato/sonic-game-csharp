using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sonic_final
{
	/// <summary>
	/// Essa é a lógica e classe da principal arma do inimigo.
	/// </summary>
	public class HeroMissile : Character
	{
	    public Timer moveTimer = new Timer();
	    private MainForm mainForm;
		
		public HeroMissile(MainForm mainForm, Hero heroi)
	    {
	        // A direção do míssil será a mesma do herói quando o míssil for disparado
	        this.mainForm = mainForm;
	        this.direcao = heroi.direcao;
	        
	        if (this.direcao == 1)
	        {
	            Load("heroimissil.gif");
	        }
	        else
	        {
	            Load("heroimissil_invertido.gif");
	        }
	        
	        // Configura o Timer
		    moveTimer.Interval = 30; // Move o míssil a cada 30 milissegundos
		    moveTimer.Tick += MoveTimer_Tick;
		    moveTimer.Start();
	    }
		
		// Método assíncrono para mostrar a caixa de mensagem com botões "Sim" e "Não"
		private async void ShowGameWinMessage()
		{
		    // Desabilita o fundo do jogo
		    MainForm.fundo.Visible = false;
		    
		    // Altera a imagem do inimigo para a imagem da explosão
    		mainForm.inimigo.Load("explosion.gif");
		    
		    // Remove o herói, inimigo e para a música e carrega a imagem de corações vazios.
		    MainForm.fundo.Controls.Remove(mainForm.inimigo);
		    
		    // Chama o método EndGame na instância mainForm
		    mainForm.EndGame();
		    mainForm.game.sound.Stop();
		    
		    // Para o timer da bola de fogo
    		mainForm.inimigo.timerFireGen.Stop();
		    
		    // Aguarda 1,5 segundos
		    await Task.Delay(1500);
		
		    // Mostra a caixa de mensagem de forma assíncrona
		    DialogResult result = await Task.Run(() => MessageBox.Show("Deseja reiniciar o jogo?", "Você ganhou!", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
		    
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
		
		private void MoveTimer_Tick(object sender, EventArgs e)
		{
		    // Move o míssil
		    Left += direcao * speed;
		    
		    if (Left < 0 || Left > MainForm.fundo.Width)
		    {
		    	Hero.missileOnScreen = false;
		    }
		
		    // Verifica se o míssil atingiu o inimigo
		    if (mainForm.inimigo != null && mainForm.inimigo.Bounds.IntersectsWith(Bounds))
		    {    	
		    	// Reduz a vida do inimigo em 20 pontos
        		mainForm.inimigo.healthBar.Value -= 20;
        		
        		// Incrementa a pontuação
        		mainForm.pontos++;
        		System.Diagnostics.Debug.WriteLine("Pontos: " + mainForm.pontos);
        		
        		// Atualiza a pontuação na MainForm
        		mainForm.UpdateScore(mainForm.pontos);
        		
        		// Remove o míssil
   			 	this.Dispose();
   			 	Hero.missileOnScreen = false;
   			 	
        		if(mainForm.inimigo.healthBar.Value == 0)
        		{
			    	// Define gameWon como true e mostra a caixa de mensagem de vitória
			        mainForm.gameWon = true;
			        ShowGameWinMessage();
        		}
		
		        // Para o Timer
		        moveTimer.Stop();
		        moveTimer.Dispose();
		    }
		}
	}
}
