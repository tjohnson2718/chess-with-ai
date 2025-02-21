using System.Drawing;

namespace ChessGame;
public interface IController
{
    void Start(bool is690);
    bool Select(Point coordinate);
}