using System;
using System.Drawing;
using System.Windows.Forms;

namespace sonic_final
{
	/// <summary>
	/// Essa é a classe do principal inimigo do Jogo.
	/// </summary>
	public class Enemy : Character
	{
		static public Timer myTimer = new Timer();
    	static public Timer myTimer2 = new Timer();
    	
    	public Timer timerFireGen = new Timer();
    	public ProgressBar healthBar = new ProgressBar();
    	public Label vidaInimigo = new Label();
    	
    	public Hero heroi;
    	private MainForm mainForm;
		
		public Enemy(MainForm mainForm, Hero heroi)
		{
			this.mainForm = mainForm;
			this.heroi = heroi;
			
			Top = 110;
			Left = 600;
			Load("inimigo.gif");
			Size = new Size(100, 120);
			
			healthBar.Maximum = 100; // A vida máxima do inimigo
			healthBar.Value = 100; // A vida atual do inimigo
			healthBar.Size = new Size(100, 20); // O tamanho da barra de progresso
			healthBar.Left = 620; // A posição da barra de progresso na tela
			healthBar.Top = 20; // A posição da barra de progresso na tela
			MainForm.fundo.Controls.Add(healthBar); // Adicione a "healthbar" no cenário.
			
			vidaInimigo.Text = "Vida do Inimigo:";
			vidaInimigo.AutoSize = true;
			vidaInimigo.Left = 610;
			vidaInimigo.Top = healthBar.Top - 20;
			vidaInimigo.ForeColor = Color.Black;
			vidaInimigo.Font = new Font("Comic Sans MS", 10f, FontStyle.Bold);
			MainForm.fundo.Controls.Add(vidaInimigo);
			
			myTimer.Interval = 50;
	        myTimer.Tick += myTimerTick;
	        myTimer.Enabled = false;
	
	        myTimer2.Interval = 50;
	        myTimer2.Tick += myTimer2Tick;
	        myTimer2.Enabled = false;
	        
	        timerFireGen.Tick += timerFireGen_Tick;
	        timerFireGen.Interval = 2000;  // Gera uma bola de fogo a cada 2 segundos.
	        timerFireGen.Enabled = false;
		}
		
		void timerFireGen_Tick(object sender, EventArgs e)
		{
			// Se o jogo terminou, não crie uma nova bola de fogo
		    if (mainForm.gameWon || heroi.isExploding)
		    {
		    	timerFireGen.Stop();
		    	return;
		    }
			
			Fireball fireball = new Fireball(mainForm, heroi);
			fireball.Parent = MainForm.fundo;
			fireball.fireTimer.Enabled = true;
			fireball.Left = Left;
			fireball.Top = Top + (Height/2);
			fireball.direcao = direcao;
			
			fireball.heroi = heroi;
			
			MainForm.lista.Items.Add(fireball);
		}
		
		public void myTimerTick(object sender, EventArgs e)
		{
		    Top += 5;
		
		    if (Top >= 200)
		    {
		    	myTimer.Enabled = false;
		    	myTimer2.Enabled = true;
		    	Top -= 5;
		    }
		}
		
		public void myTimer2Tick(object sender, EventArgs e)
		{	
			Top -= 5;
		
		    if (Top == 5)
		    {
		    	myTimer2.Enabled = false;
		    	myTimer.Enabled = true;
		    	Top -= 5;
		    }
		}
		
		public void Start()
		{
			myTimer.Enabled = true;
			timerFireGen.Enabled = true;
		}
	}
}
