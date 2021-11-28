using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        private List<Board> boards = new List<Board>();

        public IReadOnlyCollection<Board> Boards => boards.AsReadOnly();

        public User() {}

        public User(Guid userId, string username) {
            if (username.Length < 5) {
                throw new ArgumentException("Username cannot be smaller than 5 letters.");
            }


            Username = username;
        }


        public void OpenNewBoard() {
            Board board = Board.CreateEmptyBoard(this);

            boards.Add(board);
        }

        public static User New(Guid id, string username) {
            return new User(id, username);
        }
    }
}