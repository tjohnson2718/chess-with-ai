using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChessGame.Model;
using ChessGame.Service;

namespace ChessGame;

public class Controller : IController
{
    private Point? _selectedSquare;
    private const int BoardRows = 8;
    private const int BoardColumns = 8;
    private readonly IEngineAdapterService _adapter;
    private readonly IMovementService _movementService;

    public Controller(IMovementService movementService, IEngineAdapterService adapter)
    {
        _movementService = movementService;
        _adapter = adapter;

        for (var i = 0; i < BoardRows; i++)
            for (var j = 0; j < BoardColumns; j++)
                Coordinates.Board[i, j] = new Square();

    }

    public void Start(bool is690)
    {
        if (is690)
        {
            InitPawns(6, Team.White);
            InitPawns(1, Team.Black);
            InitBackRow690(new List<Team> { Team.White, Team.Black });
            UpdateAll();

            if (_movementService.GameSettings.PlayAgainstAi
                && _movementService.GameSettings.CurrentTeam == Team.Black)
            {
                var outputFromAi = _adapter.MakeEngineMove();
                ProcessAiOutput(outputFromAi);
            }
        }
        else
        {
            InitPawns(6, Team.White);
            InitBackRow(7, Team.White);
            InitPawns(1, Team.Black);
            InitBackRow(0, Team.Black);
            UpdateAll();

            if (_movementService.GameSettings.PlayAgainstAi
                && _movementService.GameSettings.CurrentTeam == Team.Black)
            {
                var outputFromAi = _adapter.MakeEngineMove();
                ProcessAiOutput(outputFromAi);
            }
        }
        
    }

    private void UpdateAll()
    {
        for (var i = 0; i < BoardRows; i++)
            for (var j = 0; j < BoardColumns; j++)
                _movementService.Update(new Point(i, j));
    }

    private void InitPawns(int row, Team team)
    {
        for (var i = 0; i < 8; i++)
            _movementService.Place(row, i, new Pawn(team));
    }

    private void InitBackRow(int row, Team team)
    {
        _movementService.Place(row, 0, new Rook(team));
        _movementService.Place(row, 1, new Knight(team));
        _movementService.Place(row, 2, new Bishop(team));
        _movementService.Place(row, 3, new Queen(team));
        _movementService.Place(row, 4, new King(team));
        _movementService.Place(row, 5, new Bishop(team));
        _movementService.Place(row, 6, new Knight(team));
        _movementService.Place(row, 7, new Rook(team));
    }

