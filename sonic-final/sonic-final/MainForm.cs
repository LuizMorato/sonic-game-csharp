using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace sonic_final
{
	/// <summary>
	/// Aqui há a lógica do jogo e as demais classes criadas.
	/// </summary>
	public partial class MainForm : Form
	{
		public Game game;
		Button iniciar = new Button();
		Label pontuacao = new Label();
	    static public PictureBox fundo = new PictureBox();
	    public Hero heroi;
	    public Enemy inimigo;
	    static public ListBox lista = new ListBox();
	    
	    public int pontos = 0;
	    
	    public bool gameWon = false;
		
		public MainForm()
		{
			InitializeComponent();
			
			game = new Game();
			heroi = new Hero(this);
			inimigo = new Enemy(this, heroi);
			fundo.Parent = this;
			
			// Define o ícone do Form.
			Icon = new Icon("sonic.ico");
			BackColor = Color.Blue;
			
			// Define a posição inicial do formulário como centralizada
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
           
            // Desativa o botão de maximizar.
            MaximizeBox = false;
            
            // Define o tamanho da MainForm
    		Size = new Size(750, 425);
    		
    		this.Load += MainForm_Load;
    		this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
		}
		
		void MainForm_Load(object sender, EventArgs e)
		{		   
		    // Botão iniciar
		    iniciar.Parent = game.menu_inicial;
		    iniciar.Text = "Iniciar";
		    iniciar.Font = new Font("Comic Sans MS", 16f, FontStyle.Bold);
		    iniciar.AutoSize = true;
		    iniciar.ForeColor = Color.White;
		    iniciar.Size = new Size(100, 50);
		    iniciar.Top = 300;
		    iniciar.Left = 325;
		    iniciar.FlatStyle = FlatStyle.Flat;
			iniciar.FlatAppearance.BorderSize = 1;
			iniciar.FlatAppearance.BorderColor = Color.White;
		    iniciar.Click += Iniciar_Click;
		    
		    //Label pontuação do jogador 
			pontuacao.Parent = this;
			pontuacao.Top = fundo.Bottom + 280;
			pontuacao.Left = 550;
			pontuacao.Font = new Font("Arial", 16f, FontStyle.Bold);
			pontuacao.AutoSize = true;
			
			// Adicione o menu inicial à MainForm
		    Controls.Add(game.GameStyle());
		    
		    KeyPreview = true;
		}
		
		void MainForm_KeyDown(object sender, KeyEventArgs e)
		{ 
			heroi.Commands(e.KeyCode);
			game.ChangeBackground(heroi, fundo);
			heroi.KeepHero();
		}
		
		void Iniciar_Click(object sender, EventArgs e)
		{
			// Carrega os cenários
			game.SetupBackground(fundo, this, heroi, inimigo);
			
			// Inicia o timer do inimigo
			inimigo.Start();
		
		    // Remove o menu inicial
		    game.menu_inicial.Visible = false;
		    
		    // Fica "loopando" a música.
		    game.sound.PlayLooping();
		}
		
		public void UpdateScore(int score)
		{
		    pontuacao.Text = "Pontuação: " + score;
		}
		
		public void EndGame()
		{
		    // Remove o herói
		    fundo.Controls.Remove(heroi);
		}	
	}
}
