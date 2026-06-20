using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BulanikMantik_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        //Tabloyu Gösterip Saklama
        private void TabloButton_Click(object sender, EventArgs e)
        {
            // Tabloyu gösterip saklama
            if (TabloPanel.Visible == true)
                TabloPanel.Visible = false;
            else
                TabloPanel.Visible = true;
        }

        //Grafikleri Oluşturma
        private void GrafikDuzenle(object sender, EventArgs e)
        {
            if (nUD_Vize_Min.Value > nUD_Vize_Max.Value)
            {
                nUD_Vize_Max.Value = nUD_Vize_Min.Value;
            }
            if (NUD_Vize.Value > nUD_Vize_Max.Value)
            {
                nUD_Vize_Max.Value = NUD_Vize.Value;
            }
            if (NUD_Sonuc_D.Value > (NUD_Sonuc_F.Value + NUD_Sonuc_Ref.Value))
            {
                NUD_Sonuc_D.Value = NUD_Sonuc_F.Value; 
            }
            //Vize Grafiği
            Graphics gr_1 = pB_Grafik_Vize.CreateGraphics();
            gr_1.Clear(Color.FromKnownColor(KnownColor.Control));
            gr_1.DrawLine(new Pen(Brushes.Red,2),new Point(0,0),new Point((int)nUD_Vize_Min.Value*3,300));
            gr_1.DrawLine(new Pen(Brushes.Yellow, 2), new Point(0, 300), new Point((int)nUD_Vize_Min.Value * 3, 0));
            gr_1.DrawLine(new Pen(Brushes.Yellow, 2), new Point((int)nUD_Vize_Min.Value*3, 0), new Point((((int)nUD_Vize_Min.Value + (int)nUD_Vize_Max.Value) / 2) * 3, 300));
            gr_1.DrawLine(new Pen(Brushes.Blue, 2), new Point((int)nUD_Vize_Min.Value * 3, 300), new Point((((int)nUD_Vize_Min.Value + (int)nUD_Vize_Max.Value) / 2) * 3, 0));
            gr_1.DrawLine(new Pen(Brushes.Blue, 2), new Point((((int)nUD_Vize_Min.Value + (int)nUD_Vize_Max.Value) / 2) * 3, 0), new Point((int)nUD_Vize_Max.Value * 3, 300));
            gr_1.DrawLine(new Pen(Brushes.Green, 2), new Point((((int)nUD_Vize_Min.Value + (int)nUD_Vize_Max.Value) / 2) * 3, 300), new Point(300, 0));            
            Font f = new Font("Verdana",6);
            gr_1.FillRectangle(Brushes.Tomato,new Rectangle(0,0,(int)gr_1.MeasureString("1",f).Width+2,10));
            gr_1.DrawString("1",f,new SolidBrush(Color.Black),new Point(0,0));
            gr_1.FillRectangle(Brushes.Tomato, new Rectangle(282, 290, (int)gr_1.MeasureString("100", f).Width + 2, 10));
            gr_1.DrawString("100", f, new SolidBrush(Color.Black), new Point(282, 290));
            gr_1.FillRectangle(Brushes.Tomato, new Rectangle((int)nUD_Vize_Min.Value*3, 288, (int)gr_1.MeasureString(nUD_Vize_Min.Value.ToString(), f).Width + 2, 10));
            gr_1.DrawString(nUD_Vize_Min.Value.ToString(), f, new SolidBrush(Color.Black), new Point((int)nUD_Vize_Min.Value*3, 288));
            gr_1.FillRectangle(Brushes.Tomato, new Rectangle((((int)nUD_Vize_Min.Value + (int)nUD_Vize_Max.Value) / 2) * 3, 288, (int)gr_1.MeasureString(nUD_Vize_Min.Value.ToString(), f).Width + 2, 10));
            gr_1.DrawString((((int)nUD_Vize_Min.Value + (int)nUD_Vize_Max.Value) / 2).ToString(), f, new SolidBrush(Color.Black), new Point((((int)nUD_Vize_Min.Value + (int)nUD_Vize_Max.Value) / 2) * 3, 288));
            gr_1.FillRectangle(Brushes.Tomato, new Rectangle((int)nUD_Vize_Max.Value * 3, 288, (int)gr_1.MeasureString(nUD_Vize_Max.Value.ToString(), f).Width + 2, 10));
            gr_1.DrawString(nUD_Vize_Max.Value.ToString(), f, new SolidBrush(Color.Black), new Point((int)nUD_Vize_Max.Value * 3, 288));

            //Kesişimler
            Point Kesisim_1 = new Point();
            Point Kesisim_2 = new Point();
            int vize_Durum = 0; // ZO,OI,IP durumlarından birini belirtecek
            if (NUD_Vize.Value <= nUD_Vize_Min.Value)
            {
                Kesisim_1 = KesisimBul(0, 0, (double)nUD_Vize_Min.Value, 100, (double)NUD_Vize.Value, 0, (double)NUD_Vize.Value, 100);
                Kesisim_2 = KesisimBul((double)NUD_Vize.Value, 0, (double)NUD_Vize.Value, 100, (double)nUD_Vize_Min.Value, 0, 0, 100);
             }
            else if ((NUD_Vize.Value > nUD_Vize_Min.Value) && ((NUD_Vize.Value <= (nUD_Vize_Min.Value + nUD_Vize_Max.Value) / 2)))
            {
                Kesisim_1 = KesisimBul((double)nUD_Vize_Min.Value, 0, (double)(((double)nUD_Vize_Min.Value + (double)nUD_Vize_Max.Value) / 2), 100, (double)NUD_Vize.Value, 0, (double)NUD_Vize.Value, 100);
                Kesisim_2 = KesisimBul((double)NUD_Vize.Value, 0, (double)NUD_Vize.Value, 100, (double)(((double)nUD_Vize_Min.Value + (double)nUD_Vize_Max.Value) / 2), 0, (double)nUD_Vize_Min.Value, 100);
                vize_Durum = 1;
            }
            else
            {
                Kesisim_1 = KesisimBul((double)(((double)nUD_Vize_Min.Value + (double)nUD_Vize_Max.Value) / 2), 0, (double)nUD_Vize_Max.Value, 100, (double)NUD_Vize.Value, 0, (double)NUD_Vize.Value, 100);
                Kesisim_2 = KesisimBul((double)NUD_Vize.Value, 0, (double)NUD_Vize.Value, 100, 100, 0, (double)(((double)nUD_Vize_Min.Value + (double)nUD_Vize_Max.Value) / 2), 100);
                vize_Durum = 2;
            }
            gr_1.DrawLine(new Pen(Brushes.Gray), Kesisim_1, new Point(Kesisim_1.X, 300));
            gr_1.DrawLine(new Pen(Brushes.Gray), Kesisim_1, new Point(0, Kesisim_1.Y));
            gr_1.DrawArc(new Pen(Brushes.Gray, 2), Kesisim_1.X - 6, Kesisim_1.Y - 6, 12, 12, 0, 360);
            gr_1.DrawString("(" + Kesisim_1.X / 3 + "," + Kesisim_1.Y / 3 + ")", f, Brushes.Brown, new Point(Kesisim_1.X + 8, Kesisim_1.Y));
            gr_1.DrawLine(new Pen(Brushes.Gray), Kesisim_2, new Point(Kesisim_2.X, 300));
            gr_1.DrawLine(new Pen(Brushes.Gray), Kesisim_2, new Point(0, Kesisim_2.Y));
            gr_1.DrawArc(new Pen(Brushes.Gray, 2), Kesisim_2.X - 6, Kesisim_2.Y - 6, 12, 12, 0, 360);
            gr_1.DrawString("(" + Kesisim_2.X / 3 + "," + Kesisim_2.Y / 3 + ")", f, Brushes.Brown, new Point(Kesisim_2.X + 8, Kesisim_2.Y));
            gr_1.FillRectangle(Brushes.Tomato, new Rectangle(0, Kesisim_1.Y, (int)gr_1.MeasureString(((double)(1 - ((double)Kesisim_1.Y / 300))).ToString(), f).Width + 2, 10));
            gr_1.DrawString(((double)(1 - ((double)Kesisim_1.Y / 300))).ToString(), f, new SolidBrush(Color.Black), new Point(0, Kesisim_1.Y));
            gr_1.FillRectangle(Brushes.Tomato, new Rectangle(0, Kesisim_2.Y, (int)gr_1.MeasureString(((double)(1 - ((double)Kesisim_2.Y / 300))).ToString(), f).Width + 2, 10));
            gr_1.DrawString(((double)(1 - ((double)Kesisim_2.Y / 300))).ToString(), f, new SolidBrush(Color.Black), new Point(0, Kesisim_2.Y));

            if (nUD_Final_Min.Value > nUD_Final_Max.Value)
            {
                nUD_Final_Max.Value = nUD_Final_Min.Value;
            }
            if (NUD_Final.Value > nUD_Final_Max.Value)
            {
                nUD_Final_Max.Value = NUD_Final.Value;
            }
            
            //Final Grafiği
            Graphics gr_2 = pB_Grafik_Final.CreateGraphics();
            gr_2.Clear(Color.FromKnownColor(KnownColor.Control));
            gr_2.DrawLine(new Pen(Brushes.Red, 2), new Point(0, 0), new Point((int)nUD_Final_Min.Value * 3, 300));
            gr_2.DrawLine(new Pen(Brushes.Yellow, 2), new Point(0, 300), new Point((int)nUD_Final_Min.Value * 3, 0));
            gr_2.DrawLine(new Pen(Brushes.Yellow, 2), new Point((int)nUD_Final_Min.Value * 3, 0), new Point((((int)nUD_Final_Min.Value + (int)nUD_Final_Max.Value) / 2) * 3, 300));
            gr_2.DrawLine(new Pen(Brushes.Blue, 2), new Point((int)nUD_Final_Min.Value * 3, 300), new Point((((int)nUD_Final_Min.Value + (int)nUD_Final_Max.Value) / 2) * 3, 0));
            gr_2.DrawLine(new Pen(Brushes.Blue, 2), new Point((((int)nUD_Final_Min.Value + (int)nUD_Final_Max.Value) / 2) * 3, 0), new Point((int)nUD_Final_Max.Value * 3, 300));
            gr_2.DrawLine(new Pen(Brushes.Green, 2), new Point((((int)nUD_Final_Min.Value + (int)nUD_Final_Max.Value) / 2) * 3, 300), new Point(300, 0));
            gr_2.FillRectangle(Brushes.Tomato, new Rectangle(0, 0, (int)gr_2.MeasureString("1", f).Width + 2, 10));
            gr_2.DrawString("1", f, new SolidBrush(Color.Black), new Point(0, 0));
            gr_2.FillRectangle(Brushes.Tomato, new Rectangle(282, 290, (int)gr_2.MeasureString("100", f).Width + 2, 10));
            gr_2.DrawString("100", f, new SolidBrush(Color.Black), new Point(282, 290));
            gr_2.FillRectangle(Brushes.Tomato, new Rectangle((int)nUD_Final_Min.Value * 3, 288, (int)gr_2.MeasureString(nUD_Final_Min.Value.ToString(), f).Width + 2, 10));
            gr_2.DrawString(nUD_Final_Min.Value.ToString(), f, new SolidBrush(Color.Black), new Point((int)nUD_Final_Min.Value * 3, 288));
            gr_2.FillRectangle(Brushes.Tomato, new Rectangle((((int)nUD_Final_Min.Value + (int)nUD_Final_Max.Value) / 2) * 3, 288, (int)gr_2.MeasureString(nUD_Final_Min.Value.ToString(), f).Width + 2, 10));
            gr_2.DrawString((((int)nUD_Final_Min.Value + (int)nUD_Final_Max.Value) / 2).ToString(), f, new SolidBrush(Color.Black), new Point((((int)nUD_Final_Min.Value + (int)nUD_Final_Max.Value) / 2) * 3, 288));
            gr_2.FillRectangle(Brushes.Tomato, new Rectangle((int)nUD_Final_Max.Value * 3, 288, (int)gr_2.MeasureString(nUD_Final_Max.Value.ToString(), f).Width + 2, 10));
            gr_2.DrawString(nUD_Final_Max.Value.ToString(), f, new SolidBrush(Color.Black), new Point((int)nUD_Final_Max.Value * 3, 288));

            //Kesişimler
            Point Kesisim_1_1 = new Point();
            Point Kesisim_2_1 = new Point();
            int final_Durum = 0; // ZO,OI,IP durumlarından birini belirtecek
            if (NUD_Final.Value <= nUD_Final_Min.Value)
            {
                Kesisim_1_1 = KesisimBul(0, 0, (double)nUD_Final_Min.Value, 100, (double)NUD_Final.Value, 0, (double)NUD_Final.Value, 100);
                Kesisim_2_1 = KesisimBul((double)NUD_Final.Value, 0, (double)NUD_Final.Value, 100, (double)nUD_Final_Min.Value, 0, 0, 100);
            }
            else if ((NUD_Final.Value > nUD_Final_Min.Value) && ((NUD_Final.Value <= (nUD_Final_Min.Value + nUD_Final_Max.Value) / 2)))
            {
                Kesisim_1_1 = KesisimBul((double)nUD_Final_Min.Value, 0, (double)(((double)nUD_Final_Min.Value + (double)nUD_Final_Max.Value) / 2), 100, (double)NUD_Final.Value, 0, (double)NUD_Final.Value, 100);
                Kesisim_2_1 = KesisimBul((double)NUD_Final.Value, 0, (double)NUD_Final.Value, 100, (double)(((double)nUD_Final_Min.Value + (double)nUD_Final_Max.Value) / 2), 0, (double)nUD_Final_Min.Value, 100);
                final_Durum = 1;
            }
            else
            {
                Kesisim_1_1 = KesisimBul((double)(((double)nUD_Final_Min.Value + (double)nUD_Final_Max.Value) / 2), 0, (double)nUD_Final_Max.Value, 100, (double)NUD_Final.Value, 0, (double)NUD_Final.Value, 100);
                Kesisim_2_1 = KesisimBul((double)NUD_Final.Value, 0, (double)NUD_Final.Value, 100, 100, 0, (double)(((double)nUD_Final_Min.Value + (double)nUD_Final_Max.Value) / 2), 100);
                final_Durum = 2;
            }
            gr_2.DrawLine(new Pen(Brushes.Gray), Kesisim_1_1, new Point(Kesisim_1_1.X, 300));
            gr_2.DrawLine(new Pen(Brushes.Gray), Kesisim_1_1, new Point(0, Kesisim_1_1.Y));
            gr_2.DrawArc(new Pen(Brushes.Gray, 2), Kesisim_1_1.X - 6, Kesisim_1_1.Y - 6, 12, 12, 0, 360);
            gr_2.DrawString("(" + Kesisim_1_1.X / 3 + "," + Kesisim_1_1.Y / 3 + ")", f, Brushes.Brown, new Point(Kesisim_1_1.X + 8, Kesisim_1_1.Y));
            gr_2.DrawLine(new Pen(Brushes.Gray), Kesisim_2_1, new Point(Kesisim_2_1.X, 300));
            gr_2.DrawLine(new Pen(Brushes.Gray), Kesisim_2_1, new Point(0, Kesisim_2_1.Y));
            gr_2.DrawArc(new Pen(Brushes.Gray, 2), Kesisim_2_1.X - 6, Kesisim_2_1.Y - 6, 12, 12, 0, 360);
            gr_2.DrawString("(" + Kesisim_2_1.X / 3 + "," + Kesisim_2_1.Y / 3 + ")", f, Brushes.Brown, new Point(Kesisim_2_1.X + 8, Kesisim_2_1.Y));
            gr_2.FillRectangle(Brushes.Tomato, new Rectangle(0, Kesisim_1_1.Y, (int)gr_2.MeasureString(((double)(1 - ((double)Kesisim_1_1.Y / 300))).ToString(), f).Width + 2, 10));
            gr_2.DrawString(((double)(1 - ((double)Kesisim_1_1.Y / 300))).ToString(), f, new SolidBrush(Color.Black), new Point(0, Kesisim_1_1.Y));
            gr_2.FillRectangle(Brushes.Tomato, new Rectangle(0, Kesisim_2_1.Y, (int)gr_2.MeasureString(((double)(1 - ((double)Kesisim_2_1.Y / 300))).ToString(), f).Width + 2, 10));
            gr_2.DrawString(((double)(1 - ((double)Kesisim_2_1.Y / 300))).ToString(), f, new SolidBrush(Color.Black), new Point(0, Kesisim_2_1.Y));

            //tablo değerleri matrise alınıyor
            string[,] tablo = new string[4,4]; // uzman tablosunu tutar
            tablo[0, 0] = Tablo_TB_00.Text;
            tablo[0, 1] = Tablo_TB_01.Text;
            tablo[0, 2] = Tablo_TB_02.Text;
            tablo[0, 3] = Tablo_TB_03.Text;
            tablo[1, 0] = Tablo_TB_10.Text;
            tablo[1, 1] = Tablo_TB_11.Text;
            tablo[1, 2] = Tablo_TB_12.Text;
            tablo[1, 3] = Tablo_TB_13.Text;
            tablo[2, 0] = Tablo_TB_20.Text;
            tablo[2, 1] = Tablo_TB_21.Text;
            tablo[2, 2] = Tablo_TB_22.Text;
            tablo[2, 3] = Tablo_TB_23.Text;
            tablo[3, 0] = Tablo_TB_30.Text;
            tablo[3, 1] = Tablo_TB_31.Text;
            tablo[3, 2] = Tablo_TB_32.Text;
            tablo[3, 3] = Tablo_TB_33.Text;

            //harf ve değer listeleri
            double[] degerler = new double[4];
            string[] harfler = new string[4];
            int i = 0; // liste indeksi

            // ve işlemi ve harflerin listeye yerleştirilmesi
            if (tablo[final_Durum, vize_Durum] != "")
            {
                harfler[i] = tablo[final_Durum, vize_Durum];
                if (((double)(1 - ((double)Kesisim_1_1.Y / 300))) < ((double)(1 - ((double)Kesisim_1.Y / 300))))
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_1_1.Y / 300)));
                }
                else
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_1.Y / 300)));
                }
                i++;
            }
            if (tablo[final_Durum+1, vize_Durum] != "")
            {
                harfler[i] = tablo[final_Durum+1, vize_Durum];
                if (((double)(1 - ((double)Kesisim_2_1.Y / 300))) < ((double)(1 - ((double)Kesisim_1.Y / 300))))
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_2_1.Y / 300)));
                }
                else
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_1.Y / 300)));
                }
                i++;
            }
            if (tablo[final_Durum, vize_Durum+1] != "")
            {
                harfler[i] = tablo[final_Durum, vize_Durum+1];
                if (((double)(1 - ((double)Kesisim_1_1.Y / 300))) < ((double)(1 - ((double)Kesisim_2.Y / 300))))
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_1_1.Y / 300)));
                }
                else
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_2.Y / 300)));
                }
                i++;
            }
            if (tablo[final_Durum+1, vize_Durum+1] != "")
            {
                harfler[i] = tablo[final_Durum+1, vize_Durum+1];
                if (((double)(1 - ((double)Kesisim_2_1.Y / 300))) < ((double)(1 - ((double)Kesisim_2.Y / 300))))
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_2_1.Y / 300)));
                }
                else
                {
                    degerler[i] = ((double)(1 - ((double)Kesisim_2.Y / 300)));
                }
                i++;
            }

            //sonuç grafiği çizimi
            Graphics gr_3 = pB_Grafik_Sonuc.CreateGraphics();
            int A = (int)NUD_Sonuc_A.Value;
            int B = (int)NUD_Sonuc_B.Value;
            int C = (int)NUD_Sonuc_C.Value;
            int D = (int)NUD_Sonuc_D.Value;
            int F = (int)NUD_Sonuc_F.Value;
            int R = (int)NUD_Sonuc_Ref.Value;
            Point[] LD = new Point[3];
            LD[0] = new Point((D - R) * 3, 300);
            LD[1] = new Point(((C + (D - R)) / 2) * 3, 0);
            LD[2] = new Point(C * 3, 300);
            Point[] LC = new Point[3];
            LC[0] = new Point((C - R) * 3, 300);
            LC[1] = new Point(((B + (C - R)) / 2) * 3, 0);
            LC[2] = new Point(B * 3, 300);
            Point[] LB = new Point[3];
            LB[0] = new Point((B - R) * 3, 300);
            LB[1] = new Point(((A + (B - R)) / 2) * 3, 0);
            LB[2] = new Point(A * 3, 300);
            gr_3.Clear(Color.FromKnownColor(KnownColor.Control));
            gr_3.DrawLine(new Pen(Brushes.Brown,2), new Point(0, 0), new Point(F * 3, 300));
            gr_3.DrawLines(new Pen(Brushes.CadetBlue, 2), LD);
            gr_3.DrawLines(new Pen(Brushes.DarkOliveGreen, 2), LC);
            gr_3.DrawLines(new Pen(Brushes.DarkOrange, 2), LB);
            gr_3.DrawLine(new Pen(Brushes.DarkRed, 2), new Point((A-R) * 3, 300), new Point(300, 0));
            gr_3.FillRectangle(Brushes.Tomato, new Rectangle(0, 0, (int)gr_3.MeasureString("F", f).Width + 2, 10));
            gr_3.DrawString("F", f, new SolidBrush(Color.Black), new Point(0, 0));
            gr_3.FillRectangle(Brushes.Tomato, new Rectangle(LD[1].X, LD[1].Y, (int)gr_3.MeasureString("D", f).Width + 2, 10));
            gr_3.DrawString("D", f, new SolidBrush(Color.Black), LD[1]);
            gr_3.FillRectangle(Brushes.Tomato, new Rectangle(LC[1].X, LC[1].Y, (int)gr_3.MeasureString("C", f).Width + 2, 10));
            gr_3.DrawString("C", f, new SolidBrush(Color.Black), LC[1]);
            gr_3.FillRectangle(Brushes.Tomato, new Rectangle(LB[1].X, LB[1].Y, (int)gr_3.MeasureString("B", f).Width + 2, 10));
            gr_3.DrawString("B", f, new SolidBrush(Color.Black), LB[1]);
            gr_3.FillRectangle(Brushes.Tomato, new Rectangle(290, 0, (int)gr_3.MeasureString("A", f).Width + 2, 10));
            gr_3.DrawString("A", f, new SolidBrush(Color.Black), new Point(290, 0));

            //3. grafik kesişim bulma ve alan hesabı
            int[] sonuc;
            int moment = 0;
            int alan = 0;
            for (i--; i >= 0; i--)
            {
                int d = (int)((1 - degerler[i]) * 100);
                Point k1 = new Point(); //kesişim noktaları
                Point k2 = new Point();
                if (harfler[i] == "A")
                {
                    k1 = KesisimBul(0, d, 100, d, 100, 0, A - R, 100);
                    k2 = new Point(300, k1.Y);
                    Point[] Koseler = { k2, k1, new Point((A - R) * 3, 300), new Point(300, 300) };
                    sonuc = momentHesapla(k2, k1, new Point((A - R) * 3, 300), new Point(300, 300));
                    moment += sonuc[1];
                    alan += sonuc[0];
                    gr_3.FillPolygon(Brushes.DarkRed,Koseler);
                }
                if (harfler[i] == "B")
                {
                    k1 = KesisimBul(0, d, 100, d, LB[1].X / 3, LB[1].Y / 3, LB[0].X / 3, LB[0].Y / 3);
                    k2 = KesisimBul(0, d, 100, d, LB[1].X / 3, LB[1].Y / 3, LB[2].X / 3, LB[2].Y / 3);                    
                    Point[] Koseler = { k2, k1, LB[0], LB[2] };
                    sonuc = momentHesapla(k2, k1, LB[0], LB[2]);
                    moment += sonuc[1];
                    alan += sonuc[0];
                    gr_3.FillPolygon(Brushes.DarkOrange, Koseler);
                 }
                if (harfler[i] == "C")
                {
                    k1 = KesisimBul(0, d, 100, d, LC[1].X / 3, LC[1].Y / 3, LC[0].X / 3, LC[0].Y / 3);
                    k2 = KesisimBul(0, d, 100, d, LC[1].X / 3, LC[1].Y / 3, LC[2].X / 3, LC[2].Y / 3);
                    Point[] Koseler = { k2, k1, LC[0], LC[2] };
                    sonuc = momentHesapla(k2, k1, LC[0], LC[2]);
                    moment += sonuc[1];
                    alan += sonuc[0];
                    gr_3.FillPolygon(Brushes.DarkOliveGreen, Koseler);
                }
                if (harfler[i] == "D")
                {
                    k1 = KesisimBul(0, d, 100, d, LD[1].X / 3, LD[1].Y / 3, LD[0].X / 3, LD[0].Y / 3);
                    k2 = KesisimBul(0, d, 100, d, LD[1].X / 3, LD[1].Y / 3, LD[2].X / 3, LD[2].Y / 3);
                    Point[] Koseler = { k2, k1, LD[0], LD[2] };
                    sonuc = momentHesapla(k2, k1, LD[0], LD[2]);
                    moment += sonuc[1];
                    alan += sonuc[0];
                    gr_3.FillPolygon(Brushes.CadetBlue, Koseler);
                }
                if (harfler[i] == "F")
                {
                    k1 = KesisimBul(0, d, 100, d, 0, 0, F, 100);
                    k2 = new Point(0, k1.Y);
                    Point[] Koseler = { k1, k2, new Point(0, 300), new Point(F*3, 300) };
                    sonuc = momentHesapla(k1, k2, new Point(0, 300), new Point(F * 3, 300));
                    moment += sonuc[1];
                    alan += sonuc[0];
                    gr_3.FillPolygon(Brushes.Brown, Koseler);
                }
                gr_3.DrawLine(new Pen(Brushes.Black,2), new Point(0, d * 3), new Point(300, d * 3));
                gr_3.FillRectangle(Brushes.Tomato, new Rectangle(2, d * 3, (int)gr_3.MeasureString(degerler[i].ToString(), f).Width + 2, 10));
                gr_3.DrawString(degerler[i].ToString(), f, Brushes.Black, new Point(2, d * 3));
            }
            double BasariNotu = ((double)moment / (double)alan) / 3;
            Sonuc_Label.Text = BasariNotu.ToString();
            gr_3.FillEllipse(Brushes.Red, (float)BasariNotu * 3, 296, 4, 4);

            Not_Label.Text = "";
            int harfSay = 0;
            while (harfSay < 2)
            {
                if (BasariNotu < F && harfSay<2)
                {
                    Not_Label.Text += "F";
                    harfSay++;
                }
                if ((BasariNotu >= (D - R)) && (BasariNotu < C) && harfSay<2)
                {
                    Not_Label.Text += "D";
                    harfSay++; 
                }
                if ((BasariNotu >= (C - R)) && (BasariNotu < B) && harfSay < 2)
                {
                    Not_Label.Text += "C";
                    harfSay++;
                }
                if ((BasariNotu >= (B - R)) && (BasariNotu < A) && harfSay < 2)
                {
                    Not_Label.Text += "B";
                    harfSay++;
                }
                if ((BasariNotu >= A-R) && harfSay < 2)
                {
                    Not_Label.Text += "A";
                    harfSay++;
                }
            }
            
        }

        //İki doğrunun kesişimini bulan fonksiyon
        public Point KesisimBul(double Xi, double Yi, double Xj, double Yj, double Xl, double Yl, double Xk, double Yk)
        {
            double d = (double)((Xj - Xi) * (Yk - Yl)) - ((Xk - Xl) * (Yj - Yi));
            if (d <= 0.00009)
            {
                return new Point();
            }
            double p1 = (((Xk - Xl) * (Yi - Yk)) - ((Xi - Xk) * (Yk - Yl))) / d;
            double p2 = (((Xi - Xk) * (Yj - Yi)) - ((Xj - Xi) * (Yi - Yk))) / d;

            if (!(((p1 <= 1) && (p1 >= 0)) && ((p2 <= 1) && (p2 >= 0))))
            {
                return new Point();
            }

            double Xs = Xi + p1 * (Xj - Xi);
            double Ys = Yi + p1 * (Yj - Yi);
            return new Point((int)Xs*3, (int)Ys*3);
        }

        //yamukların moment hesabı
        public int[] momentHesapla(Point p1, Point p2, Point p3, Point p4)
        {
            int h = Math.Abs((300 - p1.Y) - (300 - p3.Y));
            int L1 = Math.Abs(p1.X - p2.X);
            int L2 = Math.Abs(p3.X - p4.X);

            int alan = ((L1 + L2) / 2) * h;
            int uzaklik = (p3.X + p4.X)/2;
            int[] sonuc = { alan, alan * uzaklik };
            return sonuc;
        }

    }
}
