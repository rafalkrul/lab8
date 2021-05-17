using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafikaLab8
{
    public partial class Form1 : Form
    {
        //laplace
        List<List<int>> Laplace1 = new List<List<int>>() { new List<int>() { 0, 1, 0 },new List<int>() { 1, -4, 1 },new List<int>() { 0, 1, 0 }};
        List<List<int>> Laplace2 = new List<List<int>>() { new List<int>() { 1, 1, 1 },new List<int>() { 1, -8, 1 },new List<int>() { 1, 1, 1 }};
        List<List<int>> Laplace3 = new List<List<int>>() { new List<int>() { 2, -1, 2 },new List<int>() { -1, -4, -1 },new List<int>() { 2, -1, 2 }};
       //Roberts
        List<List<int>> RobertsPion = new List<List<int>>() { new List<int>() { 0, 0, 0 },new List<int>() { 0, 1, -1 },new List<int>() { 0, 0, 0 }};
        List<List<int>> RobertsPoziom = new List<List<int>>() { new List<int>() { 0, 0, 0 },new List<int>() { 0, 1, 0 },new List<int>() { 0, -1, 0 }};
        //Prewitt
        List<List<int>> PrewittPion = new List<List<int>>() { new List<int>() { 1, 1, 1 },new List<int>() { 0, 0, 0 },new List<int>() { -1, -1, -1 }};
        List<List<int>> PrewittPoziom = new List<List<int>>() { new List<int>() { 1, 0, -1 },new List<int>() { 1, 0, -1 },new List<int>() { 1, 0, -1 }};
       //Sobel
        List<List<int>> SobelPion = new List<List<int>>() { new List<int>() { 1, 2, 1 },new List<int>() { 0, 0, 0 },new List<int>() { -1, -2, -1 }};
        List<List<int>> SobelPoziom = new List<List<int>>() { new List<int>() { 1, 0, -1 },new List<int>() { 2, 0, -2 },new List<int>() { 1, 0, -1 }};
        Filtry filtr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filtr = new Filtry((Bitmap)pictureBox1.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.Lap1(RobertsPion);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.Lap1(RobertsPoziom);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.Lap1(PrewittPion);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.Lap1(PrewittPoziom);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.Lap1(SobelPion);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.Lap1(SobelPoziom);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.Lap1(Laplace2);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.max((Bitmap)pictureBox1.Image, (Bitmap)pictureBox2.Image);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.mediana((Bitmap)pictureBox1.Image, (Bitmap)pictureBox2.Image);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filtr.min((Bitmap)pictureBox1.Image, (Bitmap)pictureBox2.Image);
        }
    }

    class Filtry
    {
        Bitmap Zdj;

        private int checkIfInRgb(double temp)
        {
            if (temp > 255) return 255;
            else if (temp < 0) return 0;
            return (int)temp;
        }

        public Filtry(Bitmap image)
        {
            this.Zdj = image;
        }

        public Bitmap Lap1(List<List<int>> mask)
        {
            Color[,] colors = new Color[3, 3];
            var image2 = new Bitmap(Zdj);
            for (int x = 1; x < Zdj.Width - 1; x++)
            {
                for (int y = 1; y < Zdj.Height - 1; y++)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            colors[i + 1, j + 1] = Zdj.GetPixel(x + i, y + j);
                        }
                    }
                    int r = 0, g = 0, b = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            r += colors[i + 1, j + 1].R * mask[i + 1][j + 1];
                            g += colors[i + 1, j + 1].G * mask[i + 1][j + 1];
                            b += colors[i + 1, j + 1].B * mask[i + 1][j + 1];

                        }
                    }
                    int avg = (r + g + b) / 3;
                    if (avg > 255) avg = 255;
                    if (avg < 0) avg = 0;
                    image2.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            return image2;
        }

        public Bitmap max(Bitmap Zdjecie1, Bitmap Zdjecie2)
        {
            Bitmap z1 = Zdjecie1;
            Bitmap z2 = Zdjecie2;
            Color[,] k = new Color[3, 3];
            int r = 0, g = 0, b = 0;
            int[] red = new int[9];
            int[] green = new int[9];
            int[] blue = new int[9];
            List<int> maska = new List<int>() { -1, 0, -1, 0, 4, 0, -1, 0, -1 };
            int indeks;


            for (int x = 0; x < z1.Width; x++)
            {
                if (x == 0 || x == z1.Width - 1)
                {
                    continue;
                }
                for (int y = 0; y < z1.Height; y++)
                {
                    if (y == 0 || y == z1.Height - 1)
                    {
                        continue;
                    }
                    indeks = 0;
                    r = 0;
                    g = 0;
                    b = 0;
                    for (int i = -1; i < 2; i++)
                    {

                        for (int j = -1; j < 2; j++)
                        {

                            k[i + 1, j + 1] = z1.GetPixel(x + i, y + j);

                            red[indeks] = k[i + 1, j + 1].R;
                            green[indeks] = k[i + 1, j + 1].G;
                            blue[indeks] = k[i + 1, j + 1].B;
                            indeks++;
                        }
                    }


                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);
                    r = red[8];
                    g = green[8];
                    b = blue[8];

                    z2.SetPixel(x, y, Color.FromArgb(checkIfInRgb(r), checkIfInRgb(g), checkIfInRgb(b)));

                }
            }
            return z2;
        }

        public Bitmap min(Bitmap Zdjecie1, Bitmap Zdjecie2)
        {
            Bitmap z1 = Zdjecie1;
            Bitmap z2 = Zdjecie2;
            Color[,] k = new Color[3, 3];
            int r = 0, g = 0, b = 0;
            int[] red = new int[9];
            int[] green = new int[9];
            int[] blue = new int[9];
            int indeks;


            for (int x = 0; x < z1.Width; x++)
            {
                if (x == 0 || x == z1.Width - 1)
                {
                    continue;
                }
                for (int y = 0; y < z1.Height; y++)
                {
                    if (y == 0 || y == z1.Height - 1)
                    {
                        continue;
                    }
                    indeks = 0;
                    for (int i = -1; i < 2; i++)
                    {

                        for (int j = -1; j < 2; j++)
                        {

                            k[i + 1, j + 1] = z1.GetPixel(x + i, y + j);

                            red[indeks] = k[i + 1, j + 1].R;
                            green[indeks] = k[i + 1, j + 1].G;
                            blue[indeks] = k[i + 1, j + 1].B;
                            indeks++;
                        }
                    }
                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);
                    r = red[0];
                    g = green[0];
                    b = blue[0];

                    z2.SetPixel(x, y, Color.FromArgb(r, g, b));

                }
            }
            return z2;
        }



        public Bitmap mediana(Bitmap Zdjecie1, Bitmap Zdjecie2)
        {
            Bitmap z1 = Zdjecie1;
            Bitmap z2 = Zdjecie2;
            Color[,] k = new Color[3, 3];
            int r = 0, g = 0, b = 0;
            int[] red = new int[9];
            int[] green = new int[9];
            int[] blue = new int[9];
            int indeks;


            for (int x = 0; x < z1.Width; x++)
            {
                if (x == 0 || x == z1.Width - 1)
                {
                    continue;
                }
                for (int y = 0; y < z1.Height; y++)
                {
                    if (y == 0 || y == z1.Height - 1)
                    {
                        continue;
                    }
                    indeks = 0;
                    r = 0;
                    g = 0;
                    b = 0;
                    for (int i = -1; i < 2; i++)
                    {

                        for (int j = -1; j < 2; j++)
                        {

                            k[i + 1, j + 1] = z1.GetPixel(x + i, y + j);

                            red[indeks] = k[i + 1, j + 1].R;
                            green[indeks] = k[i + 1, j + 1].G;
                            blue[indeks] = k[i + 1, j + 1].B;
                            indeks++;
                        }
                    }
                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);

                    r = red[4];
                    g = green[4];
                    b = blue[4];

                    z2.SetPixel(x, y, Color.FromArgb(r, g, b));

                }
            }
            return z2;
        }

     
    }
}
