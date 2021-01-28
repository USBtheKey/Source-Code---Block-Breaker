using System.Collections;
using System.Collections.Generic;


public class PlayerScore 
{
    private string name;
    private int score;

    public PlayerScore() { }
    public PlayerScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string Name { get => name; set => name = value; }
    public int Score { get => score; set => score = (value < 0)? 0 : (value > 99999999) ? 99999999 : value; }
}
