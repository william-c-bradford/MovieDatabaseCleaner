namespace MovieDatabaseCleaner {
    internal class Program {
        static void Main(string[] args) {
            List<string[]> data;

            data = ReadCSV();
            data = FormatYearColumn(data);
            data = MoveYearFromTitle(data);
            WriteCSV(data);

        }//end Main

        #region Functions

        static List<string[]> ReadCSV() {
            // Create a new StreamReader object to read the CSV file
            StreamReader reader = new StreamReader("C:\\Users\\MCA Coder\\Downloads\\movies_dated.csv");

            // Create a new List<string[]> to store the data from the CSV file
            List<string[]> data = new List<string[]>();

            // Create string variable to temporarily store each line
            string line;

            // While the line is not null, read each line from the CSV file

            while ((reader.EndOfStream == false))
            {
                line = reader.ReadLine();

                // Split the line and add it to the List<string[]>
                data.Add(line.Split(','));
                
            }// End while

            // Close the StreamReader object
            reader.Close();

            return data;
        }

        static List<string[]> FormatYearColumn(List<string[]> data) {
            foreach (string[] row in data) {
                // Remove the "-" from the front of the year data
                if (row[2].StartsWith("-")) {
                    row[2] = row[2].Substring(1);

                    //if year starts with (
                } else if (row[2].StartsWith("(")) {

                    //get year and first char of year
                    string unformattedYear = row[2];
                    char firstChar = unformattedYear[1];

                    //check if year starts with number
                    if (char.IsNumber(firstChar)) {
                        string newYear = "";

                        //create new year string
                        for (int i = 1; i < 5; i++) {
                            newYear += unformattedYear[i];
                        }
                    }
                }
                // If the genre data is empty, move the year data to the genre
                if (row[3].Length == 0) {
                    row[3] = row[2];
                    row[2] = "";
                }// End if

                // If the genre beings with "-", it is a year
                if (row[3].StartsWith("-")) {
                    // Check if year data is alternate title
                    if (row[2].StartsWith("(")) {
                        // Append the alternate title to the end of the title
                        row[1] = row[1] += $" {row[2]}";
                    }// End if

                    // Remove the "-"
                    row[3] = row[3].Substring(1);

                    // Copy the year from the genre column to the year column
                    row[2] = row[3];

                    // Copy the genre from column 5 to the genre column
                    row[3] = row[4];

                    // Remove the genre from the extra column
                    row[4] = "";
                }// End if

                string year = "";
                string altTitle = "";

                //if year starts with (
                if (row[2].StartsWith('(')) {

                    //jump to char 7 of year string
                    for (int j = 6; j < row[2].Length; j++) {

                        //look for (
                        if (row[2][j] == '(') {

                            //build alt title with content in the ()
                            for (int k = j; k < row[2].Length; k++)
                                altTitle += row[2][k];
                        }
                    }

                    //build year from char 2-6
                    for (int i = 1; i < 5; i++) {
                        year += row[2][i];

                    }
                    //replace title and year row with new title and year
                    row[1] = row[1] += $" {altTitle}";
                    row[2] = year;
                }
            }
            return data;
        }

        static List<string[]> MoveYearFromTitle(List<string[]> data) {
            foreach (string[] row in data) {

                string year = "";

                // If title row contains a (, and year is blank
                if (row[1].Contains('(') && (row[2] == "")) {

                    //Loop through the title string
                    for (int i = 0; i < row[1].Length; i++) {

                        //Find the open parantheses
                        if (row[1][i] == '(') {

                            //If the next char is a number
                            if (char.IsNumber(row[1][i + 1])) {

                                //Begin where i left off / for loop for building string
                                for (int j = i; j < row[1].Length; j++) {

                                    //build year string
                                    year += row[1][j];

                                    //if next char is not a number
                                    if (char.IsNumber(row[1][j + 1]) == false) {

                                        //if year is more than 4 chars
                                        if (year.Length < 4) {
                                            //discard
                                            break;
                                        } else {
                                            //else, string is most likely the year
                                            row[2] = year.Substring(1);
                                            //shorten the title string to remove the year
                                            row[1] = (row[1].Remove(row[1].Length - 7));
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }// End foreach
            return data;
        }

        static void WriteCSV(List<string[]> data) {
            // Create a new StreamWriter object to write the fixed data to a new CSV file
            string path = "C:\\Users\\MCA Coder\\Desktop\\output.csv";
            StreamWriter writer = new StreamWriter(path);

            // Write each line from the List<string[]> to the new CSV file
            foreach (string[] row in data) {
                writer.WriteLine(string.Join(",", row.Take(4)));
            }// End foreach

            //Test Comment for Pull Request test

            // Close the StreamWriter object
            writer.Close();
            Console.WriteLine($"File written to {path}");
        }

        #endregion 

    }// End Program
}// End