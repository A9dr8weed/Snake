using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelScore;
        private int dirX, dirY;
        private int width_ = 900;
        private int height_ = 790;
        private int sizeOfSides_ = 40;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Snake";
            this.Width = width_;
            this.Height = height_;
            dirX = 1;
            dirY = 0;
            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(sizeOfSides_ - 1, sizeOfSides_ - 1);
            snake[0].BackColor = Color.Red;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(sizeOfSides_, sizeOfSides_);
            generateMap_();
            generateFruit_();
            timer.Tick += new EventHandler(update_);
            timer.Interval = 200;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }

        private void generateFruit_()
        {
            Random r = new Random();
            rI = r.Next(0, height_ - sizeOfSides_);
            int tempI = rI % sizeOfSides_;
            rI -= tempI;
            rJ = r.Next(0, height_ - sizeOfSides_);
            int tempJ = rJ % sizeOfSides_;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }

        private void checkBorders_()
        {
            if(snake[0].Location.X < 0)
            {
                for(int i_ = 1; i_ <= score; i_++)
                {
                    this.Controls.Remove(snake[i_]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = 1;
            }

            if (snake[0].Location.X > height_)
            {
                for (int i_ = 1; i_ <= score; i_++)
                {
                    this.Controls.Remove(snake[i_]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = -1;
            }

            if (snake[0].Location.Y < 0)
            {
                for (int i_ = 1; i_ <= score; i_++)
                {
                    this.Controls.Remove(snake[i_]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = 1;
            }

            if (snake[0].Location.Y > height_)
            {
                for (int i_ = 1; i_ <= score; i_++)
                {
                    this.Controls.Remove(snake[i_]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = -1;
            }
        }

        private void eatItself()
        {
            for(int i_ = 1; i_ < score; i_++)
            {
                if(snake[0].Location == snake[i_].Location)
                {
                    for(int j_ = i_; j_ <= score; j_++)
                    {
                        this.Controls.Remove(snake[j_]);
                    }
                    score = score - (score - i_ + 1);
                    labelScore.Text = "Score: " + score;
                }
            }
        }

        private void eatFruit_()
        {
            if(snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * dirX, snake[score - 1].Location.Y - 40 * dirY);
                snake[score].Size = new Size(sizeOfSides_ - 1, sizeOfSides_ - 1);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                generateFruit_();
            }
        }

        private void generateMap_()
        {
            for(int i = 0; i < width_ / sizeOfSides_; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, sizeOfSides_ * i);
                pic.Size = new Size(width_ - 100, 1);
                this.Controls.Add(pic);
            }

            for (int i = 0; i <= height_ / sizeOfSides_; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(sizeOfSides_ * i, 0);
                pic.Size = new Size(1, width_);
                this.Controls.Add(pic);
            }
        }

        private void moveSnake_()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (sizeOfSides_), snake[0].Location.Y + dirY * (sizeOfSides_));
            eatItself();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void update_(object myObject, EventArgs eventArgs)
        {
            checkBorders_();
            eatFruit_();
            moveSnake_();
        }

        private void OKP(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode.ToString())
            {              
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;  
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }
    }
}