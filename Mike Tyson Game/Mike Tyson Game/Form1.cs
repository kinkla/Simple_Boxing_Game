namespace Mike_Tyson_Game
{
    public partial class Form1 : Form
    {
        bool playerBlock = false;
        bool enemyBlock = false;
        Random random = new Random();
        int enemySpeed = 5;
        int index = 0;
        int playerHealth = 100;
        int enemyHealth = 100;
        List<string> enemyAttack = new List<string> { "left", "right", "block" };

        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

        private void BoxerAttackTimerEvent(object sender, EventArgs e)
        {
            index = random.Next(0, enemyAttack.Count);

            switch (enemyAttack[index].ToString())
            {
                case "left":
                    Boxer.Image = Properties.Resources.enemy_punch1;
                    enemyBlock = false;

                    if (Boxer.Bounds.IntersectsWith(Player.Bounds) && playerBlock == false) 
                    {
                        playerHealth -= 5;
                    }

                    break;

                case "right":
                    Boxer.Image = Properties.Resources.enemy_punch2;
                    enemyBlock = false;

                    if (Boxer.Bounds.IntersectsWith(Player.Bounds) && playerBlock == false)
                    {
                        playerHealth -= 5;
                    }

                    break;

                case "block":
                    Boxer.Image = Properties.Resources.enemy_block;
                    enemyBlock = true;
                    break;
            }
        }

        private void BoxerMoveTimerEvent(object sender, EventArgs e)
        {
            // Set Up both healt bars
            if (playerHealth > 1)
            {
                PlayerHealthBar.Value = playerHealth;
            }
            if (enemyHealth > 1) 
            {
                BoxerHealthBar.Value = enemyHealth;
            }

            // Move the boxer
            Boxer.Left += enemySpeed;

            if(Boxer.Left > 430)
            {
                enemySpeed = -5;
            }
            if (Boxer.Left < 220)
            {
                enemySpeed = 5;
            }

            // Check for the end of game scenario

            if(enemyHealth < 1)
            {
                BoxerAttackTimer.Stop();
                BoxerMoveTimer.Stop();
                MessageBox.Show("You WIN!!! Click OK to play again", "MOO says");
                ResetGame();
            }
            if (playerHealth < 1)
            {
                BoxerAttackTimer.Stop();
                BoxerMoveTimer.Stop();
                MessageBox.Show("Tough Rob WINS!!! Click OK to play again", "MOO says");
                ResetGame();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Left)
            {
                Player.Image = Properties.Resources.boxer_left_punch;
                playerBlock = false;

                if (Player.Bounds.IntersectsWith(Boxer.Bounds) && enemyBlock == false)
                {
                    enemyHealth -= 5;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                Player.Image = Properties.Resources.boxer_right_punch;
                playerBlock = false;

                if (Player.Bounds.IntersectsWith(Boxer.Bounds) && enemyBlock == false)
                {
                    enemyHealth -= 5;
                }
            }
            if(e.KeyCode == Keys.Down)
            {
                Player.Image = Properties.Resources.boxer_block;
                playerBlock = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            Player.Image = Properties.Resources.boxer_stand;
            playerBlock = false;
        }

        private void ResetGame()
        {
            BoxerAttackTimer.Start();
            BoxerMoveTimer.Start();
            playerHealth = 100;
            enemyHealth = 100;

            Boxer.Left = 400;
        }
    }
}