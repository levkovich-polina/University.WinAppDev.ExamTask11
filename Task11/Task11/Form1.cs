using Timer = System.Threading.Timer;

namespace Task11
{
    public partial class Form1 : Form
    {

        public class Square
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }

            public int Width { get; set; }
            public int Height { get; set; }

            public Color Color { get; set; }
            public Square(int x, int y, int width, int height, Color color)
            {
                PositionX = x;
                PositionY = y;
                Width = width;
                Height = height;
                Color = color;
            }
        }
        public class Player
        {
            public int PositionPlayerX { get; set; }
            public int PositionPlayerY { get; set; }

            public int Width { get; set; }
            public int Height { get; set; }

            public Color Color { get; set; }
            public Player(int x, int y, int width, int height, Color color)
            {
                PositionPlayerX = x;
                PositionPlayerY = y;
                Width = width;
                Height = height;
                Color = color;
            }
        }
        List<Square> _squares = new List<Square>();
        private Timer _timer;
        Random _random = new Random();
        Player _player;
        List<int> _listPositionX = new List<int>();
        private const int _gameFieldSize = 30;

        public Form1()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            _squares.Clear();
            _player = new Player(15, 30, 1, 1, Color.Red);

            TimerCallback tm = new TimerCallback(OnTimerTicked);
            _timer = new Timer(tm, 0, 0, 200);
        }
        public void OnTimerTicked(object obj)
        {
            _listPositionX.Clear();

            for (int j = 0; j < _gameFieldSize; j++)
            {
                int chance = _random.Next(1, 11);
                if (chance <= 1)
                {
                    _listPositionX.Add(j);
                }
            }
            if (_squares.Count > 1)
            {
                for (int i = 0; i < _squares.Count; i++)
                {
                    if (_player.PositionPlayerY  == _squares[i].PositionY && _player.PositionPlayerX <= _squares[i].PositionX && _squares[i].PositionX <= (_player.PositionPlayerX + _player.Width - 1))
                    {
                        _timer.Dispose();
                        MessageBox.Show("Вы врезались в огорождение! Игра окончена.");
                    }
                }
            }
            if (_squares.Count > 1)
            {
                for (int i = 0; i < _squares.Count; i++)
                {
                    _squares[i].PositionY++;
                }
            }
            for (int i = 0; i < _listPositionX.Count; i++)
            {
                Square square = new Square(_listPositionX[i], 0, 1, 1, Color.Gray);
                _squares.Add(square);
            }
            Draw();
        }

        private void Draw()
        {
            Graphics g = Panel.CreateGraphics();
            g.Clear(Color.White);
            int width = Panel.ClientSize.Width / _gameFieldSize;
            int height = Panel.ClientSize.Height / _gameFieldSize;
            for (int i = 0; i < _squares.Count; i++)
            {
                var dx = _squares[i].PositionX * width;
                var dy = _squares[i].PositionY * height;
                var dWidth = _squares[i].Width * width;
                var dHeight = _squares[i].Height * height;
                var brush = _squares[i].Color;
                Invoke(() =>
                {
                    g.FillRectangle(new SolidBrush(brush), dx, dy, dWidth, dHeight);
                });
            }
            g.FillRectangle(new SolidBrush(_player.Color), _player.PositionPlayerX * width, _player.PositionPlayerY * height, _player.Width * width, _player.Height * height);

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _timer.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                if (_player.PositionPlayerX > 0)
                {
                    _player.PositionPlayerX--;
                }
            }
            if (e.KeyCode == Keys.D)
            {
                if (_player.PositionPlayerX < _gameFieldSize)
                {
                    _player.PositionPlayerX++;
                }
            }
            if (e.KeyCode == Keys.W)
            {
                if (_player.PositionPlayerY > 0)
                {
                    _player.PositionPlayerY--;
                }
            }
            if (e.KeyCode == Keys.S)
            {
                if (_player.PositionPlayerY < _gameFieldSize)
                {
                    _player.PositionPlayerY++;
                }
            }
        }
    }
}