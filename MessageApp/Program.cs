using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MessageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool noquit = true;
            SqlConnection connect;
            connect = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\USERS\BAYERJ\DOCUMENTS\VISUAL STUDIO 2015\PROJECTS\10-17\MESSAGEAPP\MESSAGEAPP\MESSAGES.MDF;Integrated Security=True");

            connect.Open();

            while (noquit==true)

            {
                Console.WriteLine("Do you want to [W]rite or [R]etrieve a message or[Q]uit?");
                string choice = Console.ReadLine();
                choice = choice.ToUpper();

                if (choice == "W")
                {
                    Console.WriteLine("Enter a message.");
                    string usermessage = Console.ReadLine();

                    SqlCommand insertCommand = new SqlCommand("INSERT INTO [Table] (Messages) VALUES (@usermessage); SELECT @@Identity as ID", connect);
                    insertCommand.Parameters.AddWithValue("@usermessage", usermessage);
                    SqlDataReader dataReader;
                                       

                    dataReader = insertCommand.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        Console.WriteLine("I just entered message #:" + dataReader["Id"]);

                    }
                    dataReader.Close();
                }

                if (choice == "R")
                {
                    Console.WriteLine("Which message do you wish to retrieve?");
                    int rm = Convert.ToInt32(Console.ReadLine());
                    SqlCommand retrieve = new SqlCommand("SELECT MESSAGES FROM [TABLE] WHERE Id = (@rm)", connect);
                    retrieve.Parameters.AddWithValue("@rm", rm);
                    
                    SqlDataReader messageReader = retrieve.ExecuteReader();
                    if (messageReader.HasRows)
                    {
                        messageReader.Read();
                        Console.WriteLine(messageReader["Messages"]);
                    }
                    else
                    {
                        Console.WriteLine("That message doesn't exist buddy!");
                    }
                    messageReader.Close();
                    
                }

                if (choice == "Q")
                {
                    noquit = false;
                }
               
            }
            connect.Close();
        }
    }
}
