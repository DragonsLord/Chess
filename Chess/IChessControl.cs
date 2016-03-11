using Chess.Figures;
    
namespace Chess
{
    interface IChessControler
    {
        King @King { get; set; }
        bool Control(Cell pos, MoveAction action);
    }
    interface IFirstMove
    {
        bool FirstMove { get; set; }
    }
}
