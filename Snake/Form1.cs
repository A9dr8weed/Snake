using System;
using System.Drawing;
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
        private int width = 900;
        private int height = 790;
        private int sizeOfSides = 40;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            Text = "Snake";
            Width = width;
            Height = height;
            dirX = 1;
            dirY = 0;

            labelScore = new Label
            {
                Text = "Score: 0",
                Location = new Point(810, 10)
            };
            Controls.Add(labelScore);

            snake[0] = new PictureBox
            {
                Location = new Point(201, 201),
                Size = new Size(sizeOfSides - 1, sizeOfSides - 1),
                BackColor = Color.Red
            };
            Controls.Add(snake[0]);

            fruit = new PictureBox
            {
                BackColor = Color.Yellow,
                Size = new Size(sizeOfSides, sizeOfSides)
            };

            generateMap();
            generateFruit();
            timer.Tick += update;
            timer.Interval = 200;
            timer.Start();
            KeyDown += OKP;
        }

        private void generateFruit()
        {
            Random r = new Random();
            rI = r.Next(0, height - sizeOfSides);
            int tempI = rI % sizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, height - sizeOfSides);
            int tempJ = rJ % sizeOfSides;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            Controls.Add(fruit);
        }

        private void checkBorders()
        {
            if(snake[0].Location.X < 0)
            {
                for(int i = 1; i <= score; i++)
                {
                    Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = 1;
            }

            if (snake[0].Location.X > height)
            {
                for (int i = 1; i <= score; i++)
                {
                    Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = -1;
            }

            if (snake[0].Location.Y < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = 1;
            }

            if (snake[0].Location.Y > height)
            {
                for (int i = 1; i <= score; i++)
                {
                    Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = -1;
            }
        }

        private void eatItself()
        {
            for(int i = 1; i < score; i++)
            {
                if(snake[0].Location == snake[i].Location)
                {
                    for(int j = i; j <= score; j++)
                    {
                        Controls.Remove(snake[j]);
                    }
                    score -= (score - i + 1);
                    labelScore.Text = "Score: " + score;
                }
            }
        }

        private void eatFruit()
        {
            if(snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;

                snake[score] = new PictureBox
                {
                    Location = new Point(snake[score - 1].Location.X + (40 * dirX), snake[score - 1].Location.Y - (40 * dirY)),
                    Size = new Size(sizeOfSides - 1, sizeOfSides - 1),
                    BackColor = Color.Red
                };
                Controls.Add(snake[score]);

                generateFruit();
            }
        }

        private void generateMap()
        {
            for(int i = 0; i < width / sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox
                {
                    BackColor = Color.Black,
                    Location = new Point(0, sizeOfSides * i),
                    Size = new Size(width - 100, 1)
                };
                Controls.Add(pic);
            }

            for (int i = 0; i <= height / sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox
                {
                    BackColor = Color.Black,
                    Location = new Point(sizeOfSides * i, 0),
                    Size = new Size(1, width)
                };
                Controls.Add(pic);
            }
        }

        private void moveSnake()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + (dirX * sizeOfSides), snake[0].Location.Y + (dirY * sizeOfSides));
            eatItself();
        }

        private void update(object myObject, EventArgs eventArgs)
        {
            checkBorders();
            eatFruit();
            moveSnake();
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