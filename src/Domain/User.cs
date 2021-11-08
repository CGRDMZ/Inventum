using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public ICollection<Board> Boards;

        public User() {}

        public User(string username) {
            if (username.Length < 5) {
                throw new ArgumentException("Username cannot be smaller than 5 letters.");
            }


            UserId = Guid.NewGuid();
            Username = username;
            Boards = new List<Board>();
        }


        public void OpenNewBoard() {
            Board board = Board.CreateEmptyBoard(this);

            Boards.Add(board);
        }
    }
}