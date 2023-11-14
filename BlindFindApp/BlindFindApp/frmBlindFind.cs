namespace BlindFindApp
{
    public partial class frmBlindFind : Form
    {

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

            btnGameStage.Click += BtnGameStage_Click;
        }

        private void ChangeToRandomColor()
        {
            Random r = new();
            DateTime starttime = DateTime.Now;
            Color c = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            lstbuttons.ForEach(b => b.BackColor = c);
            while ((DateTime.Now - starttime).TotalSeconds <= 5)
            {
                Application.DoEvents();
            }
            lstbuttons.ForEach(b => b.BackColor = Color.White);
        }

        private void BtnGameStage_Click(object? sender, EventArgs e)
        {
            if(btnGameStage.Text == "Start")
            {
                btnGameStage.Text = "Next Round";
                ChangeToRandomColor();

            }
        }
    }
}