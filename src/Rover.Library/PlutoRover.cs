using System;
using System.Collections.Generic;
using System.Linq;

namespace Rover.Library
{
    public class PlutoRover
    {
        private readonly Pluto _pluto;
        private Position _position;
        private readonly Dictionary<char, Func<Position>> _stepHandlerByCommand;
        private readonly Dictionary<Orientation, Func<Position, Position>> _moveForward;
        private readonly Dictionary<Orientation, Func<Position, Position>> _moveBackward;

        public Position Position => _position;

        public PlutoRover(Pluto pluto, Position initialPosition)
        {
            _pluto = pluto;
            _position = initialPosition;

            _stepHandlerByCommand = new Dictionary<char, Func<Position>>
            {
                {'F', MoveForward},
                {'B', MoveBackward},
                {'R', TurnRight}
            };

            _moveForward = new Dictionary<Orientation, Func<Position, Position>>
            {
                { Orientation.N, p => new Position(p.X, p.Y + 1, p.Orientation) },
                { Orientation.E, p => new Position(p.X + 1, p.Y, p.Orientation) },
                { Orientation.S, p => new Position(p.X, p.Y - 1, p.Orientation) },
                { Orientation.W, p => new Position(p.X - 1, p.Y, p.Orientation) }
            };

            _moveBackward = new Dictionary<Orientation, Func<Position, Position>>
            {
                { Orientation.N, p => new Position(p.X, p.Y - 1, p.Orientation) },
                { Orientation.E, p => new Position(p.X - 1, p.Y, p.Orientation) },
                { Orientation.S, p => new Position(p.X, p.Y + 1, p.Orientation) },
                { Orientation.W, p => new Position(p.X + 1, p.Y, p.Orientation) }
            };
        }

        public void Move(string command)
        {
            var step = command.First();
            _position = _stepHandlerByCommand[step]();
        }

        private Position MoveForward() => _moveForward[_position.Orientation](_position);
        private Position MoveBackward() => _moveBackward[_position.Orientation](_position);
        private Position TurnRight() => new Position(_position.X, _position.Y, Orientation.E);
    }
}
