using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

string SudokuSolver(string input)
{
    char[] characters = input.ToCharArray();
    char[][] sudoku = new char[9][];

    for (int i = 0; i < characters.Length; i++)
    {
        if (i % 9 == 0)
        {
            sudoku[i / 9] = new char[9];
        }

        sudoku[i / 9][i % 9] = characters[i];

        if (i % 9 == 8)
        {
            Console.WriteLine(string.Join(" ", sudoku[i / 9]));
        }
    }

    bool IsValid(char[][] boardLocal, int row, int column, char c)
    {
        for (int i = 0; i < 9; i++)
        {
            if (boardLocal[row][i] == c || boardLocal[i][column] == c) return false;
            int squareRow = 3 * (row / 3) + i / 3;
            int squareColumn = 3 * (column / 3) + i % 3;
            if (boardLocal[squareRow][squareColumn] == c) return false;
        }

        return true;
    }

    bool SolveSudoku(char[][] board)
    {
        for (int row = 0; row < board.Length; row++)
        {
            for (int column = 0; column < board[row].Length; column++)
            {
                if (board[row][column] == '.')
                {
                    for (char c = '1'; c <= '9'; c++)
                    {
                        if (IsValid(board, row, column, c))
                        {
                            board[row][column] = c;
                            if (SolveSudoku(board))
                            {
                                return true;
                            }
                            else
                            {
                                board[row][column] = '.';
                            }
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }

    SolveSudoku(sudoku);

    Console.WriteLine("-----------------");
    for (int i = 0; i < sudoku.GetLength(0); i++)
    {
        Console.WriteLine(string.Join(" ", sudoku[i]));
    }


    Console.WriteLine("-----------------");
    // returns the solved sudoku as a string
    return string.Join("", sudoku.SelectMany(x => x));
}

// if you are getting sudoku input from an endpoint with multiple sudokus boards on each line
// you can use the following code to solve them and send them back to the endpoint

// string url = "http://ip:8080";
//
// using var client = new HttpClient();
// client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
//
// try
// {
//     HttpResponseMessage response = await client.GetAsync(url);
//     response.EnsureSuccessStatusCode();
//     string responseBody = await response.Content.ReadAsStringAsync();
//     string[] solvedSudokus = responseBody.Split("\n").Select(SudokuSolver).ToArray();
//     string result = string.Join("\n", solvedSudokus);
//     Console.WriteLine(responseBody);
//
//     var postData = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");
//     response = await client.PostAsync(url, postData);
//
//     response.EnsureSuccessStatusCode();
//     responseBody = await response.Content.ReadAsStringAsync();
//     Console.WriteLine(responseBody);
// }
// catch (HttpRequestException e)
// {
//     Console.WriteLine("\nException Caught!");
//     Console.WriteLine("Message :{0} ", e.Message);
// }


Console.WriteLine(SudokuSolver("53..7....6..195....98....6.8...6...34..8.3..17...2...6.6....28....419..5....8..79"));
