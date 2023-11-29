namespace BlindFindApp
{
    public partial class frmBlindFind : Form
    {
        List<Button> lstbuttons;
        List<Button> lstblackbuttons;

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
            btnGameStage.Click += BtnGameStage_Click;
        }

        private void GameIntro()
        {
            DateTime starttime = DateTime.Now;
            if (btnIntro.Text == "Click Anywhere to Begin")
            {
                btnIntro.Text = "Instructions";
                while ((DateTime.Now - starttime).TotalSeconds <= 5)
                {
                    Application.DoEvents();
                }
                btnIntro.Visible = false;

            }
        }

        private void ChangeToRandomColor()
        {


            lstbuttons.ForEach(b => b.BackColor = Color.White);
            Random r = new();
            DateTime starttime = DateTime.Now;
            lstbuttons.OrderBy(x => r.Next()).Take(5).ToList().ForEach(b => b.BackColor = Color.Black);
            for (int i = 1; i < 45; i++)
            {
                Color c = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                lstbuttons.Where(b => b.BackColor == Color.White).ToList().OrderBy(x => r.Next()).Take(1).ToList().ForEach(b => b.BackColor = c);
            }
            while ((DateTime.Now - starttime).TotalSeconds <= 5)
            {
                Application.DoEvents();
            }
            lstbuttons.ForEach(b => b.BackColor = Color.White);

            lstblackbuttons = new();
            lstblackbuttons.AddRange(lstbuttons.Where(b => b.BackColor == Color.Black));

            //List<Button> CaptureBlackButtons()
            //{
            //    List<Button> lstblack = new();
            //    lstblack = lstbuttons.Where(b => b.BackColor == Color.Black).ToList();
            //    return lstblack;
            //}
            //CaptureBlackButtons();
            //Create new list to capture buttons which turn black
            //Func<List<Button>, List<Button>> CaptureBlackButtons = lstblack => lstblack = lstbuttons.Where(b => b.BackColor == Color.Black).ToList();
            //List<Button> lstblack = new();
            //lstblack = lstbuttons.Where(b => b.BackColor == Color.Black).ToList();
            //return lstblack;
        }

     
        //private void CaptureBlackButtons(List<Button> lst)
        //{
        //    //List<Button> lst = lstblackbuttons.ToList();
        //    //lst = lstbuttons.Where(b => b.BackColor == Color.Black).ToList();

        //    //var newlst = new List<Button>(lstblackbuttons);
        //    //if (lstbuttons.Where(b => b.BackColor == Color.Black).ToList() != lst.Where(b => b.BackColor == Color.Black))
        //    //{
        //    //    lst.ForEach(b => b.BackColor = Color.Red);

        //    //}
        //}
        private void UserSelects(Button btn)
        {
            if (lstbuttons.Count(b => b.BackColor == Color.Black) < 5)
            {
                if (btn.BackColor == Color.White)
                {
                    btn.BackColor = Color.Black;
                }
                else
                {
                    btn.BackColor = Color.White;
                }
            }
            else
            {
                btn.BackColor = Color.White;
            }
            //let user pick only five buttons - disable other controls
            //allow for change of mind - maybe create enter button so user can decide when to enter response

        }

        private void DoRound()
        {
            ChangeToRandomColor();
            //if (btnGameStage.Text == "Start")
            //{
            // btnGameStage.Text = "Next Round";

            //}
        }

        private void DoTurn()
        {
            //DoRound x 10
        }



        private void BtnEnter_Click(object? sender, EventArgs e)
        {
            List<Button> newlist = new();
            newlist = lstbuttons.Where(b => b.BackColor == Color.Black).ToList();
            if (lstblackbuttons != newlist)
            {
                lstbuttons.ForEach(b => b.BackColor = Color.Red);
            }
            //lstbuttons.ForEach(b => b.Enabled = false);
            //DetectWinningButtons()
            //List<Button> lstblack = ;
            //lstblack.ForEach(b => b.BackColor = Color.Black);

            //maybe chained ienumerable - check if black buttons in lstbuttons are same as buttons in lstblack
        }

        private void SelectButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                UserSelects((Button)sender);
            }
        }

        private void BtnIntro_Click(object? sender, EventArgs e)
        {
            GameIntro();
        }

        private void BtnGameStage_Click(object? sender, EventArgs e)
        {
            //eventually change this to DoTurn
            DoRound();
        }

    }
}