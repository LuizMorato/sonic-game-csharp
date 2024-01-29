using System;
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace sonic_final
{
	/// <summary>
	/// Essa classe contém o design do menu e a lógica dos cenários.
	/// </summary>
	public class Game
	{
		public int FundoAtual { get; set; } 
		
		public Button iniciar = new Button();
	    public Panel menu_inicial = new Panel();
	    public PictureBox sonic = new PictureBox();
	    public PictureBox tails = new PictureBox();
	    public PictureBox life = new PictureBox();
	    public Label rules = new Label();
	    public Label nome_jogo = new Label();
	    
	    public SoundPlayer sound = new SoundPlayer("videoplayback.wav");
			
		public Game()
		{
			FundoAtual = 1;
		}
		
		public Panel GameStyle()
		{
		   	menu_inicial.BackColor = Color.Blue;
			menu_inicial.Size = new Size(750,425);
			menu_inicial.BringToFront();
			
			// Nome do jogo
			nome_jogo.Parent = menu_inicial;
			nome_jogo.BackColor = Color.Blue;
			nome_jogo.Text = "Jogo do Sonic";
			nome_jogo.Size = new Size(250,50);
			nome_jogo.Top = 150;
			nome_jogo.Left = 280;
			nome_jogo.Font = new Font("Comic Sans MS", 24f, FontStyle.Bold);
			nome_jogo.ForeColor = Color.White;
			
			// imagem do sonic
			sonic.Parent = menu_inicial;
			sonic.Size = new Size(200,200);
			sonic.Load("sonic.png");
			sonic.SizeMode = PictureBoxSizeMode.StretchImage;
			sonic.Top = 100;
			sonic.Left = 50;
			
			// regras
			rules.Parent = menu_inicial;
			rules.AutoSize = true;
			rules.Top = 200;
			rules.Left = 295;
			rules.Text = "Atire no inimigo e pontue!";
			rules.Font = new Font("Comic Sans MS", 12f, FontStyle.Bold);
			
			// imagem do Tails
			tails.Parent = menu_inicial;
			tails.Size = new Size(200,200);
			tails.Load("tails.png");
			tails.SizeMode = PictureBoxSizeMode.StretchImage;
			tails.Top = 100;
			tails.Left = 550;
		
		    return menu_inicial;
		}
		
		public void SetupBackground(PictureBox fundo, Form form, Hero heroi, Enemy inimigo)
	    {
		    fundo.Parent = form;
		    fundo.Height = form.Height - 120;
		    fundo.Width = form.Width;
		    fundo.Load("fundo1.gif");
		    fundo.SizeMode = PictureBoxSizeMode.StretchImage;
		    
		    life.Height = 30;
		    life.Width = 100;
		    life.Top = 10;
		    life.Left = 10;
		    life.Load("hearts1.png");
		    life.BackColor = Color.Transparent;
		    life.SizeMode = PictureBoxSizeMode.StretchImage;
		     
		    // Adiciona os personagens ao fundo
    		fundo.Controls.Add(heroi);
    		fundo.Controls.Add(inimigo);
    		fundo.Controls.Add(life);
	    }
		
		public void ChangeBackground(Hero heroi, PictureBox fundo)
		{
			string nomeImagem = "hearts" + heroi.timesStruck + ".png";
			Image imagem = Image.FromFile(nomeImagem);
			life.Image = imagem;
			life.Update();
			
			int posX = heroi.Bounds.X;
			
		    if (posX > 710)
		    {
		        FundoAtual++;
		        if (FundoAtual > 5) // Volta para o primeiro fundo se passar do quinto
		        {
		            FundoAtual = 1;
		        }  
		        string nomeFundo = "fundo" + FundoAtual + ".gif";
		        fundo.Load(nomeFundo);
		            
		        heroi.Left = 10;
		    }
		    
		    if (posX < -130)
		    {   
		        FundoAtual--;
		        
		        if (FundoAtual < 1)
		        {
		            FundoAtual = 5;
		        }
		        
		        string nomeFundo = "fundo" + FundoAtual + ".gif";
		        fundo.Load(nomeFundo);
		        
		        heroi.Left = 700;
		    }
		    
		    heroi.Parent = fundo;
		}
	}
}
