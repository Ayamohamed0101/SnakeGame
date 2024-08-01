using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {//// Number of columns & rows in the game grid
        int cols = 50, //Game Grid (50 columns x 25 rows)

            rows = 25,
            score = 0,
            // Direction of the snake's movement along the x-axis & y-axis
            dx = 0,
            dy = 0,
            // Index for the front (head) & tail of the snake
            front = 0,
            back = 0;

        // Array to store the snake's body parts
        Piece[] snake = new Piece[1250];//size ==> hould be large enough to accommodate the maximum possible length of the snake within the grid.       
        // List to keep track of available positions for food
        List<int> available = new List<int>();
        // 2D array to track the visited cells on the grid
        bool[,] visit;

        Random rand = new Random();

        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            intial();
            launchTimer();
          
        }
        private void intial()
        {
            visit = new bool[rows, cols];// from position to another pos
            Piece head = new Piece((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);

            lblFood.Location = new Point((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);

            for (int i = 0; i < rows; i++)
            {


                for (int j = 0; j < cols; j++)
                {
                    visit[i, j] = false; // Mark all cells as unvisited
                    available.Add(i * cols + j); // Add all positions to the available list

                }//??
            }
            // Mark the initial position of the snake's head as visited
            visit[head.Location.Y / 20, head.Location.X / 20] = true;
            // Remove the initial position of the snake's head from the available list
            available.Remove(head.Location.Y / 20 * cols + head.Location.X / 20);//?
             // Add the snake's head to the form controls
            Controls.Add(head);
            // Set the snake's head as the initial position in the 'snake' array
            snake[front] = head;
        }
        private void launchTimer()
        {
            timer.Interval = 50;
            timer.Tick += move;
            timer.Start();
        }
        private void move(object sender, EventArgs e)
        {
            // Get the current head position of the snake
            int x = snake[front].Location.X;
            int  y = snake[front].Location.Y;

            if (dx == 0 && dy == 0) 
                return;
            if (game_over(x + dx, y + dy))
            {
                timer.Stop();
                MessageBox.Show("Game Over");
                return;
            }
            // check is necessary to determine the
            // new position of the snake's head after it moves
            if (collisionFood(x + dx, y + dy)) // The variables dx and dy represent the direction and
                                               // distance the snake will move on the x and y axes
                                        //When the snake moves, its head will be at a new position
                                        //calculated by adding dx to x and dy to y.
            {
                score += 1;
                lblScore.Text = "Score: " + score.ToString();
                if (hits((y + dy) / 20, (x + dx) / 20))
                    return;
                Piece head = new Piece(x + dx, y + dy);
                front = (front - 1 + 1250) % 1250;
                snake[front] = head;
                visit[head.Location.Y / 20, head.Location.X / 20] = true;
                Controls.Add(head);
                randomFood();
            }
            else
            {
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                visit[snake[back].Location.Y / 20, snake[back].Location.X / 20] = false;
                front = (front - 1 + 1250) % 1250;//////////////////
                snake[front] = snake[back];
                snake[front].Location = new Point(x + dx, y + dy);
                back = (back - 1 + 1250) % 1250;//////////////////////
                visit[(y + dy) / 20, (x + dx) / 20] = true;
            }
        }

        private void randomFood()
        {
            available.Clear();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (!visit[i, j]) available.Add(i * cols + j);
            int idx = rand.Next(available.Count) % available.Count;
            lblFood.Left = (available[idx] * 20) % Width;
            lblFood.Top = (available[idx] * 20) / Width * 20;
        }

        private bool hits(int x, int y)
        {
            if (visit[x, y])
            {
                timer.Stop();
                MessageBox.Show("Snake Hit his Body");
                return true;
            }
            return false;
        }

        private bool collisionFood(int x, int y)
        {
            // Compare the given coordinates with the food's current location
            return x == lblFood.Location.X && y == lblFood.Location.Y;
        }

        private bool game_over(int x, int y)
        {
            //The values 980 and 480 correspond to the width and height of the game area in pixels,
            //    considering each grid cell is 20x20 pixels and the grid has dimensions of 50 columns by 25 rows.
           
            // Check if the x-coordinate is less than 0 (out of the left boundary)
            // or greater than 980 (out of the right boundary)
            bool isOutOfXBounds = x < 0 || x > 980;//980,480

            // Check if the y-coordinate is less than 0 (out of the top boundary)
            // or greater than 480 (out of the bottom boundary)
            bool isOutOfYBounds = y < 0 || y > 480;

            // If either the x or y coordinates are out of bounds, return true (game over)
            return isOutOfXBounds || isOutOfYBounds;
        }
        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            dx = dy = 0;
            switch (e.KeyCode)
            {
                case Keys.Right:
                    dx = 20;
                    break;
                case Keys.Left:
                    dx = -20;
                    break;
                case Keys.Up:
                    dy = -20;
                    break;
                case Keys.Down:
                    dy = 20;
                    break;

//        Up
//        ↑
//        |
//        | (0, -20)
//        |
//        |
//Left ← (0, 0) → Right
//        |           (-20, 0)(20, 0)
//        |
//        |
//        |
//        ↓
//       Down
//            (0, 20)






            }
        }

        private void Snake_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
