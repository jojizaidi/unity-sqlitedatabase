using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SqliteTest : MonoBehaviour {


    public void InsertItem()
    {
        string id = "'1'";
        string name = "'zohaib'";
        string contact = "'03xx1234566'";

        SqliteDatabase dbName = new SqliteDatabase("testDb.db");
        string insertQuery = "INSERT INTO profile(id , name , contact) VALUES(" + id + "," + name + "," + contact + ")";

        dbName.ExecuteNonQuery(insertQuery);
    }

    public void QueryItem()
    {
        string query = "SELECT * from profile WHERE 1";
        SqliteDatabase dbName = new SqliteDatabase("testDb.db");
        DataTable queryResult = dbName.ExecuteQuery(query);
        List<DataRow> allItems = queryResult.Rows;

        foreach (DataRow item in allItems)
        {
            Debug.Log("Id : " + item["id"].ToString());
            Debug.Log("Name : " + item["name"].ToString());
            Debug.Log("Contact : " + item["contact"].ToString());
        }
    }
}
