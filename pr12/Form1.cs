using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pr12
{
    public partial class Form1 : Form
    {

        private List<int> numbers;
        private List<int> clickedNumbers;
        private Stopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeGame()
        {
            numbers = new List<int>();
            clickedNumbers = new List<int>();
            for (int i = 1; i <= 50; i++)
            {
                numbers.Add(i);
            }
            numbers.Shuffle(); // перемешиваем числа для размещения на форме в случайном порядке
            stopwatch = new Stopwatch();
            stopwatch.Start();

            DrawNumbers();
        }

        private void DrawNumbers()
        {
            const int startX = 20;
            const int startY = 20;
            const int spacing = 40;
            const int width = 30;
            const int height = 30;

            for (int i = 0; i < numbers.Count; i++)
            {
                int row = i / 10;
                int col = i % 10;
                Button button = new Button
                {
                    Text = numbers[i].ToString(),
                    Location = new Point(startX + col * spacing, startY + row * spacing),
                    Size = new Size(width, height),
                    Tag = numbers[i]
                };
                button.Click += NumberButton_Click;
                Controls.Add(button);
            }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int clickedNumber = (int)button.Tag;
            if (clickedNumber == clickedNumbers.Count + 1)
            {
                clickedNumbers.Add(clickedNumber);
                button.Enabled = false;
                if (clickedNumbers.Count == 50)
                {
                    stopwatch.Stop();
                    MessageBox.Show($"Game Over!\nПоследний нажатый номер: {clickedNumber}\nПрошло время: {stopwatch.Elapsed.TotalSeconds:F2} секунд");
                    ResetGame();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, нажмите на цифры в порядке возрастания.");
            }
        }

        private void ResetGame()
        {
            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.Dispose();
                }
            }
            InitializeGame();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) { this.Close(); }
        }
    }

    public static class ListExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}
