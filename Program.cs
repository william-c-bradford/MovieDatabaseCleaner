﻿namespace MovieDatabaseCleaner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool headerRow = true;

            // Create a new StreamReader object to read the CSV file
            StreamReader reader = new StreamReader("C:\\Users\\Jenni\\OneDrive\\Desktop\\Movies Database Files\\movies_dated.csv");

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

            // Iterate through each index of each row in the List<string[]>
            foreach (string[] row in data)
            {
                // Remove the "-" from the front of the year data
                if (row[2].StartsWith("-"))
                {
                    row[2] = row[2].Substring(1);
                }// End if

                // If the genre data is empty, move the year data to the genre
                if (row[3].Length == 0)
                {
                    row[3] = row[2];
                    row[2] = "";
                }// End if

                // If the genre beings with "-", it is a year
                if (row[3].StartsWith("-"))
                {
                    // Remove the "-"
                    row[3] = row[3].Substring(1);

                    // Copy the year from the genre column to the year column
                    row[2] = row[3];

                    // Copy the genre from column 5 to the genre column
                    row[3] = row[4];

                    // Remove the genre from the extra column
                    row[4] = "";
                }// End if


                if (row[2].StartsWith("(") ) {//then

                    row[2] = row[2].Substring(1, 4);

                }//end if

            }// End foreach

            // Create a new StreamWriter object to write the fixed data to a new CSV file
            StreamWriter writer = new StreamWriter("C:\\Users\\Jenni\\OneDrive\\Desktop\\Movies Database Files\\output.csv");

            // Write each line from the List<string[]> to the new CSV file
            foreach (string[] row in data)
            {
                writer.WriteLine(string.Join(",", row));
            }// End foreach

            //Test Comment for Pull Request test

            // Close the StreamWriter object
            writer.Close();

            //test master branch commit/pull

        }// End Main
    }// End Program
}// End namespace