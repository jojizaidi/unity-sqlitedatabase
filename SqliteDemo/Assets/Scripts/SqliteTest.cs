using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class SqliteTest : MonoBehaviour {


    public InputField idField;
    public InputField nameField;
    public InputField contactField;

    public GameObject itemPrefab;
    public Transform itemParent;

    void Start()
    {
        QueryItem();
    }

    public void InsertItem()
    {
        if(idField.text != "" && nameField.text != "" && contactField.text != "")
        {
            string id = "'" + idField.text + "'";
            string name = "'" + nameField.text + "'";
            string contact = "'" + contactField.text + "'";

            //string id= "'2'";
            //string name = "'test'";
            //string contact = "'99999'";

            SqliteDatabase dbName = new SqliteDatabase("testDb.db");
            string insertQuery = "INSERT INTO profile(id , name , contact) VALUES(" + id + "," + name + "," + contact + ")";

            try
            {
                dbName.ExecuteNonQuery(insertQuery);
            }
            catch(System.Exception e)
            {
                Debug.Log("Already exists");
            }



            idField.text = "";
            nameField.text = "";
            contactField.text = "";


            QueryItem();
        }
    }

    public void QueryItem()
    {
        string query = "SELECT * from profile WHERE 1";
        SqliteDatabase dbName = new SqliteDatabase("testDb.db");
        DataTable queryResult = dbName.ExecuteQuery(query);
        List<DataRow> allItems = queryResult.Rows;

        DeleteAllItemsFromDisplay();

        foreach (DataRow item in allItems)
        {
            Debug.Log("Id : " + item["id"].ToString());
            Debug.Log("Name : " + item["name"].ToString());
            Debug.Log("Contact : " + item["contact"].ToString());

            GameObject go = Instantiate(itemPrefab) as GameObject;
            go.transform.parent = itemParent;
            go.transform.GetChild(0).GetComponent<Text>().text = item["id"].ToString();
            go.transform.GetChild(1).GetComponent<Text>().text = item["name"].ToString();
            go.transform.GetChild(2).GetComponent<Text>().text = item["contact"].ToString();
        }
    }

    void DeleteAllItemsFromDisplay()
    {
        foreach(Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }
    }

    void ClearDatabase()
    {
        SqliteDatabase dbName = new SqliteDatabase("testDb.db");
        string deleteQuery = "DELETE from profile WHERE 1";
        dbName.ExecuteNonQuery(deleteQuery);
    }
}
