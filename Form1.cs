namespace Game
{
    public partial class Form1 : Form
    {
        int dx = 5, dy = 5;   // topun sürəti (x və y istiqamətində)
        int xal = 0;          // xal

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyPreview = true;        // Form düymələri panelə baxmadan tutsun
            string yol = Path.Combine(Application.StartupPath, "ball.png");
            if (File.Exists(yol))
            {
                pictureBox1.Image = Image.FromFile(yol);   // top şəkli
            }
            label1.Text = "0";
            timer1.Interval = 20;
            timer1.Start();
        }

        // Panelin sağa-sola hərəkəti
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && panel1.Left > 0)
            {
                panel1.Left -= 20;
            }
            if (e.KeyCode == Keys.Right && panel1.Right < ClientSize.Width)
            {
                panel1.Left += 20;
            }
        }

        // Topun hərəkəti
        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left += dx;
            pictureBox1.Top += dy;

            // Sol, sağ və yuxarı divardan əks olunma
            if (pictureBox1.Left <= 0)
            {
                dx = Math.Abs(dx);
            }
            if (pictureBox1.Right >= ClientSize.Width)
            {
                dx = -Math.Abs(dx);
            }
            if (pictureBox1.Top <= 0)
            {
                dy = Math.Abs(dy);
            }

            // Top panelə dəyəndə (aşağı gedəndə) xal artır
            if (pictureBox1.Bounds.IntersectsWith(panel1.Bounds) && dy > 0)
            {
                dy = -Math.Abs(dy);
                pictureBox1.Top = panel1.Top - pictureBox1.Height;
                xal++;
                label1.Text = xal.ToString();
            }
            // Top paneli keçdisə oyun bitir
            else if (pictureBox1.Top > panel1.Bottom)
            {
                Oyunbitdi();
            }
        }

        void Oyunbitdi()
        {
            timer1.Stop();

            MessageBox.Show(
                "Oyun bitdi! Yekun xal: " + xal +
                "\nYeni oyun üçün OK düyməsinə basın",
                "Oyun bitdi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            YeniOyun();
        }

        void YeniOyun()
        {
            xal = 0;
            label1.Text = "0";
            dx = 5;
            dy = 5;
            pictureBox1.Left = 70;
            pictureBox1.Top = 60;
            timer1.Start();
        }
    }
}