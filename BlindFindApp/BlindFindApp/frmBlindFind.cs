using Microsoft.VisualBasic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.PerformanceData;
using System.Diagnostics.Tracing;

namespace BlindFindApp
{
    public partial class frmBlindFind : Form
    {
        int count;

        Button btnPlayAgain = new();
        List<Button> lstbuttons;

        public frmBlindFind()
        {
            InitializeComponent();

            lstbuttons = new()
            {
                btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10,
                btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19, btn20,
                btn21, btn22, btn23, btn24, btn25, btn26, btn27, btn28, btn29, btn30,
                btn31, btn32, btn33, btn34, btn35, btn36, btn37, btn38, btn39, btn40,
                btn41, btn42, btn43, btn44, btn45, btn46, btn47, btn48, btn49
            };

            lstbuttons.ForEach(b => b.Click += SelectButton_Click);
            btnEnter.Click += BtnEnter_Click;
            btnIntro.Click += BtnIntro_Click;
            btnInstructions.Click += BtnInstructions_Click;
            btnGameStage.Click += BtnGameStage_Click;
        }


        private void GameIntro()
        {
            DateTime starttime = DateTime.Now;
            while ((DateTime.Now - starttime).TotalSeconds <= 2)
            {
                btnIntro.Text = "Get Ready!";
                Application.DoEvents();
            }
            btnIntro.Visible = false;
        }

        private void ChangeToRandomColor()
        {
            lstbuttons.ForEach(b => { b.BackColor = Color.White; b.ForeColor = Color.Black; b.FlatStyle = FlatStyle.Standard; b.Text = ""; });
            Random r = new();
            DateTime starttime = DateTime.Now;
            lstbuttons.OrderBy(x => r.Next()).Take(5).ToList().ForEach(b => { b.BackColor = Color.Black; b.ForeColor = Color.LimeGreen; });
            for (int i = 1; i < 45; i++)
            {
                Color c = Color.FromArgb(r.Next(1, 255), r.Next(1, 255), r.Next(1, 255));
                lstbuttons.Where(b => b.BackColor == Color.White).ToList().OrderBy(x => r.Next()).Take(1).ToList().ForEach(b => b.BackColor = c);
            }
            while ((DateTime.Now - starttime).TotalSeconds <= 5)
            {
                btnInstructions.Enabled = false;
                btnEnter.Enabled = false;
                lstbuttons.ForEach(b => b.Enabled = false);
                Application.DoEvents();
            }
            btnInstructions.Enabled = true;
            btnEnter.Enabled = true;
            lstbuttons.ForEach(b => { b.Enabled = true; b.BackColor = Color.White; });
        }

