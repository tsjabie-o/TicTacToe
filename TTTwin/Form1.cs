using System;
using System.Drawing;
using System.Windows.Forms;
using Classlib;

namespace TTTwin
{
    public class Form1 : Form
    {
        // * Spel aan
        bool spel_aan;

        // * Afmetingen en locatie spelbord
        int bordbreedte = 300;
        int bordhoogte = 300;
        float bordx;
        float bordy;

        // * Controls
        Label beurt = new Label();
        Label winnaar = new Label();
        Button nieuw_spel = new Button();

        // *  Object back-end spel
        Spel spel = new Spel();

        // * Constructor
        public Form1()
        {
            // * Eigenschappen van form
            this.WindowState = FormWindowState.Maximized;

            // * Controls
            Label kopTekst = new Label();
            kopTekst.Text = "TicTacToe";
            kopTekst.Location = new Point(870, 100);
            kopTekst.Font = new Font("Tahoma", 30);
            kopTekst.ForeColor = Color.MediumPurple;
            kopTekst.AutoSize = true;
            Controls.Add(kopTekst);
            Controls.Add(nieuw_spel);
            nieuw_spel.Text = "Nieuw spel";
            nieuw_spel.AutoSize = true;
            nieuw_spel.Location = new Point(925, 600);
            Controls.Add(beurt);
            beurt.AutoSize = true;
            beurt.Visible = false;
            Controls.Add(winnaar);

            // * Eventhandlers
            this.Paint += Spelbord_Paint;
            this.MouseClick += Muisklik;
            nieuw_spel.Click += nieuwSpel;
        }

        // * Nieuw spel methode
        private void nieuwSpel(object sender, EventArgs e)
        {
            spel.new_game();
            beurt.Visible = true;
            beurt.Location = new Point(925, 550);
            beurt.Font = new Font("Tahoma", 14);
            winnaar.Visible = false;
            spel_aan = true;
            beurt.Text = "Player 1";
            this.Invalidate();
        }

        // * Winnaar methode
        private void Winnaar()
        {
            if (spel.Winner != 0)
            {
                winnaar.ForeColor = Color.Green;
                winnaar.Location = new Point(780, this.Size.Height / 2);
                winnaar.Visible = true;
                winnaar.AutoSize = true;
                winnaar.Font = new Font("Tahoma", 20);
                beurt.Visible = false;
                spel_aan = false;
                if (spel.Winner == 1) 
                {
                    winnaar.Text = "Player 1 has won the game!";
                }
                else 
                {
                    winnaar.Text = "Player 2 has won the game!";
                }
            }
        }

        // * Muisklik methode
        private void Muisklik(object sender, MouseEventArgs e)
        {
            if (spel_aan)
            {
                int index_i;
                int index_j;
                for (float i = 0; i < 3; i++)
                {
                    if (bordx + (i / 3) * bordbreedte < e.X && e.X < bordx + ((i + 1) / 3) * bordbreedte)
                    {
                        index_i = Convert.ToInt32(i);
                        for (float j = 0; j < 3; j++)
                        {
                            if (bordy + (j / 3) * bordhoogte < e.Y && e.Y < bordy + ((j + 1) / 3) * bordhoogte)
                            {
                                index_j = Convert.ToInt32(j);
                                spel.Add_value(index_i, index_j);
                                this.Winnaar();
                                if (spel.player1)
                                {
                                    beurt.Text = "Player 1";
                                }
                                else 
                                {
                                    beurt.Text = "Player 2";
                                }
                                this.Invalidate();
                            }
                        }
                    }
                }
            }
        }
        // * Schermteken methode
        private void Spelbord_Paint(object sender, PaintEventArgs e)
        {
            // * Plek van bord op basis van schermgrootte
            bordx = (float) this.Size.Width / 2 - (float) bordbreedte / 2;
            bordy = (float) this.Size.Height / 3 - (float) bordbreedte / 2;

            // * Grid tekenen
            for (float i = 1; i < 3; i ++)
            {
                e.Graphics.DrawLine(Pens.Black, (i / 3) * bordbreedte + bordx, bordy, (i/3) * bordbreedte + bordx, bordy + bordhoogte);
            }
            for (float i = 1; i < 3; i ++)
            {
                e.Graphics.DrawLine(Pens.Black, bordx, (i / 3) * bordhoogte + bordy, bordx + bordbreedte, (i/3) * bordhoogte + bordy);
            }

            // * Grid invullen
            for (float x = 0; x < 3; x++)
            {
                for (float y = 0; y < 3; y++)
                {
                    if (spel.Spelbord[Convert.ToInt32(x), Convert.ToInt32(y)] != 0)
                    {
                        float icon_x = (x / 3) * bordbreedte + bordx;
                        float icon_y = (y / 3) * bordhoogte + bordy;
                        float icon_dim = (1f / 3) * bordhoogte;
                        if (spel.Spelbord[Convert.ToInt32(x), Convert.ToInt32(y)] == 1)
                        {
                            e.Graphics.FillEllipse(Brushes.Black, icon_x, icon_y, icon_dim, icon_dim);
                        }
                        else
                        {
                            e.Graphics.FillEllipse(Brushes.Red, icon_x, icon_y, icon_dim, icon_dim);
                        }
                    }
                }
            }
        }

    }
}