    public void InitBackRow690(List<Team> teams)
    {
        int rook_one_position = -1;
        int rook_two_position = -1;
        int king_position = -1;
        int bishop_one_position = -1;
        int bishop_two_position = -1;
        int knight_one_position = -1;
        int knight_two_position = -1;
        int queen_position = -1;

        List<int> available_positions = new List<int> {0, 1, 2, 3, 4, 5, 6, 7};

        foreach (Team team in teams)
        {
            if (team == Team.White)
            {
                rook_one_position = GetRookPositions_690(available_positions, rook_one_position, rook_two_position);
                rook_two_position = GetRookPositions_690(available_positions, rook_one_position, rook_two_position);
                king_position = GetKingPosition_690(available_positions, rook_one_position, rook_two_position);
                bishop_one_position = GetBishopPositions_690(available_positions, bishop_one_position, bishop_two_position);
                bishop_two_position = GetBishopPositions_690(available_positions, bishop_one_position, bishop_two_position);
                knight_one_position = GetRemainingPositions(available_positions);
                knight_two_position = GetRemainingPositions(available_positions);
                queen_position = GetRemainingPositions(available_positions);

                _movementService.Place(7, rook_one_position, new Rook(team));
                _movementService.Place(7, rook_two_position, new Rook(team));
                _movementService.Place(7, king_position, new King(team));
                _movementService.Place(7, bishop_one_position, new Bishop(team));
                _movementService.Place(7, bishop_two_position, new Bishop(team));
                _movementService.Place(7, knight_one_position, new Knight(team));
                _movementService.Place(7, knight_two_position, new Knight(team));
                _movementService.Place(7, queen_position, new Queen(team));
            }
            else
            {
                _movementService.Place(0, rook_one_position, new Rook(team));
                _movementService.Place(0, rook_two_position, new Rook(team));
                _movementService.Place(0, king_position, new King(team));
                _movementService.Place(0, bishop_one_position, new Bishop(team));
                _movementService.Place(0, bishop_two_position, new Bishop(team));
                _movementService.Place(0, knight_one_position, new Knight(team));
                _movementService.Place(0, knight_two_position, new Knight(team));
                _movementService.Place(0, queen_position, new Queen(team));
            }
        }
    }
    public int GetRemainingPositions(List<int> available_positions)
    {
        Random random = new Random();
        int index = random.Next(available_positions.Count() - 1);
        int position = available_positions.ElementAt(index);
        available_positions.RemoveAt(index);
        return position;
    }
    public int GetBishopPositions_690(List<int> available_positions, int b1_position, int b2_position)
    {
        Random random = new Random();
        if (b1_position == -1)
        {
            int index = random.Next(available_positions.Count() - 1);
            int position = available_positions.ElementAt(index);
            available_positions.RemoveAt(index);
            return position;
        }
        else
        {
            while (true)
            {
                int index = random.Next(available_positions.Count() - 1);
                if (validateBishopPositions_690(b1_position, available_positions.ElementAt(index)))
                {
                    int position = available_positions.ElementAt(index);
                    available_positions.RemoveAt(index);
                    return position;
                }
            }
        }
    }
    public int GetRookPositions_690(List<int> available_positions, int r1_position, int r2_position)
    {
        Random random = new Random();
        if (r1_position == -1)
        {
            int index = random.Next(available_positions.Count() - 1);
            int position = available_positions.ElementAt(index);
            available_positions.RemoveAt(index);
            return position;
        }
        else
        {
            while (true)
            {
                int index = random.Next(available_positions.Count() - 1);
                if (validateRookPositions_690(r1_position, available_positions.ElementAt(index)))
                {
                    int position = available_positions.ElementAt(index);
                    available_positions.RemoveAt(index);
                    return position;
                }
            }
        }
    }
    public int GetKingPosition_690(List<int> available_positions, int rook1_position, int rook2_position)
    {
        Random random = new Random();
        bool p1_is_greater = rook1_position > rook2_position;

        if (p1_is_greater)
        {
            if (Math.Abs(rook1_position - rook2_position) == 2)
            {
                return rook2_position + 1;
            }
            else
            {
                while (true)
                {
                    int position = random.Next(rook2_position + 1, rook1_position - 1);
                    if (position != rook1_position && position != rook2_position)
                    {
                        available_positions.RemoveAt(available_positions.IndexOf(position));
                        return position;
                    }
                }
            }

        }
        else
        {
            if (Math.Abs(rook1_position - rook2_position) == 2)
            {
                return rook1_position + 1;
            }
            else
            {
                while (true)
                {
                    int position = random.Next(rook1_position + 1, rook2_position - 1);
                    if (position != rook1_position && position != rook2_position)
                    {
                        available_positions.RemoveAt(available_positions.IndexOf(position));
                        return position;
                    }
                   
                }
            }
        }    
    }
    public bool validateBishopPositions_690(int position_one, int position_two)
    {
        int position_difference = Math.Abs(position_one - position_two);
        if ((position_two % 2) != (position_one % 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool validateRookPositions_690(int position_one, int position_two)
    {
        int position_difference = Math.Abs(position_one - position_two);
        if (position_difference > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int GetLargestAvailablePosition(List<int> available_positions)
    {
        int largest_position = -1;
        foreach (int position in available_positions)
        {
            if (position > largest_position)
            {
                largest_position = position;
            }
        }
        return largest_position;
    }
    //RESPONSE TO USER INPUT CODE

    public bool Select(Point coordinate)
    {
        try
        {
            var piece = Coordinates.Board[coordinate.X, coordinate.Y].Piece;

            if (piece != null && piece.Team == _movementService.GameSettings.CurrentTeam)
            {
                _selectedSquare = coordinate;
                Program.GameWindow.PossibleMoves = piece.GetMoves(coordinate.X, coordinate.Y);
                return true;
            }

            if (Program.GameWindow.PossibleMoves.Contains(coordinate))
            {
                Program.GameWindow.PossibleMoves.Clear();
                ProcessMouseClickMove(coordinate);
                _selectedSquare = null;
                return true;
            }
        }
        catch (IndexOutOfRangeException) { }
        Program.GameWindow.PossibleMoves.Clear();
        return false;
    }


    public void ProcessMouseClickMove(Point coordinate)
    {
        var kingIsCastlingLeft = false;
        var kingIsCastlingRight = false;
        var movingPieceIsKing = false;
        var forbiddenSquares = new List<Point?>();

        _movementService.Move(_selectedSquare.Value, coordinate);

        //if the moving piece is King
        if (Coordinates.Board[coordinate.X, coordinate.Y].Piece is King)
        {
            movingPieceIsKing = true;
            forbiddenSquares.Add(new Point(coordinate.X, coordinate.Y));

            //if the king is castling
            if (Math.Abs(_selectedSquare.Value.X - coordinate.X) == 2)
                switch (coordinate.X)
                {
                    //add the squares in between castling to "forbidden squares" - these squares cannot be checked by enemy
                    case 2:
                        kingIsCastlingLeft = true;
                        forbiddenSquares.Add(coordinate with { X = 3 });
                        forbiddenSquares.Add(coordinate with { X = 4 });
                        break;
                    case 6:
                        kingIsCastlingRight = true;
                        forbiddenSquares.Add(coordinate with { X = 4 });
                        forbiddenSquares.Add(coordinate with { X = 5 });
                        break;
                }
        }
        //otherwise, add king to the forbidden squares - the king cannot be checked
        else
            forbiddenSquares.Add(_movementService.GameSettings.GetMyKingLocation());

        //check every move to see whether the enemy can check either forbidden squares
        //if the enemy can, undo the move

        for (var i = 0; i <= 7; i++)
            for (var j = 0; j <= 7; j++)
            {
                var toCheck = Coordinates.Board[i, j].Piece;
                if (toCheck == null)
                    continue;

                //the piece has to be from the opposite team
                //the piece should be able to check one of the forbidden squares
                if (toCheck.Team == _movementService.GameSettings.CurrentTeam)
                    continue;

                var getMoves = toCheck.GetMoves(i, j);

                if (!getMoves.Intersect(forbiddenSquares).Any())
                    continue;

                _movementService.UndoMove(_selectedSquare.Value, coordinate);
                return;
            }


        if (Coordinates.Board[coordinate.X, coordinate.Y].Piece is Pawn)
            if (coordinate.Y is 0 or 7)
                _movementService.UpgradePawn(coordinate.X, coordinate.Y);

        if (movingPieceIsKing)
        {
            var king = Coordinates.Board[coordinate.X, coordinate.Y].Piece as King;
            king.HasMoved = true;

            //once we have moved, update the position of the king
            _movementService.GameSettings.UpdateKingLocation(coordinate.X, coordinate.Y);

            //also if castling has been done, move the rook
            if (kingIsCastlingLeft)
                _movementService.Move(_selectedSquare.Value with { X = 0 }, _selectedSquare.Value with { X = 3 });

            else if (kingIsCastlingRight)
                _movementService.Move(_selectedSquare.Value with { X = 7 }, _selectedSquare.Value with { X = 5 });

        }

        _movementService.GameSettings.CurrentTeam =
            _movementService.GameSettings.CurrentTeam == Team.White ? Team.Black : Team.White;

        //logic if it is an ai
        if (_movementService.GameSettings.PlayAgainstAi)
            DoAiMove(_selectedSquare.Value.X, _selectedSquare.Value.Y, coordinate.X, coordinate.Y);
    }


    //RESPONSE TO AI

    private void DoAiMove(int moverX, int moverY, int destinationX, int destinationY)
    {
        var outputFromAi = _adapter.MakeMove((byte)moverX, (byte)moverY, (byte)destinationX, (byte)destinationY);

        switch (outputFromAi?.Length)
        {
            case 4:
                ProcessAiOutput(outputFromAi);
                _movementService.GameSettings.CurrentTeam =
                    _movementService.GameSettings.CurrentTeam == Team.White ? Team.Black : Team.White;
                break;
            case > 4:
                ProcessAiOutput(outputFromAi[..4]);
                MessageBox.Show(outputFromAi[4..], @"Game over!");
                _movementService.GameSettings.CurrentTeam =
                    _movementService.GameSettings.CurrentTeam == Team.White ? Team.Black : Team.White;
                break;
            default:
                MessageBox.Show(@"Move is invalid. Please try again.");
                break;
        }
    }

    private void ProcessAiOutput(string outputFromAi)
    {
        var x1 = int.Parse(outputFromAi[0].ToString());
        var y1 = int.Parse(outputFromAi[1].ToString());
        var x2 = int.Parse(outputFromAi[2].ToString());
        var y2 = int.Parse(outputFromAi[3].ToString());

        var origin = new Point(x1, y1);
        var destination = new Point(x2, y2);

        _movementService.Move(origin, destination);

        if (Math.Abs(x1 - x2) != 2 || Coordinates.Board[x2, y2].Piece is not King)
            return;

        switch (x2)
        {
            case 2:
                //left side
                _movementService.Move(new Point(0, y2), new Point(3, y2));
                break;
            case 6:
                //right side
                _movementService.Move(new Point(7, y2), new Point(5, y2));
                break;
        }
    }
}