        private void UserSelects(Button btn)
        {
            if (lstbuttons.Count(b => b.BackColor == Color.Black) < 5)
            {
                btn.BackColor = btn.BackColor == Color.White ? Color.Black : Color.White;
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }

        private void DoRound()
        {
            switch (btnGameStage.Text)
            {
                case "Play Again":
                    btnIntro.Visible = true;
                    btnIntro.Text = "Play Again!";
                    break;
                default:
                    btnGameStage.Text = "Round " + count;
                    ChangeToRandomColor();
                    break;
            }
        }

        private void DisplayFinalScore()
        {
            int i = int.Parse(lblScoreNumber.Text);
            string msg = "";

            btnIntro.Visible = true;

            if (i == 50)
            {
                msg = "You did it! Perfect score!!";
            }
            else if (i < 50 && i >= 40)
            {
                msg = "Well done! Almost there...";
            }
            else if (i < 40 && i >= 25)
            {
                msg = "Not too bad, keep trying...";
            }
            else if (i < 25)
            {
                msg = "Come on! You can do better...";
            }
            btnIntro.Text = "Final Score: " + Environment.NewLine + lblScoreNumber.Text + " / 50" + Environment.NewLine + Environment.NewLine + msg;
        }

        private void DoTurn()
        {
            if (count < 11)
            {
                DoRound();
            }
            else
            {
                DisplayFinalScore();

                btnPlayAgain.Visible = true;
                btnPlayAgain.AutoSize = true;
                btnPlayAgain.Anchor = AnchorStyles.None;
                btnPlayAgain.Location = new Point(800, 700);
                btnPlayAgain.Height = 100;
                btnPlayAgain.Width = 300;
                btnPlayAgain.BackColor = Color.White;
                btnPlayAgain.ForeColor = Color.Black;
                btnPlayAgain.Text = "Play Again?";
                btnIntro.Controls.Add(btnPlayAgain);
                btnPlayAgain.Click += BtnPlayAgain_Click;
            }
        }

        private void BtnPlayAgain_Click(object? sender, EventArgs e)
        {


            btnIntro.Visible = false;
            btnGameStage.Text = "Start";
            lstbuttons.ForEach(b => { b.BackColor = Color.White; b.ForeColor = Color.Black; b.FlatStyle = FlatStyle.Standard; b.Text = ""; });
            lblScoreNumber.Text = "0";
        }

        private void BtnEnter_Click(object? sender, EventArgs e)
        {
            if (lstbuttons.Where(b => b.BackColor == Color.Black).ToList().Count() == 5 && lstbuttons.Where(b => b.BackColor != Color.Black).ToList().Where(b => b.BackColor == Color.White).ToList().Count() == 44)
            {
                List<Button> newlist = new();
                lstbuttons.Where(b => b.ForeColor == Color.LimeGreen).ToList().ForEach(b => { b.FlatAppearance.BorderColor = Color.LimeGreen; b.FlatAppearance.BorderSize = 10; b.FlatStyle = FlatStyle.Flat; });
                newlist = lstbuttons.Where(b => b.BackColor == Color.Black).ToList();
                newlist.Where(b => b.ForeColor == Color.LimeGreen).ToList().ForEach(b => b.Text = "\u2713");
                lblScoreNumber.Text = (int.Parse(lblScoreNumber.Text) + newlist.Where(b => b.ForeColor == Color.LimeGreen).ToList().Count()).ToString();
                btnGameStage.Text = btnGameStage.Text != "Round 10" ? "Next Round" : "Game Over";
            }
        }

        private void SelectButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                UserSelects((Button)sender);
            }
        }

        private string GetInstructions()
        {
            string instructions;

            instructions =
                           "How to Play: " + Environment.NewLine + Environment.NewLine +
                           "1)  The screen will display a grid of 49 white buttons. Press 'Start'." + Environment.NewLine + Environment.NewLine +
                           "2)  5 of the buttons in the grid will turn black. The rest of the buttons will each adopt a new random color. The buttons will remain like this for 5 seconds before returning to white." + Environment.NewLine + Environment.NewLine +
                           "3)  Press the 5 buttons which you believe have previously turned black (to unselect - click again). Press 'Enter'." + Environment.NewLine + Environment.NewLine +
                           "4)  The correct selections will be marked and the scoreboard will be updated accordingly (one point for each correct selection). Press 'Next Round'." + Environment.NewLine + Environment.NewLine +
                           "5)  Repeat steps 1-4 for the next 9 rounds." + Environment.NewLine + Environment.NewLine +
                           "6)  See final score. Then play again to beat your record!" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
                           "HAVE FUN!";

            return instructions;
        }

        private void BtnInstructions_Click(object? sender, EventArgs e)
        {
            btnIntro.Visible = true;
            btnPlayAgain.Visible = false;

            Label lblinstructions = new()
            {
                Dock = DockStyle.Fill,
                Text = GetInstructions()
            };
            btnIntro.Controls.Add(lblinstructions);

            Button btnstartgame = new()
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Dock = DockStyle.None,
                Location = new Point(1300, 600),
                Height = 200,
                Width = 500,
                Text = "Start Game"
            };
            lblinstructions.Controls.Add(btnstartgame);

            btnstartgame.Click += Btnstartgame_Click;
        }

        private void Btnstartgame_Click(object? sender, EventArgs e)
        {
            btnIntro.Controls.Clear();
            btnIntro.Visible = false;
        }

        private void BtnIntro_Click(object? sender, EventArgs e)
        {
            GameIntro();
        }

        private void BtnGameStage_Click(object? sender, EventArgs e)
        {
            if (count < 11)
            {
                if (btnGameStage.Text == "Start" || btnGameStage.Text == "Next Round" || btnGameStage.Text == "Game Over")
                {
                    ++count;
                    DoTurn();
                }
            }
            else
            {
                count = 0;
                count++;
                DoTurn();
            }
        }
    }
}