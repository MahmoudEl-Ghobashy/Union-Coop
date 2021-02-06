using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SqlManager : MonoBehaviour
{
    public static SqlManager Instance;
    public Text Notes;

    private DataTable myTable;

    private void Awake() //Create a singleton.
    {
        if (Instance != null)
        {
            GameObject.Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public async void StartAsyncQuery() //Start the async task to run query on SQL DB.
    {
        Notes.text = "Please note that the SQL Select query was tested on a local database so it's not possible to test on your machine. \nBut of-course you can have a look at the code. \n\nThis was the last task, Thank you for the opportunity.";
        myTable = await GetDataFromSQL();
    }

    private async Task<DataTable> GetDataFromSQL()
    {
        //Please note that the selected database is a db that was created locally on my machine by the help of this page https://www.sqlservertutorial.net/.

        //Set Connection properties "server, database name, username, password".
        string myCon = "Server=localhost;" +
            "Database=Test;" +
            "User ID=sa;" +
            "Password=SQL123;";

        SqlConnection connection = new SqlConnection(myCon); //Create the connection.
        await connection.OpenAsync().ConfigureAwait(false); //Open an asynchronous connection with the db.

        SqlCommand Command = connection.CreateCommand(); //Create the query command.
        Command.CommandText = "select * from production.brands"; //Add the query command line. this is to select the whole table named brands from a schema named production in my database name test.
        SqlDataReader myReader = Command.ExecuteReader(); //Execute the command and read the return of the command in myReader variable.

        DataTable dt = new DataTable(); //Create Datatable to hold the data.
        dt.Load(myReader); //Load data from myReader into the datatable.
        return dt;
    }
}
