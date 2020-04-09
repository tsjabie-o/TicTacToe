using System;
namespace Classlib
{
    public class Spel
    {
        // *Members
        int[,] spelbord = new int[3, 3];
        int last_col;
        int last_row;
        public bool player1;

        // *Constructor
        public Spel()
        {
            // * Spelbord startwaarden geven
            for (int i = 0; i < 3; i ++)
            {
                for (int j = 0; j < 3; j ++)
                {
                    spelbord[i, j] = 0;
                }
            }
            player1 = true;
        }

        // * Spelbord property
        public int[,] Spelbord
        {
            get
            {
                return spelbord;
            }
        }

        // * Beurten methode
        private void Beurten()
        {
            if (player1)
            {
                player1 = false;
            }
            else 
            {
                player1 = true;
            }
        }

        // * Get scores
        private int X_score
        {
            get 
            {
                int x = 0;
                for (int i = 0; i < 3; i ++)
                {
                    for (int j = 0; j < 3; j ++)
                    {
                        if (spelbord[i,j] == 1)
                        {
                            x ++;
                        }
                    }
                }
                return x;
            }
        }
        private int O_score
        {
            get
            {
                int o = 0;
                for (int i = 0; i < 3; i ++)
                {
                    for (int j = 0; j < 3; j ++)
                    {
                        if (spelbord[i, j] == 2)
                        {
                            o ++;
                        }
                    }
                }
                return o;
            }
        }

        // * Winner check
        public int Winner
        {
            get
            {
                if (spelbord[last_row, 0] == spelbord[last_row, 1] && spelbord[last_row, 1] == spelbord[last_row, 2])
                {
                    return spelbord[last_row, 0];
                }
                else if (
                    spelbord[0, last_col] == spelbord[1, last_col] 
                    && spelbord[1, last_col] == spelbord[2, last_col]
                    )
                {
                    return spelbord[0, last_col];
                }
                else if (spelbord[1, 1] != 0 && ((spelbord[0,0] == spelbord[1,1] && spelbord[1,1] == spelbord[2,2]) || (spelbord[2,0] == spelbord[1,1] && spelbord[1,1] == spelbord[0,2])))
                {
                    return spelbord[1,1];
                }
                else
                {
                    return 0;
                }
            }
        }

        // * Add value
        public void Add_value(int location_i, int location_j)
        {
            int player;
            if (player1)
            {
                player = 1;
            }
            else
            {
                player = 2;
            }
            if (spelbord [location_i, location_j] == 0)
            {
                spelbord[location_i, location_j] = player;
                last_row = location_i;
                last_col = location_j;
                this.Beurten();
            }
        }

        public void new_game()
        {
            for (int i = 0; i < 3; i ++)
            {
                for (int j = 0; j < 3; j ++)
                {
                    spelbord[i, j] = 0;
                }
            }
            player1 = true;
        }
    }
}