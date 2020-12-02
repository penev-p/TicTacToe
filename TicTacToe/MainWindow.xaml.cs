using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Board will hold 9 elements for every cell of TicTacToe
        private SymbolType[] board;

        private bool firstPlayerTurn = true;
        private bool aiTurn = false;

        private bool gameEnded;

        public MainWindow()
        {
            InitializeComponent();

            StartNewGame();
        }

        private void StartNewGame()
        {
            //Creates empty array for 9 cells
            board = new SymbolType[9];

            for (int i = 0; i < board.Length; i++)
            {
                board[i] = SymbolType.Free;
            }

            //Player 1 will always start the game
            firstPlayerTurn = true;

            //Iterating every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Setting buttons Content and Background to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
            });

            //Making sure the game is on
            gameEnded = false;
           
        }

        //Whenever button is clicked we will project the button on our SymbolType[] board.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
            {
                StartNewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //This equation will give us a button index(0-8) using its column and row number.
            var index = column + (row * 3);

            //Prevents changing button content if there is already X or O.
            if (board[index] != SymbolType.Free)
                return;

           
            if (firstPlayerTurn)
            {
                board[index] = SymbolType.X;
                button.Foreground = Brushes.Blue;
                button.Content = "X";

                aiTurn = true;
                firstPlayerTurn = false;
            }

            if (aiTurn)
            {
                //Computer will return best possible move(free index on the board) using minimax algorithm.
                aiMove(MinimaxAI.findBestMove(board));

                aiTurn = false;
                firstPlayerTurn = true;
            }

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            bool draw = true;
            
            //Check for horizontal wins
            //Row 0
            if (board[0] != SymbolType.Free && (board[0] & board[1] & board[2]) == board[0])
            {
                gameEnded = true;
                draw = false;
                //Highlight the winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //Check for horizontal wins
            //Row 1
            if (board[3] != SymbolType.Free && (board[3] & board[4] & board[5]) == board[3])
            {
                gameEnded = true;
                draw = false;

                //Highlight the winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //Check for horizontal wins
            //Row 2
            if (board[6] != SymbolType.Free && (board[6] & board[7] & board[8]) == board[6])
            {
                gameEnded = true;
                draw = false;

                //Highlight the winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //Check for vertical wins
            //Column 0
            if (board[0] != SymbolType.Free && (board[0] & board[3] & board[6]) == board[0])
            {
                gameEnded = true;
                draw = false;

                //Highlight the winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Check for vartical wins
            //Column 1
            if (board[1] != SymbolType.Free && (board[1] & board[4] & board[7]) == board[1])
            {
                gameEnded = true;
                draw = false;

                //Highlight the winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Check for vartical wins
            //Column 2
            if (board[2] != SymbolType.Free && (board[2] & board[5] & board[8]) == board[2])
            {
                gameEnded = true;
                draw = false;

                //Highlight the winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Check for diagonal wins
            //Top left - down righ
            if (board[0] != SymbolType.Free && (board[0] & board[4] & board[8]) == board[0])
            {
                gameEnded = true;
                draw = false;

                //Highlight the winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Check for diagonal wins
            //Top right - down left
            if (board[2] != SymbolType.Free && (board[2] & board[4] & board[6]) == board[2])
            {
                gameEnded = true;
                draw = false;

                //Highlight the winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Check for winner and full board
            if (!board.Any(f => f == SymbolType.Free) && draw == true)
            {
                //Game ended
                gameEnded = true;

                //Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }

        /// <summary>
        /// Changes the specific button Content and Foreground and changes the Symbol on specific board index.
        /// </summary>
        /// <param name="index">Number of the cell in TicTacToe board(0-8).</param>
        private void aiMove(int index)
        {
            switch (index)
            {
                case 0:
                    Button0_0.Foreground = Brushes.Red;
                    Button0_0.Content = "O";
                    board[0] = SymbolType.O;
                    break;
                case 1:
                    Button1_0.Foreground = Brushes.Red;
                    Button1_0.Content = "O";
                    board[1] = SymbolType.O;
                    break;
                case 2:
                    Button2_0.Foreground = Brushes.Red;
                    Button2_0.Content = "O";
                    board[2] = SymbolType.O;
                    break;
                case 3:
                    Button0_1.Foreground = Brushes.Red;
                    Button0_1.Content = "O";
                    board[3] = SymbolType.O;
                    break;
                case 4:
                    Button1_1.Foreground = Brushes.Red;
                    Button1_1.Content = "O";
                    board[4] = SymbolType.O;
                    break;
                case 5:
                    Button2_1.Foreground = Brushes.Red;
                    Button2_1.Content = "O";
                    board[5] = SymbolType.O;
                    break;
                case 6:
                    Button0_2.Foreground = Brushes.Red;
                    Button0_2.Content = "O";
                    board[6] = SymbolType.O;
                    break;
                case 7:
                    Button1_2.Foreground = Brushes.Red;
                    Button1_2.Content = "O";
                    board[7] = SymbolType.O;
                    break;
                case 8:
                    Button2_2.Foreground = Brushes.Red;
                    Button2_2.Content = "O";
                    board[8] = SymbolType.O;
                    break;
            }
        }
    }
}
